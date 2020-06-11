using  System;

namespace Roguelike
{
    /// <summary>
    /// Defines the properties necessary for core game functions.
    /// </summary>
    public class GameValues
    {


        /// <summary>
        /// Auto-implemented property that represents the vertical dimention of 
        /// the game board. 
        /// </summary>
        /// <value> Number of tiles in the y axis. </value>
        public int Height { get; }

        /// <summary>
        /// Auto-implemented property that represents the horizontal 
        /// dimention of the game board.
        /// </summary>
        /// <value> Number of tiles in the x axis. </value>
        public int Width { get; }

        public int Hp { get; set; }

        public int BoardSize => Width * Height; 

        public int Level { get; set; }

        public int MinionNumb => 
        (int)MathF.Max(2,
           (BoardSize * 
                MathF.Min(
                    BoardSize / 2, 
                    (float)ProcGenFunctions.Logistic(Level, 0.2f, 17, 0.28f))));
        
             
        public int BossNumb => 
        (int)MathF.Max(1,
           (BoardSize * 
                MathF.Min(
                BoardSize / 4, 
                (float)ProcGenFunctions.Logistic(Level, 0.05f, 17, 0.28f))));
            

        public int ObstclNumb{get; set;} 
        

        private float PuPNumbPercent =>
            MathF.Max(0.01f,
            0.01f *  MathF.Min(15, 
            (float)ProcGenFunctions.Logistic(Level, 25, 25, -0.08)));
            
        
        public int PowUPSmallNumb => 
            (int)(0.5f * (BoardSize * PuPNumbPercent));  

        public int PowUPSMediumNumb =>
            (int)(0.35f * (BoardSize * PuPNumbPercent)); 


        public int PowUPLargeNumb => 
            (int)(0.25f * (BoardSize * PuPNumbPercent)); 



        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static GameValues ConvertArgs(string[] args)
        {  

            int height = 0;
            int width = 0;

            //If the user doesn't give all the arguments
            //the program ends with an error message
            if(args.Length != 4 && args.Length != 2)
            {
                UI.WriteMessage("Not enough arguments.");
                Environment.Exit(0);
            }

            //Checks if the user is trying to load the game
            if(args[0] == "-l")
            {
               int[] values = SaveManager.Load(args[1]);           
               return 
                    new GameValues(values[0], values[1], values[2], values[3]);
            }

            // Argument parsing
            for(int i = 0; i < 4; i++)
            {
                if(i == 3)
                {
                    UI.WriteMessage("Not enough arguments.");
                    Environment.Exit(0);
                }

                //Documentation used to Find Replace Method
                string currentArg = "";
                try 
                {
                    currentArg = args[i].Replace("-","");
                }
                catch (IndexOutOfRangeException)
                {
                    UI.WriteMessage("Invalid Arguments. Please input -c and "  +
                        "-r for number of columns and rows, each followed by " +
                        "their respective number, or -l to load a previous "   +
                        "game with the name of the file aftwerwards.");

                    Environment.Exit(0);
                }
                
                string nextArg =  args[i+1];

                if(currentArg == "r")
                {
                    if(!int.TryParse(nextArg, out height))
                    {
                        UI.WriteMessage("Invalid row height number.");
                        Environment.Exit(0);
                    }

                    i++;
                }
                if(currentArg == "c")
                {
                    if(!int.TryParse(nextArg, out width))
                    {
                        UI.WriteMessage("Invalid column width number.");
                        Environment.Exit(0);
                    }
                    i++;
                }
            }

            return new GameValues(height, width);
        }

        /// <summary>
        /// Constructor used normaly
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        private GameValues(int width, int height)
        {
            Height = height;
            Width = width;
        }


        /// <summary>
        /// Constructor of GameValues to be used when the user loads the game
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="level"></param>
        /// <param name="hp"></param>
        private GameValues(int width, int height, int level, int hp)
        {
            Height = height;
            Width = width;
            Level = level;
            Hp = hp;
        }



    }
}