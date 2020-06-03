using System.Collections.Generic;

namespace Roguelike
{
    /// <summary>
    /// The class that represents a level in the game.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Private instance variable that contains a reference to the 
        /// bi-dimensional array that represents the game's 'board' (made by 
        /// 'tiles' of Entities).
        /// </summary>
        private Entity[,] board;


        /// <summary>
        /// Private instance variable that contains reference to the Entities
        /// that are hidden under an enemy. This is used to store the Power-Ups
        /// when enemies are in the same tiles as them.
        /// </summary>
        private List<Entity> hiddenPowerUps;

        /// <summary>
        /// Creates a new instance of a level (or board).
        /// </summary>
        /// <param name="height">Horizontal dimensions of the level.</param>
        /// <param name="width">Vertical dimensions of the level.</param>
        public Board(int width, int height)
        {
            board = new Entity[width,height];   
        }

        /// <summary>
        /// Property that represents the horizontal dimension of the level.
        /// </summary>
        /// <value>Horizontal dimension of the level.</value>
        public int Width => board.GetLength(0);

        /// <summary>
        /// Property that represents the vertical dimension of the level.
        /// </summary>
        /// <value>Vertical dimension of the level.</value>
        public int Height => board.GetLength(1);

        /// <summary>
        /// Method that returns the Entity of a position indicated at the 
        /// parameter <param name="pos">.
        /// </summary>
        /// <param name="pos">Position in the current game 'board'.</param>
        /// <returns>Entity in the position indicated in the paramenter
        /// <param name="pos">.</returns>
        // TODO : IEntity?
        public Entity GetEntityAt(Coord pos)
        {       
            pos = Normalize(pos);
            return board[pos.x, pos.y];
        } 

        /// <summary>
        /// Method that indicates if an entity in the indicated position on the
        /// parameter <param name="c"> exists.
        /// </summary>
        /// <param name="c">Position on the 'board'.</param>
        /// <returns><c>true</c> if an entity in the given position exists in
        /// <param name="c">, <c>false</c> otherwise
        /// </returns>
        public bool IsOccupied(Coord c)
        {
            c = Normalize(c);
            // Return false if the Coord is empty
            // and return true otherwise
            return board[c.x, c.y] != null;
        }


        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool IsObstructed(Coord c){
            c = Normalize(c);

            // if the tile is not occupied, it is not obstructed.
            if (IsOccupied(c) == false)
                return false;

            // if the tile is occupied but not by a Power Up, it is obstructed.
            if (GetEntityAt(c).kind != EntityKind.PowerUpS &&
                GetEntityAt(c).kind != EntityKind.PowerUpM &&
                GetEntityAt(c).kind != EntityKind.PowerUpL)
                return true;
            // otherwise, it is not obstructed.
            return false;
        }

        /// <summary>
        /// Method that returns the Neighbour position of the given position in
        /// the parameter <param name="coord">, in the direction of the 
        /// parameter <param name="direction">.
        /// </summary>
        /// <param name="coord">Position in the 'board'</param>
        /// <param name="direction">Direction to check the Neighbour</param>
        /// <returns>Neighbour position of the given position in the paramenter
        /// <param name="coord"> in the direction of the paramenter
        /// <param name="direction">.
        /// </returns>
        public Coord GetNeighbor(Coord coord, Direction direction)
        {
            // Declaration of the neighbour coordinate to be written to.
            Coord neighbor;

            // Verifies the direction and obtains the Neighbour Coord.
            switch(direction)
            {
                case Direction.Up:
                    neighbor = coord - new Coord(0, -1);
                    break;
                case Direction.Right:
                    neighbor = coord - new Coord(1, 0);
                    break;
                case Direction.Down:
                    neighbor = coord - new Coord(0, 1);
                    break;
                case Direction.Left:
                    neighbor = coord - new Coord(-1, 0);
                    break;
                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException
                    ("Direção não reconhecida.");
            }
            neighbor = Normalize(neighbor);
            return neighbor;
        }

        /// <summary>
        /// Moves entity <param name="entity"> to the position 
        /// <param name="coord">
        /// </summary>
        /// <param name="entity">Entity to be moved</param>
        /// <param name="coord">Destination coordinate of the entity</param>
        public void MoveEntity(Entity entity, Coord coord)
        {
            PlaceEntity(entity, coord);
            board[entity.Pos.x, entity.Pos.y] = null;
        }


        /// <summary>
        /// Places entity <param name="entity"> in the position 
        /// </summary>
        /// <param name="entity">Entity to be placed</param>
        /// <param name="coord">Destination coordinate of the entity</param>
        public void PlaceEntity(Entity entity, Coord coord)
        {
            coord = Normalize(coord);
            board[coord.x, coord.y] = entity;
        }

        /// <summary>
        /// Normalizes a coordinate, assuring that its still inside the level's
        /// bounds.
        /// </summary>
        /// <param name="c">Coord to be normalized.</param>
        /// <returns>Normalized Coord.</returns>
        public Coord Normalize(Coord c)
        {
            // Initially, the original values
            int x = c.x;
            int y = c.y;
            
            // Observes if the horizontal value is inside the bounds
            while (x>= Width) x -= Width;
            while (x < 0) x += Width;

            // Observes if the vertical value is inside the bounds
            while (y>= Height) y -= Height;
            while (y < 0) y += Height;

            return new Coord(x, y);
        }

        /// <summary>
        /// Stores the Entity from a given position <param name="c"> in the 
        /// hiddenPowerUps List.
        /// </summary>
        /// <param name="c">The position on the board of the entity to be 
        /// stored.</param>
        public void StorePowerUp(Coord c)
        {
            hiddenPowerUps.Add(GetEntityAt(c));
        }

        /// <summary>
        /// Restores a Entity in hiddenPowerUps List to a given position
        /// <param name="c"> on the board.
        /// </summary>
        /// <param name="c">The position of the entity to be restored.</param>
        public void RestorePowerUp(Coord c)
        {
            foreach (Entity e in hiddenPowerUps)
            {
                if (e.Pos == c)
                {
                    board[c.x, c.y] = e;
                    hiddenPowerUps.Remove(e);
                    continue;
                }
            }
        }

    }
}