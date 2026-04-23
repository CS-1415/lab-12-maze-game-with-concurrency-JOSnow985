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