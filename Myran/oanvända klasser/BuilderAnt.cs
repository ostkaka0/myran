using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myran.Ants
{
    class BuilderAnt : Ant_
    {
        public BuilderAnt(Board board, Random random, int color, Queen queen)
            : base(board, random, color, 20, queen)
        {

        }

        protected override bool IsWalkAble(Node node, Random random)
        {
            return (node == null ||
                node is Square &&
                (GetColorDifference(((Square)node).color) < 32 ||
                random.Next(GetColorDifference(((Square)node).color) >> 4) == 0));
        }

        protected override void OnWalk(Board board, Random random, int oldX, int oldY, int newX, int newY, Node node)
        {
            board.SetSquare(oldX, oldY, new Square(board, this.steps, random, this.pathColor));
        }
    }
}
