using System;
using System.Collections.Generic;
using Ibarra.MarsRover.ExplorationVehicles;
using Ibarra.MarsRover.Navigation;


namespace Ibarra.MarsRover.Commands {
    public class ExploreCommand : IExplorerCommand {
        private Explorer _explorer;

        public ExploreCommand(IList<Movement> movements) {
            Movements = movements ??
                        throw new ArgumentNullException(nameof(movements),
                            "List of movements cannot be null.");
        }

        private IList<Movement> Movements { get; }
        public CommandChainType CommandChainType => CommandChainType.Explore;

        public void SetExplorer(Explorer explorer) => _explorer = explorer;

        /// <inheritdoc />
        public bool Execute() {
            if (_explorer == null) {
                return false;
            }
            _explorer.Move(Movements);
            return true;
        }
    }
}