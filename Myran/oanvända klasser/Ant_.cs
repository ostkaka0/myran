using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Myran.Ants
{
    abstract class Ant_ : KonstigAnt
    {
        protected Queen queen;
        protected Color pathColor;
        public Ant_(Board board, Random random, int color, int maxEnergy, Queen queen)
            : base(board, random, color, maxEnergy)
        {
            this.queen = queen;
            this.pathColor = ColorFromHue(color, 95);
        }
    }
}
