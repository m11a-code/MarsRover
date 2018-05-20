using System;
using System.Collections;
using System.Collections.Generic;
using Ibarra.MarsRover.CommandControl;
using Ibarra.MarsRover.Commands;
using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.CommandControl {
    [TestClass]
    public class MissionControlCenterTests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Deployment zone cannot be null.")]
        public void InitializeMcc_NullDeploymentZone_ThrowsArgumentNullException() {
            var mcc = new MissionControlCenter(null);
        }

        [TestMethod]
        public void HasDeployedAnExplorer_ExplorerLaunched_IsTrue() {
            IDeploymentZoneChart deploymentZoneChart = new Plateau {
                Size = new Size(5, 5)
            };
            MissionControlCenter mcc = new MissionControlCenter(deploymentZoneChart);
            Explorer explorer = new Rover(mcc.Explorers);
            mcc.Explorers.Add(explorer);

            explorer.Launch(new Position(4, 4), Heading.East);

            Assert.IsTrue(mcc.HasDeployedAnExplorer());
        }

        [TestMethod]
        public void HasDeployedAnExplorer_ExplorerNotLaunched_IsFalse() {
            IDeploymentZoneChart deploymentZoneChart = new Plateau {
                Size = new Size(5, 5)
            };
            MissionControlCenter mcc = new MissionControlCenter(deploymentZoneChart);
            Explorer explorer = new Rover(mcc.Explorers);
            mcc.Explorers.Add(explorer);

            Assert.IsFalse(mcc.HasDeployedAnExplorer());
        }

        [TestMethod]
        public void ExecuteAllTest() {
            IDeploymentZoneChart deploymentZoneChart = new Plateau {
                Size = new Size(5, 5)
            };
            var mcc = new MissionControlCenter(deploymentZoneChart);
            mcc.ExecuteAll();

            Assert.IsTrue(mcc.NumberTotalCommandsExecutedSuccessfully == 0);

            mcc.SetCommands(new List<IExplorerCommand> {
                new DeploymentZoneChartCommand(new Size(4, 4))
            });

            mcc.ExecuteAll();
            Assert.IsTrue(mcc.NumberTotalCommandsExecutedSuccessfully == 1);
        }
    }
}