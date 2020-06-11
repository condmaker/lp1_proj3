using System.IO;
using System;

namespace Roguelike
{
    /// <summary>
    /// Class responsible for saving the game state.
    /// </summary>
    public static class SaveManager
    {

        /// <summary>
        /// Saves the data of the Highscore Table in a file
        /// </summary>
        /// <param name="highscore">Highscore table to be saved</param>
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

            //Close file
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

            //Cycle through every line inside the file
            while( (scoreData = sr.ReadLine()) != null)
            {
                //Split the data in two string:
                //name and score
                string[] splitData = scoreData.Split();
                int score;
                    
                //Parse the score data into an int
                if(!int.TryParse(splitData[1], out score))
                {
                    //Execption
                }
                
                //Add score to new HighscoreTable
                highscore.AddScore(splitData[0], score);      

            }   

            //Close file
            sr.Close();     
                   
            return highscore;
        }




        /// <summary>
        /// Saves the game progress inside a file 
        /// </summary>
        /// <param name="gameValues"> Class that contains the data to be 
        /// saved</param>
        /// <param name="fileName"> Name of the file choosen by the user 
        /// where the data is saved</param>
        public static void Save(GameValues gameValues, string fileName)
        {
            //Open file in writeable mode
            StreamWriter sw = new StreamWriter(fileName);


            //Save the game data in the file
            sw.WriteLine(gameValues.Width);
            sw.WriteLine(gameValues.Height);
            sw.WriteLine(gameValues.Level);
            sw.WriteLine(gameValues.Hp);

            
            //Close file
            sw.Close();
        }


        /// <summary>
        /// Loads the game data from a file 
        /// </summary>
        /// <param name="fileName"> Name of the file where the data is saved on
        /// given by the user</param>
        /// <returns></returns>
         public static int[] Load(string fileName)
         {
           
            int[] values = new int[4];
              
            //Open file in readeable mode 
            StreamReader sr = new StreamReader(fileName);

            //Cycle trough every line and retrieve the game data
            for(int i = 0; i < 4; i++)
            {
                
                if(!int.TryParse(sr.ReadLine(), out values[i]))
                {
                    //Exception
                }
               
            }

            //Close file
            sr.Close();

            return values;
        }
    }
}