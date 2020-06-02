using System;

namespace Roguelike
{
    class Enemy : Agent
    {
        public Enemy (Coord pos, EntityKind kind, int health)
            : base(pos, kind) {}

        public override Coord WhereToMove()
        {
            //TODO
        }
    }
}
