using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.WallTypes
{
    internal class Gap: Wall
    {
        public override bool IsPassable(Line? line)
        {
            return false;
        }
    }
}
