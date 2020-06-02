namespace Roguelike
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Coord
    {
        private int x, y;

        public Coord(int x=0, int y=0)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public int GetX() => x;

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public int GetY() => y;

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public  override string ToString() => $"({x}, {y})";

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public static Coord operator +(Coord a, Coord b) => new Coord(a.GetX() + b.GetX(), a.GetY() + b.GetY());

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public static Coord operator -(Coord a, Coord b) => new Coord(a.GetX() - b.GetX(), a.GetY() - b.GetY());

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Coord operator *(Coord a, int b) => new Coord(a.GetX() * b, a.GetY() * b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Coord operator /(Coord a, int b)
        {
            if (b == 0)
                throw new System.DivideByZeroException();
            return new Coord(a.GetX() / b, a.GetY() / b);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int DistanceTo(Coord other)
        {
            return ((System.Math.Abs(this.x - other.GetX())) + (System.Math.Abs(this.y - other.GetY())));
        }
    }
}