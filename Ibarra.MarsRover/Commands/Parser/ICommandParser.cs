using System.Collections.Generic;

namespace Ibarra.MarsRover.Commands.Parser {
    public interface ICommandParser {
        IEnumerable<IExplorerCommand> ParseCommandBlock(string commandBlock);
    }
}