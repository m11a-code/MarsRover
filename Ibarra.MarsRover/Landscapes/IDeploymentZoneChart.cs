using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.Landscapes {
    /// <summary>
    /// Map of an area that various <c>Explorer</c>s can be deployed.
    /// </summary>
    public interface IDeploymentZoneChart {
        Size Size { get; set; }
        /// <summary>
        /// Determines whether the provided position is within the proper bounds of the zone.
        /// </summary>
        /// <param name="position">The position to test for validity.</param>
        /// <returns>True if the position is valid.</returns>
        bool IsValid(Position position);
    }
}