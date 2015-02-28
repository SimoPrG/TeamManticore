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
                    CreateWalls();

                    //System.Threading.Thread.Sleep(1000);

                    while (true)
                    {
                        ReadPlayerKeys();

                        MovePlayer();

                        //MoveWalls();

                        //CheckForCollisions();

                        DrawGame();

                        System.Threading.Thread.Sleep(200);
                        Console.Clear();
                    }

                    break;

                case 1:
                    Console.Clear();
                    Console.WriteLine("You selected High Scores!");


                    try
                    {
                        string fileName = @"..\..\..\test.txt";
                        StreamReader reader = new StreamReader(fileName);
                        //using (StreamReader sr = new StreamReader(fileName))
                        using (reader)
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

                        Console.ReadKey();
                        break;
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("There are no High Scores recorded!");
                        System.Threading.Thread.Sleep(99999);
                    }
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("You exited the game!");
                    System.Threading.Thread.Sleep(99999);
                    break;
            }
        }







        static Random rnd = new Random();

        const int playerX = 5;
        static int playerY = Console.WindowHeight / 2;
        static int pastPlayerY = 0;

        static int velocity = 0;

        static int numberOfWals = 4;
        static int wallWidth = 5;
        static int[,] wallsYAxis = new int[numberOfWals, 2];
        static int[] wallsXAxis = new int[numberOfWals];

        static char[,] gameField;

        static void Main()
        {
            Console.WindowHeight = 50;
            Console.BufferHeight = 50;
            Console.WindowWidth = Console.BufferWidth = 120;

            int gameHighth = Console.WindowHeight - 1;
            int gameWidth = Console.WindowWidth - 1;

            gameField = new char[gameHighth, gameWidth];

            while (true)
            {
                PrintMenu(mainMenu);
                HandleInput();
                Thread.Sleep(150);
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
            score += 5;//score
            playerY += (-1) * velocity;
        }

        private static void MoveWalls()
        {
            throw new NotImplementedException();
        }

        private static void GenerateNewWall()
        {
            throw new NotImplementedException();
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
