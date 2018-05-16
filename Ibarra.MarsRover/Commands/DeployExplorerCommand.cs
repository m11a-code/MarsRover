using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.Commands {
    /// <summary>
    /// The command responsible for deploying an explorer.
    /// </summary>
    public class DeployExplorerCommand : IExplorerCommand {
        private Explorer AssociatedExplorer { get; set; }
        public CommandChainType CommandChainType => CommandChainType.DeployExplorer;

        public readonly Position Position;
        public readonly Heading Heading;

        public DeployExplorerCommand(Position position, Heading heading) {
            Position = position;
            Heading = heading;
        }

        public void SetExplorer(Explorer explorer) => AssociatedExplorer = explorer;

        /// <inheritdoc />
        public void Execute() {
            AssociatedExplorer.Launch(Position, Heading);
        }
    }
}