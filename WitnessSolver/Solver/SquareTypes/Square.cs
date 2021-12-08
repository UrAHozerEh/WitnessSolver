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
        public Square[] Neighbors { get; set; } = new Square[4];
        public Wall[] Walls { get; set; } = new Wall[4];

        public Square(int row, int col)
        {
            Row = row;
            Column = col;
        }

        public abstract bool IsSolved();

        public void Replace(Square oldSquare)
        {
            foreach (var direction in Directions.All)
            {
                var square = oldSquare.Neighbors[(int)direction];
                Neighbors[(int)direction] = square;
                if (square != null)
                    square.Neighbors[(int)direction.Reverse()] = this;
                Walls[(int)direction] = oldSquare.Walls[(int)direction];
            }
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
                var square = Neighbors[(int)direction];
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
