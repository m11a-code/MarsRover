using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.Commands {
    public class DeployExplorerCommand : IExplorerCommand {
        private Explorer AssociatedExplorer { get; set; }
        public CommandChainType CommandChainType => CommandChainType.DeployExplorer;

        public readonly Position Position;
        public readonly Heading Heading;


        public DeployExplorerCommand(Position p, Heading h) {
            Position = p;
            Heading = h;
        }

        public void SetExplorer(Explorer explorer) {
            AssociatedExplorer = explorer;
        }

        public void Execute() {
            AssociatedExplorer.Launch(Position, Heading);
        }
    }
}