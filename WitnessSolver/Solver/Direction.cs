using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Solver
{
    internal enum Direction : int
    {
        Up = 0,
        Right,
        Down,
        Left
    }

    internal static class Directions
    {
        public static List<Direction> All { get; } = new() { Direction.Up, Direction.Right, Direction.Down, Direction.Left };

        public static (int X, int Y) GetPoint(this Direction direction, int x, int y)
        {
            switch (direction)
            {
                case Direction.Up:
                    ++y;
                    break;
                case Direction.Right:
                    ++x;
                    break;
                case Direction.Down:
                    --y;
                    break;
                case Direction.Left:
                    --x;
                    break;
                default:
                    break;
            }
            return (x, y);
        }

        public static Direction Reverse(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Down,
                Direction.Right => Direction.Left,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                _ => Direction.Up,
            };
        }

        public static Direction FlipHorizontal(this Direction direction)
        {
            return direction switch
            {

                Direction.Right => Direction.Left,
                Direction.Left => Direction.Right,
                _ => direction,
            };
        }

        public static Direction FlipVertical(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                _ => direction,
            };
        }

        public static Direction FlipDiagonal(this Direction direction)
        {
            return direction.FlipVertical().FlipHorizontal();
        }

        public static Direction Rotate(this Direction direction, int count = 1)
        {
            var newDirection = ((int)direction + count) % 4;
            return (Direction)newDirection;
        }
    }
}
