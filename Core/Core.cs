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
        const int wallWidth = 5;
        const int wallHole = 15;

        static Random rnd = new Random();

        const int playerX = 5;
        static int playerY = Console.WindowHeight / 2;

        static int pastPlayerY = 0;
        static int velocity = 0;

        static int[,] walls = new int[4, 2]; //matrix for the wall; the first col keeps the Y position of the wall; the second col keeps something random between [10, 35)

        static void Main()
        {
            Console.WindowHeight = Console.BufferHeight = 50;
            Console.WindowWidth = Console.BufferWidth = 160;

            CreateWalls();

            System.Threading.Thread.Sleep(1000);

            while (true)
            {
                ReadPlayerKeys();

                MovePlayer();

                MoveWalls();

                DrawGame();

                CheckForCollisions();

                System.Threading.Thread.Sleep(40);
            }
        }

        private static void CreateWalls()
        {
            for (int i = 0; i < walls.GetLength(0); i++)
            {
                walls[i, 0] = 40 + i * 25;
                walls[i, 1] = 5 + rnd.Next(5, 30);
            }
        }

        private static void DrawGame()
        {
            DrawPoint(playerX, pastPlayerY, ' ', ConsoleColor.Black);
            try
            {
                DrawPoint(playerX, playerY, '*', ConsoleColor.Magenta);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.Clear();
                int leftOffSet = (Console.WindowWidth / 2-3);  // Sest the position of the cursor, so that the text "GAME OVER" is centered at the screen of the console. The text is colored red.
                int topOffSet = (Console.WindowHeight / 2-2);
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

            for (int i = 0; i < walls.GetLength(0); i++)
            {
                var wallXToDraw = walls[i, 0];
                var wallHoleToDraw = walls[i, 1];

                for (int height = 0; height < Console.WindowHeight; height++)
                {
                    if (height < wallHoleToDraw || wallHoleToDraw + wallHole < height)
                    {
                        DrawPoint(wallXToDraw, height, '-');
                        DrawPoint(wallXToDraw + 4, height, ' ', ConsoleColor.Black);
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

                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    velocity = 2;
                }
                if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    velocity = -2;
                }
            }
        }

        private static void MovePlayer()
        {
            pastPlayerY = playerY;
            playerY += (-1) * velocity;
        }

        private static void CheckForCollisions()
        {
            var firstWallX = walls[0, 0];
            var firstWallHole = walls[0, 1];

            if (firstWallX <= playerX)
            {
                if (playerY < firstWallHole || firstWallHole + wallHole < playerY)
                {
                    Console.Beep();
                    Console.Beep();
                    Console.Beep();
                    Console.Beep();
                    Console.Beep();
                    Console.Beep();
                    System.Threading.Thread.Sleep(3000);
                }
            }
        }

        private static void MoveWalls()
        {
            for (int row = 0; row < walls.GetLength(0); row++)
            {
                walls[row, 0]--;
                if (walls[row, 0] == 0)
                {
                    for (int i = 0; i < walls.GetLength(0) - 1; i++)
                    {
                        for (int col = 0; col < walls.GetLength(1); col++)
                        {
                            walls[i, col] = walls[i + 1, col];
                        }
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        for (int h = 0; h < Console.WindowHeight; h++)
                        {
                            DrawPoint(i, h, ' ', ConsoleColor.Black);
                        }
                    }

                    DrawPoint(playerX, playerY, '*', ConsoleColor.Magenta);

                    GenerateNewWall();
                }
            }
        }

        private static void GenerateNewWall()
        {
            var len = rnd.Next(5, 30);

            walls[walls.GetLength(0) - 1, 0] = Console.WindowWidth - wallWidth;
            walls[walls.GetLength(0) - 1, 1] = len;
        }

        static void DrawPoint(int x, int y, char symb, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            var pastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(symb);
            Console.ForegroundColor = pastColor;
        }
    }
}
