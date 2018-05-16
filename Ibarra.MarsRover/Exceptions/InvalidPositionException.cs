using System;

namespace Ibarra.MarsRover.Exceptions {
    /// <summary>
    /// Simple wrapper for an exception that shows that this is specifically due to the information provided to a
    /// <c>Position</c> object is invalid.
    /// </summary>
    public class InvalidPositionException : Exception {
        public InvalidPositionException(string message) : base(message) {
        }
    }
}