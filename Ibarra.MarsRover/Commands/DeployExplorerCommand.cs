using System;
using Ibarra.MarsRover.Exceptions;
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
            Position = position ?? throw new ArgumentNullException(nameof(position),
                           "Provided position cannot be null.");
            Heading = heading;
        }

        public void SetExplorer(Explorer explorer) {
            AssociatedExplorer =
                explorer ?? throw new ArgumentNullException(nameof(explorer), "Explorer cannot be null.");
        }

        /// <inheritdoc />
        public bool Execute() {
            if (AssociatedExplorer == null) {
                return false;
            }

            try {
                AssociatedExplorer.Launch(Position, Heading);
            } catch (InvalidPositionException positionException) {
                Console.WriteLine("The provided position " + Position + " was invalid. Please provide another " +
                                  "position. Exception message: " + positionException.Message, positionException);
                return false;
            }

            return true;
        }
    }
}