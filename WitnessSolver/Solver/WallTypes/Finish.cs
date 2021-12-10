using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.WallTypes
{
    internal class Finish: Wall
    {
        public override void Draw(Graphics graphics, Rectangle drawRect, Color backgroundColor, Color wallColor)
        {
            base.Draw(graphics, drawRect, backgroundColor, Color.Red);
        }
    }
}
