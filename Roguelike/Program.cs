using System;
using System.ComponentModel;

namespace Roguelike
{
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            //Gets the arguments from the command line and creates a new 
            //GameValue object with the same arguments
            GameValues gameValues = GameValues.ConvertArgs(args);
            Game lele = new Game(gameValues);
            lele.Initiate();
           
        }
    }
}
