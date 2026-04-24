// Jaden Olvera, 4/24/26, Lab 12 Maze Game with Concurrency

// Intro & Story
using Lab12;

Console.WriteLine("Welcome to a slightly-less-basic, super-cool maze game with concurrency, the guards will move independently of you!");
Console.WriteLine("--- Rules Placeholder ---");
Console.WriteLine("Insert Coin to Play...");
Console.ReadKey();

// Load Map, clear screen & test draw
Console.Clear();
string[] mapArray = File.ReadAllLines("map.txt");
List<List<char>> map = [];

// Pull all of the characters out of the map array into a list of lists of chars
foreach(string line in mapArray)
{
    List<char> charList = [];
    foreach(char c in line)
    {
        charList.Add(c);
    }
    map.Add(charList);
}

// Now print the map cell by cell
foreach(List<char> row in map)
{
    foreach(char cell in row)
    {
        Console.Write(cell);
    }
    Console.WriteLine();
}
Console.ReadKey();

// Create player object, pass map as a reference
Player player = new(ref map);

// Loop until the key pressed is Escape, print proposed directions
ConsoleKey lastKey = ConsoleKey.None;
Console.Clear();
do
{
    lastKey = Console.ReadKey(true).Key;
    switch (lastKey)
    {
        case ConsoleKey.W or ConsoleKey.UpArrow:
            player.Move(Movement.Direction.Up);
            break;
        case ConsoleKey.D or ConsoleKey.RightArrow:
            player.Move(Movement.Direction.Right);
            break;
        case ConsoleKey.S or ConsoleKey.DownArrow:
            player.Move(Movement.Direction.Down);
            break;
        case ConsoleKey.A or ConsoleKey.LeftArrow:
            player.Move(Movement.Direction.Left);
            break;
        case ConsoleKey.Escape:
            break;
        default:
            break;
    }
} while (lastKey != ConsoleKey.Escape);