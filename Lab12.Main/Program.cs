// Jaden Olvera, 4/24/26, Lab 12 Maze Game with Concurrency

// Intro & Story
using Lab12;

Console.Clear();
Console.WriteLine("Welcome to a slightly-less-basic, super-cool maze game with concurrency, the guards will move independently of you!");
Console.WriteLine("--- Symbols ---\n* - Wall, can't walk through these!\n| - Gate, they'll disappear if you get 1000 score!\n^ - Coin, worth 100 score!\n$ - Gem, worth 200 score!\n# - Exit, get here to win the game!");
Console.WriteLine("\n\n--- Insert Coin to Play ---");
Console.ReadKey();

// Load Map and create Player on map
Map map = new("map.txt");
Player player = new(map);

// Create renderer object, pass map and player, move to new thread and start rendering
Renderer renderer = new(map, player);
Thread renderThread = new(renderer.RenderMap);
renderThread.Start();

// Set up Guard objects and threads into a list we can iterate over to work on all the guards at the same time
List<Thread> Guards = [];
foreach (Entity entity in map.MapEntities)
{
    if (entity is Guard guard)
    {
        Guards.Add(new(guard.GuardStuff));
    }
}
foreach (Thread guardThread in Guards)
{
    guardThread.Start();
}

// Set up player's collision detector so we know if they've died
Thread playerCollisionThread = new(player.CheckForGuard);
playerCollisionThread.Start();

// Loop until the key pressed is Escape or Player status is either Dead or Escaped
ConsoleKey lastKey;
do {
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

    // Drop the gate if the player's score is high enough
    if (player.Score >= 1000)
    {
        map.DisableGateSymbols();
    }

} while (lastKey != ConsoleKey.Escape && player.CurrentStatus == Entity.Status.Alive);

renderer.IsRendering = false;   // Kill map render loop
renderThread.Join();            // Wait for final frame to draw

// End screen handler here so we can use the player's status enum
Console.Clear();
if (player.CurrentStatus is Entity.Status.Dead)
{
    Console.WriteLine("You died, but I'm sure you'll do better if you try again!");
}
else if (player.CurrentStatus is Entity.Status.Escaped)
{
    Console.WriteLine("You win! Placeholder");
}
else
    Console.WriteLine("Bye!");


// Kill player to make sure we're not still checking coordinates
player.CurrentStatus = Entity.Status.Dead;
playerCollisionThread.Join();

// Kill Guards to make sure they're not moving around still
foreach (Entity entity in map.MapEntities)
{
    if (entity is Guard guard)
    {
        guard.CurrentStatus = Entity.Status.Dead;
    }
}
foreach (Thread guardThread in Guards)
{
    guardThread.Join();
}