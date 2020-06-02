namespace Roguelike
{
    public abstract class Agent: Entity
    {
        public Agent(Coord pos, EntityKind kind) : base(pos, kind){}
        public abstract Coord WhereToMove();
    }
}