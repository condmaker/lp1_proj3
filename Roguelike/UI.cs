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
        public static void ShowTutorial()
        {
            // Creates an example board with 6x6 dimensions to show to the 
            // player.
            Board exampleBoard = new Board(6, 6);

            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
            Console.WriteLine("|           Introduction          |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
            Console.WriteLine("| Welcome. In this introduction,  |");
            Console.WriteLine("| you will understand how to play |");
            Console.WriteLine("| this Roguelike game. Let's      |");
            Console.WriteLine("| begin by the very basics: You   |");
            Console.WriteLine("| control a little adventurer,    |");
            Console.WriteLine("| that is exploring an unknown    |");
            Console.WriteLine("| dungeon. You have a set         |");
            Console.WriteLine("| quantity of HP, determined by   |");
            Console.WriteLine("| the dungeon's size.             |");
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
            Console.WriteLine("| to large:            ♥  ɾ♥ɿ ѧ♥ѧ |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| ...And the door that will lead  |");
            Console.WriteLine("| you further beyond this         |");
            Console.WriteLine("| mysterious dungeon:         _∩_ |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| Now that you understand the     |");
            Console.WriteLine("| basics and know every entity    |");
            Console.WriteLine("| in the game, let us continue    |");
            Console.WriteLine("| to explain the dungeon itself.  |");

            if (ContinueTutorial() == false) return;

            Console.WriteLine("\nΨѧ-------------------------------ѧΨ");
            Console.WriteLine("| The Dungeon (Map or Game Board) |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
            Console.WriteLine("| The Dungeon is divided in       |");
            Console.WriteLine("| floors, and when you, the       |");
            Console.WriteLine("| player (.☻.) leaves the current |");
            Console.WriteLine("| floor through an door (_∩_)     |");
            Console.WriteLine("| you will descend to the next    |");
            Console.WriteLine("| floor. But beware- it only gets |");
            Console.WriteLine("| more and more deadly as you go  |");
            Console.WriteLine("| down...                         |");
            Console.WriteLine("| Here is an small, 6x6 example   |");
            Console.WriteLine("| of an empty Dungeon floor:      |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ\n");
            
            ShowBoard(exampleBoard, true);

            Console.WriteLine("\nΨѧ-------------------------------ѧΨ");
            Console.WriteLine("| As you can see, the '———' are   |");
            Console.WriteLine("| empty tiles, where the player,  |");
            Console.WriteLine("| and enemies, can and will pass  |");
            Console.WriteLine("| pass through. There is a player |");
            Console.WriteLine("| phase and an enemy phase. In    |");
            Console.WriteLine("| the player phase, only the      |");
            Console.WriteLine("| player can move, and he can     |");
            Console.WriteLine("| move two times (two tiles in    |");
            Console.WriteLine("| the Game Board) in one turn.    |");
            Console.WriteLine("| You, the player, can't pass     |");
            Console.WriteLine("| through obstacles (|||), the    |");
            Console.WriteLine("| floor's bounds, and enemies,    |");
            Console.WriteLine("| which when adjacent to one of   |");
            Console.WriteLine("| the player's neighbour tiles,   |");
            Console.WriteLine("| will damage you.                |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| You also need to be fast, since |");
            Console.WriteLine("| every time you move from one    |");
            Console.WriteLine("| tile, you will lose 1HP,        |");
            Console.WriteLine("| meaning you can lose 2HP per    |");
            Console.WriteLine("| turn. And the only way to       |");
            Console.WriteLine("| replenish it is by grabbing     |");
            Console.WriteLine("| the health Power-Ups, which are |");
            Console.WriteLine("| automatically used if you are   |");
            Console.WriteLine("| on a tile which contains one.   |");
            Console.WriteLine("| Here is an example of a full    |");
            Console.WriteLine("| Dungeon Floor:                  |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ\n");

            exampleBoard.PlaceEntity(
            new Player(new Coord(0, 2), 20), new Coord(0, 2));

            exampleBoard.PlaceEntity(
            new Enemy(new Coord(2, 4), EntityKind.Minion, 20), new Coord(2, 4));

            exampleBoard.PlaceEntity(
            new Enemy(new Coord(1, 1), EntityKind.Minion, 20), new Coord(1, 1));

            exampleBoard.PlaceEntity(
            new Enemy(new Coord(0, 0), EntityKind.Boss, 20), new Coord(0, 0));

            exampleBoard.PlaceEntity(
            new Entity(new Coord(4, 2), EntityKind.Obstacle), new Coord(4, 2));

            exampleBoard.PlaceEntity(
            new Entity(new Coord(0, 5), EntityKind.PowerUpS), new Coord(0, 5));

            exampleBoard.PlaceEntity(
            new Entity(new Coord(3, 3), EntityKind.PowerUpL), new Coord(3, 3));

            exampleBoard.PlaceEntity(
            new Entity(new Coord(5, 1), EntityKind.PowerUpL), new Coord(5, 1));
            
            exampleBoard.PlaceEntity(
            new Entity(new Coord(5, 5), EntityKind.Exit), new Coord(5, 5));

            ShowBoard(exampleBoard);

            Console.WriteLine();
            
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
                Console.Write("—————");
            Console.WriteLine("——");

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
                        Console.Write(" ——— ");
                    }
                }

                Console.Write("|");

                //Space between lines 
                Console.WriteLine("\n");
            }

            for (int i = 0; i < board.Width; i++)
                Console.Write("‾‾‾‾‾");
            Console.WriteLine("‾‾");
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