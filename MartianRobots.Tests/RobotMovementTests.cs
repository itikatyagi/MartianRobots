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
        public void MoveOffGrid_AllDirections_ProducesLostAndScent()
        {
            var grid = new Grid(2, 2);

            // North
            var rN = new Robot(1, 2, new NorthDirection());
            rN.MoveForward(grid);
            Assert.True(rN.IsLost);
            Assert.True(grid.HasScent(1, 2, 'N'));

            // East
            var rE = new Robot(2, 1, new EastDirection());
            rE.MoveForward(grid);
            Assert.True(rE.IsLost);
            Assert.True(grid.HasScent(2, 1, 'E'));

            // South
            var rS = new Robot(1, 0, new SouthDirection());
            rS.MoveForward(grid);
            Assert.True(rS.IsLost);
            Assert.True(grid.HasScent(1, 0, 'S'));

            // West
            var rW = new Robot(0, 1, new WestDirection());
            rW.MoveForward(grid);
            Assert.True(rW.IsLost);
            Assert.True(grid.HasScent(0, 1, 'W'));
        }

        [Fact]
        public void TurnAndMove_Sequence_UpdatesCorrectly()
        {
            var grid = new Grid(5, 5);
            var r = new Robot(2, 2, new NorthDirection());
            r.TurnRight(); // E
            r.MoveForward(grid); // 3,2
            r.TurnRight(); // S
            r.MoveForward(grid); // 3,1
            r.TurnLeft(); // E
            r.MoveForward(grid); // 4,1

            Assert.Equal((4, 1), (r.X, r.Y));
            Assert.IsType<EastDirection>(r.Direction);
        }

        [Fact]
        public void MultipleRobots_ScentsPreventRepeatedLoss()
        {
            var grid = new Grid(3, 3);

            var r1 = new Robot(3, 3, new NorthDirection());
            r1.MoveForward(grid); // lost, scent at 3,3,N

            var r2 = new Robot(3, 3, new NorthDirection());
            r2.MoveForward(grid); // should ignore move, not lost

            Assert.True(r1.IsLost);
            Assert.False(r2.IsLost);
            Assert.Equal((3, 3), (r2.X, r2.Y));
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
                var dirs = new Type[] {
                    typeof(NorthDirection), typeof(EastDirection),
                    typeof(SouthDirection), typeof(WestDirection)
                };
                var dirType = dirs[rnd.Next(dirs.Length)];
                var dir = (IDirection)Activator.CreateInstance(dirType);
                var robot = new Robot(x, y, dir);

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

                if (!robot.IsLost)
                {
                    Assert.InRange(robot.X, 0, grid.MaxX);
                    Assert.InRange(robot.Y, 0, grid.MaxY);
                }
            }
        }
    }
}
