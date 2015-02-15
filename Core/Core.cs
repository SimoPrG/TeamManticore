/*General General RequirementsRequirementsRequirementsRequirementsRequirements
Please define and implement the following assets in your project:
 At least 1 multi-dimensional array
 At least 3 one-dimensional arrays
 At least 10 methods (separating the application’s logic)
 At least 3 existing .NET classes (like System.Math or System.DateTime)
 At least 2 exception handlings
 At least 1 use of external text file
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    class Core
    {
        // Score position!
        static void PrintOnPosition(int x, int y, char c,
            ConsoleColor color = ConsoleColor.Green)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(c);
        }
        static void PrintStringOnPosition(int x, int y, string str,
            ConsoleColor color = ConsoleColor.Green)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        //the array which keeps the symbols which will be printed on the screen
        static int windowHight = 30;
        static int windowWidth = 100;
        static char[,] gameField = new char[windowHight, windowWidth];
        static char[,] telerikBird = { { '\\', ' ', ' ', '/' }, { ' ', '\\', '/', ' ' }, { ' ', '/', '\\', ' ' }, { ' ', '\\', '/', ' ' } };
        static List<char> tunnelUp;
        static List<char> tunnelDown;


        static void Main(string[] args)
        {
            Console.BufferHeight = Console.WindowHeight = windowHight;
            Console.BufferWidth = Console.WindowWidth = windowWidth;



            while (true)
            {
                // Draw info
                PrintStringOnPosition(2, 0, "Lives: ", ConsoleColor.White);
                PrintStringOnPosition(10, 0, "Speed: ", ConsoleColor.White);
                PrintStringOnPosition(25, 0, "Acceleration: ", ConsoleColor.White);
                Console.WriteLine();

                StringBuilder printBird = new StringBuilder();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        printBird.Append(telerikBird[i, j]);
                    }
                    printBird.Append('\n');
                }
                Console.WriteLine(printBird);


                //TODO: read keybord input

                //TODO: move bird

                //TODO: move tunnel

                //TODO: check if the bird is dead or it took a bonus or someting else

                //TODO: print on the screen
                Thread.Sleep(100);
                Console.Clear();
            }
        }
    }
}