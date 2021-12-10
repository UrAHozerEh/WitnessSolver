using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitnessSolver.Solver.WallTypes;

namespace WitnessSolver.Solver.SquareTypes
{
    internal abstract class Square
    {
        public int Row { get; }
        public int Column { get; }
        private Square?[] Neighbors { get; set; } = new Square[4];
        private Wall?[] Walls { get; set; } = new Wall[4];

        public Square(int row, int col)
        {
            Row = row;
            Column = col;
        }

        public abstract void DrawSquare(Graphics graphics, Rectangle drawRect, Color backgroundColor, Color wallColor);

        public abstract bool IsSolved();

        public Square? GetSquare(Direction direction)
        {
            return Neighbors[(int)direction];
        }

        public void SetSquare(Direction direction, Square? square)
        {
            Neighbors[(int)direction] = square;
        }

        public Wall? GetWall(Direction direction)
        {
            return Walls[(int)direction];
        }

        public void SetWall(Direction direction, Wall? wall)
        {
            Walls[(int)direction] = wall;
        }

        public int GetFilledWallCount()
        {
            var wallCount = 0;
            foreach (var wall in Walls)
            {
                if (wall != null && wall.Line != null)
                    wallCount++;
            }
            return wallCount;
        }

        public void Replace(Square oldSquare)
        {
            foreach (var direction in Directions.All)
            {
                var curSquare = oldSquare.GetSquare(direction);
                SetSquare(direction, curSquare);
                if (curSquare != null)
                    curSquare.SetSquare(direction.Reverse(), this);

                var curWall = oldSquare.GetWall(direction);
                SetWall(direction, curWall);
                if (curWall != null)
                    curWall.SetSquare(direction.Reverse(), this);
            }
        }

        public List<T> GetEnclosed<T>() where T : Square
        {
            var enclosed = GetEnclosed(new List<Square>());
            var output = new List<T>();
            foreach (var square in enclosed)
            {
                if (square is T t)
                {
                    output.Add(t);
                }
            }
            return output;
        }

        public List<Square> GetAllSquares()
        {
            return GetAllSquares(new List<Square>());
        }

        private List<Square> GetAllSquares(List<Square> current)
        {
            if (current.Contains(this))
                return current;
            current.Add(this);
            foreach (var direction in Directions.All)
            {
                var square = GetSquare(direction);
                if (square == null)
                    continue;

                current = square.GetEnclosed(current);
            }

            return current;
        }

        public List<Square> GetEnclosed()
        {
            return GetEnclosed(new List<Square>());
        }

        private List<Square> GetEnclosed(List<Square> current)
        {
            if (current.Contains(this))
                return current;
            current.Add(this);
            foreach (var direction in Directions.All)
            {
                var wall = Walls[(int)direction];
                var square = GetSquare(direction);
                if (square == null)
                    continue;
                if (wall?.Line == null)
                {
                    current = square.GetEnclosed(current);
                }
            }

            return current;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Square)
                return false;
            Square square = (Square)obj;
            return square.Row == Row && square.Column == Column;
        }

        public override int GetHashCode()
        {
            return Row.GetHashCode() ^ Column.GetHashCode();
        }
    }
}
