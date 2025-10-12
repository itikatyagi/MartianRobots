using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MartianRobots.Parsing;

namespace MartianRobots.Parsing
{
    /// <summary>Parses the classic Martian Robots textual format.</summary>
    public class SimpleTextParser : IInputParser
    {
        public ParseResult Parse(TextReader reader)
        {
            var lines = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line == "") continue;
                lines.Add(line);
            }
            if (lines.Count == 0) throw new InvalidDataException("Input is empty.");

            var gridParts = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (gridParts.Length != 2) throw new InvalidDataException("Invalid grid definition.");
            int gx = int.Parse(gridParts[0]), gy = int.Parse(gridParts[1]);

            var robots = new List<RobotScenario>();
            for (int i = 1; i + 1 < lines.Count; i += 2)
            {
                var pos = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (pos.Length != 3) throw new InvalidDataException($"Invalid robot position line: {lines[i]}");
                int rx = int.Parse(pos[0]), ry = int.Parse(pos[1]);
                char dir = pos[2][0];
                var commands = lines[i + 1].Trim().ToUpperInvariant();
                robots.Add(new RobotScenario(rx, ry, dir, commands));
            }

            return new ParseResult((gx, gy), robots);
        }
    }
}