namespace FlappyTelerikBird
{
    using System;
    class TelerikBird
    {
        public const int WIDTH = 4; //the width of the char array of the bird
        public const int HEIGHT = 4; //the height of the char array of the bird
        public char[][] array = new char[HEIGHT][]; //this char array represents the bird.
        private static string wingsTopStreight =    @"\  /";
        private static string wingsBottomStreight = @" \/ ";
        private static string wingsTopFlap =        @"    ";
        private static string wingsBottomFlap =     @"/\/\";
        private static string bodyTop =             @" /\ ";
        private static string bodyBottom =          @" \/ ";

        private int coordX; // the X coordinate of the bird field
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
                this.coordY = value;
            }
        }

        public bool IsAlive { get; set; }

        public TelerikBird(int xCoordinate, int yCoordinate) //bird constructor
        {
            array[0] = wingsTopFlap.ToCharArray();
            array[1] = wingsBottomFlap.ToCharArray();
            array[2] = bodyTop.ToCharArray();
            array[3] = bodyBottom.ToCharArray();
            isFlap = true;
            CoordX = xCoordinate;
            CoordY = yCoordinate;
            IsAlive = true;
        }

        private bool isFlap; // this bool is used to make the bird to flap.
        public void Flap() //this method uses isFlap and makes the instance to flap
        {
            if (isFlap)
            {
                array[0] = wingsTopStreight.ToCharArray();
                array[1] = wingsBottomStreight.ToCharArray();
                isFlap = false;
            }
            else
            {
                array[0] = wingsTopFlap.ToCharArray();
                array[1] = wingsBottomFlap.ToCharArray();
                isFlap = true;
            }
        }

        public void Kill()
        {
            IsAlive = false;
        }

        public void Resurrect()
        {
            CoordX = Core.DISPLAYWIDTH / 12;
            CoordY = (Core.DISPLAYHEIGHT - HEIGHT) / 2 - 1;
            IsAlive = true;
            if (!isFlap)
            {
                Flap();
            }
        }
    }
}
