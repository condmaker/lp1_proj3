using System;

namespace Roguelike
{
    /// <summary>
    /// Class responsible for the Game's Score.
    /// </summary>
    public class Score: IComparable<Score>
    {
        /// <summary>
        /// Property that represents the name of the score's owner.
        /// </summary>
        /// <value>Name of the score's owner.</value>
        public string Name{get;}
        /// <summary>
        /// Property that represents the player's score.
        /// </summary>
        /// <value>The player's score.</value>
        public int NewScore{get;}

        public Score(string name, int newscore)
        {
            Name = name;
            NewScore = newscore;
        }


        /// <summary>
        /// Compares two Score objects.
        /// </summary>
        /// <param name="other">Second Score object.</param>
        /// <returns><c>-1</c> if <param name="other"> is not a Score,
        /// difference between <param name="other"> and this Score otherwise.
        /// </returns>
        public int CompareTo(Score other)
        {
            if (other == null) return -1;
            return other.NewScore - NewScore;
        }

    }
}