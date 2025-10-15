namespace MartianRobots.Models.Directions
{
    public class WestDirection : IDirection
    {
        public char Name => 'W';
        public IDirection TurnLeft() => new SouthDirection();
        public IDirection TurnRight() => new NorthDirection();
        public (int dx, int dy) MoveVector() => (-1, 0);
    }
}
