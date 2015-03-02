namespace FlappyTelerikBird
{
    using System;
    class Selector
    {
        public const int WIDTH = 1; //the width of the char array of the MainMenu
        public const int HEIGHT = 1; //the height of the char array of the MainMenu
        public char[][] array = new char[HEIGHT][]; //this char array represents the MainMenu

        private static string selection = "☻";

        private int coordX; // the X coordinate of the MainMenu field
        public int CoordX // the X coordinate of the bird property
        {
            get
            {
                return this.coordX;
            }
            set
            {
                if (value >= 0)
                {
                    this.coordX = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("CoordX cannot be set to negative!");
                }
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
                this.coordY = value;
            }
        }

        public Selector(int xCoordinate, int yCoordinate) //Selector constructor
        {
            array[0] = selection.ToCharArray();

            CoordX = xCoordinate;
            CoordY = yCoordinate;
        }
    }
}
