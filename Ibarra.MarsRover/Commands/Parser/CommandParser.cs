using System;
using System.Collections.Generic;
using System.Linq;
using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.Commands.Parser {
    /// <inheritdoc />
    public class CommandParser : ICommandParser {
        private readonly IDictionary<CommandChainType, Func<string, IExplorerCommand>> _commandChainDictionary;

        /// <summary>
        /// Constructs the command parser.
        /// </summary>
        public CommandParser() {
            _commandChainDictionary = new Dictionary<CommandChainType, Func<string, IExplorerCommand>> {
                {
                    CommandChainType.InitializeDeploymentZone,
                    (deploymentZoneString) => {
                        var arguments = deploymentZoneString.Split(' ');
                        var zoneSize = new Size(int.Parse(arguments[0]), int.Parse(arguments[1]));
                        return new DeploymentZoneChartCommand(zoneSize);
                    }
                }, {
                    CommandChainType.Explore,
                    (explorationCommandString) => {
                        var arguments = explorationCommandString.ToCharArray();
                        var movements = arguments.Select(argument => MovementMethods.FromString(argument.ToString()))
                            .ToList();
                        return new ExploreCommand(movements);
                    }
                }, {
                    CommandChainType.DeployExplorer,
                    (deployExplorerCommand) => {
                        var arguments = deployExplorerCommand.Split(' ');
                        var position = new Position(int.Parse(arguments[0]), int.Parse(arguments[1]));
                        var heading = HeadingMethods.FromString(arguments[2]);
                        return new DeployExplorerCommand(position, heading);
                    }
                }
            };
        }


        /// <inheritdoc />
        /// <returns>List of parsed commands or null if an exception occurs.</returns>
        public IEnumerable<IExplorerCommand> ParseCommandBlock(string commandBlock) {
            var commandChains = commandBlock.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            return commandChains.Select(commandChain => {
                if (commandChain == "") {
                    return null;
                }

                try {
                    var type = CommandChainTypeMethods.CommandChainTypeActions.First(
                        commandChainRegex => commandChainRegex.Key.IsMatch(commandChain));
                    return _commandChainDictionary[type.Value].Invoke(commandChain);
                } catch (InvalidOperationException exception) {
                    Console.WriteLine("The command input provided doesn't match any expected command input.",
                        exception);
                    return null;
                }
            }).ToList();
        }
    }
}