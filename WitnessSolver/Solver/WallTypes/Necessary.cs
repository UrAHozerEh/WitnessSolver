using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.WallTypes
{
    internal class Necessary : Wall
    {
        public Color? Color { get; }

        public Necessary(Color? color = null)
        {
            Color = color;
        }

        public override bool IsPassable(Line? line)
        {
            if (Color == null || line == null)
                return base.IsPassable(line);
            return Color == line.Color;

        }

        public override bool IsSolved()
        {
            if (Line == null)
                return false;
            if (Color != null && Line.Color != Color)
                return false;
            return true;
        }
    }
}
