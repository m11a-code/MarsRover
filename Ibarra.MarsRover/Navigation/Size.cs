using System;

namespace Ibarra.MarsRover.Navigation {
    /// <summary>
    /// Simple struct for the size of a deployment zone.
    /// </summary>
    public class Size {
        public int Width { get; }
        public int Height { get; }

        public Size(int width, int height) {
            if (!ValidateWidth(width)) {
                throw new ArgumentOutOfRangeException($"X value '{width}' cannot be less than or equal to zero.");
            }

            if (!ValidateHeight(height)) {
                throw new ArgumentOutOfRangeException($"Y value '{height}' cannot be less than or equal to zero.");
            }

            Width = width;
            Height = height;
        }

        private static bool ValidateWidth(int width) => width >= 1;

        private static bool ValidateHeight(int height) => height >= 1;
    }
}