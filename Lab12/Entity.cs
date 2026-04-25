namespace Lab12;

public class Entity
{
    public char Symbol { get; private set; }
    public Status CurrentStatus = Status.Alive;     // All entities begin the game "Alive"
    public int X { get; set; }
    public int Y { get; set; }
    public Map CurrentMap { get; private set; }
    public Entity(Map map, int startingX, int startingY, char c)
    {
        CurrentMap = map;
        X = startingX;
        Y = startingY;
        Symbol = c;
    }
    public void MoveToken(int targetX, int targetY) // Update entity's coordinates and the map's characters
    {
        CurrentMap.ChangeCell(X, Y, ' ');           // Clear entity's old cell
        (X, Y) = (targetX, targetY);                // Update entity's current coordinates
        CurrentMap.ChangeCell(X, Y, Symbol);        // Write entity character to new coordinates
    }

    public enum Status { Alive, Dead, Escaped }     // Alive and Dead should be for any entity, Escaped should only be set for the player on the win condition
}

public class Player : Entity
{
    public int Score { get; set; }
    public Player(Map map) : base(map, 0, 0, 'P') {}

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

        // Call TryMove, if true, evaluate possible result from target cell's symbol, then call MoveToken to update player's coordinates
        if (Movement.TryMove(targetX, targetY, this))
        {
            // Grab target cell's symbol and do something if we need to
            char targetSymbol = CurrentMap.Layout[targetY][targetX];
            switch (targetSymbol)
            {
                case '$':                               // Gem symbol, increment score by 200
                    Score += 200;
                    break;
                case '^':                               // Coin symbol, increment score by 100
                    Score += 100;
                    break;
                case '#':
                    CurrentStatus = Status.Escaped;     // Exit symbol, set status to escaped
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
