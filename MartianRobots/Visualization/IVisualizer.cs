using MartianRobots.Models;

namespace MartianRobots.Visualization
{
    /// <summary>Pluggable visualization for robot trajectories and grid.</summary>
    public interface IVisualizer
    {
        void Render(Grid grid, System.Collections.Generic.IEnumerable<(int X, int Y, char Dir, bool Lost)> robotStates);
    }
}