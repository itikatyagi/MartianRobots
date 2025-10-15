using System;
using System.Linq;
using System.Collections.Generic;
using MartianRobots.Models;

namespace MartianRobots.Visualization
{
    /// <summary>Simple ASCII visualization of the final grid state and robot markers.</summary>
    public class AsciiVisualizer : IVisualizer
    {
        public void Render(Grid grid, IEnumerable<(int X, int Y, char Dir, bool Lost)> robotStates)
        {
            // Build empty grid (Y descending for display)
            var rows = new List<string>();
            for (int y = grid.MaxY; y >= 0; y--)
            {
                var row = "";
                for (int x = 0; x <= grid.MaxX; x++) row += ".";
                rows.Add(row);
            }

            // mark robots (last wins)
            foreach (var s in robotStates)
            {
                if (s.X < 0 || s.Y < 0 || s.X > grid.MaxX || s.Y > grid.MaxY) continue;
                var rowIdx = grid.MaxY - s.Y;
                var colIdx = s.X;
                var ch = s.Lost ? 'X' : s.Dir;
                var row = rows[rowIdx].ToCharArray();
                row[colIdx] = ch;
                rows[rowIdx] = new string(row);
            }

            Console.WriteLine("ASCII Grid (top row is Y=max):");
            foreach (var r in rows) Console.WriteLine(r);
        }
    }
}