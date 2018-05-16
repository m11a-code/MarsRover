using System;

namespace Ibarra.MarsRover.Navigation {
    public class Position {
        public Position(int x, int y) {
            if (!ValidateX(x)) {
                throw new ArgumentOutOfRangeException($"X value '{x}' cannot be negative.");
            }

            if (!ValidateY(y)) {
                throw new ArgumentOutOfRangeException($"Y value '{y}' cannot be negative.");
            }

            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        private static bool ValidateX(int x) => x >= 0;

        private static bool ValidateY(int y) => y >= 0;
    }
}