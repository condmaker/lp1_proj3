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
        private Random rand;

        /// <summary>
        /// Game board reference
        /// </summary>
        public Board board;

        public Game(GameValues gameValues)
        {
            this.gameValues = gameValues;
            
            

            board = new Board(gameValues.Height, gameValues.Width)
        }

        public void Initiate()
        {

        }

    }
}