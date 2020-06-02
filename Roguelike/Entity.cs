namespace Roguelike
{
    public abstract class Entity
    {
        public Coord Pos{
            get
            {
                return Pos;
            }
            set
            {
                Pos = new Coord(0,0);    
            }
        }
    
    
    }
}