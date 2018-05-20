using System;
using Ibarra.MarsRover.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.Navigation {
    [TestClass]
    public class SizeTests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SizeTest_InvalidWidth() {
            var size = new Size(0, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SizeTest_InvalidHeight() {
            var size = new Size(5, -1);
        }
    }
}