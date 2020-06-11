using System;

namespace Roguelike
{
    /// <summary>
    /// Class responsible for the user interface on console, and for the game 
    /// tutorial.
    /// </summary>
    public static class UI
    {
        // The single string that manages all inputs on the game
        public static string Input { get; private set; } = "";

        /// <summary>
        /// A simple method that shows to the player a graphic depiction of 
        /// the Main Menu's commands.
        /// </summary>
        public static void MainMenu()
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
        /// Writes a given message <param name="str"> on the console.
        /// </summary>
        /// <param name="str">Message to write.</param>
        public static void WriteMessage(String str)
        {
            Console.WriteLine(str);
        }
        
        /// <summary>
        /// Shows game credits on Console.
        /// </summary>
        public static void ShowCredits()
        {
            Console.WriteLine("Ψѧ--------------+Ѡ+--------------ѧΨ");
            Console.WriteLine("|           -Roguelike-           |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("|             Made by:            |");
            Console.WriteLine("|         Daniel Fernandes,       |");
            Console.WriteLine("|          Pedro Bezerra,         |");
            Console.WriteLine("|          Marco Domingos         |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("|          -Made in 2020-         |");
            Console.WriteLine("Ψѧ--------------+Ѡ+--------------ѧΨ");
        }

        /// <summary>
        /// Prints a message when the player starts a game.
        /// </summary>
        public static void ShowStartingMessage(int level)
        {
            if(level == 1){
                Console.WriteLine("Ψѧ----------------------------");
                Console.WriteLine(
                    "| As you were adventuring, you find yourself trapped in "+
                    "a strange dungeon... The only option is to proceed. ");
                Console.WriteLine("Ψѧ----------------------------");
            }
            else
            {
                Console.WriteLine("Ψѧ----------------------------");
                Console.WriteLine(
                    "| You reached the end of this section and decided to "+
                    "continue exploring... You are now in the level " +
                    $"{level}. ");
                Console.WriteLine("Ψѧ----------------------------");
            }
        }

        /// <summary>
        /// Writes message showing the last player movement.
        /// </summary>
        /// <param name="dir">Direction of movement.</param>
        public static void ShowBoardInformation(Direction dir)
        {   
            Console.WriteLine($"You moved {dir.ToString()}.");
        }

        /// <summary>
        /// Writes message showing the last enemy's movement.
        /// </summary>
        /// <param name="coord">Enemy's position after movement.</param>
        /// <param name="entity">Kind of enemy.</param>
        public static void ShowBoardInformation(Coord coord, EntityKind entity)
        {
            Console.WriteLine(
                    $"An enemy {entity.ToString()} moved to " +
                    $"{coord.ToString()}.");
        }

        /// <summary>
        /// Shows current game information.
        /// </summary>
        /// <remarks>
        /// Shows player health, current turn and current floor.
        /// </remarks>
        /// <param name="playerHealth">Player health.</param>
        /// <param name="currentTurn">Current turn.</param>
        /// <param name="currentFloor">Current floor.</param>
        public static void ShowCurrentInformation(int playerHealth, 
        String currentTurn, int currentFloor)
        {
            Console.WriteLine("Ψѧ--------");
            Console.WriteLine($"| HP: {playerHealth}");
            Console.WriteLine("Ψѧ");
            Console.WriteLine($"| {currentTurn} Turn");
            Console.WriteLine("Ψѧ");
            Console.WriteLine($"| Floor: -{currentFloor}");
            Console.WriteLine("Ψѧ--------");
        }

        /// <summary>
        /// Writes message for leaving the game.
        /// </summary>
        public static void ShowEndMessage()
        {
            Console.WriteLine(
                "Ψѧ----------------------+Ѡ+-------------------ѧΨ");
            Console.WriteLine(
                "| Thank you for playing. Till the next time... |");
            Console.WriteLine(
                "Ψѧ----------------------+Ѡ+-------------------ѧΨ");
        }

        /// <summary>
        /// Writes the game tutorial.
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
            new Enemy(new Coord(2, 4), EntityKind.Minion), new Coord(2, 4));

            exampleBoard.PlaceEntity(
            new Enemy(new Coord(1, 1), EntityKind.Minion), new Coord(1, 1));

            exampleBoard.PlaceEntity(
            new Enemy(new Coord(0, 0), EntityKind.Boss), new Coord(0, 0));

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

            Console.WriteLine("\nΨѧ-------------------------------ѧΨ");
            Console.WriteLine("|    How to play, How to Score    |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
            Console.WriteLine("| You can move your character     |");
            Console.WriteLine("| with the keyboard arrows or     |");
            Console.WriteLine("| with the WSAD binds, only at    |");
            Console.WriteLine("| the player phase, obviously.    |");
            Console.WriteLine("| The game also has a Highscore   |");
            Console.WriteLine("| Board, which can be verified at |");
            Console.WriteLine("| the Main Menu. The score is     |");
            Console.WriteLine("| equivalent to the last floor    |");
            Console.WriteLine("| the player reached on that      |");
            Console.WriteLine("| session.                        |");
            Console.WriteLine("| The game ends when you die or   |");
            Console.WriteLine("| when you press the 'Escape'     |");
            Console.WriteLine("| key. After ending, the game     |");
            Console.WriteLine("| will ask you to input your name |");
            Console.WriteLine("| to be recorded alongside your   |");
            Console.WriteLine("| score.                          |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
            Console.WriteLine("| You have reached the end of the |");
            Console.WriteLine("|  tutorial and returned to the   |");
            Console.WriteLine("|            Main Menu.           |");
            Console.WriteLine("Ψѧ-------------------------------ѧΨ");
        }

        /// <summary>
        /// Writes message and awaits input to continue tutorial.
        /// </summary>
        /// <returns><c>true</c> if the player input the command to continue
        /// tutorial, <c>false</c> otherwise.
        /// </returns>
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
        /// Gets the player username if they got in the highscore board
        /// </summary>
        /// <returns> The username given by the user </returns>
        public static string PromptUsername()
        {
           Console.WriteLine("");
           Console.WriteLine("Ψѧ----------------------------ѧΨ");
           Console.WriteLine("|    You are in the top 10!    |");
           Console.WriteLine("|     Enter your username      |");
           Console.WriteLine("Ψѧ----------------------------ѧΨ\n");

           UI.WriteOnString();
           return Input; 
        }


        /// <summary>
        /// Gets a filename to save the data to
        /// </summary>
        /// <returns> The filename given by the user </returns>
        public static string PromptSaveFile()
        {
           Console.WriteLine("");
           Console.WriteLine("Ψѧ-----------------------------------ѧΨ");
           Console.WriteLine("|    Do you want to save the game?    |");
           Console.WriteLine("|    (y/n)                            |");
           Console.WriteLine("Ψѧ-----------------------------------ѧΨ\n");

           UI.WriteOnString();

           if(Input == "y")
           {
                Console.WriteLine("");
                Console.WriteLine("Ψѧ-----------------------ѧΨ");
                Console.WriteLine("|    Enter a filename.    |");
                Console.WriteLine("Ψѧ-----------------------ѧΨ\n");
                UI.WriteOnString();
           }

           return Input; 
        }


        /// <summary>
        /// Function that renders the highscore table in the console
        /// </summary>
        /// <param name="highscore">"Highscore table"</param>
        public static void ShowHighscoreTable(HighscoreTable highscore)
        {
            Console.WriteLine("Ψѧ-----|");

            //Checks every possible spot of the table.
            for(int i  = 0; i < 10; i++)
            {

                //Gets the score correspondent to the spot.
                Score score = highscore.GetScore(i);

                //If theres a score in the expecifict spot, print it.
                if(score != null) 
                {
                    Console.WriteLine ($"| Name: {score.Name} " +
                            $"+Ѡ+ Score: {score.NewScore}");
                }

            }

            Console.WriteLine("Ψѧ-----|");
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

        public static void ShowBoardInstructions()
        {
            // Basic instructions
            Console.WriteLine("Ψѧ----------------------------");
            Console.WriteLine(
                "| Use WASD or the Arrow Keys to move, or press 'q' to leave " +
                "in your turn.");
            Console.WriteLine("Ψѧ----------------------------");
            Console.WriteLine("| .☻. Player | .¤. Minion | ◄☼► Boss | " +
                              "||| Obstacle | ♥ Small HP PowerUp | ɾ♥ɿ Mid " +
                              "HP PowerUp | ѧ♥ѧ Large HP PowerUp | _∩_ Exit");
            Console.WriteLine("Ψѧ----------------------------");
        }

    /// <summary>
    /// Asks the player and awaits input to move again or not.
    /// </summary>
    /// <remarks> Outdated.
    /// </remarks>
    /// <returns><c>true</c> if the player wants to move again,
    /// <c>false</c> otherwise.
    /// </returns>
        public static bool MoveAgain()
        {
            bool willMove = false;

            Console.WriteLine("Do you wish to move again? (y/n)");
            WriteOnString(true);

            do
            {
                if (Input == "y")
                    willMove = true;
                else if (Input == "n")
                    willMove = false;
                else 
                {
                    Console.WriteLine("Unknown Input. Please answer with y/n.");
                    WriteOnString(true);
                }
            }
            while (Input != "y" && Input != "n");

            return willMove;
        } 

        /// <summary>
        /// Changes the instance Input.
        /// </summary>
        public static void WriteOnString(bool keyBuffer = false)
        {
            ConsoleKeyInfo InputTest;

            Console.Write(">");

            if (!keyBuffer)
                Input = Console.ReadLine().ToLower();
            else 
            {
                InputTest = Console.ReadKey();

                if (InputTest.Key == ConsoleKey.Escape)
                    Environment.Exit(0);

                Input = InputTest.KeyChar.ToString().ToLower();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Asks and awaits user input for direction of movement.
        /// </summary>
        /// <returns>Direction of movement.</returns>
        public static Direction InputDirection()
        {
            ConsoleKeyInfo InputTest;

            Console.Write(">");
            InputTest = Console.ReadKey();

            if (InputTest.Key == ConsoleKey.Escape)
                Environment.Exit(0);

            Input = InputTest.KeyChar.ToString().ToLower();
            Console.WriteLine();

            if (Input == "w")
                return Direction.Up;
            else if (Input == "a")
                return Direction.Left;
            else if (Input == "s")
                return Direction.Down;
            else if (Input == "d")
                return Direction.Right;
            else if (Input == "q")
                return Direction.None;

            return Direction.Undefined;
        }

    }
}