using System;
using System.Text;

namespace Roguelike
{
    public static class UI
    {
        // The single string that manages all inputs on the game.
        public static string Input { get; private set; } = "";
        
        // A splitted array of strings used to read commands.
        public static string[] SplitInput { get; private set;} 

        /// <summary>
        /// A simple method that shows to the player a graphic depiction of 
        /// the Main Menu's commands.
        /// </summary>
        private static void MainMenu()
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
        }

        /// <summary>
        /// Function that renders the highscore table in the console
        /// </summary>
        /// <param name="highscore">"Highscore table"</param>

        public static void ShowHighscoreTable(HighscoreTable highscore)
        {

            //Checks every possible spot of the table.
            for(int i  = 0; i < 10; i++)
            {

                //Gets the score correspondent to the spot.
                Score score = highscore.GetScore(i);

                //If theres a score in the expecifict spot, print it.
                if(score != null)
                     Console.WriteLine ($"Name: {score.Name} " +
                            $"-- Score: {score.NewScore}");
            }

        }

        /// <summary>
        /// Function that renders the game board in the console
        /// </summary>
        /// <param name="board">"Current game board"</param>
        public static void ShowBoard(Board board, bool empty = false)
        {
            // Allows the console to print other Unicode characters 
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (int i = 0; i < board.Width; i++)
                Console.Write("-----");
            Console.WriteLine("--");

            //Cycle through every line 
            for(int y = 0; y < board.Height; y++)
            {
                Console.Write("|");
                //Cycle through every column in the current line 
                for(int x = 0; x < board.Width; x++)
                {
                    //Get current coordinate
                    Coord coord = new Coord(x,y);

                        
                    //Checks if there is some entity occupying the position
                    //and if the board was setuped to be printed in an empty
                    //state
                    if(board.IsOccupied(coord) && !empty)
                    {
                        //Prints its image
                        Console.Write
                        (board.GetEntityAt(coord).ToString());
                    }
                    else
                    {
                        //Prints empty tile image
                        Console.Write(" ... ");
                    }
                }

                Console.Write("|");

                //Space between lines 
                Console.WriteLine("\n");
            }

            for (int i = 0; i < board.Width; i++)
                Console.Write("-----");
            Console.WriteLine("--");
        }      

    }
}