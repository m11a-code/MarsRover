using System.Collections.Generic;
using Ibarra.MarsRover.Exceptions;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.ExplorationVehicles {
    public abstract class Explorer {
        protected Explorer(IDeploymentZoneChart deploymentZoneChart, ExplorationUnit crew) {
            DeploymentZoneChart = deploymentZoneChart;
            ExplorationUnit = crew;
        }

        public Position Position { get; protected set; }
        public Heading Heading { get; protected set; }
        public IList<Explorer> ExplorationUnit { get; set; }

        public IDeploymentZoneChart DeploymentZoneChart { get; protected set; }

        public abstract void Move(IEnumerable<Movement> movements);

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