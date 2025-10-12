using System;
using MartianRobots.Commands;

namespace MartianRobots
{
    /// <summary>Factory to create command instances from characters.</summary>
    public static class CommandFactory
    {
        public static ICommand Create(char c)
        {
            switch (char.ToUpper(c)) // normalize input to uppercase
            {
                case 'L': return new TurnLeftCommand();
                case 'R': return new TurnRightCommand();
                case 'F': return new MoveForwardCommand();
                default:
                    throw new ArgumentException($"Unknown command '{c}'");
            }
        }
    }
}
