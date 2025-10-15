using Xunit;
using System.IO;
using MartianRobots.Parsing;
using MartianRobots.Runner;
using MartianRobots.Visualization;
using System.Text;
using System;

namespace MartianRobots.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void SampleInput_ProducesExpectedOutputLines()
        {
            var sample = @"5 3
                           1 1 E
                           RFRFRFRF
                           3 2 N
                           FRRFLLFFRRFLL
                           0 3 W
                           LLFFFLFLFL
                           ";
            using var sr = new StringReader(sample);
            var sb = new StringBuilder();
            using var sw = new StringWriter(sb);

            var runner = new SimulationRunner(new SimpleTextParser(), sw);
            runner.Run(sr);

            var output = sb.ToString();
            Assert.Contains("1 1 E", output);
            Assert.Contains("3 3 N LOST", output);
            Assert.Contains("2 3 S", output);
        }

        [Fact]
        public void Robot_Loses_WhenMovingOffGrid()
        {
            var input = @"3 3
                          3 3 N
                          F
                          ";
            using var sr = new StringReader(input);
            var sb = new StringBuilder();
            using var sw = new StringWriter(sb);

            var runner = new SimulationRunner(new SimpleTextParser(), sw);
            runner.Run(sr);

            var output = sb.ToString();
            Assert.Contains("3 3 N LOST", output);
        }

        [Fact]
        public void Robot_Ignores_Move_WhenScentExists()
        {
            var input = @"3 3
                          3 3 N
                          F
                          3 3 N
                          F
                          ";
            using var sr = new StringReader(input);
            var sb = new StringBuilder();
            using var sw = new StringWriter(sb);

            var runner = new SimulationRunner(new SimpleTextParser(), sw);
            runner.Run(sr);

            var output = sb.ToString();
            Assert.Contains("3 3 N LOST", output);
            Assert.Contains("3 3 N", output); // second robot ignored fatal move
            Assert.DoesNotContain("LOST", output.Split('\n')[1]); // second robot not lost
        }

        [Fact]
        public void MultipleRobots_Correctly_UpdatePositions()
        {
            var input = @"5 5
                          0 0 N
                          FRFRFRFR
                          1 1 E
                          RFLF
                          ";
            using var sr = new StringReader(input);
            var sb = new StringBuilder();
            using var sw = new StringWriter(sb);

            var runner = new SimulationRunner(new SimpleTextParser(), sw);
            runner.Run(sr);

            var output = sb.ToString();
            Assert.Contains("0 0 N", output);
            Assert.Contains("2 0 E", output);
        }

        [Fact]
        public void Handles_EmptyInput_ThrowsInvalidDataException()
        {
            var input = @"";
            using var sr = new StringReader(input);
            var sb = new StringBuilder();
            using var sw = new StringWriter(sb);

            var runner = new SimulationRunner(new SimpleTextParser(), sw);

            Assert.Throws<InvalidDataException>(() => runner.Run(sr));
        }

        [Fact]
        public void Handles_InvalidCommands_ThrowsException()
        {
            var input = @"3 3
                          0 0 N
                          X";
            using var sr = new StringReader(input);
            var sb = new StringBuilder();
            using var sw = new StringWriter(sb);

            var runner = new SimulationRunner(new SimpleTextParser(), sw);

            Assert.Throws<ArgumentException>(() => runner.Run(sr));
        }
    }
}
