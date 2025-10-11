namespace MartianRobots.Models.Directions
{
    /// <summary>Directional behavior for robots.</summary>
    public interface IDirection
    {
        char Name { get; }
        IDirection TurnLeft();
        IDirection TurnRight();
        (int dx, int dy) MoveVector();
    }
}