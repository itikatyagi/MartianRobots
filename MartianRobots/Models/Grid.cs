using System.Collections.Generic;

namespace MartianRobots.Models
{
    /// <summary>
    /// Represents the bounded rectangular Martian grid and scent registry.
    /// </summary>
    public class Grid
    {
        /// <summary>Maximum X coordinate (inclusive).</summary>
        public int MaxX { get; }

        /// <summary>Maximum Y coordinate (inclusive).</summary>
        public int MaxY { get; }

        private readonly HashSet<(int x, int y, char dir)> _scents = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        public Grid(int maxX, int maxY)
        {
            if (maxX < 0 || maxY < 0)
                throw new System.ArgumentException("Grid coordinates must be non-negative.");
            MaxX = maxX;
            MaxY = maxY;
        }

        /// <summary>Checks whether coords are out of bounds.</summary>
        public bool IsOutOfBounds(int x, int y)
            => x < 0 || y < 0 || x > MaxX || y > MaxY;

        /// <summary>Add a scent marker (last safe cell & heading) to prevent repeated losses.</summary>
        public void AddScent(int x, int y, char dir) => _scents.Add((x, y, dir));

        /// <summary>Checks if a scent exists at the coordinate and orientation.</summary>
        public bool HasScent(int x, int y, char dir) => _scents.Contains((x, y, dir));
    }
}
