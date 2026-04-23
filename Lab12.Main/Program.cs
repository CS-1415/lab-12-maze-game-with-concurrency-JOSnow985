// Jaden Olvera, 4/24/26, Lab 12 Maze Game with Concurrency

// Intro & Story
Console.WriteLine("Welcome to a slightly-less-basic, super-cool maze game with concurrency, the guards will move independently of you!");
Console.WriteLine("--- Rules Placeholder ---");
Console.WriteLine("Insert Coin to Play...");
Console.ReadKey();

// Load Map, clear screen & test draw
Console.Clear();
string[] mapStrings = File.ReadAllLines("map.txt");

foreach(string line in mapStrings)
{
    Console.WriteLine(line);
}
Console.ReadKey();

// Loop until the key pressed is Escape, print proposed directions
ConsoleKey lastKey = ConsoleKey.None;
Console.Clear();
do
{
    lastKey = Console.ReadKey(true).Key;
    switch (lastKey)
    {
        case ConsoleKey.W:
        case ConsoleKey.UpArrow:
            Console.WriteLine("Up");
            break;
        case ConsoleKey.D:
        case ConsoleKey.RightArrow:
            Console.WriteLine("Right");
            break;
        case ConsoleKey.S:
        case ConsoleKey.DownArrow:
            Console.WriteLine("Down");
            break;
        case ConsoleKey.A:
        case ConsoleKey.LeftArrow:
            Console.WriteLine("Left");
            break;
        case ConsoleKey.Escape:
            break;
        default:
            break;
    }
} while (lastKey != ConsoleKey.Escape);