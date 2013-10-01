using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Myran
{
    public class Ant : Node
    {
        protected Color color;
        protected Color pathColor;
        int angle;

        public Ant(Board board, Random random) : base(board, 1, random)
        {
            this.pathColor = ColorFromHue(random.Next(1536));
            this.color = Color.FromArgb(pathColor.R + 64, pathColor.G + 64, pathColor.B + 64);
            this.angle = random.Next(4);
        }

        public Ant(Board board, Random random, int color) : base(board, 1, random)
        {
            this.pathColor = ColorFromHue(color);
            this.color = Color.FromArgb(pathColor.R + 64, pathColor.G + 64, pathColor.B + 64);
        }

        public Ant(Board board, Random random, Color color, int angle) : base(board, 1, random)
        {
            this.color = color;
            this.color = Color.FromArgb(pathColor.R + 64, pathColor.G + 64, pathColor.B + 64);
            this.angle = angle;
        }

        public override void OnChange(Board board, Random random, int x, int y)
        {
            int newX = x + ((angle % 2 == 0) ? angle - 1 : 0) + board.Columns;
            int newY = y + ((angle % 2 == 1) ? angle - 2 : 0) + board.Rows;

            newX %= board.Columns;
            newY %= board.Rows;

            if (board.getSquare(newX, newY) is Square &&
                GetColorDifference(((Square)board.getSquare(newX, newY)).color) < 64)
            {
                //angle = random.Next(4);

                //newX = x + ((angle % 2 == 0) ? angle - 1 : 0);
                //newY = y + ((angle % 2 == 1) ? angle - 2 : 0);
                if (random.Next(64) == 0)
                    angle = random.Next(4);
            }
            else
            {
                //if (random.Next(2) == 0)
                    angle = random.Next(4);
            }

            Node oldSquare = board.getSquare(newX, newY);

            if (oldSquare == null ||
                oldSquare is Square && (GetColorDifference(((Square)oldSquare).color) < 127 || random.Next(GetColorDifference(((Square)oldSquare).color)>>4) == 0))
            {
                if (board.SetSquare(newX, newY, this))
                {
                        board.SetSquare(x, y, new Square(board, this.steps, random, this.pathColor));
                }
                else
                {
                    board.SetSquare(x, y, this);
                }
            }
            else
            {
                board.SetSquare(x, y, this);
            }
        }

        public override void Draw(Board board, int x, int y, ref Bitmap bitmap)
        {
            DrawSquare(board, x, y, ref bitmap, this.color);//Color.White);//bitmap.SetPixel(x * board.SquareSize, y * board.SquareSize, Color.White);
        }

        private int GetColorDifference(Color c)
        {
            return Math.Abs(this.pathColor.R - c.R) + Math.Abs(this.pathColor.G - c.G) + Math.Abs(this.pathColor.B - c.B);
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
