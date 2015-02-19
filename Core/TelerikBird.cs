using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class TelerikBird
    {
        class TelerikBird
        {
            public char[][] array = new char[4][]; //this char array represents the bird.
            public int coordX { get; set; } // the X coordinate of the bird.
            public int coordY { get; set; } // the Y coordinate of the bird.
            bool isFlap { get; set; } // this bool is used to make the bird to flap.

            public TelerikBird() //bird constructor
            {
                array[0] = @"\  /".ToCharArray();
                array[1] = @" \/ ".ToCharArray();
                array[2] = @" /\ ".ToCharArray();
                array[3] = @" \/ ".ToCharArray();
                isFlap = true;
                coordX = 2;
                coordY = Console.WindowHeight / 2;
            }

            public void Flap() //this method uses isFlap and makes the instance to flap
            {
                if (isFlap)
                {
                    array[0] = @"    ".ToCharArray();
                    array[1] = @"/\/\".ToCharArray();
                    isFlap = false;
                }
                else
                {
                    array[0] = @"\  /".ToCharArray();
                    array[1] = @" \/ ".ToCharArray();
                    isFlap = true;
                }
            }
        }
    }
}
