using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;

namespace Ibarra.MarsRover.CommandControl {
    /// <summary>
    /// A control conter controls the actions of <c>ExplorationTeam</c> that is actively deployed.
    /// <see cref="ExplorationTeam"/> 
    /// </summary>
    public interface IControlCenter {
        /// <summary>
        /// The destination that all explorers are to be deployed.
        /// </summary>
        IDeploymentZoneChart DeploymentDestination { get; }

        /// <summary>
        /// The list of current explorers being monitored by this command center.
        /// </summary>
        ExplorationTeam Explorers { get; }

        /// <summary>
        /// Executes all commands that have been provided to the exploration unit.
        /// </summary>
        void ExecuteAll();
    }
}