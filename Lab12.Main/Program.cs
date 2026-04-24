// Jaden Olvera, 4/24/26, Lab 12 Maze Game with Concurrency

// Intro & Story
using Lab12;

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

// placeholder "player" coordinates
(int X, int Y) player = (0,0);
do
{
    lastKey = Console.ReadKey(true).Key;
    switch (lastKey)
    {
        case ConsoleKey.W:
        case ConsoleKey.UpArrow:
            if (Helpers.TryMove(player.X, player.Y - 1))
            {
                player.Y -= 1;
                Console.WriteLine($"Up - now at ({player.X},{player.Y})");
            }
            else
                Console.WriteLine($"Up - still at ({player.X},{player.Y})");
            break;
        case ConsoleKey.D:
        case ConsoleKey.RightArrow:
            if (Helpers.TryMove(player.X + 1, player.Y))
            {
                player.X += 1;
                Console.WriteLine($"Right - now at ({player.X},{player.Y})");
            }
            else
                Console.WriteLine($"Right - still at ({player.X},{player.Y})");
            break;
        case ConsoleKey.S:
        case ConsoleKey.DownArrow:
            Console.WriteLine("Down");
            if (Helpers.TryMove(player.X, player.Y + 1))
            {
                player.Y += 1;
                Console.WriteLine($"Down - now at ({player.X},{player.Y})");
            }
            else
                Console.WriteLine($"Down - still at ({player.X},{player.Y})");
            break;
        case ConsoleKey.A:
        case ConsoleKey.LeftArrow:
            Console.WriteLine("Left");
            if (Helpers.TryMove(player.X - 1, player.Y))
            {
                player.X -= 1;
                Console.WriteLine($"Left - now at ({player.X},{player.Y})");
            }
            else
                Console.WriteLine($"Left - still at ({player.X},{player.Y})");
            break;
        case ConsoleKey.Escape:
            break;
        default:
            break;
    }
} while (lastKey != ConsoleKey.Escape);