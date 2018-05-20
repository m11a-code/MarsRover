using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.Landscapes {
    public class Plateau : IDeploymentZoneChart {
        /// <summary>
        /// The size of the plateau.
        /// </summary>
        public Size Size { get; set; }

        /// <inheritdoc />
        public bool IsPositionWithinBounds(Position position) =>
            Size.Width >= position.X && Size.Height >= position.Y;
    }
}