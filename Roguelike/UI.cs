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
        

        /*
        /// <summary>
        ///  
        /// </summary>
        /// <param name="type">Entity to print</param>
        /// <returns>String that represent the entity</returns>
        private string GetEntityString(EntityKind entity)
        {
            string entityStr = "";

            //Converts entity type to corresponding string
            switch(entity)
            {
                case EntityKind.Player:
                    entityStr = " PPP ";
                    break;
                case EntityKind.Minion:
                    entityStr = " mmm ";
                    break;
                case EntityKind.Boss:
                    entityStr = " BBB ";
                    break;
                case EntityKind.Obstacle:
                    entityStr = " OOO ";
                    break;
                case EntityKind.PowerUpL:
                    entityStr = " -X- ";
                    break;
                case EntityKind.PowerUpM:
                    entityStr = " -*- ";
                    break;
                case EntityKind.PowerUpS:
                    entityStr = " -+- ";
                    break;
            }

            return entityStr; 
        }
        */

    }
}