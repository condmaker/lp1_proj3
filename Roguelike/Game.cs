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
        public UI ui;

        public Game(GameValues gameValues)
        {
            this.gameValues = gameValues;

            board = new Board(gameValues.Height, gameValues.Width);
            ui = new UI();
        }

        public void Initiate()
        {
            Coord a = new Coord(0,0);

            ui.ShowBoard(board);
            //Console.WriteLine(board.GetTile(a));
        }

    }
}