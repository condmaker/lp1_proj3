namespace Roguelike
{
    /// <summary>
    /// Class that defines abstractly the units that can move in the game.
    /// </summary>
    public abstract class Agent: Entity
    {
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="pos">The position in which the entity is located on 
        /// the level.</param>
        /// <param name="kind">The kind of entity this is.</param>
        public Agent(Coord pos, EntityKind kind) : base(pos, kind){}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public abstract Coord WhereToMove(Board board);
    }
}