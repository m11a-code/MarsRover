using System;
using Ibarra.MarsRover.Commands;
using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.Commands {
    [TestClass]
    public class DeployExplorerCommandTests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Provided position cannot be null.")]
        public void DeployExplorerCommandTest() {
            var deployExplorerCommand = new DeployExplorerCommand(null, Heading.East);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Explorer cannot be null.")]
        public void SetExplorerTest() {
            var command = new DeployExplorerCommand(new Position(1, 1), Heading.East);
            command.SetExplorer(null);
        }

        [TestMethod]
        public void ExecuteTest() {
            var command = new DeployExplorerCommand(new Position(5, 5), Heading.North);
            IDeploymentZoneChart deploymentZoneChart = new Plateau {
                Size = new Size(5, 5)
            };
            command.SetExplorer(new Rover(new ExplorationTeam(deploymentZoneChart)));
            Assert.IsTrue(command.Execute());
        }

        [TestMethod]
        public void ExecuteTest_NoExplorerSet() {
            var command = new DeployExplorerCommand(new Position(5, 5), Heading.North);
            Assert.IsFalse(command.Execute());
        }
    }
}