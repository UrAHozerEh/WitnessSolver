using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.SquareTypes
{
    internal class Triangle : Square
    {
        public int Count { get; }

        public Triangle(int count, int row, int col) : base(row, col)
        {
            Count = count;
        }

        public override bool IsSolved()
        {
            return GetFilledWallCount() == Count;
        }

        public override void DrawSquare(Graphics graphics, Rectangle drawRect, Color backgroundColor, Color wallColor)
        {
            var font = new Font(FontFamily.GenericSerif, drawRect.Height, FontStyle.Bold);
            var format = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            graphics.DrawString(Count.ToString(), font, (IsSolved() ? Brushes.Green : Brushes.Red), drawRect, format);
        }
    }
}
