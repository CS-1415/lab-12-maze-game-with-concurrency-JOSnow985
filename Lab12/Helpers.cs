namespace Lab12;

public static class Helpers
{
    public static bool TryMove(int targetX, int targetY)
    {
        if (targetX > Console.BufferWidth || targetX < 0)
            return false;
        if (targetY > Console.BufferHeight || targetY < 0)
            return false;
        // If the target coordinates don't fail the two previous conditions, return true
        return true;
    }
}
