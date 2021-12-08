using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.SquareTypes
{
    internal class ColorSquare : Square
    {
        public Color Color { get; }
        public ColorSquare(int row, int col, Color color) : base(row, col)
        {
            Color = color;
        }

        public override bool IsSolved()
        {
            var enclosed = GetEnclosed();
            foreach(var square in enclosed)
            {
                if(square is ColorSquare otherColor)
                {
                    if (otherColor.Color != Color)
                        return false;
                }
            }
            return true;
        }
    }
}
