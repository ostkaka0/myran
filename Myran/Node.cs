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

        public abstract void Draw(int x, int y, ref Bitmap bitmap);
    }
}
