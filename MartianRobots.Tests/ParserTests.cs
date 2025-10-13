using Xunit;
using MartianRobots.Parsing;
using System.IO;
using System.Text;
using System.Linq;

namespace MartianRobots.Tests
{
    public class ParserTests
    {
        [Fact]
        public void Parser_Should_ParseGridSizeCorrectly()
        {
            var input = new StringBuilder();
            input.AppendLine("5 3");
            using var sr = new StringReader(input.ToString());

            IInputParser parser = new SimpleTextParser();
            var result = parser.Parse(sr);

            Assert.Equal((5, 3), result.GridSize);
        }

        [Fact]
        public void Parser_Should_ParseSingleRobotScenario()
        {
            var input = new StringBuilder();
            input.AppendLine("5 3");
            input.AppendLine("1 1 E");
            input.AppendLine("RFRFRFRF");

            using var sr = new StringReader(input.ToString());

            IInputParser parser = new SimpleTextParser();
            var result = parser.Parse(sr);

            Assert.Single(result.Robots);
            var robot = result.Robots.First();
            Assert.Equal(1, robot.X);
            Assert.Equal(1, robot.Y);
            Assert.Equal('E', robot.Direction);
            Assert.Equal("RFRFRFRF", robot.Commands);
        }

        [Fact]
        public void Parser_Should_ParseMultipleRobots()
        {
            var input = new StringBuilder();
            input.AppendLine("5 5");
            input.AppendLine("0 0 N");
            input.AppendLine("FRFR");
            input.AppendLine("2 2 S");
            input.AppendLine("LLFF");

            using var sr = new StringReader(input.ToString());

            IInputParser parser = new SimpleTextParser();
            var result = parser.Parse(sr);

            Assert.Equal(2, result.Robots.Count);

            var first = result.Robots[0];
            Assert.Equal(0, first.X);
            Assert.Equal(0, first.Y);
            Assert.Equal('N', first.Direction);
            Assert.Equal("FRFR", first.Commands);

            var second = result.Robots[1];
            Assert.Equal(2, second.X);
            Assert.Equal(2, second.Y);
            Assert.Equal('S', second.Direction);
            Assert.Equal("LLFF", second.Commands);
        }

        [Fact]
        public void Parser_Should_Throw_OnInvalidDirection()
        {
            var input = new StringBuilder();
            input.AppendLine("5 5");
            input.AppendLine("0 0 X");
            input.AppendLine("FRFR");

            using var sr = new StringReader(input.ToString());

            IInputParser parser = new SimpleTextParser();

            Assert.Throws<System.ArgumentException>(() => parser.Parse(sr));
        }

        [Fact]
        public void Parser_Should_Throw_OnInvalidCommand()
        {
            var input = new StringBuilder();
            input.AppendLine("5 5");
            input.AppendLine("0 0 N");
            input.AppendLine("FXFR"); // 'X' invalid

            using var sr = new StringReader(input.ToString());

            IInputParser parser = new SimpleTextParser();

            Assert.Throws<System.ArgumentException>(() => parser.Parse(sr));
        }
    }
}