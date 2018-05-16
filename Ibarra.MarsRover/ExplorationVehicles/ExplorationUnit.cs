using System.Collections.Generic;
using Ibarra.MarsRover.Landscapes;

namespace Ibarra.MarsRover.ExplorationVehicles {
    public class ExplorationUnit : List<Explorer> {
        public IDeploymentZoneChart DeploymentZoneChart { get; }

        public ExplorationUnit(IDeploymentZoneChart deploymentZoneChart) {
            DeploymentZoneChart = deploymentZoneChart;
        }
    }
}