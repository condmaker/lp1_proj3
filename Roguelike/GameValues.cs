namespace Roguelike
{
    public class GameValues
    {


        /// <summary>
        /// Auto-implemented property that represents the vertical dimention of 
        /// the game board. 
        /// </summary>
        /// <value> Number of tiles in the y axis. </value>
        public int Height{ get; }

        /// <summary>
        /// Auto-implemented property that represents the horizontal 
        /// dimention of the game board.
        /// </summary>
        /// <value> Number of tiles in the x axis. </value>
        public int Width{ get; }

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
            if(args.Length != 4)
            {
                //Temp for testing
                return null;
            }

            // A not very efficient way to parse the arguments
            for(int i = 0; i < 4; i++)
            {
                 if(i == 3)
                    //Exeption of some sort -> Args were passed in the wrong way
                    continue;

                //Documentation used to Find Replace Method
                string currentArg = args[i].Replace("-","");
                string nextArg =  args[i+1];

                if(currentArg == "r")
                {
                    if(!int.TryParse(nextArg, out height))
                    {
                        //Exeption
                    }

                    i++;
                }
                if(currentArg == "c")
                {
                    if(!int.TryParse(nextArg, out width))
                    {
                        //Exeption
                    }
                    i++;
                }
            }

            return new GameValues(height, width);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        private GameValues(int height, int width)
        {
            Height = height;
            Width = width;
        }

    }
}