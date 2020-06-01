using System;

namespace Roguelike
{
    public class Board
    {

        // Board coordinates
        private int width, height;

        // Game board 
        private IEntity[,] board;



        /// <summary>
        /// AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        /// a
        /// </summary>
        public Board(int height, int width)
        {
            this.height = height;
            this.width = width;
            board = new IEntity[height,width];     
        }

        /// <summary>
        /// Get the tile
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public IEntity GetTile(Coord pos)
        {       
            return board[pos.x, pos.y];
        } 


        /// <summary>
        /// Does thing yes
        /// </summary>
        /// <returns></returns>
        public bool IsOnBoard(Coord pos)
        {
            // Não é sexual
            bool isInside = true;

            if (pos.x < 0)
                isInside = false;
            if (pos.x >= width)
                isInside = false;
            if (pos.y < 0)
                isInside = false;
            if (pos.y >= height)
                isInside = false;

            return isInside;
            
        }  

    }
}