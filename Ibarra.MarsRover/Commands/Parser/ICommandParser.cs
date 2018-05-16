using System.Collections.Generic;

namespace Ibarra.MarsRover.Commands.Parser {
    /// <summary>
    /// Provides command parsing functionality.
    /// </summary>
    public interface ICommandParser {

        /// <summary>
        /// Parses a provided string for all relevant commands.
        /// </summary>
        /// <param name="commandBlock">String of commands to parse.</param>
        /// <returns>A collection of relevant commands.</returns>
        IEnumerable<IExplorerCommand> ParseCommandBlock(string commandBlock);
    }
}