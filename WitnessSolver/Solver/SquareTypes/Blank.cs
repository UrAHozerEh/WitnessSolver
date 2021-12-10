using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.SquareTypes
{
    internal class Blank : Square
    {
        public Blank(int row, int col) : base(row, col)
        {
        }

        public override void DrawSquare(Graphics graphics, Rectangle drawRect, Color backgroundColor, Color wallColor)
        {

        }

        public override bool IsSolved()
        {
            return true;
        }
    }
}
