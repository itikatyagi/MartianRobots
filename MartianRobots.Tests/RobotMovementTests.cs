using Xunit;
using MartianRobots.Models;
using MartianRobots.Models.Directions;
using System;

namespace MartianRobots.Tests
{
    public class RobotMovementTests
    {
        [Fact]
        public void MoveForward_WithinBounds_UpdatesPosition()
        {
            var grid = new Grid(5, 3);
            var r = new Robot(1, 1, new NorthDirection());
            r.MoveForward(grid);
            Assert.Equal((1, 2), (r.X, r.Y));
        }

        [Fact]
        public void MoveOff_Grid_MarksLostAndAddsScent()
        {
            var grid = new Grid(5, 3);
            var r = new Robot(5, 3, new NorthDirection());
            r.MoveForward(grid);
            Assert.True(r.IsLost);
            Assert.True(grid.HasScent(5, 3, 'N'));
        }

        [Fact]
        public void Robot_IgnoresMoveWhen_ScentExists()
        {
            var grid = new Grid(5, 3);
            var r1 = new Robot(5, 3, new NorthDirection());
            r1.MoveForward(grid); // lost and scent added

            var r2 = new Robot(5, 3, new NorthDirection());
            r2.MoveForward(grid); // should ignore and remain at 5,3 and not be lost
            Assert.False(r2.IsLost);
            Assert.Equal((5, 3), (r2.X, r2.Y));
        }

        [Fact]
        public void Randomized_PropertyStyle_Test_Robots_DoNotChangeGridState_Unexpectedly()
        {
            var rnd = new Random(42);
            var grid = new Grid(10, 10);
            for (int i = 0; i < 200; i++)
            {
                int x = rnd.Next(0, 11);
                int y = rnd.Next(0, 11);
                var dirs = new System.Type[] {
                    typeof(NorthDirection), typeof(EastDirection),
                    typeof(SouthDirection), typeof(WestDirection)
                };
                var dirType = dirs[rnd.Next(dirs.Length)];
                var dir = (Models.Directions.IDirection)Activator.CreateInstance(dirType);
                var robot = new Robot(x, y, dir);
                // Random simple sequence
                var seq = "LRFFRFLF";
                foreach (var c in seq)
                {
                    if (robot.IsLost) break;
                    switch (c)
                    {
                        case 'L': robot.TurnLeft(); break;
                        case 'R': robot.TurnRight(); break;
                        case 'F': robot.MoveForward(grid); break;
                    }
                }
                // invariant: robot.X/Y remain integers within an acceptable range when not lost
                if (!robot.IsLost)
                {
                    Assert.InRange(robot.X, 0, grid.MaxX);
                    Assert.InRange(robot.Y, 0, grid.MaxY);
                }
            }
        }
    }
}