namespace MartianRobots.Models.Directions
{
    public class SouthDirection : IDirection
    {
        public char Name => 'S';
        public IDirection TurnLeft() => new EastDirection();
        public IDirection TurnRight() => new WestDirection();
        public (int dx, int dy) MoveVector() => (0, -1);
    }
}
