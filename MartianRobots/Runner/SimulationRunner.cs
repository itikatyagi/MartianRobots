using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MartianRobots.Models;
using MartianRobots.Models.Directions;
using MartianRobots.Parsing;
using MartianRobots.Commands;
using MartianRobots.Visualization;

namespace MartianRobots.Runner
{
    /// <summary>
    /// Runs the complete Martian Robots simulation.
    /// Parses input, executes all robot commands, 
    /// writes text output, and optionally renders visualization.
    /// </summary>
    public class SimulationRunner
    {
        private readonly IInputParser _parser;
        private readonly TextWriter _output;
        private readonly IVisualizer _visualizer;

        /// <summary>
        /// Creates a new simulation runner.
        /// </summary>
        /// <param name="parser">The parser to interpret input format.</param>
        /// <param name="output">The output stream for textual results.</param>
        /// <param name="visualizer">
        /// Optional visualizer for rendering final grid and robot states (e.g., ASCII view or UI renderer).
        /// </param>
        public SimulationRunner(IInputParser parser, TextWriter output, IVisualizer visualizer = null)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _visualizer = visualizer; // can be null — visualization is optional
        }

        /// <summary>
        /// Executes the entire simulation from input stream.
        /// </summary>
        /// <param name="input">Input text defining grid and robot commands.</param>
        public void Run(TextReader input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Parse the scenario
            var parsed = _parser.Parse(input);
            var grid = new Grid(parsed.GridSize.X, parsed.GridSize.Y);

            // Collect final robot states for visualization
            var robotStates = new List<(int X, int Y, char Dir, bool Lost)>();

            foreach (var r in parsed.Robots)
            {
                var direction = DirectionFactory.Create(r.Direction);
                var robot = new Robot(r.X, r.Y, direction);

                foreach (var c in r.Commands)
                {
                    if (robot.IsLost) break;
                    var cmd = CommandFactory.Create(c);
                    cmd.Execute(robot, grid);
                }

                // Output result line (standard text output)
                _output.WriteLine(robot.ToString());

                // Store final robot position and state for visualization
                robotStates.Add((robot.X, robot.Y, robot.Direction.Name, robot.IsLost));
            }

            // If visualization is enabled, render the grid and robot states
            _visualizer?.Render(grid, robotStates);
        }
    }
}
