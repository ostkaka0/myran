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

        public Ant(Board board, Random random) : base(board, 1, random)
        {
            color = Color.FromArgb(random.Next());
        }

        public override void OnChange(Board board, Random random, int x, int y)
        {
            int angle = random.Next(4);

            int newX = x + ((angle % 2 == 0) ? angle - 1 : 0);
            int newY = y + ((angle % 2 == 1) ? angle - 2 : 0);

            Node oldSquare = board.getSquare(newX, newY);

            if (oldSquare == null ||
                oldSquare is Square && getColorDifference(((Square)oldSquare).color) < 255)
            {
                if (board.SetSquare(newX, newY, this))
                {
                    board.SetSquare(x, y, new Square(board, this.steps, random, this.color));
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

        public override void Draw(int x, int y, ref Bitmap bitmap)
        {
            bitmap.SetPixel(x, y, Color.White);
        }

        private int getColorDifference(Color c)
        {
            return Math.Abs(this.color.R - c.R) + Math.Abs(this.color.G - c.G) + Math.Abs(this.color.B - c.B);
        }
    }
}
