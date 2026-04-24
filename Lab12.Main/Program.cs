// Jaden Olvera, 4/24/26, Lab 12 Maze Game with Concurrency

// Intro & Story
using Lab12;

Console.WriteLine("Welcome to a slightly-less-basic, super-cool maze game with concurrency, the guards will move independently of you!");
Console.WriteLine("--- Rules Placeholder ---");
Console.WriteLine("Insert Coin to Play...");
Console.ReadKey();

// Load Map
Map map = new("map.txt");

// Create player object, pass map as a reference
Player player = new(map);

// Loop until the key pressed is Escape, print proposed directions
ConsoleKey lastKey = ConsoleKey.None;
do {
    // Clear screen and print the map cell by cell
    Console.Clear();
    map.PrintMap();

    // Print player score as a line under map
    Console.WriteLine(player.Score);

    // Player input switch
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

    // Check if player's score is high enough to drop the gate
    if (player.Score >= 1000)
    {
        map.DisableGateSymbols();
    }

} while (lastKey != ConsoleKey.Escape);