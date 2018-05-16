using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ibarra.MarsRover.Commands {
    public interface IExplorerCommand {
        CommandChainType CommandChainType { get; }

        void Execute();
    }

    public enum CommandChainType {
        InitializeDeploymentZone,
        DeployExplorer,
        Explore
    }

    public static class CommandChainTypeMethods {
        public static readonly IDictionary<Regex, CommandChainType> CommandChainTypeActions =
            new Dictionary<Regex, CommandChainType> {
                {new Regex(@"^\d+ \d+$"), CommandChainType.InitializeDeploymentZone},
                {new Regex(@"^\d+ \d+ [NSEWnsew]$"), CommandChainType.DeployExplorer},
                {new Regex(@"^[LRMlrm]+$"), CommandChainType.Explore}
            };
    }
}