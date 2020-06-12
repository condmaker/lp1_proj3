namespace Roguelike
{
    /// <summary>
    /// A enum defining all the kinds of members present in Entity.
    /// </summary>
    public enum EntityKind
    {
        /// <summary>
        /// The player's controllable character.
        /// </summary>
        Player,
        /// <summary>
        /// An enemy minion.
        /// </summary>
        Minion,
        /// <summary>
        /// An enemy Boss.
        /// </summary>
        Boss,
        /// <summary>
        /// An immovable obstacle.
        /// </summary>
        Obstacle,
        /// <summary>
        /// A small Power up.
        /// </summary>
        PowerUpS,
        /// <summary>
        /// A medium Power up.
        /// </summary>
        PowerUpM,
        /// <summary>
        /// A large Power up.
        /// </summary>
        PowerUpL,
        /// <summary>
        /// The exit from the level.
        /// </summary>
        Exit
    }
}