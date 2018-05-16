using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.Landscapes {
    public class Plateau : IDeploymentZoneChart {
        public Plateau(Size size) => Size = size;
        public Size Size { get; set; }
        public bool IsValid(Position position) => Size.Width > position.X && Size.Height > position.Y;
    }
}