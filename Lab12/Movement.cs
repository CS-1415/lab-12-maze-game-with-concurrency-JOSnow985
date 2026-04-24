namespace Lab12;

public static class Movement
{
    public static bool TryMove(int targetX, int targetY, List<List<char>> mapList)
    {
        // If the check fails, we set passing to false
        if (!CheckScreenDimensions(targetX, targetY))
            return false;

        if (!CheckMapDimensions(targetX, targetY, mapList))
            return false;

        if (!CheckForWall(targetX, targetY, mapList))
            return false;
        
        // If we got here after all the other checks, return true
        return true;
    }
    private static bool CheckScreenDimensions(int targetX, int targetY)
    {
        // Check screen size
        if (targetX > Console.BufferWidth || targetX < 0)
            return false;
        if (targetY > Console.BufferHeight || targetY < 0)
            return false;
        // If the target coordinates don't fail the two previous conditions, return true
        return true;
    }
    private static bool CheckMapDimensions(int targetX, int targetY, List<List<char>> mapList)
    {
        // Check first row's count, don't have to check against zeroes here because we did it above
        if (targetX > mapList[1].Count)
            return false;
        // Check map list's couunt overall, remember it's zero-indexed so it's not ">="
        if (targetY > mapList.Count)
            return false;
        
        return true;
    }
    private static bool CheckForWall(int targetX, int targetY, List<List<char>> mapList)
    {
        // Check if the target cell is a wall character
        if ("*|".Contains(mapList[targetY][targetX]))
            return false;

        return true;
    }

    public enum Direction { Up, Right, Down, Left}
}
