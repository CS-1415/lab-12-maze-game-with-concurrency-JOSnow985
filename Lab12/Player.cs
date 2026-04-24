namespace Lab12;

public class Player
{
    public int X { get; private set; } = 0;
    public int Y { get; private set; } = 0;
    private List<List<char>> currentMap;
    public Player(ref List<List<char>> mapList)
    {
        currentMap = mapList;
    }

    public void Move(Movement.Direction targetDirection)
    {
        // Use direction to figure out target coordinates
        (int targetX, int targetY) = targetDirection switch
        {
            Movement.Direction.Up    => (X,     Y - 1),
            Movement.Direction.Right => (X + 1, Y),
            Movement.Direction.Down  => (X,     Y + 1),
            Movement.Direction.Left  => (X - 1, Y),
            _ => (X, Y)
        };

        // Call TryMove to see if that's a valid location
        if (Movement.TryMove(targetX, targetY, currentMap))
            (X, Y) = (targetX, targetY);    // If the location is valid, update player's coordinates
    }
}
