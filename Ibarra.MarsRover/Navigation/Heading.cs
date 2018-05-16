using System.ComponentModel;

namespace Ibarra.MarsRover.Navigation {
    public enum Heading {
        North,
        South,
        East,
        West
    }

    public static class HeadingMethods {
        public static string GetString(this Heading heading) {
            switch (heading) {
                case Heading.North:
                    return "N";
                case Heading.South:
                    return "S";
                case Heading.East:
                    return "E";
                case Heading.West:
                    return "W";
                default:
                    throw new InvalidEnumArgumentException("An unknown heading was provided.");
            }
        }

        public static Heading FromString(string heading) {
            switch (heading.ToLower()) {
                case "n":
                case "north":
                    return Heading.North;
                case "s":
                case "south":
                    return Heading.South;
                case "e":
                case "east":
                    return Heading.East;
                case "w":
                case "west":
                    return Heading.West;
                default:
                    throw new InvalidEnumArgumentException(
                        $"The provided string '{heading}' could not be converted into a Heading.");
            }
        }
    }
}