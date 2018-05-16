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
                // I can either take a file and parse that for the commands or take console input directly.
                case 0:
                    DirectCommandLineInput();
                    break;
                case 1 when IsHelpCommandInput(args[0]):
                    OutputHelpInformation();
                    break;
                case 1:
                    // FileCommandLineInput();
                    Console.WriteLine(
                        "Reading input from a file is not currently supported. Type --help for help menu.");
                    break;
                default:
                    Console.WriteLine("Invalid input type found. Type --help for help menu.");
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

                deploymentZone = new Plateau(deploymentZoneChartCommand.Size);
                deploymentZoneChartCommand.Execute();
                Console.WriteLine("Deployment zone initialized successfully.");
            } while (deploymentZone == null);

            Console.WriteLine("Bringing Mission Control Center (MCC) online...");
            var mcc = new MissionControlCenter(deploymentZone);
            Console.WriteLine("MCC Online.");

            // Must deploy at least one rover before providing any other commands.
            do {
                Console.WriteLine("Enter deployment coordinates and heading of exploration module:  ");
                consoleInput = Console.ReadLine()?.Trim();

                var launchRoverCommand = commandParser.ParseCommandBlock(consoleInput);
                if (launchRoverCommand == null) {
                    continue;
                }

                var explorerCommands = launchRoverCommand.ToList();
                var command = explorerCommands.First();
                if (!(command is DeployExplorerCommand)) {
                    continue;
                }

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
                if (commandList.Contains(null)) {
                    break;
                }

                mcc.SetCommands(commandList);
                mcc.ExecuteAll();
            } while (true);

            Console.WriteLine("Composing exploration report...");
            Console.WriteLine(ComposeControlCenterReports(mcc));
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static void FileCommandLineInput() {
        }

        private static void OutputHelpInformation() {
        }

        private static bool IsHelpCommandInput(string token) {
            return token.Equals("--?") || token.Equals("-?") || token.Equals("?") || token.Equals("--help") ||
                   token.Equals("-help") || token.Equals("help") || token.Equals("--h") || token.Equals("-h") ||
                   token.Equals("h");
        }

        private static string ComposeControlCenterReports(IControlCenter controlCenter) {
            var reports = new StringBuilder();
            foreach (var explorer in controlCenter.Explorers) {
                reports.AppendFormat("{0} {1} {2}", explorer.Position.X, explorer.Position.Y,
                    explorer.Heading.GetString());
            }

            return reports.ToString();
        }
    }
}