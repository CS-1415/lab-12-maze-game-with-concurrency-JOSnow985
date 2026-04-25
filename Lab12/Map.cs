namespace Lab12;

public class Map
{
    public int Height => Layout.Count;        // Map's height boundary is the count of the list of list of cells
    public int Width => Layout[0].Count;      // Map's width boundary is the count of cells in the first list of cells
    private readonly object _layoutlock = new();
    private List<List<char>> layout = [];
    public List<List<char>> Layout
    {
        get         { lock (_layoutlock) { return layout;  } }
        private set { lock (_layoutlock) { layout = value; } }
    }
    public List<Entity> MapEntities { get; private set; }
    public Map(string mapfile)
    {
        Layout = LoadMapFile(mapfile);
        MapEntities = LoadEntityList();     // I don't know if it's safe to call a method like this from inside the constructor
    }
    private static List<List<char>> LoadMapFile(string filepath)
    {
        string[] mapArray = File.ReadAllLines(filepath);
        List<List<char>> map = [];

        // Pull all of the characters out of the map array into a list of lists of chars
        foreach (string line in mapArray)
        {
            List<char> charList = [];
            foreach (char c in line)
            {
                charList.Add(c);
            }
            map.Add(charList);
        }

        return map;
    }
    private List<Entity> LoadEntityList()
    {
        List<Entity> entities = [];
        for(int y = 0; y < Layout.Count; y++)
        {
            for(int x = 0; x < Layout[y].Count; x++)
            {
                if (Layout[y][x] == '%')
                {
                    entities.Add(new Guard(this, x, y));
                }
            }
        }

        return entities;
    }
    public void MoveSymbol(int oldCol, int oldRow, int newCol, int newRow, char c)
    {
        lock (_layoutlock)
        {
            Layout[oldRow][oldCol] = ' ';
            Layout[newRow][newCol] = c;
        }
    }
    public void DisableGateSymbols()
    {
        List<List<char>> degatedMap = [];

        lock (_layoutlock)      // Need to wrap this entire operation in a lock
        {
            foreach (List<char> row in Layout)
            {
                List<char> degatedRow = [];
                foreach (char symbol in row)
                {
                    if (symbol == '|')
                    {
                        degatedRow.Add(' ');
                    }
                    else
                        degatedRow.Add(symbol);
                }
                degatedMap.Add(degatedRow);
            }
            Layout = degatedMap;
        }
    }
    public void PrintMap()
    {
        lock (_layoutlock)
        {
            // Iterate over rows and characters and print them
            foreach (List<char> row in Layout)
            {
                foreach (char cell in row)
                {
                    Console.Write(cell);
                }
                Console.WriteLine();
            }
        }
    }
}
