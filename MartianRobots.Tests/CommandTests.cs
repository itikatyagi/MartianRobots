using Xunit;
using MartianRobots.Models;
using MartianRobots.Models.Directions;
using MartianRobots.Commands;

namespace MartianRobots.Tests
{
    public class CommandTests
    {
        [Fact]
        public void TurnCommands_ChangeDirection()
        {
            var r = new Robot(0, 0, new NorthDirection());
            new TurnRightCommand().Execute(r, null);
            Assert.Equal('E', r.Direction.Name);
            new TurnLeftCommand().Execute(r, null);
            Assert.Equal('N', r.Direction.Name);
        }

        [Fact]
        public void MoveCommand_MovesTheRobot()
        {
            var grid = new Grid(5, 5);
            var r = new Robot(1, 1, new EastDirection());
            new MoveForwardCommand().Execute(r, grid);
            Assert.Equal((2, 1), (r.X, r.Y));
        }
    }
}