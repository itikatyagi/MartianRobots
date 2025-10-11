namespace MartianRobots.Models.Directions
{
    public class NorthDirection : IDirection
    {
        public char Name => 'N';
        public IDirection TurnLeft() => new WestDirection();
        public IDirection TurnRight() => new EastDirection();
        public (int dx, int dy) MoveVector() => (0, 1);
    }
}