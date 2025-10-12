using Xunit;
using MartianRobots;

namespace MartianRobots.Tests
{
    public class CommandFactoryTests
    {
        [Fact]
        public void Factory_Creates_CorrectCommandInstances()
        {
            Assert.IsType<Commands.TurnLeftCommand>(CommandFactory.Create('L'));
            Assert.IsType<Commands.TurnRightCommand>(CommandFactory.Create('R'));
            Assert.IsType<Commands.MoveForwardCommand>(CommandFactory.Create('F'));
        }
    }
}