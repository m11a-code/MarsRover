using System.Collections.Generic;
using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.ExplorationVehicles {
    [TestClass]
    public class RoverTests {
        [TestMethod]
        public void MoveTest() {
            var deploymentZone = new Plateau { Size = new Size(5, 5) };

            var team = new ExplorationTeam(deploymentZone);
            var rover1 = new Rover(team);
            var rover2 = new Rover(team);
            rover1.Launch(new Position(1, 2), Heading.North);
            team.Add(rover1);
            rover1.Move(new List<Movement> {
                // LMLMLMLMM
                Movement.Left,
                Movement.Forward,
                Movement.Left,
                Movement.Forward,
                Movement.Left,
                Movement.Forward,
                Movement.Left,
                Movement.Forward,
                Movement.Forward
            });
            rover2.Launch(new Position(3, 3), Heading.East);
            team.Add(rover2);
            rover2.Move(new List<Movement> {
                // MMRMMRMRRM
                Movement.Forward,
                Movement.Forward,
                Movement.Right,
                Movement.Forward,
                Movement.Forward,
                Movement.Right,
                Movement.Forward,
                Movement.Right,
                Movement.Right,
                Movement.Forward
            });
        }
    }
}