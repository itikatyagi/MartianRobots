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

        [Fact]
        public void MoveCommand_MarksRobotAsLost_WhenMovingOffGrid()
        {
            var grid = new Grid(2, 2);
            var r = new Robot(2, 2, new NorthDirection());
            new MoveForwardCommand().Execute(r, grid);
            Assert.True(r.IsLost);
            Assert.True(grid.HasScent(2, 2, 'N'));
        }

        [Fact]
        public void MoveCommand_IgnoresMove_WhenScentExists()
        {
            var grid = new Grid(2, 2);
            var r1 = new Robot(2, 2, new NorthDirection());
            new MoveForwardCommand().Execute(r1, grid); // lost and scent added

            var r2 = new Robot(2, 2, new NorthDirection());
            new MoveForwardCommand().Execute(r2, grid); // should ignore move
            Assert.False(r2.IsLost);
            Assert.Equal((2, 2), (r2.X, r2.Y));
        }

        [Fact]
        public void SequenceOfCommands_MovesRobotCorrectly()
        {
            var grid = new Grid(5, 5);
            var r = new Robot(1, 1, new NorthDirection());
            var commands = new ICommand[]
            {
                new MoveForwardCommand(),
                new TurnRightCommand(),
                new MoveForwardCommand(),
                new TurnLeftCommand(),
                new MoveForwardCommand()
            };

            foreach (var cmd in commands)
                cmd.Execute(r, grid);

            Assert.Equal((2, 3), (r.X, r.Y));
            Assert.Equal('N', r.Direction.Name);
        }

        [Fact]
        public void MoveForwardCommand_DoesNotThrow_WithValidGrid()
        {
            var r = new Robot(0, 0, new NorthDirection());
            var grid = new Grid(5, 5);
            var cmd = new MoveForwardCommand();
            var ex = Record.Exception(() => cmd.Execute(r, grid));
            Assert.Null(ex);
        }

    }
}
