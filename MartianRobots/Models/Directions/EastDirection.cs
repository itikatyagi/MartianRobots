namespace MartianRobots.Models.Directions
{
    public class EastDirection : IDirection
    {
        public char Name => 'E';
        public IDirection TurnLeft() => new NorthDirection();
        public IDirection TurnRight() => new SouthDirection();
        public (int dx, int dy) MoveVector() => (1, 0);
    }
}
