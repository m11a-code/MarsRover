using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.Landscapes {
    public interface IDeploymentZoneChart {
        Size Size { get; set; }
        bool IsValid(Position position);
    }
}