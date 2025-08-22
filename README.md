# Lab-12-Maze-Game-with-Concurrency
Objectives
Design a slightly-less-basic, super-cool maze game using object-oriented design.
Practice with arrays, text files, and Console I/O.
Introduce concurrency: have guards move independently of player input using either threads/timers or async/await + tasks.

Files & Setup
Accept the GitHub Classroom assignment and clone the repo
Create a Console project in the repo.
Add a map.txt file and paste the provided maze 
Ensure your terminal buffer is tall/wide enough to render the map.

Program Requirements

Implement the game with these features (incremental commit checkpoints encouraged):

Intro & Story

Add a file header comment (author, date, lab name).

Display a short description of the game when it starts.

Load Map

string[] mapRows = File.ReadAllLines("map.txt");

Clear screen; print the map.

Basic Player Controls (do-while loop)

Read keys with Console.ReadKey(true).Key.

Esc exits the game loop.

Arrow keys adjust a proposed position (do not set the cursor directly; see TryMove below).

Stay on the Map

Prevent moving off-screen or outside the row width.

Use a helper like TryMove(int proposedTop, int proposedLeft) that validates bounds before committing.

Enforce Walls

Do not move into '*'.

Coins

'^' are coins. Moving onto one removes it and +100 points. Show score.

Opening the Gate

After collecting all 10 coins (or reaching 1000 points), open a doorway in the central chamber so the player can reach $.

$ are gems worth +200 each.

Win/Lose

Win: stepping on '#' ends the game: clear and print a congrats with final score and elapsed time.

Lose: if a guard collides with the player: clear and print “You Lose” with stats.

Guards Move Independently (Concurrency Requirement)

% are guards. They must move on their own interval (e.g., every 150–300 ms), independent of player key presses.

You must implement one of:

Thread/Timer approach (e.g., System.Threading.Timer or a dedicated Thread per guard), or

Async/await + Task loops with a CancellationToken.

Guards can move randomly, patrol, bounce, etc. Collisions with walls must be handled (no walking through *).

Use thread-safe sharing of game state (e.g., a lock around read/write of positions or a message queue). No flickering/tearing.

Rendering

Ensure multi-entity updates don’t corrupt Console output. (See Discussion page for approaches.)

Show status line(s): score, coins remaining, time.

OOP Requirements

Design and implement classes “as necessary and appropriate.” You choose names and structure. Constraints:

Minimum: more than one class; avoid a single Program god-class.

Suggested (not required) roles you might separate:

Maze / Map loading & tile queries

Entity base (position, symbol) with Player and Guard subclasses

Movement/Rules engine (collision, scoring, win/lose)

Renderer (Console drawing & double-buffering)

Game loop / Coordinator

Use encapsulation (private fields, properties, small public surface).

Keep cross-class coupling reasonable. Avoid global mutable statics for core state.

Favor small, single-purpose methods.
