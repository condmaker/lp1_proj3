using System;

namespace Roguelike
{
    class Enemy : IEntity
    {
        
        int x,y;

        public string GetInt()
        { 
            return $" - -{x} + {y}- -";    
        } 

        public Enemy(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
