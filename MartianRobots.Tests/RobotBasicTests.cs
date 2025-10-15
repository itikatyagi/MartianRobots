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

        [Fact]
        public void Robot_Multiple_Left_Turns_Cycle_Through_Directions()
        {
            var robot = new Robot(0, 0, new NorthDirection());
            robot.TurnLeft(); // N -> W
            Assert.Equal('W', robot.Direction.Name);
            robot.TurnLeft(); // W -> S
            Assert.Equal('S', robot.Direction.Name);
            robot.TurnLeft(); // S -> E
            Assert.Equal('E', robot.Direction.Name);
            robot.TurnLeft(); // E -> N
            Assert.Equal('N', robot.Direction.Name);
        }

        [Fact]
        public void Robot_Multiple_Right_Turns_Cycle_Through_Directions()
        {
            var robot = new Robot(0, 0, new NorthDirection());
            robot.TurnRight(); // N -> E
            Assert.Equal('E', robot.Direction.Name);
            robot.TurnRight(); // E -> S
            Assert.Equal('S', robot.Direction.Name);
            robot.TurnRight(); // S -> W
            Assert.Equal('W', robot.Direction.Name);
            robot.TurnRight(); // W -> N
            Assert.Equal('N', robot.Direction.Name);
        }

        [Fact]
        public void Robot_Can_Turn_Full_Circle_Left_And_Right()
        {
            var robot = new Robot(0, 0, new NorthDirection());
            for (int i = 0; i < 4; i++) robot.TurnLeft();
            Assert.Equal('N', robot.Direction.Name);

            for (int i = 0; i < 4; i++) robot.TurnRight();
            Assert.Equal('N', robot.Direction.Name);
        }
    }
}
