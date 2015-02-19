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
            public char[][] bird = new char[4][];
            public int birdX { get; set; }
            public int birdY { get; set; }
            bool isFlap { get; set; }

            public TelerikBird()
            {
                bird[0] = @"\  /".ToCharArray();
                bird[1] = @" \/ ".ToCharArray();
                bird[2] = @" /\ ".ToCharArray();
                bird[3] = @" \/ ".ToCharArray();
                isFlap = true;
                birdX = 2;
                birdY = Console.WindowHeight / 2;
            }

            public void Flap()
            {
                if (isFlap)
                {
                    bird[0] = @"    ".ToCharArray();
                    bird[1] = @"/\/\".ToCharArray();
                    isFlap = false;
                }
                else
                {
                    bird[0] = @"\  /".ToCharArray();
                    bird[1] = @" \/ ".ToCharArray();
                    isFlap = true;
                }
            }
        }
    }
}
