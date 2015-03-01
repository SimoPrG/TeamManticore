namespace FlappyTelerikBird
{
    using System;
    using System.Collections.Generic;
    class Column
    {
        public char[][] array; //this char array represents the column
        private static char solid = '█';
        private static char hole = ' ';

        private int width; //the width of the char array of the column
        public int Width
        {
            get
            {
                return this.width;
            }
        }
        private int hight; //the height of the char array of the column
        public int Hight
        {
            get
            {
                return this.hight;
            }
        }
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

        public Column(int xCoordinate, int holeX, int holeSize, int width, int hight)
        {

            this.width = width;
            this.hight = hight;
            char[][] array = new char[hight][];

            for (int i = 0; i < holeX; i++)
            {
                array[i] = new String(solid, width).ToCharArray();
            }
            for (int i = holeX; i < holeX + holeSize; i++)
            {
                array[i] = new String(hole, width).ToCharArray();
            }
            for (int i = holeX + holeSize; i < hight; i++)
            {
                array[i] = new String(solid, width).ToCharArray();
            }

            CoordX = xCoordinate;

            this.array = array;

        }

        public static void generateRandomColumn(Random generator, List<Column> columns)
        {
            //generate the wall in such a way that the bird can have a chance to go trough the hole in it
            int holeXCoord = generator.Next(0, Core.DISPLAYHEIGHT - (TelerikBird.HEIGHT + 5));
            int holeSize = generator.Next(TelerikBird.HEIGHT + 5, Core.DISPLAYHEIGHT - holeXCoord);
            int columnWidth = Core.DISPLAYWIDTH / 20;
            columns.Add(new Column(Core.DISPLAYWIDTH - columnWidth, holeXCoord, holeSize, columnWidth, Core.DISPLAYHEIGHT));
        }
    }
}
