using System.IO;
using System.Collections.Generic;
using MartianRobots.Models;

namespace MartianRobots.Parsing
{
    /// <summary>Abstracts input parsing so multiple input formats are supported.</summary>
    public interface IInputParser
    {
        /// <summary>Parses input stream and returns grid & robot scenarios.</summary>
        ParseResult Parse(TextReader reader);
    }

    public class RobotScenario
    {
        public int X { get; }
        public int Y { get; }
        public char Direction { get; }
        public string Commands { get; }

        public RobotScenario(int x, int y, char direction, string commands)
        {
            X = x; Y = y; Direction = direction; Commands = commands;
        }
    }

    public class ParseResult
    {
        public (int X, int Y) GridSize { get; }
        public List<RobotScenario> Robots { get; }

        public ParseResult((int X, int Y) gridSize, List<RobotScenario> robots)
        {
            GridSize = gridSize;
            Robots = robots;
        }
    }

}