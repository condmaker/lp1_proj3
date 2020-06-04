using System.Collections.Generic;

namespace Roguelike
{
    class Enemy : Agent
    {
        public Enemy (Coord pos, EntityKind kind, int health)
            : base(pos, kind) {}

        public override Coord WhereToMove(Board board)
        {
            Entity target = null;
            Entity[,] level = board.GetBoard();
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

            // Creates a list with every adjacent tile
            List<Coord> possibleDestiny = new List<Coord>();
            possibleDestiny.Add(board.GetNeighbor(Pos, Direction.Up));
            possibleDestiny.Add(board.GetNeighbor(Pos, Direction.Right));
            possibleDestiny.Add(board.GetNeighbor(Pos, Direction.Down));
            possibleDestiny.Add(board.GetNeighbor(Pos, Direction.Left));

            // Removes from list tiles that are obstructed
            for (int i = possibleDestiny.Count - 1; i >= 0; i--)
            {
                if (board.IsObstructed(possibleDestiny[i]))
                {
                    possibleDestiny.RemoveAt(i);
                }
            }

            // Creates copy of the possibleDestiny list
            List<Coord> pathsToTarget = possibleDestiny;

            // Removes from new list destinies that don't make the distace to 
            // target shorter
            for (int i = pathsToTarget.Count - 1; i >= 0; i--)
            {
                int newDistanceToTarget = 
                    pathsToTarget[i].DistanceTo(target.Pos);
                if (newDistanceToTarget >= distanceToTarget)
                    pathsToTarget.RemoveAt(i);
            }

            // Returns possible destiny in which the distance is shorter, if it
            // exists
            if (pathsToTarget.Count > 0)
            {
                int random = Game.rand.Next(pathsToTarget.Count);

                return pathsToTarget[random];
            }

            // If no path shortens the distance to target, returns a random
            // possible destiny
            if (possibleDestiny.Count > 0)
            {
                int random = Game.rand.Next(possibleDestiny.Count);

                return possibleDestiny[random];
            }
                

            // if there is no possible direction to move, stays at same position
            return Pos;
        }
    }
}
