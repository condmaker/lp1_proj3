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
        /// Static constructor of the class. Will make the output encode
        /// support Unicode characters.
        /// </summary>
        static UI()
        {
            // Allows the console to print other Unicode characters 
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        /// <summary>
        /// A simple method that shows to the player a graphic depiction of 
        /// the Main Menu's commands.
        /// </summary>
        private static void MainMenu()
        {
            Console.WriteLine("Ψѧ--------------+Ѡ+--------------ѧΨ");
            Console.WriteLine("|           -Roguelike-           |");
            Console.WriteLine("Ψѧ--------------+Ѡ+--------------ѧΨ");
            Console.WriteLine("| n - New Game                    |");
            Console.WriteLine("| h - High Scores                 |");
            Console.WriteLine("| i - Instructions                |");
            Console.WriteLine("| c - Credits                     |");
            Console.WriteLine("| q - Quit                        |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ShowTutorial()
        {
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
            Console.WriteLine("|           Introduction          |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
            Console.WriteLine("| Welcome. In this introduction,  |");
            Console.WriteLine("| you will understand how to play |");
            Console.WriteLine("| this Roguelike game. Let's      |");
            Console.WriteLine("| begin by the very basics: You   |");
            Console.WriteLine("| control a little adventurer,    |");
            Console.WriteLine("| that is exploring an unknown    |");
            Console.WriteLine("| dungeon.                        |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| You look like this:         .☻. |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| When at the dungeon, you will   |");
            Console.WriteLine("| be faced with obstacles,:   ||| |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| small but scary enemies,:   .¤. |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| big, dangerous bosses,:     ◄☼► |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| various types of health         |");
            Console.WriteLine("| power-ups, varying from small   |");
            Console.WriteLine("| to large:           s♥s m♥m l♥l |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| ...And the door that will lead  |");
            Console.WriteLine("| you further beyond this         |");
            Console.WriteLine("| mysterious dungeon:         _∩_ |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| Now that you understand the     |");
            Console.WriteLine("| basics and knows every entity   |");
            Console.WriteLine("| in the game, let us continue    |");
            Console.WriteLine("| to explain the dungeon itself.  |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");

            if (ContinueTutorial() == false) return;

            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static bool ContinueTutorial()
        {
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
            Console.WriteLine("|       Press c to continue,      |");
            Console.WriteLine("| or, anything else to go back... |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");

            WriteOnString();

            if (Input == "c") return true;
            return false;
        }

        /// <summary>
        /// Function that renders the game board in the console
        /// </summary>
        /// <param name="board">"Current game board"</param>
        public static void ShowBoard(Board board, bool empty = false)
        {
            
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
                        Console.Write(" ___ ");
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

        /// <summary>
        /// Changes the instance Input.
        /// </summary>
        public static void WriteOnString()
        {
            Console.Write(">");
            Input = Console.ReadLine();
        }

    }
}