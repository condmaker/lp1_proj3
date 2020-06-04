using System.IO;
using System;

namespace Roguelike
{
    public static class SaveManager
    {
        public static void Save(HighscoreTable highscore)
        {
            //Open file in writeable mode
            StreamWriter sw = new StreamWriter("scores.txt");

            //Checks every possible spot of the table.
            for(int i  = 0; i < 10; i++)
            {

                //Gets the score correspondent to the spot.
                Score score = highscore.GetScore(i);

                //If theres a score in the expecifict spot, print it.
                if(score != null)
                     sw.WriteLine (score.Name + " " + score.NewScore);
                           
            }
          
            sw.Close();
        }


        /// <summary>
        /// Load the data of an Highscore Table form a file
        /// </summary>
        /// <returns> Properly filled Highscore Table </returns>
        public static HighscoreTable Load()
        {
            //Instatiate a new highscore table reference
            HighscoreTable highscore = new HighscoreTable();

            string scoreData = "";

            //Open file in readeable mode 
            StreamReader sr = new StreamReader("scores.txt");

            while( (scoreData = sr.ReadLine()) != null)
            {
                string[] splitData = scoreData.Split();
                int score;
                    
                if(!int.TryParse(splitData[1], out score))
                {
                    //Execption
                }
                    
                highscore.AddScore(splitData[0], score);      

            }        
                   
            return highscore;
        }

    }
}