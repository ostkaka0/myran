using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Myran
{
    public abstract class Node
    {
        protected int steps;

        public Node(Board board, int steps, Random random)
        {
            this.steps = steps;
        }

        public abstract void OnChange(Board board, Random random, int x, int y);

        public abstract void Draw(Board board, int x, int y, ref Bitmap bitmap);

        protected void DrawSquare(Board board, int x, int y, ref Bitmap bitmap, Color color)
        {
            if (board.SquareSize == 1)
            {
                bitmap.SetPixel(x, y, color);
            }
            else
            {
                for (int yy = 0; yy < board.SquareSize; yy++)
                {
                    for (int xx = 0; xx < board.SquareSize; xx++)
                    {
                        bitmap.SetPixel(x * board.SquareSize + xx, y * board.SquareSize + yy, color);
                    }
                }
            }
        }
    }
}
