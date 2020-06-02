namespace Roguelike
{
    /// <summary>
    /// Define uma posição no mundo de jogo.
    /// </summary>
    public class Coord
    {
        /// <summary>
        /// Propriedade que define uma posição horizontal no mundo de jogo.
        /// </summary>
        /// <value>Posição horizontal no mundo de jogo.</value>
        public int x { get; }

        /// <summary>
        /// Propriedade que define uma posição vertical no mundo de jogo.
        /// </summary>
        /// <value>Posição vertical no mundo de jogo.</value>
        public int y { get; }

        /// <summary>
        /// Cria uma posição no mundo de jogo.
        /// </summary>
        /// <param name="x">Posição horizontal no mundo de jogo.</param>
        /// <param name="y">Posição vertical no mundo de jogo.</param>
        public Coord(int x=0, int y=0)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Devolve a posição devidamente formatada.
        /// </summary>
        /// <returns>A posição devidamente formatada.</returns>
        public  override string ToString() => $"({x}, {y})";

        /// <summary>
        /// Devolve a soma de duas posições.
        /// </summary>
        /// <param name="a">Uma posição</param>
        /// <param name="b">Uma posição</param>
        /// <returns>A soma de duas posições.</returns>
        public static Coord operator +(Coord a, Coord b) 
        => new Coord(a.x + b.x, a.y + b.y);

        /// <summary>
        /// Devolve a subtração de duas posições.
        /// </summary>
        /// <param name="a">Uma posição</param>
        /// <param name="b">Uma posição</param>
        /// <returns>A subtração de duas posições.</returns>
        public static Coord operator -(Coord a, Coord b) 
        => new Coord(a.x - b.x, a.y - b.y);

        /// <summary>
        /// Devolve a multiplicação de uma posição por um escalar.
        /// </summary>
        /// <param name="a">Uma posição</param>
        /// <param name="b">Um escalar</param>
        /// <returns>A multiplicação de uma posição por um escalar.</returns>
        public static Coord operator *(Coord a, int b) 
        => new Coord(a.x * b, a.y * b);

        /// <summary>
        /// Devolve a divisão de uma posição por um escalar.
        /// </summary>
        /// <param name="a">Uma posição</param>
        /// <param name="b">Um escalar</param>
        /// <returns>A divisão de uma posição por um escalar.</returns>
        public static Coord operator /(Coord a, int b)
        {
            if (b == 0)
                throw new System.DivideByZeroException();
            return new Coord(a.x / b, a.y / b);
        }

        /// <summary>
        /// Devolve a distância von Neumann até outra posição.
        /// </summary>
        /// <param name="other">Uma posição</param>
        /// <returns>A distância von Neumann até outra posição.</returns>
        public int DistanceTo(Coord other)
        {
            return ((System.Math.Abs(this.x - other.x)) 
            + (System.Math.Abs(this.y - other.y)));
        }
    }
}