using System;

namespace Roguelike
{

    public class Game
    {

        public Board board;

        public Game(GameValues gameValues)
        {
            Console.WriteLine(gameValues.Height + "");
            board = new Board(4,4);
        }

        public void Initiate()
        {
            Coord a = new Coord(0,0);

            Console.WriteLine(board.GetTile(a));
        }

    }
}