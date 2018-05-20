using System;
using System.Collections.Generic;
using System.Text;
using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.ExplorationVehicles {
    [TestClass]
    public class ExplorationTeamTests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Deployment zone chart cannot be null.")]
        public void ExplorationTeamTest_NullDeploymentZoneChart() {
            var team = new ExplorationTeam(null);
        }

        [TestMethod]
        public void GenerateExplorationReportTest_ExplorerNotLaunched_ExpectedOutput() {
            var team = new ExplorationTeam(new Plateau());
            team.Add(new Rover(team));
            var expectedReport = new StringBuilder();
            expectedReport.Append("Explorer not launched.");
            expectedReport.AppendLine();
            Assert.AreEqual(expectedReport.ToString(), team.GenerateExplorationReport());
        }

        [TestMethod]
        public void GenerateExplorationReportTest_OneExplorerLaunched_ExpectedOutput() {
            var deploymentZone = new Plateau {Size = new Size(5, 5)};

            var team = new ExplorationTeam(deploymentZone);
            var rover = new Rover(team);
            rover.Launch(new Position(1, 2), Heading.North);
            team.Add(rover);
            var expectedReport = new StringBuilder();
            expectedReport.Append("1 2 N");
            expectedReport.AppendLine();
            Assert.AreEqual(expectedReport.ToString(), team.GenerateExplorationReport());
        }

        [TestMethod]
        public void GenerateExplorationReportTest_TwoExplorersLaunched_ExpectedOutput() {
            var deploymentZone = new Plateau {Size = new Size(5, 5)};

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

            var expectedReport = new StringBuilder();
            expectedReport.Append("1 3 N");
            expectedReport.AppendLine();
            expectedReport.Append("5 1 E");
            expectedReport.AppendLine();
            Assert.AreEqual(expectedReport.ToString(), team.GenerateExplorationReport());
        }

        [TestMethod]
        public void GenerateExplorationReportTest_TwoExplorersCollide_ExpectedOutput() {
            var deploymentZone = new Plateau {Size = new Size(5, 5)};

            var team = new ExplorationTeam(deploymentZone);
            var rover1 = new Rover(team);
            var rover2 = new Rover(team);
            rover1.Launch(new Position(5, 5), Heading.North);
            team.Add(rover1);
            rover1.Move(new List<Movement>());
            rover2.Launch(new Position(5, 4), Heading.North);
            team.Add(rover2);
            rover2.Move(new List<Movement> {
                // MMRMMRMRRM
                Movement.Forward,
                Movement.Forward,
                Movement.Forward
            });

            var expectedReport = new StringBuilder();
            expectedReport.Append("5 5 N");
            expectedReport.AppendLine();
            expectedReport.Append("5 4 N");
            expectedReport.AppendLine();
            Assert.AreEqual(expectedReport.ToString(), team.GenerateExplorationReport());
        }

        [TestMethod]
        public void GenerateExplorationReportTest_HitWall_ExpectedOutput() {
            var deploymentZone = new Plateau {Size = new Size(5, 5)};

            var team = new ExplorationTeam(deploymentZone);
            var rover = new Rover(team);
            rover.Launch(new Position(5, 5), Heading.North);
            team.Add(rover);
            rover.Move(new List<Movement> {
                Movement.Forward,
                Movement.Forward,
                Movement.Forward,
                Movement.Forward,
                Movement.Forward,
                Movement.Forward,
                Movement.Forward,
                Movement.Forward,
                Movement.Forward,
                Movement.Forward,
                Movement.Forward
            });

            var expectedReport = new StringBuilder();
            expectedReport.Append("5 5 N");
            expectedReport.AppendLine();
            Assert.AreEqual(expectedReport.ToString(), team.GenerateExplorationReport());
        }
    }
}