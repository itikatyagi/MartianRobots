using Xunit;
using MartianRobots.Parsing;
using System.IO;
using System.Text;

namespace MartianRobots.Tests
{
    public class ParserTests
    {
        [Fact]
        public void Parser_Interface_Should_ParseGridAndRobotBlocks_FromText()
        {
            var input = new StringBuilder();
            input.AppendLine("5 3");
            input.AppendLine("1 1 E");
            input.AppendLine("RFRFRFRF");
            using var sr = new StringReader(input.ToString());

            IInputParser parser = new SimpleTextParser();
            var result = parser.Parse(sr);

            Assert.Equal((5, 3), result.GridSize);
            Assert.Single(result.Robots);
        }
    }
}