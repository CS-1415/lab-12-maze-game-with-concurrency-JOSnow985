namespace Lab12;

public class Entity
{
    public char Symbol { get; private set; }
    public Status CurrentStatus = Status.Alive;     // All entities begin the game "Alive"
    private readonly object _xylock = new();
    private int _x;
    private int _y;
    public int X
    {
        get         { lock (_xylock) { return _x;  } }
        private set { lock (_xylock) { _x = value; } }
    }
    public int Y
    {
        get         { lock (_xylock) { return _y;  } }
        private set { lock (_xylock) { _y = value; } }
    }
    public Map CurrentMap { get; private set; }
    public Entity(Map map, int startingX, int startingY, char c)
    {
        CurrentMap = map;
        X = startingX;
        Y = startingY;
        Symbol = c;
    }
    public (int, int) CheckCoordinates()
    {
        lock (_xylock)
        {
            return (X, Y);
        }
    }
    public void MoveToken(int targetX, int targetY) // Update entity's coordinates and the map's characters
    {
        lock (_xylock)
        {
            CurrentMap.MoveSymbol(X, Y, targetX, targetY, Symbol);
            (X, Y) = (targetX, targetY);                // Update entity's current coordinates
        }
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
        (int targetX, int targetY) = Movement.DirectionToCoordinates(X, Y, targetDirection);

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

    public void CheckForGuard()
    {
        while (CurrentStatus == Status.Alive)       // Watches for any change in status, both Dead and Escaped will end this
        {
            // Acquire player coordinates to check against Guards, use this method for proper locking
            (int, int) playerCoordinates = CheckCoordinates();

            foreach (Entity entity in CurrentMap.MapEntities)
            {
                if (entity is Guard guard)
                {
                    // Acquire guard coordinates, method for locking again
                    (int, int) guardCoordinates = guard.CheckCoordinates();

                    if (playerCoordinates == guardCoordinates)
                    {
                        CurrentStatus = Status.Dead;      // ow
                    }
                }
            }

            Thread.Sleep(100);  // Only try to run this check every x ms
        }
    }
}

public class Guard : Entity
{
    private static Random GuardRNG = new();
    public Guard(Map map, int startingX, int startingY) : base(map, startingX, startingY, '%') {}
    public void GuardStuff()
    {
        while (CurrentStatus == Status.Alive)
        {
            // [0,4) range to match valid Direction enumerations
            Movement.Direction targetDirection = (Movement.Direction)GuardRNG.Next(0,4);

            // Convert generated direction to target coordinates
            // Might not need a lock, because the method that would change X and Y is the method this call is coming from?
            (int targetX, int targetY) = Movement.DirectionToCoordinates(X, Y, targetDirection);

            // Only move if we get chose a valid direction
            if (Movement.TryMove(targetX, targetY, this))
            {
                MoveToken(targetX, targetY);
            }

            // Wait to try moving again
            Thread.Sleep(200);
        }
    }
}
