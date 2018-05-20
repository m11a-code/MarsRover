using System;
using System.Collections.Generic;
using Ibarra.MarsRover.Commands;
using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.Commands {
    [TestClass]
    public class ExploreCommandTests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "List of movements cannot be null.")]
        public void ExploreCommandTest() {
            var command = new ExploreCommand(null);
        }

        [TestMethod]
        public void SetExplorerTest() {
        }

        [TestMethod]
        public void ExecuteTest_ExplorerSet_ReturnsTrue() {
            var command = new ExploreCommand(new List<Movement>());
            command.SetExplorer(new Rover(new ExplorationTeam(new Plateau())));
            Assert.IsTrue(command.Execute());
        }

        [TestMethod]
        public void ExecuteTest_NoExplorerSet_ReturnsFalse() {
            var command = new ExploreCommand(new List<Movement>());
            Assert.IsFalse(command.Execute());
        }
    }
}