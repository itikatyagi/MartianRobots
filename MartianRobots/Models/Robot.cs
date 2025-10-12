using MartianRobots.Models.Directions;
using MartianRobots.Logging;

namespace MartianRobots.Models
{
    /// <summary>
    /// A robot navigating the Martian grid.
    /// </summary>
    public class Robot
    {
        private readonly ILogger _logger;

        public int X { get; private set; }
        public int Y { get; private set; }
        public IDirection Direction { get; private set; }
        public bool IsLost { get; private set; }

        /// <summary>
        /// Create a robot. Accepts a logger to allow observability/injection of different loggers.
        /// </summary>
        public Robot(int x, int y, IDirection direction, ILogger logger = null)
        {
            if (x < 0 || y < 0) throw new System.ArgumentException("Initial coordinates must be non-negative.");
            X = x; Y = y; Direction = direction ?? throw new System.ArgumentNullException(nameof(direction));
            _logger = logger ?? new ConsoleLogger();
        }

        /// <summary>Turn left 90 degrees.</summary>
        public void TurnLeft()
        {
            Direction = Direction.TurnLeft();
            _logger.Log($"TurnLeft -> Now facing {Direction.Name} at ({X},{Y})");
        }

        /// <summary>Turn right 90 degrees.</summary>
        public void TurnRight()
        {
            Direction = Direction.TurnRight();
            _logger.Log($"TurnRight -> Now facing {Direction.Name} at ({X},{Y})");
        }

        /// <summary>Move forward in current direction; handles LOST logic and scent checks.</summary>
        public void MoveForward(Grid grid)
        {
            if (IsLost) { _logger.Log("Move ignored: robot already LOST."); return; }

            var (dx, dy) = Direction.MoveVector();
            var nextX = X + dx;
            var nextY = Y + dy;

            _logger.Log($"Attempt move from ({X},{Y}) -> ({nextX},{nextY}) heading {Direction.Name}");

            if (grid.IsOutOfBounds(nextX, nextY))
            {
                // If scent exists at this cell heading, ignore
                if (grid.HasScent(X, Y, Direction.Name))
                {
                    _logger.Log($"Ignored move due to scent at ({X},{Y}) heading {Direction.Name}");
                    return;
                }

                // We get lost: leave scent at last valid coord & mark lost
                grid.AddScent(X, Y, Direction.Name);
                IsLost = true;
                _logger.Log($"Robot LOST at edge trying to move from ({X},{Y}) heading {Direction.Name}");
                return;
            }

            X = nextX; Y = nextY;
            _logger.Log($"Move success -> Now at ({X},{Y})");
        }

        public override string ToString() => $"{X} {Y} {Direction.Name}" + (IsLost ? " LOST" : "");
    }
}