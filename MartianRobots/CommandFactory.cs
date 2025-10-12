using System;
using MartianRobots.Commands;

namespace MartianRobots
{
    /// <summary>Factory to create command instances from characters.</summary>
    public static class CommandFactory
    {
        public static ICommand Create(char c)
        {
            return c switch
            {
                'L' => new TurnLeftCommand(),
                'R' => new TurnRightCommand(),
                'F' => new MoveForwardCommand(),
                _ => throw new InvalidOperationException($"Unknown command '{c}'")
            };
        }
    }
}