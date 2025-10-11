using Xunit;
using MartianRobots.Models;
using MartianRobots.Models.Directions;

namespace MartianRobots.Tests
{
    public class RobotBasicTests
    {
        [Fact]
        public void Robot_Turns_Left_Right()
        {
            var robot = new Robot(0, 0, new NorthDirection());
            robot.TurnRight();
            Assert.Equal('E', robot.Direction.Name);
            robot.TurnLeft();
            Assert.Equal('N', robot.Direction.Name);
        }
    }
}
