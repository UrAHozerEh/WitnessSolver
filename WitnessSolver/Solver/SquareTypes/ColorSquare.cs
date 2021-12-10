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
            var enclosed = GetEnclosed<ColorSquare>();
            foreach (var square in enclosed)
            {
                if (square.Color != Color)
                    return false;
            }
            return true;
        }

        public override void DrawSquare(Graphics graphics, Rectangle drawRect, Color backgroundColor, Color wallColor)
        {
            var fillBrush = new SolidBrush(Color);
            var solvedPen = new Pen(IsSolved() ? Color.Green : Color.Red, 5);
            graphics.FillRectangle(fillBrush, drawRect);
            graphics.DrawRectangle(solvedPen, drawRect);
        }
    }
}
