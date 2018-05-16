using System;
using System.Collections.Generic;
using System.Linq;
using Ibarra.MarsRover.Commands;
using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;

namespace Ibarra.MarsRover.CommandControl {
    /// <inheritdoc />
    public class MissionControlCenter : IControlCenter {
        private IEnumerable<IExplorerCommand> _commandList;
        private readonly IDictionary<CommandChainType, Action<IExplorerCommand>> _setCommandChainExecutors;

        /// <summary>
        /// Create the mission control center (MCC) that is responsible for the provided deployment zone.
        /// </summary>
        /// <param name="deploymentZone">The zone that this MCC is responsible for exploring with its exploration crew.</param>
        public MissionControlCenter(IDeploymentZoneChart deploymentZone) {
            DeploymentDestination = deploymentZone;
            Explorers = new ExplorationTeam(deploymentZone);
            _setCommandChainExecutors = new Dictionary<CommandChainType, Action<IExplorerCommand>> {
                {
                    CommandChainType.InitializeDeploymentZone, (command) => {
                        var deploymentZoneChartCommand = (DeploymentZoneChartCommand) command;
                        deploymentZoneChartCommand.SetDeploymentZoneChart(DeploymentDestination);
                    }
                }, {
                    CommandChainType.DeployExplorer, (command) => {
                        var deployExplorerCommand = (DeployExplorerCommand) command;
                        var rover = new Rover(Explorers);
                        deployExplorerCommand.SetExplorer(rover);
                        Explorers.Add(rover);
                    }
                }, {
                    CommandChainType.Explore, (command) => {
                        var exploreCommand = (ExploreCommand) command;
                        exploreCommand.SetExplorer(Explorers.Last());
                    }
                }
            };
        }

        /// <inheritdoc />
        public IDeploymentZoneChart DeploymentDestination { get; }

        /// <inheritdoc />
        public ExplorationTeam Explorers { get; }

        /// <summary>
        /// Set the commands that are to be executed by the most recently deployed explorer.
        /// </summary>
        /// <param name="commandList">The list of commands to be executed by the most recently deployed explorer.</param>
        public void SetCommands(IEnumerable<IExplorerCommand> commandList) {
            _commandList = commandList;
        }

        /// <inheritdoc />
        public void ExecuteAll() {
            foreach (var command in _commandList) {
                if (command == null) {
                    return;
                }

                _setCommandChainExecutors[command.CommandChainType].Invoke(command);
                command.Execute();
            }
        }
    }
}