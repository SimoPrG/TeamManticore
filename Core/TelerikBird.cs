using System;

namespace FlappyManticore
{
    public class TelerikBird
    {
        public const int WIDTH = 4; //the width of the char array of the bird
        public const int HEIGHT = 4; //the height of the char array of the bird
        public char[][] array = new char[4][]; //this char array represents the bird.
        private string wingsTopStreight    = @"\  /";
        private string wingsBottomStreight = @" \/ ";
        private string wingsTopFlap        = @"    ";
        private string wingsBottomFlap     = @"/\/\";
        private string bodyTop             = @" /\ ";
        private string bodyBottom          = @" \/ ";

        private int coordX;
        public int CoordX // the X coordinate of the bird.
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

        private int coordY;
        public int CoordY // the Y coordinate of the bird
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

        public TelerikBird(int xCoordinate, int yCoordinate) //bird constructor
        {
            array[0] = wingsTopStreight.ToCharArray();
            array[1] = wingsBottomStreight.ToCharArray();
            array[2] = bodyTop.ToCharArray();
            array[3] = bodyBottom.ToCharArray();
            isFlap = true;
            CoordX = xCoordinate;
            CoordY = yCoordinate;
        }

        private bool isFlap; // this bool is used to make the bird to flap.
        public void Flap() //this method uses isFlap and makes the instance to flap
        {
            if (isFlap)
            {
                array[0] = wingsTopFlap.ToCharArray();
                array[1] = wingsBottomFlap.ToCharArray();
                isFlap = false;
            }
            else
            {
                array[0] = wingsTopStreight.ToCharArray();
                array[1] = wingsBottomStreight.ToCharArray();
                isFlap = true;
            }
        }
    }
}
