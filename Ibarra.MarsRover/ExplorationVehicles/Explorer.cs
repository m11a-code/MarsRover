using System.Collections.Generic;
using Ibarra.MarsRover.Exceptions;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.ExplorationVehicles {
    /// <summary>
    /// Base class for units that are responsible for actively exploring other worlds.
    /// </summary>
    public abstract class Explorer {
        /// <summary>
        /// Creates an exploration unit that is responsible for exploring the provided location.
        ///
        /// This explorer is set as part of the provided team.
        /// </summary>
        /// <param name="crew">The team of explorers this explorer will belong to.</param>
        protected Explorer(ExplorationTeam crew) {
            DeploymentZoneChart = crew.DeploymentZoneChart;
            ExplorationUnit = crew;
        }

        /// <summary>
        /// The position of the current explorer.
        /// </summary>
        public Position Position { get; protected set; }

        /// <summary>
        /// The heading of the current explorer. In other words, the direction this explorer is currently facing.
        /// </summary>
        public Heading Heading { get; protected set; }

        /// <summary>
        /// The team of explorers that this explorer is with.
        /// </summary>
        public IList<Explorer> ExplorationUnit { get; set; }

        /// <summary>
        /// The map of the current location this explorer will be or is currently exploring.
        /// </summary>
        public IDeploymentZoneChart DeploymentZoneChart { get; protected set; }

        /// <summary>
        /// The means by which an explorer can navigate across the deployment zone.
        /// </summary>
        /// <param name="movements">The list of movements this explorer should perform to navigate around the
        /// deployment zone.</param>
        public abstract void Move(IEnumerable<Movement> movements);

        /// <summary>
        /// Deploy this explorer to the provided position and heading.
        /// </summary>
        /// <param name="deploymentPosition">The position in the deployment zone that this explorer should be deployed.
        /// </param>
        /// <param name="heading"></param>
        /// <exception cref="InvalidPositionException">Throws an InvalidPositionException if the provided position is
        /// already occupied.</exception>
        public void Launch(Position deploymentPosition, Heading heading) {
            if (!IsPositionAvailable(deploymentPosition)) {
                throw new InvalidPositionException(
                    "Rover(): Unable to deploy rover to position as it is out of range or already occupied.");
            }

            Position = deploymentPosition;
            Heading = heading;
        }

        /// <summary>
        /// Checks whether the provided position is available (i.e., unoccupied and exists within the region bounds).
        /// </summary>
        /// <returns>
        /// Whether the provided position is available or not.
        /// </returns>
        /// <param name="nextPosition">The position to test for availability.</param>
        public bool IsPositionAvailable(Position nextPosition) =>
            IsPositionValid(nextPosition) && !ExplorerExistsAtPosition(nextPosition);

        /// <summary>
        /// Checks if the given <c>Position</c> exists in the <c>IDeploymentZoneChart</c>.
        /// </summary>
        /// <returns>
        /// Whether the given <c>Position</c> exists in the <c>IDeploymentZoneChart</c>.
        /// </returns>
        /// <param name="position">The position to check.</param>
        protected abstract bool IsPositionValid(Position position);

        /// <summary>
        /// Check if an <c>Explorer</c> exists at the given position
        /// </summary>
        /// 
        protected abstract bool ExplorerExistsAtPosition(Position position);
    }
}