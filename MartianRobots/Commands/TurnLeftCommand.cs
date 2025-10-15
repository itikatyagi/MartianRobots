using MartianRobots.Models;

namespace MartianRobots.Commands
{
    public class TurnLeftCommand : ICommand
    {
        public void Execute(Robot robot, Grid grid) => robot.TurnLeft();
    }
}