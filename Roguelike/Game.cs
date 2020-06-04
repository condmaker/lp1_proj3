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

        private HighscoreTable highscoreTable;
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameValues"></param>
        public Game(GameValues gameValues)
        {
            this.gameValues = gameValues;

            highscoreTable = new HighscoreTable();

            //Ceate board instance
            board = new Board(gameValues.Width, gameValues.Height);

            rand = new Random();
            gameValues.Level = 2;
            GenerateLevel();



        }

        /// <summary>
        /// 
        /// </summary>
        public void Initiate()
        {           
            UI.ShowBoard(board);
            UI.ShowHighscoreTable(highscoreTable);

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
            //Instantiate the Player
            Coord pCoord = new Coord(0, rand.Next(0, gameValues.Height)); 
            board.PlaceEntity(new Player(pCoord, 10) ,pCoord);
            
            //Instantiate Exit

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