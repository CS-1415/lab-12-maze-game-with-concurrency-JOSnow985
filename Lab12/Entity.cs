namespace Lab12;

public class Entity
{
    public int X { get; set; }
    public int Y { get; set; }
    public Map currentMap { get; private set; }
    public Entity(ref Map map, int startingX, int startingY)
    {
        currentMap = map;
        X = startingX;
        Y = startingY;
    }
}

public class Player : Entity
{
    public Player(ref Map map) : base(ref map, 0, 0) {}

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
            {
                currentMap.ChangeCell(X, Y, ' ');
                (X, Y) = (targetX, targetY);    // If the location is valid, update player's coordinates
                currentMap.ChangeCell(X, Y, 'P');
            }
    }
}
