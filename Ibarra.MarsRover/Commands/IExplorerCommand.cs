namespace Ibarra.MarsRover.Commands {
    /// <summary>
    /// A basic command that <c>Explorer</c>s are able to perform.
    /// </summary>
    public interface IExplorerCommand {
        CommandChainType CommandChainType { get; }

        /// <summary>
        /// The command to be executed.
        /// </summary>
        /// <returns>true is successful.</returns>
        bool Execute();
    }
}