using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.Landscapes {
    [TestClass]
    public class PlateauTests {
        [TestMethod]
        public void IsPositionWithinBoundsTest_WidthTooLarge() {
            var plateau = new Plateau {Size = new Size(5, 5)};
            Assert.IsFalse(plateau.IsPositionWithinBounds(new Position(6, 5)));
        }

        [TestMethod]
        public void IsPositionWithinBoundsTest_HeightTooLarge() {
            var plateau = new Plateau {Size = new Size(5, 5)};
            Assert.IsFalse(plateau.IsPositionWithinBounds(new Position(5, 6)));
        }

        [TestMethod]
        public void IsPositionWithinBoundsTest_WidthAndHeightTooLarge() {
            var plateau = new Plateau {Size = new Size(5, 5)};
            Assert.IsFalse(plateau.IsPositionWithinBounds(new Position(10, 10)));
        }

        [TestMethod]
        public void IsPositionWithinBoundsTest_ValidPosition() {
            var plateau = new Plateau {Size = new Size(5, 5)};
            Assert.IsTrue(plateau.IsPositionWithinBounds(new Position(4, 3)));
        }
    }
}