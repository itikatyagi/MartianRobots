using MartianRobots.Models;

namespace MartianRobots.Commands
{
    public class TurnRightCommand : ICommand
    {
        public void Execute(Robot robot, Grid grid) => robot.TurnRight();
    }
}