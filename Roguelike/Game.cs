using System;
using System.Collections.Generic;
using System.Threading;

namespace Roguelike
{

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

            // Instantiates current player
            Coord pCoord = new Coord(0, rand.Next(0, gameValues.Height)); 
            currentPlayer = new Player(
                pCoord, (gameValues.Width * gameValues.Height) / 4);

            GenerateLevel();

        }

        /// <summary>
        /// 
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
                        GameLoop();
                        break;

                    case "m":
                        UI.MainMenu();
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
        /// 
        /// </summary>
        private void GameLoop()
        {
            
            UI.ShowStartingMessage(gameValues.Level);

            while (currentPlayer.Health > 0)
            {
                // Prints game information
                UI.ShowCurrentInformation(
                    currentPlayer.Health, "Player", gameValues.Level);
                UI.ShowBoard(board);

                // moves player
                MovePlayer();
                if (currentPlayer.Damage(1) <= 0)
                    continue;

                // Prints game info
                UI.ShowCurrentInformation(
                    currentPlayer.Health, "Player", gameValues.Level);
                UI.ShowBoard(board);

                // moves player again
                MovePlayer();
                if (currentPlayer.Damage(1) <= 0)
                    continue;


                // iterates though all enemies on board, moving them or damaging
                // the player nearby
                foreach (Enemy enemy in enemyInBoard)
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
                    // TODO POWERUP
                    else
                    {
                        // moves enemy and shows movement on console
                        board.MoveEntity(
                        enemy, enemy.WhereToMove(board));

                        // show where the enemy moved
                        UI.ShowBoardInformation(
                        enemy.Pos, enemy.kind);
                    }

                    if (currentPlayer.Health < 0) continue;

                    Thread.Sleep(1000);  
                } // end foreach (Enemy enemy in enemyInBoard)
            } // end while (currentPlayer.Health > 0)
            UI.WriteMessage("Your health reached 0. Game over.");
        }

        private void MovePlayer()
        {
            // gets the coord where player will move
            Coord dest = currentPlayer.WhereToMove(board);

            // checks if player is moving into power up and heals them
            int heal = 4;
            Entity ent = board.GetEntityAt(dest);
            if (ent != null)
            {
                if (ent.kind == EntityKind.PowerUpS)
                    currentPlayer.Heal(heal);
                if (ent.kind == EntityKind.PowerUpM)
                    currentPlayer.Heal(heal*2);
                if (ent.kind == EntityKind.PowerUpL)
                    currentPlayer.Heal(heal*4);
            }

            // moves player and updates board
            board.MoveEntity(currentPlayer, dest);

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
        /// 
        /// </summary>
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
        private void NextLevel()
        {
            gameValues.Level++;
            EmptyBoard();
            GenerateLevel();
            GameLoop();
        }

    }
}