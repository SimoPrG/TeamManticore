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
using System.Threading.Tasks;

namespace Core
{
    class Core
    {
        static char[,] gameField; //the array which keeps the symbols which will be printed on the screen
        static List<char> tunnelUp;
        static List<char> tunnelDown;


        static void Main(string[] args)
        {
            while (true)
            {
                //TODO: read keybord input

                //TODO: move bird

                //TODO: move tunnel

                //TODO: check if the bird is dead or it took a bonus or someting else

                //TODO: print on the screen
            }
        }
    }
}
