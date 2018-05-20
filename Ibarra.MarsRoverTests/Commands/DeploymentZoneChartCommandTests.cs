using System;
using Ibarra.MarsRover.Commands;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.Commands {
    [TestClass]
    public class DeploymentZoneChartCommandTests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Provided size cannot be null.")]
        public void DeploymentZoneChartCommandTest() {
            var command = new DeploymentZoneChartCommand(null);
        }

        [TestMethod]
        public void ExecuteTest_AssociatedChartNotSet_ReturnsFalse() {
            var command = new DeploymentZoneChartCommand(new Size(5, 5));
            command.SetDeploymentZoneChart(new Plateau());
            Assert.IsTrue(command.Execute());
        }

        [TestMethod]
        public void ExecuteTest_AssociatedChartSet_ReturnsTrue() {
            var command = new DeploymentZoneChartCommand(new Size(5, 5));
            Assert.IsFalse(command.Execute());
        }
    }
}