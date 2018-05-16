using System.ComponentModel;

namespace Ibarra.MarsRover.Navigation {
    /// <summary>
    /// The type of movements explorers can perform while deployed.
    /// </summary>
    public enum Movement {
        Left,
        Right,
        Forward
    }

    /// <summary>
    /// Support methods for the <c>Movement</c> enum.
    /// </summary>
    public static class MovementMethods {
        public static string GetString(this Movement rotation) {
            switch (rotation) {
                case Movement.Right:
                    return "R";
                case Movement.Left:
                    return "L";
                case Movement.Forward:
                    return "M";
                default:
                    throw new InvalidEnumArgumentException("An unknown movement was provided.");
            }
        }

        public static Movement FromString(string movement) {
            switch (movement.ToLower()) {
                case "r":
                case "right":
                    return Movement.Right;
                case "l":
                case "left":
                    return Movement.Left;
                case "m":
                case "forward":
                case "move":
                    return Movement.Forward;
                default:
                    throw new InvalidEnumArgumentException(
                        $"The provided string '{movement}' could not be converted into a Movement.");
            }
        }
    }
}