using System;
using System.Linq;
using System.Text;
using Ibarra.MarsRover.CommandControl;
using Ibarra.MarsRover.Commands;
using Ibarra.MarsRover.Commands.Parser;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover {
    class Program {
        static void Main(string[] args) {
            switch (args.Length) {
                // Can only accept user input directly from command line right now.
                // TODO: Add text file input and help menu output.
                case 0:
                    while (true) {
                        try {
                            DirectCommandLineInput();
                            break;
                        } catch (Exception e) {
                            Console.WriteLine(e);
                            Console.WriteLine("Exception occurred. \'Rebooting\' command module...");
                            Console.WriteLine("...");
                            Console.WriteLine("...");
                            Console.WriteLine("...");
                            Console.WriteLine("Reboot complete!");
                        }
                    }

                    break;
                default:
                    Console.WriteLine(
                        "Invalid input type found; only direct command line input of commands is accepted right now.");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadLine();
                    break;
            }
        }

        private static void DirectCommandLineInput() {
            string consoleInput;
            var commandParser = new CommandParser();
            IDeploymentZoneChart deploymentZone = null;
            do {
                Console.WriteLine("Enter the size of the deployment zone for your spacecraft:  ");
                consoleInput = Console.ReadLine();
                var deploymentZoneCommand = commandParser.ParseCommandBlock(consoleInput);
                if (deploymentZoneCommand == null) {
                    continue;
                }

                var explorerCommands = deploymentZoneCommand.ToList();
                var command = explorerCommands.First();
                if (!(command is DeploymentZoneChartCommand deploymentZoneChartCommand)) {
                    continue;
                }

                // Right now, only supporting one type of deployment zone: Plateau.
                // Definitely room for improvement here to make it more versile with many different kinds of deployment
                // zones. Adding a factory for this would be good.
                deploymentZone = new Plateau();
                deploymentZoneChartCommand.SetDeploymentZoneChart(deploymentZone);
                // Set the size of the new deployment zone based on the provided command input.
                deploymentZoneChartCommand.Execute();

                Console.WriteLine("Deployment zone initialized successfully.");
            } while (deploymentZone == null);

            Console.WriteLine("Bringing Mission Control Center (MCC) online...");
            var mcc = new MissionControlCenter(deploymentZone);
            Console.WriteLine("MCC Online.");

            // Must deploy at least one explorer before providing any other commands.
            do {
                Console.WriteLine("Enter deployment coordinates and heading of exploration module:  ");
                consoleInput = Console.ReadLine()?.Trim();

                var launchRoverCommand = commandParser.ParseCommandBlock(consoleInput);
                // Cannot have empty input at this point.
                if (launchRoverCommand == null) {
                    continue;
                }

                var explorerCommands = launchRoverCommand.ToList();
                var command = explorerCommands.First();
                if (!(command is DeployExplorerCommand)) {
                    continue;
                }

                // Execute the single explorer creation command.
                var commandList = explorerCommands.ToList();
                mcc.SetCommands(commandList);
                mcc.ExecuteAll();
                break;
            } while (true);

            do {
                Console.WriteLine(
                    "Execute explorer action command by entering string of movement commands, deploy a new rover to " +
                    "provided coordinates and heading, or enter nothing to compose report:  ");
                consoleInput = Console.ReadLine()?.Trim();

                var nextCommand = commandParser.ParseCommandBlock(consoleInput);
                var commandList = nextCommand.ToList();
                // User is requesting report due to blank input being provided.
                if (commandList.Contains(null)) {
                    break;
                }

                mcc.SetCommands(commandList);
                mcc.ExecuteAll();
            } while (true);

            // TODO: Add in report generation capability at any point, not just at the end.

            Console.WriteLine("Composing exploration report...");
            Console.WriteLine(mcc.Explorers.GenerateExplorationReport());
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}