using System.Linq;
using Ibarra.MarsRover.Commands;
using Ibarra.MarsRover.Commands.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibarra.MarsRoverTests.Commands.Parser {
    [TestClass]
    public class CommandParserTests {
        private const string ValidInput1 = "5 5 n";
        private const string ValidInput2 = "2 3 N";
        private const string ValidInput3 = "1 4";
        private const string ValidInput4 = "LLMRRMMRRM";
        private const string ValidInput5 = "L";
        private const string ValidInput6 = "R";
        private const string ValidInput7 = "M";
        private const string ValidInput8 = "l";
        private const string ValidInput9 = "r";
        private const string ValidInput10 = "m";

        private const string InvalidInput1 = "5";

        private const string InvalidInput2 = "10000000000000000000000000000000000000000000000000000000000000000000" +
                                             "00000000000000000000000000000000000000000000000000000000000000000000";

        private const string InvalidInput3 = "5 ";
        private const string InvalidInput4 = "1_";
        private const string InvalidInput5 = "5N";
        private const string InvalidInput6 = "-1";
        private const string InvalidInput7 = "-1 5";
        private const string InvalidInput8 = "DealerOn";
        private const string InvalidInput9 = "1.3245";
        private const string InvalidInput10 = "10 10 10";

        [TestMethod]
        public void ParseCommandBlockTest_InvalidInput_NullReturned() {
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput1).First());
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput2).First());
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput3).First());
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput4).First());
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput5).First());
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput6).First());
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput7).First());
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput8).First());
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput9).First());
            Assert.IsNull(new CommandParser().ParseCommandBlock(InvalidInput10).First());
        }

        [TestMethod]
        public void ParseCommandBlockTest_ValidInput_CommandObjectReturned() {
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput1).First());
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput2).First());
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput3).First());
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput4).First());
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput5).First());
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput6).First());
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput7).First());
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput8).First());
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput9).First());
            Assert.IsNotNull(new CommandParser().ParseCommandBlock(ValidInput10).First());
        }
    }
}