using Ibarra.MarsRover.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ibarra.MarsRover.ExplorationVehicles {
    public class Rover : Explorer {
        private readonly IDictionary<Movement, Action> _roverMovementActions;
        private readonly IDictionary<Heading, Action> _roverRotateLeftActions;
        private readonly IDictionary<Heading, Action> _roverRotateRightActions;
        private readonly IDictionary<Heading, Action> _roverMoveForwardActions;

        /// <inheritdoc />
        public override void Move(IEnumerable<Movement> movements) {
            foreach (var movement in movements) {
                _roverMovementActions[movement].Invoke();
            }
        }

        /// <inheritdoc />
        protected override bool IsPositionValid(Position position)
            => position.X > -1 && position.Y > -1 &&
               DeploymentZoneChart.IsPositionWithinBounds(position);

        /// <inheritdoc />
        protected override bool ExplorerExistsAtPosition(Position position) {
            return ExplorationUnit.ToList().FindIndex(explorer
                       => explorer != this && explorer.Position.X == position.X
                                           && explorer.Position.Y == position.Y) != -1;
        }

        /// <summary>
        /// A specific kind of explorer that can navigate around many different deployment zones that humans otherwise 
        /// could not.
        /// </summary>
        /// <param name="crew">The team this rover is assigned to.</param>
        public Rover(ExplorationTeam crew) :
            base(crew) {
            _roverMovementActions = new Dictionary<Movement, Action> {
                {Movement.Left, () => _roverRotateLeftActions[Heading].Invoke()},
                {Movement.Right, () => _roverRotateRightActions[Heading].Invoke()},
                {Movement.Forward, () => _roverMoveForwardActions[Heading].Invoke()}
            };
            _roverRotateLeftActions = new Dictionary<Heading, Action> {
                {Heading.North, () => Heading = Heading.West},
                {Heading.South, () => Heading = Heading.East},
                {Heading.East, () => Heading = Heading.North},
                {Heading.West, () => Heading = Heading.South}
            };
            _roverRotateRightActions = new Dictionary<Heading, Action> {
                {Heading.North, () => Heading = Heading.East},
                {Heading.South, () => Heading = Heading.West},
                {Heading.East, () => Heading = Heading.South},
                {Heading.West, () => Heading = Heading.North}
            };
            _roverMoveForwardActions = new Dictionary<Heading, Action> {
                {
                    Heading.North, () => {
                        var nextPosition = new Position(Position.X, Position.Y + 1);
                        if (IsPositionAvailable(nextPosition)) {
                            Position = nextPosition;
                        } else {
                            Console.WriteLine("The position " + nextPosition + " is not avaiable. " +
                                              "Skipping movement action.");
                        }
                    }
                }, {
                    Heading.South, () => {
                        var nextPosition = new Position(Position.X, Position.Y - 1);
                        if (IsPositionAvailable(nextPosition)) {
                            Position = nextPosition;
                        } else {
                            Console.WriteLine("The position " + nextPosition + " is not avaiable. " +
                                              "Skipping movement action.");
                        }
                    }
                }, {
                    Heading.East, () => {
                        var nextPosition = new Position(Position.X + 1, Position.Y);
                        if (IsPositionAvailable(nextPosition)) {
                            Position = nextPosition;
                        } else {
                            Console.WriteLine("The position " + nextPosition + " is not avaiable. " +
                                              "Skipping movement action.");
                        }
                    }
                }, {
                    Heading.West, () => {
                        var nextPosition = new Position(Position.X - 1, Position.Y);
                        if (IsPositionAvailable(nextPosition)) {
                            Position = nextPosition;
                        } else {
                            Console.WriteLine("The position " + nextPosition + " is not avaiable. " +
                                              "Skipping movement action.");
                        }
                    }
                }
            };
        }
    }
}