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
            var parser = new SimpleTextParser();
            var visualizer = new AsciiVisualizer(); // optional
            var runner = new SimulationRunner(parser, Console.Out, visualizer);

            using var reader = args.Length > 0 ? new StreamReader(args[0]) : Console.In;
            runner.Run(reader);
        }
    }
}
