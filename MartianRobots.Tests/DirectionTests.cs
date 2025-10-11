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

        // other directional tests...
    }
}
