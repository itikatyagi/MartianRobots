using Xunit;
using System.IO;
using MartianRobots.Parsing;
using MartianRobots.Runner;
using MartianRobots.Visualization;
using System.Text;

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
    }
}