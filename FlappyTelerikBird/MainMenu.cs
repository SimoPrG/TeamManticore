namespace FlappyTelerikBird
{
    using System;
    class MainMenu
    {
        public const int WIDTH = 11; //the width of the char array of the MainMenu
        public const int HEIGHT = 3; //the height of the char array of the MainMenu
        public char[][] array = new char[HEIGHT][]; //this char array represents the MainMenu

        private static string play = "   PLAY    ";
        private static string highScores = "HIGH SCORES";
        private static string exit = "   EXIT    ";

        private int coordX; // the X coordinate of the MainMenu field
        public int CoordX // the X coordinate of the bird property
        {
            get
            {
                return this.coordX;
            }
            set
            {
                this.coordX = value;
            }
        }

        private int coordY; // the Y coordinate of the bird field
        public int CoordY // the Y coordinate of the bird property
        {
            get
            {
                return this.coordY;
            }
            set
            {
                if (value >= 0)
                {
                    this.coordY = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("CoordY cannot be set to negative!");
                }
            }
        }

        public MainMenu(int xCoordinate, int yCoordinate) //MainMenu constructor
        {
            array[0] = play.ToCharArray();
            array[1] = highScores.ToCharArray();
            array[2] = exit.ToCharArray();

            CoordX = xCoordinate;
            CoordY = yCoordinate;
        }
    }
}
