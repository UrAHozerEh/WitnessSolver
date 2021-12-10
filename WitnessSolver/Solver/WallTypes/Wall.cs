using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitnessSolver.Solver.SquareTypes;

namespace WitnessSolver.Solver.WallTypes
{
    internal class Wall
    {
        private Wall?[] Walls { get; set; } = new Wall?[4];
        private Square?[] Squares { get; set; } = new Square?[4];
        public Line? Line { get; set; } = null;
        public virtual bool IsPassable(Line? line)
        {
            return Line == null;
        }
        public virtual bool IsSolved()
        {
            return true;
        }

        private Rectangle GetMiddleDrawRectangle(Rectangle drawRect)
        {
            var quarterWidth = drawRect.Width / 4;
            return new Rectangle(drawRect.X + (drawRect.Width / 2) - quarterWidth / 2, drawRect.Y + (drawRect.Height / 2) - quarterWidth / 2, quarterWidth, quarterWidth);
        }



        public virtual void Draw(Graphics graphics, Rectangle drawRect, Color backgroundColor, Color wallColor)
        {
            Brush defaultBrush = new SolidBrush(wallColor);
            Brush lineBrush = new SolidBrush(Line?.Color ?? wallColor);
            var middleRect = GetMiddleDrawRectangle(drawRect);

            foreach (var direction in Directions.All)
            {
                var neighbor = GetWall(direction);
                if (neighbor == null) continue;

                var nextX = middleRect.X;
                var nextY = middleRect.Y;
                var nextWidth = middleRect.Width;
                var nextHeight = middleRect.Height;
                var nextBrush = Line?.Color == neighbor.Line?.Color ? lineBrush : defaultBrush;
                switch (direction)
                {
                    case Direction.Up:
                        nextY = drawRect.Top;
                        nextHeight += middleRect.Top - drawRect.Top;
                        break;
                    case Direction.Right:
                        nextWidth += drawRect.Right - middleRect.Right;
                        break;
                    case Direction.Down:
                        nextHeight += drawRect.Bottom - middleRect.Bottom;
                        break;
                    case Direction.Left:
                        nextX = drawRect.Left;
                        nextWidth += middleRect.Left - drawRect.Left;
                        break;
                    default:
                        break;
                }
                var nextRect = new Rectangle(nextX, nextY, nextWidth, nextHeight);
                graphics.FillRectangle(nextBrush, nextRect);
            }
            graphics.FillRectangle(lineBrush, middleRect);
        }

        public void Replace(Wall oldWall)
        {
            foreach (var direction in Directions.All)
            {
                var curWall = oldWall.GetWall(direction);
                SetWall(direction, curWall);
                if (curWall != null)
                    curWall.SetWall(direction.Reverse(), this);

                var curSquare = oldWall.GetSquare(direction);
                SetSquare(direction, curSquare);
                if (curSquare != null)
                    curSquare.SetWall(direction.Reverse(), this);
            }
        }

        public Wall? GetWall(Direction direction)
        {
            return Walls[(int)direction];
        }

        public void SetWall(Direction direction, Wall? wall)
        {
            Walls[(int)direction] = wall;
        }

        public Square? GetSquare(Direction direction)
        {
            return Squares[(int)direction];
        }

        public void SetSquare(Direction direction, Square? square)
        {
            Squares[(int)direction] = square;
        }

        public virtual List<Direction> GetPossibleDirections(Line? line = null)
        {
            var output = new List<Direction>();
            foreach (var direction in Directions.All)
            {
                var neighbor = GetWall(direction);
                if (neighbor != null && neighbor.IsPassable(line))
                {
                    output.Add(direction);
                }
            }
            return output;
        }
    }
}
