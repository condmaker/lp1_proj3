using System;
using System.Collections.Generic;
using System.Threading;

namespace Roguelike
{
    /// <summary>
    /// Defines the game's main loop and other core functions.
    /// </summary>
    public class Game
    {

        /// <summary>
        /// Game values.
        /// </summary>
        private GameValues gameValues;

        /// <summary>
        /// Random number generator for the entire game.
        /// </summary>
        public static Random rand;

        /// <summary>
        /// Game board reference.
        /// </summary> 
        private Board board;
        
        /// <summary>
        /// List of all enemies on the current board.
        /// </summary>
        private List<Enemy> enemyInBoard = new List<Enemy>();

        /// <summary>
        /// The current player reference
        /// </summary>
        private Player currentPlayer;

        /// <summary>
        /// The computer's high score table.
        /// </summary>
        private HighscoreTable highscoreTable;
         
        /// <summary>
        /// The class constructor. Loads a GameValues instance and the 
        /// computer's high score table, creates a board and random number 
        /// generator instance, and generates the first level.
        /// </summary>
        /// <param name="gameValues">The files that will manage every important
        /// thing to begin the level. Can be 'loaded' from a saved game or 
        /// a new one.</param>
        public Game(GameValues gameValues)
        {
            // Loads the highscore table and current game values.
            this.gameValues = gameValues;
            highscoreTable = SaveManager.Load();
            
            // Create board and rand instance
            board = new Board(gameValues.Width, gameValues.Height);
            rand = new Random();

            GenerateLevel();
        }

        /// <summary>
        /// Shows the main menu, awaits for the user's input, and calls the 
        /// proper method to continue the game.
        /// </summary>
        public void Initiate()
        {           
            // The Main Menu loop. Will always loop while the input is different
            // than 'q'. Observes player input and enters in the respective 
            // command's method.
            while (UI.Input != "q")
            {
                UI.MainMenu();
                UI.WriteOnString(true);

                switch(UI.Input)
                {
                    case "n":
                        NewLevel();
                        break;

                    case "h":
                        UI.ShowHighscoreTable(highscoreTable);
                        break;

                    case "i":
                        UI.ShowTutorial();
                        break;

                    case "c":
                        UI.ShowCredits();
                        break;
                        
                    default:
                        UI.WriteMessage("Unknown Input.");
                        break;
                }
            }

            // Shows a 'Goodbye' message and saves the Highscore.
            UI.ShowEndMessage();
            SaveManager.Save(highscoreTable);
    
        }

        /// <summary>
        /// Main Game loop
        /// </summary>
        /// <remarks>
        /// Shows information and board, moves player and checks their HP 
        /// (twice), iterate all enemies to move them or damage player.
        /// </remarks>
        private void GameLoop()
        {

            // Shows a starting message for the level/floor.
            UI.ShowStartingMessage(gameValues.Level);

            // A boolean that describes if the player won or not.
            bool playerWon = false;

            // The game loop in and of itself. Will only be broken if the 
            // player dies, leaves the game, or if he finishes the level.
            while (currentPlayer.Health > 0)
            {

                // Prints game information for the player's turn
                UI.ShowCurrentInformation(
                    currentPlayer.Health, "Player", gameValues.Level);
                UI.ShowBoard(board);
                UI.ShowBoardInstructions();

                // Moves player and returns their state, observing if he won
                // or not.
                playerWon = MovePlayer();

                // Checks if player is dead or if he won.
                if (currentPlayer.Damage(1) <= 0)
                    continue;
                if (playerWon)
                    break;

                // Prints game info once again.
                UI.ShowCurrentInformation(
                    currentPlayer.Health, "Player", gameValues.Level);
                UI.ShowBoard(board);
                UI.ShowBoardInstructions();

                // Moves player again and returns their state
                playerWon = MovePlayer();

                // Checks if player is dead or if he won, once again.
                if (currentPlayer.Damage(1) <= 0)
                    continue;
                if (playerWon)
                    break;

                // Creates a list of enemies that will be present on the board.
                List<Enemy> enemies = new List<Enemy>();

                // Iterates through all enemies on board and adds their 
                // references to the list.
                foreach (Entity e in board.CurrentBoard)
                    if(e is Enemy)
                        enemies.Add((Enemy)e);

                // Will do actions for every enemy on board.
                foreach (Enemy enemy in enemies)
                {
                    // Prints current enemy information.
                    UI.ShowCurrentInformation(
                        currentPlayer.Health, "Enemy", gameValues.Level);
                    UI.ShowBoard(board);

                    // Observes if enemy is adjacent to player, and if so, 
                    // damages him
                    if (enemy.AdjacentToPlayer(board))
                    {
                        int damage = 5;
                        // bosses deal double the damage
                        if (enemy.kind == EntityKind.Boss)
                            damage *= 2;
                        currentPlayer.Damage(damage);

                        UI.WriteMessage($"You took {damage} damage!");
                    }

                    // If he is not adjacent to player, he moves towards him
                    else
                    {
                        // gets target move coordinate
                        Coord dest = enemy.WhereToMove(board);

                        // gets original coordinate
                        Coord source = enemy.Pos;

                        // checks if the is powerup on dest coordinate, and 
                        // stores it
                        if (board.IsPowerUp(dest) > 0)
                        {
                            board.StorePowerUp(dest);
                        }

                        // moves enemy
                        board.MoveEntity(enemy, dest);

                        // restores power ups if needed
                        board.RestorePowerUp(source);

                        // show where the enemy moved
                        UI.ShowBoardInformation(
                        enemy.Pos, enemy.kind);
                    }

                    if (currentPlayer.Health <= 0) continue;

                    // Pauses for 2 seconds so the player can process what 
                    // happened.
                    Thread.Sleep(2000);  
                } 
            } 

            // After leaving the loop, it is observed if the player won or not,
            // and the game updates values and enters methods accordingly.
            if(playerWon)
            {
                gameValues.Hp = currentPlayer.Health;
                SaveProgress();
                NewLevel();
            }
            else
                EndGame();
        }

        /// <summary>
        /// Method responsible for Player movement.
        /// </summary>
        /// <remarks>
        /// Asks for direction from the user, checks if Player reached the exit,
        /// heals player if he moved into a PowerUp, updates board.
        /// </remarks>
        /// <returns><c>true</c> if the player reached the exit,
        /// <c>false</c> otherwise.
        /// </returns>
        private bool MovePlayer()
        {
            // Gets the coord where player will move.
            Coord dest = currentPlayer.WhereToMove(board);

            // Checks if the player reached and exit.
            if(board.IsExit(dest))
                return true;

            // Checks if player is moving into power up and heals them.
            int heal = 4;
            currentPlayer.Heal(heal * board.IsPowerUp(dest));

            // Moves player and updates board.
            board.MoveEntity(currentPlayer, dest);

            return false;

        }
        
        
        /// <summary>
        /// Resets board to its initial empty state
        /// </summary>
        private void EmptyBoard()
        {
            // Cycle through every line 
            for(int y = 0; y < gameValues.Height; y++)
            {
                // Cycle through every column in the current line 
                for(int x = 0; x < gameValues.Width; x++)
                {
                    // Get current coordinate
                    Coord coord = new Coord(x,y);    

                    // Checks if there is some entity occupying the position
                    if(board.IsOccupied(coord))
                    {
                        // Deletes entity
                        board.PlaceEntity(null,coord);
                    }
 
                }
            }
        }

        /// <summary>
        /// Randomly assigns every entity a position on the board
        /// </summary>
        private void GenerateLevel()
        {
            // Creates a new random coordinate for the player, with the column
            // always being 0 (the first one).
            Coord pCoord = new Coord(0, rand.Next(0, gameValues.Height)); 

            // Observes what level it is so that the player instantiation 
            // needs a base HP value or a carried one.
            if (gameValues.Level == 1)
                currentPlayer = new Player(
                    pCoord, (gameValues.Width * gameValues.Height) / 4);
            else 
                currentPlayer = new Player(pCoord, gameValues.Hp);

            // Place the instantiated player on the current board.
            board.PlaceEntity(
                currentPlayer, currentPlayer.Pos);
            
            // Instantiate the Exit.
            Coord sCoord = new Coord (
                gameValues.Width - 1, rand.Next(0, gameValues.Height));
            board.PlaceEntity(new Entity(sCoord, EntityKind.Exit), sCoord);

            // Instatiate minions.
            for(int i = 0; i < gameValues.MinionNumb; i++)
            {
                CreateEntity(EntityKind.Minion);
            }

            // Instatiate Bosses.
            for(int i = 0; i < gameValues.BossNumb; i++)
            {
                CreateEntity(EntityKind.Boss);
            }

            // Instatiate Obstacle.
            gameValues.ObstclNumb 
                = rand.Next( (int)MathF.Min(gameValues.Height,gameValues.Width) - 1);
            for(int i = 0; i < gameValues.ObstclNumb; i++)
            {
                CreateEntity(EntityKind.Obstacle);
            }

            // Instatiate small PowerUp.
            for(int i = 0; i < gameValues.PowUPSmallNumb; i++)
            {
                CreateEntity(EntityKind.PowerUpS);
            }

            //Instatiate mid PowerUp.
            for(int i = 0; i < gameValues.PowUPSMediumNumb; i++)
            {
                CreateEntity(EntityKind.PowerUpM);
            }

            //Instatiate large PowerUp.
            for(int i = 0; i < gameValues.PowUPLargeNumb; i++)
            {
                CreateEntity(EntityKind.PowerUpL);
            } 
        }


        /// <summary>
        /// Creates an entity of the given kind <param name="kind">, and 
        /// randomly places it on the board.
        /// </summary>
        /// <param name="kind">The kind of enemy to create.</param>
        private void CreateEntity(EntityKind kind)
        {
            // Inicial position
            Coord pos;
            // Entity reference 
            Entity newEntity = null;
            

            // Find a coordinate unocupied to instatiate the entity.
            // **Made by Nuno Fachada
            do{
                pos = new Coord(
                    rand.Next(gameValues.Width),
                    rand.Next(gameValues.Height)
                );
            }while(board.IsOccupied(pos));


            // Creates Entity of the desired kind and assign it to the newEntity
            // reference.
            switch(kind)
            {
                case EntityKind.Minion:
                    newEntity = new Enemy(pos, kind);
                    break;
                case EntityKind.Boss:
                    newEntity = new Enemy(pos, kind);
                    break;
                default:
                    newEntity = new Entity(pos, kind);
                    break;
            }

            // Place new Entity int the generated position.
            board.PlaceEntity(newEntity, pos);
        }
  
        /// <summary>
        /// Saves the game progress if the player wishes so.
        /// </summary>
        private void SaveProgress()
        {
           string filename  = UI.PromptSaveFile();

            if(filename == "y"){
                SaveManager.Save(gameValues, filename);
            }
        }


        /// <summary>
        /// Handles the the new level creation.
        /// </summary>
        private void NewLevel()
        {
            // Iters the level count on GameValues
            gameValues.Level++;
            
            // Empties the board, generates a new level on it, and starts the
            // game once again.
            EmptyBoard();
            GenerateLevel();
            GameLoop();
        }

        /// <summary>
        /// Uses the UI to display the player's death and observes if the 
        /// player is in the top 10 scores, prompting him to input his name 
        /// if necessary.
        /// </summary>
        private void EndGame(){

            UI.WriteMessage("You died.");
       
            if(highscoreTable.IsHighscore(gameValues.Level))
            {
                //Prompts player to type their username
                string username = UI.PromptUsername();
                //Adds score to highscoreTable
                highscoreTable.AddScore(username, gameValues.Level);
                //Save changes
                SaveManager.Save(highscoreTable);
            }
          
        }

    }
}