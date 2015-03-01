namespace FlappyTelerikBird
{
    using System;
    class Column
    {
        public const int WIDTH = 4; //the width of the char array of the column
        public static int HEIGHT = 50; //the height of the char array of the column
        public char[][] array = new char[HEIGHT][]; //this char array represents the column
        private static string solid = new string('█', WIDTH);
        private static string hole = new string(' ', WIDTH);

        private int coordX; // the X coordinate of the column field
        public int CoordX // the X coordinate of the column property
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

        private int coordY = 0; // the Y coordinate of the column field is allways 0
        public int CoordY // the Y coordinate of the column property
        {
            get
            {
                return this.coordY;
            }
        }

        public Column(int xCoordinate, int holeX, int holeSize)
        {
            for (int i = 0; i < holeX; i++)
            {
                array[i] = solid.ToCharArray();
            }
            for (int i = holeX; i < holeX + holeSize; i++)
            {
                array[i] = hole.ToCharArray();
            }
            for (int i = holeX + holeSize; i < HEIGHT; i++)
            {
                array[i] = solid.ToCharArray();
            }

            CoordX = xCoordinate;
        }
    }
}
