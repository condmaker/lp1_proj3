using System;

namespace Roguelike
{
    public class UI
    {
        // The single string that manages all inputs on the game.
        public string Input { get; private set; } = "";
        // A splitted array of strings used to read commands.
        public string[] SplitInput { get; private set;} 

        private void MainMenu()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("|        Roguelike        |");
            Console.WriteLine("---------------------------");
            Console.WriteLine("| n - New Game            |");
            Console.WriteLine("| h - High Scores         |");
            Console.WriteLine("| i - Instructions        |");
            Console.WriteLine("| c - Credits             |");
            Console.WriteLine("| q - Quit                |");
            Console.WriteLine("---------------------------");
            Console.WriteLine("---------------------------");
        }
    }
}