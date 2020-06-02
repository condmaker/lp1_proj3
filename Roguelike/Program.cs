using System;
using System.Collections.Generic;

namespace Roguelike
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isCommand = false; 
            Stack<char> currentCommands = new Stack<char>();
            int rows, cols;

            // Leaves the game in case the number of arguments is higher than 4.
            if (args.Length > 4)
                return;

            // Verifies the command line arguments.
            foreach (string i in args)
            {
                switch (i)
                {
                    // Option that defines the number of rows in a board.
                    case "-r":
                        if (currentCommands.Contains('r'))
                            // TODO ERROR
                        currentCommands.Push('r');
                        isCommand = true;
                        break;

                    // Option that defines the number of columns in a board.
                    case "-c":
                        if (currentCommands.Contains('c'))
                            // TODO ERROR
                        currentCommands.Push('c');
                        isCommand = true;
                        break;
                    
                    // Loads a game (may be scrapped)
                    case "-l":
                        if (currentCommands.Contains('l'))
                            // TODO ERROR
                        currentCommands.Push('l');
                        isCommand = true;
                        break;

                    default:
                        if (isCommand)
                        {
                            switch (currentCommands.Peek())
                            {
                                case 'r':
                                    if (currentCommands.Contains('l'))
                                        // TODO ERROR
                                    rows = Int32.Parse(i);
                                    break;
                                case 'c':
                                    if (currentCommands.Contains('l'))
                                        // TODO ERROR
                                    cols = Int32.Parse(i);
                                    break;
                                case 'l':
                                    if (currentCommands.Contains('r') ||
                                        currentCommands.Contains('c'))
                                        // TODO ERROR
                                    break;
                                default:
                                    break;
                            }
                            isCommand = false;
                        }
                        // TODO Else Error

                        break;
                }
            }
        }
    }
}
