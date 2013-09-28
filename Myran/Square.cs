using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Myran
{
    public class Square : Node
    {
        protected bool changed = true;
        public Color color;

        public Square(Board board, int steps, Random random, Color color)
            : base(board, steps, random)
        {
            this.color = color;
        }

        public override void OnChange(Board board, Random random, int x, int y)
        {
            changed = true;
        } 

        public override void Draw(int x, int y, ref Bitmap bitmap)
        {
            if (changed)
            {
                changed = false;

                bitmap.SetPixel(x, y, color); 
            }
        }
    }
}
