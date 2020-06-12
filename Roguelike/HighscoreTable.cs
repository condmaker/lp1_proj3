using System.Collections.Generic;
using System;

namespace Roguelike
{
    /// <summary>
    /// Class responsible for managing High Scores.
    /// </summary>
    public class HighscoreTable
    {
        /// <summary>
        /// A list laoded everytime when the game is launched (or created), 
        /// that will contain the Top 10 scores on the current computer.
        /// </summary>
        private List<Score> highscoreTable;
        
        /// <summary>
        /// Constructor of the class. Instantiates the Highscore Table List.
        /// </summary>
        public HighscoreTable()
        {
            highscoreTable = new List<Score>{};
        }

        /// <summary>
        /// Add a new Score to the table.
        /// </summary>
        /// <param name="name">Name of the player to be added</param>
        /// <param name="score">Score of the player to be added</param>
        public void AddScore(string name, int score)
        {
            // Add new score and sort the table.
            Score newScore = new Score(name, score);

            highscoreTable.Add( newScore );
            highscoreTable.Sort();

            // If the table as more that 10 scores delete the last one.
            if(highscoreTable.Count > 10)
            {
                highscoreTable.Remove( GetScore(10) );
            }
        }
 
        /// <summary>
        /// Get Score based on its position <param name="id"> in the table
        /// </summary>
        /// <param name="id">The Score's ID</param>
        /// <returns>High score on the position <param name="id">.</returns>
        public Score GetScore(int id)
        {
            if(id > highscoreTable.Count - 1) return null;
            return highscoreTable[id];
        }

        /// <summary>
        /// Checks if the new score belongs in the table
        /// </summary>
        /// <param name="score"> The new score to be checked </param>
        /// <returns><c>true</c> if new score belongs in the table,
        /// <c>false</c> otherwise.
        /// </returns>
        public bool IsHighscore(int score)
        {
            if(highscoreTable.Count < 10)
                return true;

            if(score > GetScore(highscoreTable.Count - 1).NewScore)
                return true;
            return false;
        }
    }
}