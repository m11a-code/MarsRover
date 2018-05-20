using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.ExplorationVehicles {
    [TestClass]
    public class ExplorerTests {
        [TestMethod]
        public void LaunchTest() {
        }

        [TestMethod]
        public void IsPositionAvailableTest_NullPosition() {
            Assert.IsFalse(new Rover(new ExplorationTeam(new Plateau())).IsPositionAvailable(null));
        }

        [TestMethod]
        public void IsPositionAvailableTest_ExplorerExistsAtPositionFails() {
            var team = new ExplorationTeam(new Plateau {Size = new Size(5, 5)});
            var rover1 = new Rover(team);
            rover1.Launch(new Position(5, 5), Heading.North);
            team.Add(rover1);
            var rover2 = new Rover(team);
            Assert.IsFalse(rover2.IsPositionAvailable(new Position(5, 5)));
        }

        [TestMethod]
        public void IsPositionAvailableTest_PassIsValidAndnoExplorerAtPosition() {
            var team = new ExplorationTeam(new Plateau {Size = new Size(5, 5)});
            var rover1 = new Rover(team);
            rover1.Launch(new Position(5, 5), Heading.North);
            team.Add(rover1);
            var rover2 = new Rover(team);
            Assert.IsTrue(rover2.IsPositionAvailable(new Position(4, 5)));
        }
    }
}