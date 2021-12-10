﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.WallTypes
{
    internal class Start : Wall
    {
        public override void Draw(Graphics graphics, Rectangle drawRect, Color backgroundColor, Color wallColor)
        {
            base.Draw(graphics, drawRect, backgroundColor, Color.Green);
        }
    }
}
