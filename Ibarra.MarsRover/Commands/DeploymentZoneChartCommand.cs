using System;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.Commands {
    public class DeploymentZoneChartCommand : IExplorerCommand {
        public Size Size { get; }
        private IDeploymentZoneChart _associatedDeploymentZoneChart;

        public DeploymentZoneChartCommand(Size size) {
            Size = size ?? throw new ArgumentNullException(nameof(size),
                       "Provided size cannot be null.");
        }

        public CommandChainType CommandChainType => CommandChainType.InitializeDeploymentZone;

        public void SetDeploymentZoneChart(IDeploymentZoneChart deploymentZone) =>
            _associatedDeploymentZoneChart = deploymentZone;

        /// <inheritdoc />
        public bool Execute() {
            if (_associatedDeploymentZoneChart == null) {
                return false;
            }

            _associatedDeploymentZoneChart.Size = Size;
            return true;
        }
    }
}