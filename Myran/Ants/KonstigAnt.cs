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
        int angle;
        int energy;
        readonly int maxEnergy;


        public KonstigAnt(Board board, Random random) : base(board, 1, random)
        {
            this.color = this.ColorFromHue(random.Next(1536));
            this.angle = random.Next(4);
            this.energy = maxEnergy;
        }

        public KonstigAnt(Board board, Random random, int color) : base(board, 1, random)
        {
            this.color = this.ColorFromHue(color);
            this.angle = random.Next(4);
        }

        public override void OnChange(Board board, Random random, int x, int y)
        {
            int newX = x + ((angle % 2 == 0) ? angle - 1 : 0);
            int newY = y + ((angle % 2 == 1) ? angle - 2 : 0);

            if (/*om myran är i en myrbas med samma färg*/false)
            {
                if (random.Next(64) == 0)
                    angle = random.Next(4);
            }
            else /*om myran är utanför basen*/
            {
                angle = random.Next(4);
            }

            Node oldSquare = board.getSquare(newX, newY);

            if (oldSquare == null || /* om myran kan gå på denna ruta */
                oldSquare is Square && (GetColorDifference(((Square)oldSquare).color) < 127 || random.Next(32) == 0))
            {
                if (board.SetSquare(newX, newY, this)) //om position är giltig
                {
                    this.OnWalk();//board.SetSquare(x, y, new Square(board, this.steps, random, this.pathColor));
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

        protected abstract void OnWalk();

        protected abstract bool IsWalkAble();

        private int GetColorDifference(Color c)
        {
            int colorDifference = Math.Abs(this.color.R - c.R) + Math.Abs(this.color.G - c.G) + Math.Abs(this.color.B - c.B);
            int lightness = (c.R > c.G)?
                ((c.R > c.B)? c.R:c.B) :
                ((c.G > c.B)? c.G:c.B);

            return colorDifference * lightness / 256;
        }

        private int GetHueDifference(Color c)
        {
            int colorDifference = Math.Abs(this.color.R - c.R) + Math.Abs(this.color.G - c.G) + Math.Abs(this.color.B - c.B);
            int lightness = (c.R > c.G) ?
                ((c.R > c.B) ? c.R : c.B) :
                ((c.G > c.B) ? c.G : c.B);

            return colorDifference * 256 / lightness;
        }


        //from 0 to 1535
        private Color ColorFromHue(int hue)
        {
            hue %= 1536; // 256*6 = 1536

            Color color;

            if (hue < 256) //       red to yellow
            {
                color = Color.FromArgb(255, hue, 0);
            }
            else if (hue < 512) //  yellow to green
            {
                color = Color.FromArgb(511 - hue, 255, 0);
            }
            else if (hue < 768) //  green to cyan
            {
                color = Color.FromArgb(0, 255, hue - 512);
            }
            else if (hue < 1024) // cyan to blue
            {
                color = Color.FromArgb(0, 1023 - hue, 255);
            }
            else if (hue < 1280) // blue to magenta
            {
                color = Color.FromArgb(hue - 1024, 0, 255);
            }
            else //if (hue < 1536) // magenta to red
            {
                color = Color.FromArgb(255, 0, hue - 1280);
            }

            color = Color.FromArgb(color.R >> 1, color.G >> 1, color.B >> 1);

            return color;
        }

    }
}