namespace Roguelike
{
    public class Player: Agent
    {
        public int Health {get; set;}
        
        public Player (Coord pos, EntityKind kind, int health)
            : base(pos, kind)
        {
            Health = health;
        }

        public override Coord WhereToMove()
        {
            //TODO
        }
    }
}