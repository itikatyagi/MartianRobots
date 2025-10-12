using MartianRobots.Models;

namespace MartianRobots.Commands
{
    public class MoveForwardCommand : ICommand
    {
        public void Execute(Robot robot, Grid grid) => robot.MoveForward(grid);
    }
}