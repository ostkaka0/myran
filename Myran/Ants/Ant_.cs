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
        public Ant_()
            : base(null, null)
        {
        }
        Queen queen;
        Color pathColor;
    }
}
