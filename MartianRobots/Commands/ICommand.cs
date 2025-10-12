using MartianRobots.Models;

namespace MartianRobots.Commands
{
    /// <summary>Encapsulates a robot command (L, R, F, ...).</summary>
    public interface ICommand
    {
        void Execute(Robot robot, Grid grid);
    }
}