using System;

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
            gameValues.Level = 2;

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
            UI.MainMenu();

            while (UI.Input != "q")
            {
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
            UI.ShowStartingMessage();

            while (currentPlayer.Health > 0)
            {
                UI.ShowCurrentInformation(
                    currentPlayer.Health, "Player", gameValues.Level);
                UI.ShowBoard(board);
                
                // TODO - See if the player wants to move again
                board.MoveEntity(
                    currentPlayer, currentPlayer.WhereToMove(board));

                VerifyNeighbours();

                UI.ShowCurrentInformation(
                    currentPlayer.Health, "Enemy", gameValues.Level);
                //UI.ShowBoard(board);
                // Here goes a for/foreach for all enemies on board to move

            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void VerifyNeighbours()
        {
            Coord verUp = board.GetNeighbor(
                currentPlayer.Pos, Direction.Up);
            Coord verDown = board.GetNeighbor(
                currentPlayer.Pos, Direction.Down);
            Coord verLeft = board.GetNeighbor(
                currentPlayer.Pos, Direction.Left);
            Coord verRight = board.GetNeighbor(
                currentPlayer.Pos, Direction.Right);

            if (board.GetEntityAt(verUp) is Enemy )
            {
                // Verifies if enemy caught is a minion or a boss
                if (board.GetEntityAt(verUp).kind == EntityKind.Minion)
                    currentPlayer.Health -= 5;
                if (board.GetEntityAt(verUp).kind == EntityKind.Boss)
                    currentPlayer.Health -= 5;
            }
            if (board.GetEntityAt(verDown) is Enemy )
            {
                // Verifies if enemy caught is a minion or a boss
                if (board.GetEntityAt(verDown).kind == EntityKind.Minion)
                    currentPlayer.Health -= 5;
                if (board.GetEntityAt(verDown).kind == EntityKind.Boss)
                    currentPlayer.Health -= 5;
            }
            if (board.GetEntityAt(verLeft) is Enemy )
            {
                // Verifies if enemy caught is a minion or a boss
                if (board.GetEntityAt(verLeft).kind == EntityKind.Minion)
                    currentPlayer.Health -= 5;
                if (board.GetEntityAt(verLeft).kind == EntityKind.Boss)
                    currentPlayer.Health -= 5;
            }
            if (board.GetEntityAt(verRight) is Enemy )
            {
                // Verifies if enemy caught is a minion or a boss
                if (board.GetEntityAt(verRight).kind == EntityKind.Minion)
                    currentPlayer.Health -= 5;
                if (board.GetEntityAt(verRight).kind == EntityKind.Boss)
                    currentPlayer.Health -= 5;
            }
        }
        
        /// <summary>
        /// Resets board to its initial empty state
        /// </summary>
        private void EmptyBoard()
        {

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
  
    }
}