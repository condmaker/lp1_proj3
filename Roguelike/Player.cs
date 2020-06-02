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

        public override Coord WhereToMove()
        {
            return null;
            //TODO
        }
    }
}