using System;
using System.Collections.Generic;
using System.Linq;
using Ibarra.MarsRover.Commands;
using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;

namespace Ibarra.MarsRover.CommandControl {
    public class MissionControlCenter : IControlCenter {
        private IEnumerable<IExplorerCommand> _commandList;
        private readonly IDictionary<CommandChainType, Action<IExplorerCommand>> _setCommandChainExecutors;

        public MissionControlCenter(IDeploymentZoneChart landingZone) {
            DeploymentDestination = landingZone;
            Explorers = new ExplorationUnit(landingZone);
            _setCommandChainExecutors = new Dictionary<CommandChainType, Action<IExplorerCommand>> {
                {
                    CommandChainType.InitializeDeploymentZone, (command) => {
                        var deploymentZoneChartCommand = (DeploymentZoneChartCommand) command;
                        deploymentZoneChartCommand.SetDeploymentZoneChart(DeploymentDestination);
                    }
                }, {
                    CommandChainType.DeployExplorer, (command) => {
                        var deployExplorerCommand = (DeployExplorerCommand) command;
                        var rover = new Rover(DeploymentDestination, Explorers);
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

        public IDeploymentZoneChart DeploymentDestination { get; }
        public ExplorationUnit Explorers { get; }

        public void SetCommands(IEnumerable<IExplorerCommand> commandList) {
            _commandList = commandList;
        }

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