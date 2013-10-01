using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myran.Ants
{
    class Queen : KonstigAnt
    {
        readonly int maxEnergy = 1000;

        public Queen()
            : base(null, null)
        {
        }

        protected override bool IsWalkAble()
        {
            throw new NotImplementedException();
        }

        protected override void OnWalk()
        {
            throw new NotImplementedException();
        }
    }
}
