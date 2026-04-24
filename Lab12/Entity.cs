namespace Lab12;

public class Entity
{
    public char Symbol { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Map CurrentMap { get; private set; }
    public Entity(ref Map map, int startingX, int startingY, char c)
    {
        CurrentMap = map;
        X = startingX;
        Y = startingY;
        Symbol = c;
    }
    public void MoveToken(int targetX, int targetY)     // Update entity's coordinates and the map's characters
    {
        CurrentMap.ChangeCell(X, Y, ' ');       // Clear entity's old cell
        (X, Y) = (targetX, targetY);            // Update entity's current coordinates
        CurrentMap.ChangeCell(X, Y, Symbol);    // Write entity character to new coordinates
    }
}

public class Player : Entity
{
    public int Score { get; set; }
    public Player(ref Map map) : base(ref map, 0, 0, 'P') {}

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

        // Call TryMove, if true, call MoveToken to update player's coordinates
        if (Movement.TryMove(targetX, targetY, this))
        {
            // Probably a better place to put this
            // Grab target cell's symbol and check if it's a score symbol
            char targetSymbol = CurrentMap.Layout[targetY][targetX];
            switch (targetSymbol)
            {
                case '$':
                    Score += 200;
                    break;
                case '^':
                    Score += 100;
                    break;
                default:
                    break;
            }

            // Update player coordinates
            MoveToken(targetX, targetY);
        }
    }
}

public class Guard : Entity
{
    public Guard(Map map, int startingX, int startingY) : base(map, startingX, startingY, '%') {}
}
