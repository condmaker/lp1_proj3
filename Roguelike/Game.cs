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
        /// Game values
        /// </summary>
        private GameValues gameValues;

        /// <summary>
        /// Random number generator
        /// </summary>
        public static Random rand;

        /// <summary>
        /// Game board reference
        /// </summary> 
        private Board board;
        
        private List<Enemy> enemyInBoard = new List<Enemy>();

        /// <summary>
        /// The current player reference
        /// </summary>
        private Player currentPlayer;

        private HighscoreTable highscoreTable;
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameValues"></param>
        public Game(GameValues gameValues)
        {
            this.gameValues = gameValues;

            highscoreTable = SaveManager.Load();
            

            //Ceate board instance
            board = new Board(gameValues.Width, gameValues.Height);

            rand = new Random();
            gameValues.Level = 1;

            GenerateLevel();

        }

        /// <summary>
        /// Shows the main menu, awaits for the user's input, and calls the 
        /// proper method to continue the game.
        /// </summary>
        public void Initiate()
        {           
            while (UI.Input != "q")
            {
                UI.MainMenu();
                UI.WriteOnString();

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
                        break;
                }
            }

            UI.ShowEndMessage();
            //SaveManager.Save(highscoreTable);
    
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
            
            UI.ShowStartingMessage(gameValues.Level);

            bool playerWon = false;
            while (currentPlayer.Health > 0)
            {

                // Prints game information
                UI.ShowCurrentInformation(
                    currentPlayer.Health, "Player", gameValues.Level);
                UI.ShowBoard(board);
                UI.ShowBoardInstructions();

                // moves player and returns their state
                playerWon = MovePlayer();

                if (currentPlayer.Damage(1) <= 0)
                    continue;
                if (playerWon)
                    break;

                // Prints game info
                UI.ShowCurrentInformation(
                    currentPlayer.Health, "Player", gameValues.Level);
                UI.ShowBoard(board);
                UI.ShowBoardInstructions();

                // moves player again and returns their state
                playerWon = MovePlayer();

                if (currentPlayer.Damage(1) <= 0)
                    continue;
                if (playerWon)
                    break;

                // iterates though all enemies on board, moving them or damaging
                // the player nearby
                List<Enemy> enemies = new List<Enemy>();

                foreach (Entity e in board.CurrentBoard)
                    if(e is Enemy)
                        enemies.Add((Enemy)e);

                foreach (Enemy enemy in enemies)
                {
                    // prints current information on console
                    UI.ShowCurrentInformation(
                        currentPlayer.Health, "Enemy", gameValues.Level);
                    UI.ShowBoard(board);

                    // checks if is adjacent to player
                    if (enemy.AdjacentToPlayer(board))
                    {
                        int damage = 5;
                        // bosses deal double the damage
                        if (enemy.kind == EntityKind.Boss)
                            damage *= 2;
                        currentPlayer.Damage(damage);
                    }

                    // if it's not adjacent to player
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

                    if (currentPlayer.Health < 0) continue;

                    Thread.Sleep(1000);  
                } // end foreach (Enemy enemy in enemyInBoard)
            } // end while (currentPlayer.Health > 0)

            if(playerWon)
                NewLevel();
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
            // gets the coord where player will move
            Coord dest = currentPlayer.WhereToMove(board);

            //checks if the player reached and exit
            if(board.IsExit(dest))
                return true;

            // checks if player is moving into power up and heals them
            int heal = 4;
            currentPlayer.Heal(heal * board.IsPowerUp(dest));

            // moves player and updates board
            board.MoveEntity(currentPlayer, dest);

            return false;

        }
        
        
        /// <summary>
        /// Resets board to its initial empty state
        /// </summary>
        private void EmptyBoard()
        {
            //Cycle through every line 
            for(int y = 0; y < gameValues.Height; y++)
            {
                //Cycle through every column in the current line 
                for(int x = 0; x < gameValues.Width; x++)
                {
                    //Get current coordinate
                    Coord coord = new Coord(x,y);                    
                    //Checks if there is some entity occupying the position
                    if(board.IsOccupied(coord))
                    {
                        //Deletes entity
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
            Coord pCoord = new Coord(0, rand.Next(0, gameValues.Height)); 
            currentPlayer = new Player(
                pCoord, (gameValues.Width * gameValues.Height) / 4);

            // Instantiate the Player
            board.PlaceEntity(
                currentPlayer, currentPlayer.Pos);
            
            // Instantiate Exit
            Coord sCoord = new Coord (
                gameValues.Width - 1, rand.Next(0, gameValues.Height));
            board.PlaceEntity(new Entity(sCoord, EntityKind.Exit), sCoord);

            //Instatiate minions
            for(int i = 0; i < gameValues.MinionNumb; i++)
            {
                CreateEntity(EntityKind.Minion);
            }

            //Instatiate Bosses
            for(int i = 0; i < gameValues.BossNumb; i++)
            {
                CreateEntity(EntityKind.Boss);
            }

            //Instatiate Obstacle
            gameValues.ObstclNumb 
                = rand.Next( (int)MathF.Min(gameValues.Height,gameValues.Width) - 1);
            for(int i = 0; i < gameValues.ObstclNumb; i++)
            {
                CreateEntity(EntityKind.Obstacle);
            }

            //Instatiate PowerUp small
            for(int i = 0; i < gameValues.PowUPSmallNumb; i++)
            {
                CreateEntity(EntityKind.PowerUpS);
            }

            //Instatiate PowerUp medium
            for(int i = 0; i < gameValues.PowUPSMediumNumb; i++)
            {
                CreateEntity(EntityKind.PowerUpM);
            }

            //Instatiate PowerUp large
            for(int i = 0; i < gameValues.PowUPLargeNumb; i++)
            {
                CreateEntity(EntityKind.PowerUpL);
            }

            // Gets a list with all enemy in the board
            foreach (Entity entity in board.CurrentBoard)
                if (entity is Enemy)
                    enemyInBoard.Add((Enemy) entity);  
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
            

            //Find a coordinate unocupied to instatiate the entity]
            //Made by the teacher
            do{
                pos = new Coord(
                    rand.Next(gameValues.Width),
                    rand.Next(gameValues.Height)
                );
            }while(board.IsOccupied(pos));


            // Creates Entity of the desired kind and assign it to the newEntity
            // reference
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

            // Place new Entity int the generated position
            board.PlaceEntity(newEntity, pos);
        }
  

        /// <summary>
        /// Handles the the new level creation
        /// </summary>
        private void NewLevel()
        {
            gameValues.Level++;
            EmptyBoard();
            GenerateLevel();
            GameLoop();
        }

        private void EndGame(){
            //Prints an exit message
            //UI.ExitMessage();
            UI.WriteMessage("Player's health as reach 0 points.");

       
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