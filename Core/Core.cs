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

using System.IO;


namespace FlappyManticore
{
    class Core
    {
        static int score = 0;
        static int currentSelection = 0;
        private static string[] mainMenu = { "Play", "High Scores", "Exit" };
        static string selector = "> ";
        private static void PrintMenu(string[] menu)
        {

            for (int i = 0; i < menu.GetLength(0); i++)
            {
                if (currentSelection == i)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - menu[i].Length / 2 - selector.Length, Console.WindowHeight / 2 + 2 * i);
                    Console.Write(selector + menu[i]);
                }
                else
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - menu[i].Length / 2, Console.WindowHeight / 2 + 2 * i);
                    Console.Write(menu[i]);
                }
            }
        }
        static void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    currentSelection--;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    currentSelection++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    CheckUserChoice();
                }

                if (currentSelection < 0)
                {
                    currentSelection = mainMenu.GetLength(0) - 1;
                }
                else if (currentSelection > mainMenu.GetLength(0) - 1)
                {
                    currentSelection = 0;
                }
            }
        }
        static void CheckUserChoice()
        {

            switch (currentSelection)
            {
                case 0:
                    Console.Clear();
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
                    break;

                case 1:
                    Console.Clear();
                    Console.WriteLine("You selected High Scores!");

                    string fileName = @"..\..\..\test.txt";
                    StreamReader reader = new StreamReader(fileName);
                    //using (StreamReader sr = new StreamReader(fileName))
                    using(reader)
                    {
                        string line;
                        // Read and display lines from the file until the end of  
                        // the file is reached. 
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                        reader.Close();
                    }
                    
                    System.Threading.Thread.Sleep(99999);
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("You exited the game!");
                    System.Threading.Thread.Sleep(99999);
                    break;
            }
        }








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
            Console.WindowWidth = Console.BufferWidth = 120;


            while (true)
            {
                PrintMenu(mainMenu);
                HandleInput();
                Thread.Sleep(150);
                Console.Clear();
            }




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
                Console.WriteLine();
                WriteInFile(score);
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
                Console.WriteLine();
                WriteInFile(score);
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

                if (pressedKey.Key == ConsoleKey.Spacebar)
                {
                    velocity = 4;
                }
            }
        }

        private static void MovePlayer()
        {
            pastPlayerY = playerY;
            score += 5;//score
            playerY += (-1) * velocity;
            velocity--;
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

        static void WriteInFile(int score)
        {
            string scoreS = score.ToString();
            Console.WriteLine();
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            StreamWriter writer = new StreamWriter(@"..\..\..\test.txt", true);
            //using (writer)
            //{
                writer.WriteLine("{0}:{1}", score, name);
                writer.Close();
            //}
            Console.WriteLine("File is written!");
            //StreamReader 
            //St
            //var order = writer.Select(s => new { Str = s, Split = s.Split(':') })
            //.OrderByDescending(x => int.Parse(x.Split[0]))
            //.ThenBy(x => x.Split[1])
            //.Select(x => x.Str)
            //.ToList();
        }
    }
}