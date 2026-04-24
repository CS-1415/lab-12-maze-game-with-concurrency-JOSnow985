namespace Lab12;

public class Map
{
    public int Height => Layout.Count;        // Map's height boundary is the count of the list of list of cells
    public int Width => Layout[0].Count;      // Map's width boundary is the count of cells in the first list of cells
    public List<List<char>> Layout { get; private set; }
    public Map(string mapfile)
    {
        Layout = LoadMapFile(mapfile);
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
    public void ChangeCell(int col, int row, char c) => Layout[row][col] = c;
    public void PrintMap()
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
