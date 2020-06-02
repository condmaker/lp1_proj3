namespace Roguelike
{
    /// <summary>
    /// The class that defines what is a 'position'/'coordinate' on the game's
    /// 'board'.
    /// </summary>
    public class Coord
    {
        /// <summary>
        /// Property that defines a horizontal position on the 'board'.
        /// </summary>
        /// <value>Aforementioned horizontal position.</value>
        public int x { get; }

        /// <summary>
        /// Property that defines a vertical position on the 'board'.
        /// </summary>
        /// <value>Aforementioned vertical position.</value>
        public int y { get; }

        /// <summary>
        /// Creates a position (Coord instance) to be used on the board.
        /// </summary>
        /// <param name="x">Horizontal position</param>
        /// <param name="y">Vertical position</param>
        public Coord(int x=0, int y=0)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Returns the instance's position properly formatted in string format.
        /// </summary>
        /// <returns>A string representing the instance's position.</returns>
        public  override string ToString() => $"({x}, {y})";

        /// <summary>
        /// Retuns the sum of two positions.
        /// </summary>
        /// <param name="a">A position</param>
        /// <param name="b">A position</param>
        /// <returns>The sum of the positions.</returns>
        public static Coord operator +(Coord a, Coord b) 
        => new Coord(a.x + b.x, a.y + b.y);

        /// <summary>
        /// Retuns the subtraction of two positions.
        /// </summary>
        /// <param name="a">A position</param>
        /// <param name="b">A position</param>
        /// <returns>The subtraction of the positions.</returns>
        public static Coord operator -(Coord a, Coord b) 
        => new Coord(a.x - b.x, a.y - b.y);

        /// <summary>
        /// Retuns the mutiplication of two positions.
        /// </summary>
        /// <param name="a">A position</param>
        /// <param name="b">A position</param>
        /// <returns>The multiplication of the positions.</returns>
        public static Coord operator *(Coord a, int b) 
        => new Coord(a.x * b, a.y * b);

        /// <summary>
        /// Retuns the division of two positions.
        /// </summary>
        /// <param name="a">A position</param>
        /// <param name="b">A position</param>
        /// <returns>The division of the positions.</returns>
        public static Coord operator /(Coord a, int b)
        {
            if (b == 0)
                throw new System.DivideByZeroException();
            return new Coord(a.x / b, a.y / b);
        }

        /// <summary>
        /// Returns the von Neumann distance of the instance's position to
        /// another given position.
        /// </summary>
        /// <param name="other">Position to base off the distance.</param>
        /// <returns>The von Neumann distance to the other position.</returns>
        public int DistanceTo(Coord other)
        {
            return ((System.Math.Abs(this.x - other.x)) 
            + (System.Math.Abs(this.y - other.y)));
        }
    }
}