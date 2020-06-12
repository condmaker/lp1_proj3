using System;
using System.ComponentModel;
using System.Text;

namespace Roguelike
{
    class Program
    {
        static void Main(string[] args)
        {
            // Makes it so that the program supports unicode characters.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Gets the arguments from the command line and creates a new 
            // GameValue object with the same arguments.
            GameValues gameValues = GameValues.ConvertArgs(args);
            Game game = new Game(gameValues);

            // Starts the game.
            game.Initiate();

        }
    }
}
