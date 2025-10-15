using System;

namespace MartianRobots.Models.Directions
{
    public static class DirectionFactory
    {
        public static IDirection Create(char c) => c switch
        {
            'N' => new NorthDirection(),
            'S' => new SouthDirection(),
            'E' => new EastDirection(),
            'W' => new WestDirection(),
            _ => throw new ArgumentException($"Unknown direction '{c}'")
        };
    }
}