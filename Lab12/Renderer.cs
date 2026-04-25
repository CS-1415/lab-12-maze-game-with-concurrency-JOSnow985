namespace Lab12;

public class Renderer
{
    public bool IsRendering = true;
    private Map renderMap;
    private Player renderPlayer;
    public Renderer(Map targetMap, Player targetPlayer)
    {
        renderMap = targetMap;
        renderPlayer = targetPlayer;
    }
    public void RenderMap()
    {
        while (IsRendering)
        {
            // Clear last print and call PrintMap again
            Console.Clear();
            renderMap.PrintMap();

            // Print player score under map
            Console.WriteLine(renderPlayer.Score);

            // Use Sleep to draw a frame every xth of a second
            Thread.Sleep(1000 / 240);
        }
    }
}
