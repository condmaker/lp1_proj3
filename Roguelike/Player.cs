namespace Roguelike
{
    public class Player: Agent
    {
        public int Health {get; set;}
        
        public Player (Coord pos, int health)
            : base(pos, EntityKind.Player)
        {
            Health = health;
        }

        /// <summary>
        /// Method that asks the player a direction to move, and if it's not 
        /// obstructed, returns the coordinate to the tile adjacent to the 
        /// player in the given direction.
        /// </summary>
        /// <param name="board">The game Board.</param>
        /// <returns>The Coord of the tile adjacent to the player in the 
        /// direction of movement.</returns>
        public override Coord WhereToMove(Board board)
        {
            /*
            // Asks the player to input a direction.
            Direction direction = UI.InputDirection();

            // Gets the destination Coord and asks again if it's occupied.
            Coord dest = board.GetNeighbor(Pos, direction);
            
            while (board.IsObstructed(dest))
            {
                UI.WriteMessage("You can't move there. Try another direction.");
                direction = UI.InputDirection();
                dest = board.GetNeighbor(Pos, direction);
            }
            */
            return null;
        }
    }
}