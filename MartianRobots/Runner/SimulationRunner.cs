using System;
using System.IO;
using System.Linq;
using MartianRobots.Models;
using MartianRobots.Models.Directions;
using MartianRobots.Parsing;
using MartianRobots.Commands;

namespace MartianRobots.Runner
{
    /// <summary>Runs full simulation given an input parser and output writer.</summary>
    public class SimulationRunner
    {
        private readonly IInputParser _parser;
        private readonly TextWriter _output;
        public SimulationRunner(IInputParser parser, TextWriter output)
        {
            _parser = parser;
            _output = output;
        }

        public void Run(TextReader input)
        {
            var parsed = _parser.Parse(input);
            var grid = new Grid(parsed.GridSize.X, parsed.GridSize.Y);

            foreach (var r in parsed.Robots)
            {
                var dir = DirectionFactory.Create(r.Direction);
                var robot = new Robot(r.X, r.Y, dir);
                foreach (var c in r.Commands)
                {
                    if (robot.IsLost) break;
                    var cmd = CommandFactory.Create(c);
                    cmd.Execute(robot, grid);
                }
                _output.WriteLine(robot.ToString());
            }
        }
    }
}