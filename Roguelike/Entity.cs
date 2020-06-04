using System;
using System.Text;

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

        public override string ToString()
        {
            switch (this.kind)
            {
                case EntityKind.Player:
                    return ".☻.";

                case EntityKind.Minion:
                    return ".¤.";

                case EntityKind.Boss:
                    return "◄☼►";

                case EntityKind.Obstacle:
                    return "|||";    
                
                case EntityKind.PowerUpS:
                    return " ♥ ";

                case EntityKind.PowerUpM:
                    return "ɾ♥ɿ";
                
                case EntityKind.PowerUpL:
                    return "ѧ♥ѧ";

                case EntityKind.Exit:
                    return "_∩_";
                
                default:
                    return "";
            }
        }
    }
}