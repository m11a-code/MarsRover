using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ibarra.MarsRover.Commands {
    /// <summary>
    /// A command chain is string of letters and/or numbers that can be parsed into an actionable command.
    /// </summary>
    public enum CommandChainType {
        InitializeDeploymentZone,
        DeployExplorer,
        Explore
    }

    /// <summary>
    /// Helper methods for the <c>CommandChainType</c> enum.
    /// </summary>
    public static class CommandChainTypeMethods {
        public static readonly IDictionary<Regex, CommandChainType> CommandChainTypeActions =
            new Dictionary<Regex, CommandChainType> {
                {new Regex(@"^\d+ \d+$"), CommandChainType.InitializeDeploymentZone},
                {new Regex(@"^\d+ \d+ [NSEWnsew]$"), CommandChainType.DeployExplorer},
                {new Regex(@"^[LRMlrm]+$"), CommandChainType.Explore}
            };
    }
}