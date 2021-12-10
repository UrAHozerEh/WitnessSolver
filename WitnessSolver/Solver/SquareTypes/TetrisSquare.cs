using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.SquareTypes
{
    internal class TetrisSquare : ColorSquare
    {
        public List<TetrisSquareShape> Shapes { get; private set; }
        public bool CanRotate { get; private set; }
        public bool IsAdditive { get; private set; }

        public TetrisSquare(int row, int col, Color color, TetrisSquareShape shape, bool canRotate = false, bool isAdditive = true) : base(row, col, color)
        {
            CanRotate = canRotate;
            if (canRotate)
                Shapes = shape.GetAllRotations();
            else
                Shapes = new() { shape };
            IsAdditive = isAdditive;
        }

        public TetrisSquare(int row, int col, Color color, List<Direction> moves, bool canRotate = false, bool isAdditive = true) : this(row, col, color, new TetrisSquareShape(moves), canRotate, isAdditive)
        {
        }

        public override void DrawSquare(Graphics graphics, Rectangle drawRect, Color backgroundColor, Color wallColor)
        {
            var padding = 3;
            var shape = Shapes[0];
            var size = shape.Size;
            var paddingSize = padding * size + 1;
            var squareSize = (drawRect.Height - paddingSize) / size;

            var x = drawRect.X;
            var y = drawRect.Y;

            var fillBrush = new SolidBrush(Color);
            var solvedPen = new Pen(IsSolved() ? Color.Green : Color.Red);

            graphics.FillRectangle(fillBrush, new Rectangle(x, y, squareSize, squareSize));

            foreach (var direction in shape.Moves)
            {
                switch (direction)
                {
                    case Direction.Up:
                        y -= squareSize;
                        y -= padding;
                        break;
                    case Direction.Right:
                        x += squareSize;
                        x += padding;
                        break;
                    case Direction.Down:
                        y += squareSize;
                        y += padding;
                        break;
                    case Direction.Left:
                        x -= squareSize;
                        x -= padding;
                        break;
                    default:
                        break;
                }
                graphics.FillRectangle(fillBrush, new Rectangle(x, y, squareSize, squareSize));
            }
            graphics.DrawRectangle(solvedPen, drawRect);
        }

        public override bool IsSolved()
        {
            var allTetris = GetEnclosed<TetrisSquare>();
            var allAddTetris = allTetris.Where(t => t.IsAdditive).ToList();
            var allSubTetris = allTetris.Where(t => !t.IsAdditive).ToList();

            if (allAddTetris.Count == 0 && allSubTetris.Count != 0)
                return false;

            if (allSubTetris.Count == 0)
                return IsBasicSolved(allAddTetris);

            return IsComplexSolved(allAddTetris, allSubTetris);
        }

        private static bool IsComplexSolved(List<TetrisSquare> addTetrisSquares, List<TetrisSquare> subTetrisSquares)
        {
            return true;
        }

        private static bool IsBasicSolved(List<TetrisSquare> tetrisSquares)
        {
            if (tetrisSquares.Count == 0)
                return true;
            var enclosed = tetrisSquares[0].GetEnclosed();
            var curSolution = new Dictionary<TetrisSquare, List<Square>>();

            foreach (var square in tetrisSquares)
            {
                curSolution[square] = new List<Square>();
            }

            return GetBasicSolution(curSolution, enclosed) != null;
        }

        private static Dictionary<TetrisSquare, List<Square>>? GetBasicSolution(Dictionary<TetrisSquare, List<Square>> curSolution, List<Square> enclosed)
        {
            var nonUsedTetris = new List<TetrisSquare>();
            var nonUsedSquares = new List<Square>(enclosed);
            var allUsedSquares = new List<Square>();
            foreach (var (tetrisSquare, usedSquares) in curSolution)
            {
                if (usedSquares.Count == 0)
                {
                    nonUsedTetris.Add(tetrisSquare);
                }
                foreach (var usedSquare in usedSquares)
                {
                    if (allUsedSquares.Contains(usedSquare))
                        return null;
                    allUsedSquares.Add(usedSquare);
                    nonUsedSquares.Remove(usedSquare);
                }
            }
            if (nonUsedTetris.Count == 0 && nonUsedSquares.Count == 0)
                return curSolution;

            foreach (var tetris in nonUsedTetris)
            {
                foreach (var shape in tetris.Shapes)
                {
                    foreach (var start in nonUsedSquares)
                    {
                        var newUsedSquares = shape.GetSquares(start);
                        if (newUsedSquares == null)
                            continue;
                        var newSolution = new Dictionary<TetrisSquare, List<Square>>(curSolution)
                        {
                            [tetris] = newUsedSquares
                        };
                        newSolution = GetBasicSolution(newSolution, enclosed);
                        if (newSolution != null)
                            return newSolution;
                    }
                }
            }
            return null;
        }
    }

    internal struct TetrisSquareShape
    {
        public List<Direction> Moves { get; }
        public int Size => Moves.Count;
        public TetrisSquareShape(List<Direction> moves)
        {
            Moves = moves;
        }

        public TetrisSquareShape GetRotatedShape(int count)
        {
            return new TetrisSquareShape(Moves.Select(d => d.Rotate(count)).ToList());
        }

        public TetrisSquareShape GetFlippedHorizontalShape()
        {
            return new TetrisSquareShape(Moves.Select(d => d.FlipHorizontal()).ToList());
        }

        public TetrisSquareShape GetFlippedVerticalShape()
        {
            return new TetrisSquareShape(Moves.Select(d => d.FlipVertical()).ToList());
        }

        public List<TetrisSquareShape> GetAllRotations()
        {
            if (Moves.Count == 0)
                return new() { this };

            return new()
            {
                this,
                GetRotatedShape(1),
                GetRotatedShape(2),
                GetRotatedShape(3),
            };
        }

        public List<Square>? GetSquares(Square startSquare)
        {
            var output = new List<Square>() { startSquare };
            var curSquare = startSquare;
            foreach (var move in Moves)
            {
                curSquare = curSquare.GetSquare(move);
                if (curSquare == null)
                    return null;
                output.Add(curSquare);
            }

            return output;
        }

        public static TetrisSquareShape GetHorizontalLine(int length)
        {
            var moves = new List<Direction>();
            for (int i = 1; i < length; i++)
            {
                moves.Add(Direction.Right);
            }
            return new(moves);
        }

        public static TetrisSquareShape GetVerticalLine(int length)
        {
            return GetHorizontalLine(length).GetRotatedShape(1);
        }

        public static TetrisSquareShape GetL(int height, int width)
        {
            var moves = new List<Direction>();
            for (int i = 1; i < width; i++)
            {
                moves.Add(Direction.Left);
            }
            for (int i = 1; i < height; i++)
            {
                moves.Add(Direction.Up);
            }
            return new(moves);
        }

        public static TetrisSquareShape GetReverseL(int height, int width)
        {
            return GetL(height, width).GetFlippedHorizontalShape();
        }
    }
}
