using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver.WallTypes
{
    internal class Wall
    {
        public Wall[] Neighbors { get; set; } = new Wall[4];
        public Line? Line { get; set; } = null;
        public virtual bool IsPassable(Line? line)
        {
            return Line == null;
        }
        public virtual bool IsSolved()
        {
            return true;
        }

        public Wall? GetNeighbor(Direction direction)
        {
            return Neighbors[(int)direction];
        }

        public virtual List<Direction> GetPossibleDirections(Line? line = null)
        {
            var output = new List<Direction>();
            foreach (var direction in Directions.All)
            {
                var neighbor = GetNeighbor(direction);
                if (neighbor != null && neighbor.IsPassable(line))
                {
                    output.Add(direction);
                }
            }
            return output;
        }
    }
}
