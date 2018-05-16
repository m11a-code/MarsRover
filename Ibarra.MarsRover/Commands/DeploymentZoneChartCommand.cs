using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.Commands {
    public class DeploymentZoneChartCommand : IExplorerCommand {
        public Size Size { get; }
        private IDeploymentZoneChart _associatedDeploymentZoneChart;

        public DeploymentZoneChartCommand(Size size) {
            Size = size;
        }

        public CommandChainType CommandChainType => CommandChainType.InitializeDeploymentZone;

        public void SetDeploymentZoneChart(IDeploymentZoneChart deploymentZone) =>
            _associatedDeploymentZoneChart = deploymentZone;

        public void Execute() {
            if (_associatedDeploymentZoneChart != null) {
                _associatedDeploymentZoneChart.Size = Size;
            }
        }
    }
}