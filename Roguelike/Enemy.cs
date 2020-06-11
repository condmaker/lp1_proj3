using System.Collections.Generic;

namespace Roguelike
{
    /// <summary>
    /// Defines a implementation of the game's enemies.
    /// </summary>
    class Enemy : Agent
    {
        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="pos">The position in which the entity is located on 
        /// the level.</param>
        /// <param name="kind">The kind of entity this is.</param>
        public Enemy (Coord pos, EntityKind kind)
            : base(pos, kind) {}


        /// <summary>
        /// Method that checks if the enemy is adjacent to the player.
        /// </summary>
        /// <param name="board">Reference to the level's board.</param>
        /// <returns><c>true</c> if it is adjacent to player,
        /// <c>false</c> otherwise.</returns>
        public bool AdjacentToPlayer(Board board)
        {
            // Creates a list with every direction
            List<Direction> Directions = new List<Direction>();
            Directions.Add(Direction.Up);
            Directions.Add(Direction.Right);
            Directions.Add(Direction.Down);
            Directions.Add(Direction.Left);

            foreach (Direction d in Directions)
            {
                Coord dest = board.GetNeighbor(Pos, d);
                if(board.IsOnBoard(dest))
                {
                    Entity neighbor = board.GetEntityAt(dest);
                    if (neighbor is Player)
                        return true;
                }
                
            }
            return false;
        }

        /// <summary>
        /// Method that implements the AI movement of the minion and boss 
        /// entities.
        /// </summary>
        /// <param name="board">Reference to the level's board.</param>
        /// <returns>A coord which represents where the enemy should move.
        /// </returns>
        public override Coord WhereToMove(Board board)
        {
            Entity target = null;
            Entity[,] level = board.CurrentBoard;
            // Finds player location on board
            foreach (Entity e in level)
            {
                if (e != null)
                {
                    if (e.kind == EntityKind.Player)
                    {
                        target = e;
                        continue;
                    }
                }
            }

            // Stores current distance to target
            int distanceToTarget = Pos.DistanceTo(target.Pos);

            // Creates a list with every direction
            List<Direction> PossibleDirections = new List<Direction>();
            PossibleDirections.Add(Direction.Up);
            PossibleDirections.Add(Direction.Right);
            PossibleDirections.Add(Direction.Down);
            PossibleDirections.Add(Direction.Left);

            // creates list of shorter paths, and all possible moves
            List<Direction> legalMoves = new List<Direction>();
            List<Direction> shorterPaths = new List<Direction>();


            // for every direction, verifies if the move is legal
            foreach (Direction d in PossibleDirections)
            {
                // gets the destination
                Coord dest = board.GetNeighbor(Pos, d);

                // verifies if it is on board and not obstructed
                if (board.IsOnBoard(dest) && !board.IsObstructed(dest) &&
                    !board.IsExit(dest))
                {
                    // adds it to the legalMoves list
                    legalMoves.Add(d);

                    // verifies if it shortens the path to player
                    int newDistanceToTarget = dest.DistanceTo(target.Pos);
                    if (newDistanceToTarget < distanceToTarget)
                        shorterPaths.Add(d);
                }
            }

            // Returns possible destiny in which the distance is shorter, if it
            // exists
            if (shorterPaths.Count > 0)
            {
                int random = Game.rand.Next(shorterPaths.Count);

                return board.GetNeighbor(Pos, shorterPaths[random]);
            }

            // If no path shortens the distance to target, returns a random
            // possible destiny
            if (legalMoves.Count > 0)
            {
                int random = Game.rand.Next(legalMoves.Count);

                return board.GetNeighbor(Pos, legalMoves[random]);
            }
                

            // if there is no possible direction to move, stays at same position
            return Pos;
        }
    }
}
