using System.Collections.Generic;
using System;

namespace Roguelike
{
    public class HighscoreTable
    {
        private List<Score> highscoreTable;
        

        public HighscoreTable()
        {
            highscoreTable = new List<Score>{};
        }

        /// <summary>
        /// Add new Score to the table
        /// </summary>
        /// <param name="name"> Name of the player to be added</param>
        /// <param name="score"> Score of the player to be added</param>
        public void AddScore(string name, int score)
        {
            //Add new score and sort the table 
            Score newScore = new Score(name, score);

            highscoreTable.Add( newScore );
            highscoreTable.Sort();

            //If the table as more that 10 scores delete the last one
            if(highscoreTable.Count > 10)
            {
                highscoreTable.Remove( GetScore(10) );
            }
        }
 
        /// <summary>
        /// Get Score based on its position in the table
        /// </summary>
        /// <param name="id"> Score id </param>
        /// <returns> </returns>
        public Score GetScore(int id)
        {
            if(id > highscoreTable.Count - 1) return null;
            return highscoreTable[id];
        }


        /// <summary>
        /// Checks if the new score belongs inide the table
        /// </summary>
        /// <param name="score"> New score </param>
        /// <returns> </returns>
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