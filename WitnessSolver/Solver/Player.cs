using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitnessSolver.Solver.WallTypes;

namespace WitnessSolver.Solver
{
    internal class Player
    {
        public Board Board { get; private set; }
        public Wall? StartLocation { get; private set; }
        public Queue<Direction> Moves { get; private set; }
        public Line Line { get; private set; } = new Line(Color.Orange);

        public Player(Board board)
        {
            Board = board;
            foreach (var wall in Board.Walls)
            {
                if (wall is Start)
                    StartLocation = wall;
            }
            Moves = new Queue<Direction>();
        }

        public List<Direction>? BeginSolve()
        {
            var solution = Solve(new List<Direction>());
            Board.ClearLines();
            return solution;
        }

        private List<Direction>? Solve(List<Direction> curMoves)
        {
            if (StartLocation == null)
                return null;
            Board.ClearLines();
            var curLocation = Board.DoMoves(curMoves, StartLocation, Line);

            if (curLocation == null)
                return null;
            var possible = curLocation.GetPossibleDirections(Line);
            if (possible.Count == 0)
                return null;
            if (Board.IsSolved(curLocation))
                return curMoves;
            foreach (Direction direction in possible)
            {
                var nextMoves = new List<Direction>(curMoves)
                {
                    direction
                };
                var solution = Solve(nextMoves);
                if (solution != null)
                    return solution;
            }
            return null;
        }

        public List<List<Direction>> BeginSolveAll()
        {
            return SolveAll(new List<Direction>(), new List<List<Direction>>());
        }

        private List<List<Direction>> SolveAll(List<Direction> curMoves, List<List<Direction>> curSolutions)
        {
            if (StartLocation == null)
                return curSolutions;
            Board.ClearLines();
            var curLocation = Board.DoMoves(curMoves, StartLocation, Line);

            if (curLocation == null)
                return curSolutions;
            var possible = curLocation.GetPossibleDirections(Line);
            if (possible.Count == 0)
                return curSolutions;
            if (Board.IsSolved(curLocation))
                curSolutions.Add(curMoves);
            foreach (Direction direction in possible)
            {
                var nextMoves = new List<Direction>(curMoves)
                {
                    direction
                };
                curSolutions = SolveAll(nextMoves, curSolutions);
            }
            return curSolutions;
        }
    }
}
