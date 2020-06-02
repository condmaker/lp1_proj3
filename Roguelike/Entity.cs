namespace Roguelike
{
    public class Entity
    {
        public Coord Pos {get; set;}
        public readonly EntityKind kind;

        public Entity(Coord pos, EntityKind kind)
        {
            this.kind = kind;
            Pos = pos;
        }
    }
}