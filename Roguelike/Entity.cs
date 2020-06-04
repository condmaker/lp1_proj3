using System;
using System.Text;

namespace Roguelike
{
    /// <summary>
    /// Defines an entity in the game.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Auto implemented property which indicates the entity's position on 
        /// the board.
        /// </summary>
        /// <value></value>
        public Coord Pos {get; set;}

        /// <summary>
        /// Variable which indicates the kind of entity this is.
        /// </summary>
        public readonly EntityKind kind;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pos">A position.</param>
        /// <param name="kind">An EntityKind</param>
        public Entity(Coord pos, EntityKind kind)
        {
            this.kind = kind;
            Pos = pos;
        }

        /// <summary>
        /// Returns the instance's entity properly formatted in string format.
        /// </summary>
        /// <returns>A string representing the entity.</returns>
        public override string ToString()
        {
            switch (this.kind)
            {
                case EntityKind.Player:
                    return " .☻. ";

                case EntityKind.Minion:
                    return " .¤. ";

                case EntityKind.Boss:
                    return " ◄☼► ";

                case EntityKind.Obstacle:
                    return " ||| ";    
                
                case EntityKind.PowerUpS:
                    return "  ♥  ";

                case EntityKind.PowerUpM:
                    return " ɾ♥ɿ ";
                
                case EntityKind.PowerUpL:
                    return " ѧ♥ѧ ";

                case EntityKind.Exit:
                    return " _∩_ ";
                
                default:
                    return "";
            }
        }
    }
}