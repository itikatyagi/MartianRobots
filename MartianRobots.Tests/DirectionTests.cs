using Xunit;
using MartianRobots.Models.Directions;

namespace MartianRobots.Tests
{
    public class DirectionTests
    {
        [Fact]
        public void North_Turns_Left_To_West_And_Right_To_East()
        {
            var n = new NorthDirection();
            Assert.IsType<WestDirection>(n.TurnLeft());
            Assert.IsType<EastDirection>(n.TurnRight());
        }

        [Fact]
        public void East_Turns_Left_To_North_And_Right_To_South()
        {
            var e = new EastDirection();
            Assert.IsType<NorthDirection>(e.TurnLeft());
            Assert.IsType<SouthDirection>(e.TurnRight());
        }

        [Fact]
        public void South_Turns_Left_To_East_And_Right_To_West()
        {
            var s = new SouthDirection();
            Assert.IsType<EastDirection>(s.TurnLeft());
            Assert.IsType<WestDirection>(s.TurnRight());
        }

        [Fact]
        public void West_Turns_Left_To_South_And_Right_To_North()
        {
            var w = new WestDirection();
            Assert.IsType<SouthDirection>(w.TurnLeft());
            Assert.IsType<NorthDirection>(w.TurnRight());
        }

        [Fact]
        public void North_MoveVector_IsCorrect()
        {
            var n = new NorthDirection();
            var (dx, dy) = n.MoveVector();
            Assert.Equal(0, dx);
            Assert.Equal(1, dy);
        }

        [Fact]
        public void East_MoveVector_IsCorrect()
        {
            var e = new EastDirection();
            var (dx, dy) = e.MoveVector();
            Assert.Equal(1, dx);
            Assert.Equal(0, dy);
        }

        [Fact]
        public void South_MoveVector_IsCorrect()
        {
            var s = new SouthDirection();
            var (dx, dy) = s.MoveVector();
            Assert.Equal(0, dx);
            Assert.Equal(-1, dy);
        }

        [Fact]
        public void West_MoveVector_IsCorrect()
        {
            var w = new WestDirection();
            var (dx, dy) = w.MoveVector();
            Assert.Equal(-1, dx);
            Assert.Equal(0, dy);
        }
    }
}
