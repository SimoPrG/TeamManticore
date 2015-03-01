namespace FlappyTelerikBird
{
    using System;
    using System.IO;

    class HighScores
    {
        public static string highScoresFile = @"HighScores.txt";
        public static void PrintHighScores(string highScoresFile)
        {
            StreamReader reader = new StreamReader(highScoresFile);
            //using (StreamReader sr = new StreamReader(fileName))
            try
            {
                string line;
                // Read and display lines from the file until the end of  
                // the file is reached. 
                while ((line = reader.ReadLine()) != null)
                {
                    Console.SetCursorPosition(Core.DISPLAYWIDTH / 2, Core.DISPLAYHEIGHT / 2);
                    Console.WriteLine(line);
                }
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
