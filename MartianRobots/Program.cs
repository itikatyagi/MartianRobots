using System;
using System.IO;
using MartianRobots.Parsing;
using MartianRobots.Runner;

class Program
{
    static void Main(string[] args)
    {
        // default: read stdin (extensible). If an input file path is provided, use that.
        using var reader = args.Length > 0 ? new StreamReader(args[0]) : Console.In;
        var parser = new SimpleTextParser();
        var runner = new SimulationRunner(parser, Console.Out);
        runner.Run(reader);
    }
}