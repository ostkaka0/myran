using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myran.Ants
{
    class Queen : KonstigAnt
    {
        protected int x, y;

        public Queen(Board board, Random random, int color, int x, int y)
            : base(board, random, color, 1000)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return this.x; }
        }

        public int Y
        {
            get { return this.y; }
        }

        protected override bool IsWalkAble(Myran.Node node, System.Random random)
        {
            throw new NotImplementedException();
        }

        protected override void OnWalk(Board board, Random random, int oldX, int oldY, int newX, int newY, Node node)
        {
            throw new NotImplementedException();
        }
    }
}
