/*General Requirements
Please define and implement the following assets in your project:
 At least 1 multi-dimensional array
 At least 3 one-dimensional arrays
 At least 10 methods (separating the application’s logic)
 At least 3 existing .NET classes (like System.Math or System.DateTime)
 At least 2 exception handlings
 At least 1 use of external text file
*/

namespace FlappyTelerikBird
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.IO;
    class Core
    {
        public const int DISPLAYHEIGHT = 50;
        public const int DISPLAYWIDTH = 120;

        private static string refreshedDisplay = new string(' ', DISPLAYHEIGHT * DISPLAYWIDTH - 1);
        private static StringBuilder display = new StringBuilder(refreshedDisplay);

        private static TelerikBird bird = new TelerikBird(DISPLAYWIDTH / 12, (DISPLAYHEIGHT - TelerikBird.HEIGHT) / 2 - 1);

        static void Main()
        {
            Console.Title = "Flappy Telerik Bird";
            Console.CursorVisible = false;

            Console.WindowHeight = Console.BufferHeight = DISPLAYHEIGHT;
            Console.WindowWidth = Console.BufferWidth = DISPLAYWIDTH;

            int choice = 0; // this is used for the selection of the gamer in the menu

            while (true)
            {
                bird.Resurrect();

                choice = PrintMainMenu(choice);

                if (choice == 0) // Play
                {
                    long score = Play();
                    ScoresHelper.PrintPlayerResultOnConsole(score);
                    ScoresHelper.SavePlayerRsultInFile(score);
                }
                else if (choice == 1) // High Scores
                {
                    Console.Clear();
                    List<List<string>> highScoresList = new List<List<string>>();
                    ScoresHelper.PrintHighestScoresOnConsole();

                    while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                }
                else if (choice == 2) 
                {
                    Environment.Exit(0);
                }
            }
        }

        private static long Play()
        {
            int difficulty = 30; // represents the distance between the columns
            int columnTimer = 0; // this timer decreeses at each iteration. at zero it is set to difficulty and a new column apears
            long score = 0;
            List<Column> columns = new List<Column>();
            Random generator = new Random();

            while (true)
            {
                Console.Clear();

                display.Clear(); // with this two lines we refresh the display StringBuilder
                display.Append(refreshedDisplay);

                //generate new column if needed
                if (--columnTimer <= 0)
                {
                    columnTimer = difficulty;
                    Column.generateRandomColumn(generator, columns);
                }

                //move all columns left
                for (int i = 0; i < columns.Count; i++)
                {
                    if (columns[i].CoordX <= 0)
                    {
                        columns.RemoveAt(i);
                        score += 100;
                    }
                    columns[i].CoordX--;
                    WriteObjectInDisplay(columns[i].array, columns[i].Hight, columns[i].Width, columns[i].CoordX, columns[i].CoordY);
                }

                score++;
                char[][] scored = new char[1][];
                scored[0] = string.Format("Score: {0}", score).ToCharArray();
                WriteObjectInDisplay(scored, 1, scored[0].Length, 0, DISPLAYHEIGHT - 1);

                //controll if the bird is squished save score and return to main menu
                bird.Flap();
                WriteBirdInDisplay();
                if (bird.IsAlive == false)
                {
                    return score; 
                }
                Console.Write(display);

                //process player input
                if (Console.KeyAvailable) // if the gamer is pressing a key
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.UpArrow) //the bird goes up
                    {
                        bird.CoordY--;
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow) // the bird goes down
                    {
                        bird.CoordY++;
                    }
                    if (pressedKey.Key == ConsoleKey.P) // pause
                    {
                        while (Console.ReadKey(true).Key != ConsoleKey.P) ;

                    }
                }
                while (Console.KeyAvailable) Console.ReadKey(true); // free the keyboard buffer

                Thread.Sleep(100);
            }
        }

        private static int PrintMainMenu(int currentChoice)
        {
            MainMenu mainMenu = new MainMenu((DISPLAYWIDTH - MainMenu.WIDTH) / 2, (DISPLAYHEIGHT - MainMenu.HEIGHT) / 2);
            Selector selector = new Selector((DISPLAYWIDTH - MainMenu.WIDTH) / 2 - Selector.WIDTH - 1,
                (DISPLAYHEIGHT - MainMenu.HEIGHT) / 2);

            ConsoleKeyInfo pressedKey;

            while (true)
            {
                Console.Clear();

                display.Clear(); // with this two lines we refresh the display StringBuilder
                display.Append(refreshedDisplay);

                selector.CoordY = (DISPLAYHEIGHT - MainMenu.HEIGHT) / 2 + currentChoice;

                WriteObjectInDisplay(bird.array, TelerikBird.HEIGHT, TelerikBird.WIDTH, bird.CoordX, bird.CoordY);
                WriteObjectInDisplay(mainMenu.array, MainMenu.HEIGHT, MainMenu.WIDTH, mainMenu.CoordX, mainMenu.CoordY);
                WriteObjectInDisplay(selector.array, Selector.HEIGHT, Selector.WIDTH, selector.CoordX, selector.CoordY);

                Console.Write(display);

                pressedKey = Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    currentChoice = (currentChoice - 1 + MainMenu.HEIGHT) % MainMenu.HEIGHT;
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    currentChoice = (currentChoice + 1 + MainMenu.HEIGHT) % MainMenu.HEIGHT;
                }
                else if (pressedKey.Key == ConsoleKey.Enter)
                {
                    return currentChoice;
                }
            }
        }

        //This method puts the object char array representation in the StringBuilder display
        // matrix - the char array of the object
        // HEIGHT and WIDTH - the size of the char array
        // coordX and coordY - the coordinates of the object
        private static void WriteObjectInDisplay(char[][] matrix, int HEIGHT, int WIDTH, int coordX, int coordY)
        {
            for (int row = 0; row < HEIGHT; row++)
            {
                for (int col = 0; col < WIDTH; col++)
                {
                    display[(row + coordY) * DISPLAYWIDTH + (col + coordX)] = matrix[row][col];
                }
            }
        }

        private static void WriteBirdInDisplay() // This method writes the current state of the bird in display StringBuilder
        {
            for (int row = 0; row < TelerikBird.HEIGHT; row++)
            {
                for (int col = 0; col < TelerikBird.WIDTH; col++)
                {
                    if (bird.array[row][col] != ' ') // if we have ' ' in the matrix we must not print it
                    {
                        if (IsBirdSmashed(row, col)) // the bird crashes
                        {
                            bird.Kill();
                        }
                        else // the bird didn't crash
                        {
                            display[(row + bird.CoordY) * DISPLAYWIDTH + (col + bird.CoordX)] = bird.array[row][col];
                        }
                    }
                }
            }
        }

        // This method checks if some part of the bird has hit something
        private static bool IsBirdSmashed(int row, int col)
        {
            if (bird.CoordX >= 0 && bird.CoordX <= (DISPLAYWIDTH - TelerikBird.WIDTH) &&
                bird.CoordY >= 0 && bird.CoordY <= (DISPLAYHEIGHT - TelerikBird.HEIGHT) &&
                display[(row + bird.CoordY) * DISPLAYWIDTH + (col + bird.CoordX)] == ' ')
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
