using System;

namespace Ibarra.MarsRover.Exceptions {
    public class InvalidPositionException : Exception {
        public InvalidPositionException(string message) : base(message) {
        }
    }
}