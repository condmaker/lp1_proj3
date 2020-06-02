namespace Roguelike
{
    public class Entity
    {
        public Coord Pos {get; set;}
        public readonly EntityKind kind;

        public Entity(Coord pos, EntityKind kind)
        {
            Pos = pos;
            this.kind = kind;
        }
    }
}