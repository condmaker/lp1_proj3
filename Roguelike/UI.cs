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

        public void ShowBoard(Board board)
        {
            //Cycle through every line 
            for(int y = 0; y < board.Height; y++)
            {
                //Cycle through every column in the current line 
                for(int x = 0; x < board.Width; x++)
                {
                    //Get current coordinate
                    Coord coord = new Coord(x,y);

                    if(board.IsOccupied(coord))
                    {
                        Console.Write
                        (GetEntityString(board.GetEntityAt(coord).Kind));
                    }
                    else
                    {
                        Console.Write("  .  ");
                    }
                }

                Console.WriteLine("\n");
            }
        }
        


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
                    entityStr = "P";
                    break;
                case EntityKind.Enemy:
                    entityStr = "E";
                    break;
                case EntityKind.Obstacle:
                    entityStr = "O";
                    break;
                case EntityKind.PowerUpL:
                    entityStr = "X";
                    break;
                case EntityKind.PowerUpM:
                    entityStr = "*";
                    break;
                case EntityKind.PowerUpS:
                    entityStr = "+";
                    break;
                default:
                    entityStr = ".";
                    break;
            }

            return entityStr; 
        }

    }
}