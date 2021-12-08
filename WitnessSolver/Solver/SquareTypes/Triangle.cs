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
            var wallCount = 0;
            foreach (var wall in Walls)
            {
                if (wall.Line != null)
                    wallCount++;
            }
            return wallCount == Count;
        }
    }
}
