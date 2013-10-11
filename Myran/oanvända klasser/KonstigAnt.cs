using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Myran.Ants
{
    abstract class KonstigAnt : Node
    {
        protected Color color;
        protected int angle;
        protected int energy;
        protected int maxEnergy;

        public KonstigAnt(Board board, Random random, int maxEnergy)
            : base(board, 1, random)
        {
            this.color = this.ColorFromHue(random.Next(1536), 127);
            this.angle = random.Next(4);
            this.maxEnergy = maxEnergy;
            this.energy = maxEnergy;
        }

        public KonstigAnt(Board board, Random random, int color, int maxEnergy) : base(board, 1, random)
        {
            this.color = this.ColorFromHue(color, 127);
            this.angle = random.Next(4);
            this.maxEnergy = maxEnergy;
            this.energy = maxEnergy;
        }

        public override void OnChange(Board board, Random random, int x, int y)
        {
            int newX = x + ((angle % 2 == 0) ? angle - 1 : 0);
            int newY = y + ((angle % 2 == 1) ? angle - 2 : 0);

            if (/*om myran är i en myrbas med samma färg*/
                board.getSquare(newX, newY) is Square &&
                GetColorDifference(((Square)board.getSquare(newX, newY)).color) < 63)
            {
                if (random.Next(64) == 0)
                    angle = random.Next(4);
            }
            else /*om myran är utanför basen*/
            {
                angle = random.Next(4);
            }

            Node oldSquare = board.getSquare(newX, newY);

            if (/* om myran kan gå på denna ruta */IsWalkAble(oldSquare, random))
            {
                if (board.SetSquare(newX, newY, this)) //om position är giltig
                {
                    this.OnWalk(board, random, x, y, newX, newY, oldSquare);//this.OnWalk(newX, newY, oldSquare);//board.SetSquare(x, y, new Square(board, this.steps, random, this.pathColor));
                }
                else /*försäkrar liv på myran, fuskkodat*/
                {
                    board.SetSquare(x, y, this);
                }
            }
            else /*försäkrar liv på myran, fuskkodat*/
            {
                board.SetSquare(x, y, this);
            }
        }

        public override void Draw(Board board, int x, int y, ref Bitmap bitmap)
        {
            DrawSquare(board, x, y, ref bitmap, this.color);//Color.White);//bitmap.SetPixel(x * board.SquareSize, y * board.SquareSize, Color.White);
        }

        public int Energy
        {
            get { return this.energy; }
        }

        protected abstract void OnWalk(Board board, Random random, int oldX, int oldY, int newX, int newY, Node node);

        protected abstract bool IsWalkAble(Node node, Random random);

        protected int GetColorDifference(Color c)
        {
            int lightness = (c.R > c.G)?
                ((c.R > c.B)? c.R:c.B) :
                ((c.G > c.B)? c.G:c.B);

            int colorDifference = Math.Abs(this.color.R - c.R * 127 / lightness) +
                Math.Abs(this.color.G - c.G * 127 / lightness) +
                Math.Abs(this.color.B - c.B * 127 / lightness);

            return colorDifference * lightness / 256;
        }

        protected int GetHueDifference(Color c)
        {
            int colorDifference = Math.Abs(this.color.R - c.R) + Math.Abs(this.color.G - c.G) + Math.Abs(this.color.B - c.B);
            int lightness = (c.R > c.G) ?
                ((c.R > c.B) ? c.R : c.B) :
                ((c.G > c.B) ? c.G : c.B);

            return colorDifference * 256 / lightness;
        }


        //from 0 to 1535
        protected Color ColorFromHue(int hue, int brightness)
        {
            hue %= 1536; // 256*6 = 1536

            int red;
            int green;
            int blue;

            if (hue < 256) //       red to yellow
            {
                red = 255; green = hue; blue = 0;
            }
            else if (hue < 512) //  yellow to green
            {
                red = 511 - hue;
                green = 255;
                blue = 0;
            }
            else if (hue < 768) //  green to cyan
            {
                red = 0;
                green = 255;
                blue = hue - 512;
            }
            else if (hue < 1024) // cyan to blue
            {
                red = 0;
                green = 1023 - hue;
                blue = 255;
            }
            else if (hue < 1280) // blue to magenta
            {
                red = hue - 1024;
                green = 0;
                blue = 255;
            }
            else //if (hue < 1536) // magenta to red
            {
                red = 255;
                green = 0;
                blue = hue - 1280;
            }

            if (brightness == 127)
            {
                return Color.FromArgb(red, green, blue);
            }
            else
            {
                if (brightness > 127)
                {
                    red += (255 - red) * (brightness-127) / 127;
                    green += (255 - green) * (brightness-127) / 127;
                    blue += (255 - blue) * (brightness-127) / 127;
                }
                else
                {
                    red = red * brightness / 255;
                    green = green * brightness / 255;
                    blue = blue * brightness / 255;
                }

                if (red < 0) red = 0;
                else if (red > 255) red = 255;

                if (green < 0) green = 0;
                else if (green > 255) green = 255;

                if (blue < 0) blue = 0;
                else if (blue > 255) blue = 255;

                return Color.FromArgb(red, green, blue);
            }
        }

    }
}