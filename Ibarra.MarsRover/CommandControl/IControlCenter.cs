using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;

namespace Ibarra.MarsRover.CommandControl {
    /// <summary>
    /// Interface for
    /// </summary>
    public interface IControlCenter {
        IDeploymentZoneChart DeploymentDestination { get; }
        ExplorationUnit Explorers { get; }

        void ExecuteAll();
    }
}