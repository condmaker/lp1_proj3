using System;

namespace Roguelike
{
    public class Player: Agent
    {
        /// <summary>
        /// Auto implemented property which indicates the player's health 
        /// points.
        /// </summary>
        /// <value>The player's health points.</value>
        public int Health {get; set;}
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos">The position in which the entity is located on 
        /// the level.</param>
        /// <param name="health"></param>
        public Player (Coord pos, int health)
            : base(pos, EntityKind.Player)
        {
            Health = health;
        }

        /// <summary>
        /// Methods that subtracts a given amount <param name="damage"> from 
        /// the player's health.
        /// </summary>
        /// <param name="damage">The amount to subtract.</param>
        /// <returns>The player's health after subtracting.</returns>
        public int Damage(int damage)
        {
            Health -= damage;
            return Health;
        }

        /// <summary>
        /// Methods that adds a given amount <param name="heal"> to 
        /// the player's health.
        /// </summary>
        /// <param name="heal">The amount to add.</param>
        /// <returns>The player's health after adding.</returns>
        public int Heal(int heal)
        {
            Health += heal;
            return Health;
        }

        /// <summary>
        /// Method that asks the player a direction to move, and if it's not 
        /// obstructed, returns the coordinate to the tile adjacent to the 
        /// player in the given direction.
        /// </summary>
        /// <param name="board">Reference to the level's board.</param>
        /// <returns>The Coord of the tile adjacent to the player in the 
        /// direction of movement.</returns>
        public override Coord WhereToMove(Board board)
        {

            // Asks the player to input a direction.
            Direction direction = UI.InputDirection();

            if (direction == Direction.None)
            {
                UI.ShowEndMessage();
                Environment.Exit(0);
            }

            // Gets the destination Coord and asks again if it's occupied.
            Console.WriteLine("Debug: " + Pos);
            Coord dest = board.GetNeighbor(Pos, direction);
            
            while (board.IsObstructed(dest))
            {
                Console.WriteLine("Debug: " + Pos);
                UI.WriteMessage("You can't move there. Try another direction.");
                direction = UI.InputDirection();
                dest = board.GetNeighbor(Pos, direction);
            }
            Health--;
            return dest;
        }
    }
}