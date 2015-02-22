/*General Requirements
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

namespace FlappyManticore
{
    class Core
    {
        static Random rnd = new Random();

        const int playerX = 5;
        static int playerY = Console.WindowHeight / 2;
        static int pastPlayerY = 0;

        static int velocity = 0;

        static int numberOfWals = 4;
        const int wallWidth = 5;
        static int[,] wallsYAxis = new int[numberOfWals, 2];
        static int[] wallsXAxis = new int[numberOfWals];

        static char[,] gameField;

        static void Main()
        {
            Console.WindowHeight = Console.BufferHeight = 50;
            Console.WindowWidth = Console.BufferWidth = 120;
            int gameHighth = Console.WindowHeight;
            int gameWidth = Console.WindowWidth - 1;

            gameField = new char[gameHighth, gameWidth];

            CreateWalls();

            //System.Threading.Thread.Sleep(1000);

            while (true)
            {
                ReadPlayerKeys();

                //MovePlayer();

                //MoveWalls();

                //CheckForCollisions();

                DrawGame();

                System.Threading.Thread.Sleep(200);
                Console.Clear();
            }
        }

        private static void CreateWalls()
        {
            //generate pairs of Y coordinates for a pair of walls
            for (int i = 0; i < numberOfWals; i++)
            {
                int upperWallYAxis = rnd.Next(0, Console.WindowHeight - 6/* TODO: use constant bird hight*/); 
                int bottomWallYAxis = rnd.Next(upperWallYAxis + 6, Console.WindowHeight);

                wallsYAxis[i, 0] = upperWallYAxis;
                wallsYAxis[i, 1] = bottomWallYAxis;
            }

            //generate the starting x positions for the wall pairs 
            int currenXSector = 0;
            for (int i = 0; i < numberOfWals; i++)
            {
                int startingXPosiotion = rnd.Next(currenXSector, currenXSector + Console.WindowWidth / numberOfWals);
                wallsXAxis[i] = startingXPosiotion;
                currenXSector += Console.WindowWidth / numberOfWals;
            }
        }

        private static void DrawGame()
        {
            StringBuilder builder = new StringBuilder();

            //DrawPoint(playerX, pastPlayerY, ' ', ConsoleColor.Black);
            try
            {
                DrawPoint(playerX, playerY, '*', ConsoleColor.Magenta);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.Clear();
                int leftOffSet = (Console.WindowWidth / 2 - 3);  // Sest the position of the cursor, so that the text "GAME OVER" is centered at the screen of the console. The text is colored red.
                int topOffSet = (Console.WindowHeight / 2 - 2);
                Console.SetCursorPosition(leftOffSet, topOffSet);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("GAME OVER");
                System.Threading.Thread.Sleep(10000000);
            }

            if (playerY > Console.WindowHeight)
            {
                Console.Clear();
                int leftOffSet = (Console.WindowWidth / 2 - 3); // Set the position of the cursor, so that the text "GAME OVER" is centered at the screen of the console. The text is colored red.
                int topOffSet = (Console.WindowHeight / 2 - 2);
                Console.SetCursorPosition(leftOffSet, topOffSet);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("GAME OVER");
                System.Threading.Thread.Sleep(10000000);
            }

            //put the walls on the gamefield
            for (int i = 0; i < wallsXAxis.Length; i++)
            {
                //Left bottom point of upper wall
                int yUpper = wallsYAxis[i, 0];
                //Left upper point of bottom wall
                int yBottom = wallsYAxis[i, 1];

                int xWall = wallsXAxis[i];
                char wallSymbol = 'U';

                DrawWallOnGameField("upper", wallSymbol, yUpper, xWall);
                DrawWallOnGameField("bottom", wallSymbol, yBottom, xWall);
            }

            //fill the builder
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    builder.Append(gameField[i, j]);
                }
                builder.AppendLine();
            }

            Console.Write(builder);
            builder.Clear();
        }

        private static void DrawWallOnGameField(string upperBottom, char fillingSighn, int startPointRow, int startPointCol)
        {
            //do not try to draw column outside the right end of the gamefield
            int maxWidth = Math.Min((startPointCol + wallWidth), gameField.GetLength(1));

            //drow upper wall
            if (upperBottom == "upper")
            {
                for (int i = 0; i <= startPointRow; i++)
                {
                    for (int j = startPointCol; j < maxWidth; j++)
                    {
                        gameField[i, j] = fillingSighn;
                    }
                }
            }
            else //bottom
            {
                for (int i = startPointRow; i < gameField.GetLength(0); i++)
                {
                    for (int j = startPointCol; j < maxWidth; j++)
                    {
                        gameField[i, j] = fillingSighn;
                    }
                }
            }
        }

        private static void ReadPlayerKeys()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);//
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);//
                }

                if (pressedKey.Key == ConsoleKey.Spacebar)
                {
                    velocity = 4;
                }
            }
        }

        private static void MovePlayer()
        {
            pastPlayerY = playerY;
            playerY += (-1) * velocity;
            velocity--;
        }

        private static void MoveWalls()
        {
            throw new NotImplementedException();
        }

        private static void GenerateNewWall()
        {
            throw new NotImplementedException();
        }
    }
}
