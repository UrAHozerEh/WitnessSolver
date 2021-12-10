using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.SquareTypes
{
    internal class SpikeColorSquare : ColorSquare
    {
        public SpikeColorSquare(int row, int col, Color color) : base(row, col, color)
        {

        }

        public override bool IsSolved()
        {
            var colors = GetEnclosed<ColorSquare>();
            var count = 0;
            foreach (var color in colors)
            {
                if (color.Color == Color)
                    ++count;
            }
            return count == 2;
        }

        public override void DrawSquare(Graphics graphics, Rectangle drawRect, Color backgroundColor, Color wallColor)
        {
            var fillBrush = new SolidBrush(Color);
            var solvedPen = new Pen(IsSolved() ? Color.Green : Color.Red, 5);

            var percentRemoved = 0.1f;
            var diffWidth = (int)(drawRect.Width * percentRemoved);
            var diffHeight = (int)(drawRect.Height * percentRemoved);

            var x = drawRect.X - diffWidth / 2;
            var y = drawRect.Y - diffHeight / 2;

            var width = drawRect.Width - diffWidth;
            var height = drawRect.Height - diffHeight;

            var curRect = new Rectangle(x, y, width, height);

            graphics.FillRectangle(fillBrush, curRect);
            graphics.DrawRectangle(solvedPen, curRect);

            graphics.FillEllipse(fillBrush, curRect);
            graphics.DrawEllipse(solvedPen, curRect);
        }
    }
}
