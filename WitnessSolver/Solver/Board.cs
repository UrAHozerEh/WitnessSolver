using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitnessSolver.Solver.SquareTypes;
using WitnessSolver.Solver.WallTypes;
using ParseTriangle = WitnessSolver.Parser.Triangle;

namespace WitnessSolver.Solver
{
    internal class Board
    {
        public int Width { get; }
        public int Height { get; }
        public int WallWidth => Width * 2 + 1;
        public int WallHeight => Height * 2 + 1;
        public Square[,] Squares { get; private set; }
        public Wall[,] Walls { get; private set; }
        private int WallDrawWidth = 40;

        public Color BackgroundColor { get; set; } = Color.DarkSlateGray;
        public Color WallColor { get; set; } = Color.LightGray;

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            Squares = new Square[Width, Height];
            Walls = new Wall[WallWidth, WallHeight];

            FillSquares();
            FillWalls();

            LinkSquares();
            LinkWalls();
        }

        public static Board GetTrianlgeBoard(Rectangle[,] rectanges, List<ParseTriangle> triangles)
        {
            var output = new Board(rectanges.GetLength(0), rectanges.GetLength(1));

            foreach (var triangle in triangles)
            {
                var (tX, tY) = triangle.GetMiddle();
                for (int x = 0; x < rectanges.GetLength(0); x++)
                {
                    for (int y = 0; y < rectanges.GetLength(1); y++)
                    {
                        var curRect = rectanges[x, y];
                        if (curRect.Contains(tX, tY))
                        {
                            var curSquare = new Triangle(triangle.Count, x, y);
                            output.InsertSquare(curSquare);
                        }
                    }
                }
            }

            return output;
        }

        public static Board GetColorBoard(Color[,] colors)
        {
            var output = new Board(colors.GetLength(0), colors.GetLength(1));

            for (int x = 0; x < colors.GetLength(0); x++)
            {
                for (int y = 0; y < colors.GetLength(1); y++)
                {
                    var curColor = colors[x, y];
                    if (curColor.A == 0)
                        continue;
                    output.InsertSquare(new ColorSquare(x, y, curColor));
                }
            }


            return output;
        }

        public void ClearLines()
        {
            foreach (var wall in Walls)
            {
                if (wall != null)
                    wall.Line = null;
            }
        }

        private Wall? DrawLine(Wall start, Direction direction, Line line, bool repeatOneMove = false)
        {
            start.Line = line;
            var nextWall = start.GetWall(direction);
            if (nextWall == null || !nextWall.IsPassable(line))
                return null;
            nextWall.Line = line;
            if (repeatOneMove)
            {
                var nextPossible = nextWall.GetPossibleDirections();
                if (nextPossible.Count == 1)
                    return DrawLine(nextWall, nextPossible[0], line);
            }
            return nextWall;
        }

        public Wall? DoMoves(List<Direction> moves, Player player)
        {
            if (player.StartLocation == null)
                return null;
            return DoMoves(moves, player.StartLocation, player.Line);
        }

        public Wall? DoMoves(List<Direction> moves, Wall start, Line line)
        {
            var curWall = start;

            for (int i = 0; i < moves.Count; i++)
            {
                var move = moves[i];

                curWall = DrawLine(curWall, move, line);
                if (curWall == null)
                    return null;
            }
            curWall.Line = line;
            return curWall;
        }

        public void InsertSquare(Square square)
        {
            var replaced = Squares[square.Row, square.Column];
            square.Replace(replaced);
            Squares[square.Row, square.Column] = square;
        }

        public void InsertCorner(Wall wall, int x, int y)
        {
            var finalX = x * 2;
            var finalY = y * 2;
            InsertWall(wall, finalX, finalY);
        }

        public void InsertVerticalWall(Wall wall, int x, int y)
        {
            var finalX = x * 2;
            var finalY = y * 2 + 1;
            InsertWall(wall, finalX, finalY);
        }

        public void InsertHorizontalWall(Wall wall, int x, int y)
        {
            var finalX = x * 2 + 1;
            var finalY = y * 2;
            InsertWall(wall, finalX, finalY);
        }

        public void InsertWall(Wall wall, int x, int y)
        {
            var replaced = Walls[x, y];
            if(replaced == null)
                return;
            wall.Replace(replaced);
            Walls[x, y] = wall;
        }

        public Square? GetSquare(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return null;
            return Squares[x, y];
        }

        public Wall? GetWall(int x, int y)
        {
            if (x < 0 || y < 0 || x >= WallWidth || y >= WallHeight)
                return null;
            return Walls[x, y];
        }

        private void FillSquares()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Squares[x, y] = new Blank(x, y);
                }
            }
        }

        private void LinkSquares()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var square = Squares[x, y];
                    var (wallX, wallY) = SquareToWall(x, y);
                    foreach (var direction in Directions.All)
                    {
                        var (curX, curY) = direction.GetPoint(x, y);
                        var curSquare = GetSquare(curX, curY);
                        if (curSquare != null)
                            square.SetSquare(direction, curSquare);

                        var (curWallX, curWallY) = direction.GetPoint(wallX, wallY);
                        var curWall = GetWall(curWallX, curWallY);
                        if (curWall != null)
                        {
                            square.SetWall(direction, curWall);
                            curWall.SetSquare(direction.Reverse(), square);
                        }
                    }
                }
            }
        }

        public static (int X, int Y) SquareToWall(int x, int y)
        {
            return (x * 2 + 1, y * 2 + 1);
        }

        private void FillWalls()
        {
            for (int x = 0; x < WallWidth; x++)
            {
                for (int y = 0; y < WallHeight; y++)
                {
                    var hOdd = y % 2 != 0;
                    var wOdd = x % 2 != 0;
                    if (wOdd && hOdd)
                        continue;
                    if (x == 0 && y == 0)
                        Walls[x, y] = new Start();
                    else if (x == WallWidth - 1 && y == WallHeight - 1)
                        Walls[x, y] = new Finish();
                    else
                        Walls[x, y] = new Wall();
                }
            }
        }

        private void LinkWalls()
        {
            for (int x = 0; x < WallWidth; x++)
            {
                for (int y = 0; y < WallHeight; y++)
                {
                    var wall = Walls[x, y];
                    if (wall == null)
                        continue;
                    foreach (var direction in Directions.All)
                    {
                        var (curX, curY) = direction.GetPoint(x, y);
                        var curWall = GetWall(curX, curY);
                        if (curWall != null)
                            wall.SetWall(direction, curWall);
                        
                    }
                }
            }
        }

        public bool IsSolved(Wall currentLocation)
        {
            if (currentLocation == null || currentLocation is not Finish)
                return false;

            foreach (var square in Squares)
            {
                if (!square.IsSolved())
                    return false;
            }

            foreach (var wall in Walls)
            {
                if (wall == null)
                    continue;
                if (!wall.IsSolved())
                    return false;
            }
            return true;
        }

        public Bitmap DrawBoard()
        {
            var board = new Bitmap(WallWidth * WallDrawWidth, WallHeight * WallDrawWidth);
            DrawWalls(board);
            DrawSquares(board);
            return board;
        }

        private void DrawSquares(Bitmap image)
        {
            using Graphics g = Graphics.FromImage(image);
            var pen = new Pen(Color.Blue, 5);
            
            foreach (var square in Squares)
            {
                if (square is Blank) continue;
                var (x, y) = SquareToWall(square.Row, square.Column);
                var drawX = x * WallDrawWidth;
                var drawY = image.Height - (y + 1) * WallDrawWidth;
                var drawRect = new Rectangle(drawX, drawY, WallDrawWidth, WallDrawWidth);
                square.DrawSquare(g, drawRect, BackgroundColor, WallColor);
            }
        }

        private void DrawWalls(Bitmap image)
        {
            using Graphics g = Graphics.FromImage(image);
            var pen = new Pen(Color.Blue, 5);
            g.FillRectangle(new SolidBrush(BackgroundColor), new Rectangle(0, 0, image.Width, image.Height));
            for (int x = 0; x < WallWidth; x++)
            {
                for (int y = 0; y < WallHeight; y++)
                {
                    var wall = Walls[x, y];
                    if (wall == null) continue;
                    var drawX = x * WallDrawWidth;
                    var drawY = image.Height - (y + 1) * WallDrawWidth;
                    var drawRect = new Rectangle(drawX, drawY, WallDrawWidth, WallDrawWidth);

                    wall.Draw(g, drawRect, BackgroundColor, WallColor);
                }
            }
        }
    }
}
