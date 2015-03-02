namespace FlappyTelerikBird
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class ScoresHelper
    {
        public static string highScoresFile = @"HighScores.txt";
        public static int bestResultscount = 10;

        public static void PrintHighestScoresOnConsole()
        {
            var highScoresList = GetHighestScoresFromFile();

            //print
            int cursorTop = Core.DISPLAYHEIGHT / 3;
            for (int i = 0; i < highScoresList.Count; i++)
            {
                string currentScore = highScoresList[i][0];
                string currentPlayerName = highScoresList[i][1];
                string separator = " : ";
                cursorTop++;

                //print current score
                int cursorLeft = Core.DISPLAYWIDTH / 2 - Math.Min(Core.DISPLAYWIDTH / 2, currentScore.Length);
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.Write(currentScore + separator);

                //print current player name
                cursorLeft = Core.DISPLAYWIDTH / 2 + separator.Length;
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.Write(currentPlayerName);
            }
        }

        private static List<List<string>> GetHighestScoresFromFile()
        {
            var highScoresList = new List<List<string>>();
            try
            {
                using (StreamReader reader = new StreamReader(highScoresFile))
                {
                    string line;
                    // Read lines from the file until the end of the file is reached. 
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] scoreLine = line.Split(':');
                        highScoresList.Add(new List<string> { scoreLine[0], scoreLine[1] });
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.Clear();
                Console.SetCursorPosition(Core.DISPLAYWIDTH / 3, Core.DISPLAYHEIGHT / 5);
                Console.WriteLine("There are no High Scores recorded!");
            }
            catch (FileNotFoundException)
            {
                Console.Clear();
                Console.SetCursorPosition(Core.DISPLAYWIDTH / 3, Core.DISPLAYHEIGHT / 5);
                Console.WriteLine("There are no High Scores recorded!");
            }
            return highScoresList;
        }

        public static void PrintPlayerResultOnConsole(long score)
        {
            string displayScore = "Your score is: " + score;

            Console.SetCursorPosition(Core.DISPLAYWIDTH / 2 - displayScore.Length / 2, Core.DISPLAYHEIGHT / 2);
            Console.WriteLine(displayScore);
            if (score < 1000)
            {
                string resultMessage = "Your score is less than 1000!";
                Console.SetCursorPosition(Core.DISPLAYWIDTH / 2 - resultMessage.Length / 2, Core.DISPLAYHEIGHT / 2 + 1);
                Console.Write(resultMessage);

                string resultWarning = "If you are missing a wing please contact game admins to the rescue!!!";
                Console.SetCursorPosition(Core.DISPLAYWIDTH / 2 - resultWarning.Length / 2, Core.DISPLAYHEIGHT / 2 + 2);
                Console.Write(resultWarning);
            }
            Console.ReadKey();
        }

        // save result on the proper place - highest result should be saved on highest position; only the first 10 playses are saved
        public static void SavePlayerRsultInFile(long score)
        {
            string askForName = "Enter your name: ";
            Console.SetCursorPosition(Core.DISPLAYWIDTH / 2 - askForName.Length / 2, Core.DISPLAYHEIGHT / 2 + 3);
            Console.Write(askForName);

            Console.SetCursorPosition(Core.DISPLAYWIDTH / 2 - askForName.Length / 2, Core.DISPLAYHEIGHT / 2 + 4);
            string name = Console.ReadLine();


            var highScoresList = GetHighestScoresFromFile();
            //order by highest score
            highScoresList.Add(new List<string> { score.ToString(), name });
            var highScoresListNew = highScoresList.OrderByDescending(e => int.Parse(e[0])).ToList();
            int countResults = highScoresListNew.Count;
            while (countResults > bestResultscount)
            {
                highScoresListNew.RemoveAt(countResults - 1);
                countResults--;
            }

            //clear file
            File.WriteAllText(ScoresHelper.highScoresFile, string.Empty);

            //set new highest scores in file
            using (StreamWriter writer = new StreamWriter(ScoresHelper.highScoresFile, true))
            {
                foreach (var item in highScoresListNew)
                {
                    writer.WriteLine("{0}:{1}", item[0], item[1]);
                }
            }
        }
    }
}
