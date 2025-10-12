using Xunit;
using MartianRobots;
using System;

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

        [Fact]
        public void Factory_ThrowsArgumentException_ForInvalidCommand()
        {
            Assert.Throws<ArgumentException>(() => CommandFactory.Create('X'));
        }

        [Theory]
        [InlineData('L', typeof(Commands.TurnLeftCommand))]
        [InlineData('R', typeof(Commands.TurnRightCommand))]
        [InlineData('F', typeof(Commands.MoveForwardCommand))]
        public void Factory_Returns_CorrectCommandType(char commandChar, Type expectedType)
        {
            var cmd = CommandFactory.Create(commandChar);
            Assert.IsType(expectedType, cmd);
        }

        [Fact]
        public void Factory_Creates_Command_RegardlessOfCase()
        {
            Assert.IsType<Commands.TurnLeftCommand>(CommandFactory.Create(char.ToLower('L')));
        }

    }
}