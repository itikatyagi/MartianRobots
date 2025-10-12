using System;
using System.IO;
using MartianRobots.Runner;
using MartianRobots.Parsing;
using MartianRobots.Visualization;

namespace MartianRobots.RunnerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Select input: either from a file path argument or Console input
            using var reader = args.Length > 0
                ? new StreamReader(args[0])
                : Console.In;

            var parser = new SimpleTextParser();
            var visualizer = new AsciiVisualizer(); // optional: pass null if you don't want visualization
            var runner = new SimulationRunner(parser, Console.Out, visualizer);

            try
            {
                runner.Run(reader);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
