using AForge.Imaging;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using WitnessSolver.Solver;
using Triangle = WitnessSolver.Parser.Triangle;

namespace WitnessSolver
{
    public partial class Form1 : Form
    {
        private Bitmap OtherImage = null;
        private KeyHandler ghk;
        private List<List<Direction>> Solutions = new();
        private int CurSolution = 0;
        private Board? Board = null;
        private Player? Player = null;
        public Form1()
        {
            InitializeComponent();
            ghk = new KeyHandler(Keys.PageUp, this);
            ghk.Register();
            //var processes = Process.GetProcesses();
        }

        private void HandleHotkey()
        {
            var bitmap = CaptureApplication("witness64_d3d11");
            int quarterWidth = (int)(bitmap.Width / 4.0);
            int quarterHeight = (int)(bitmap.Height / 6.0);
            var rect = new Rectangle(quarterWidth, quarterHeight, quarterWidth * 2, quarterHeight * 4);
            var cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            if (!ParseTriangles(cropped))
                ParseGreen(cropped);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey();
            base.WndProc(ref m);
        }

        private void GetImageButtonClicked(object sender, EventArgs e)
        {
            if (!ParseTriangles(GetImageFromFile(@"C:\Users\kcron\Desktop\Witness Program\Screenshot")))
                ParseGreen(GetImageFromFile(@"C:\Users\kcron\Desktop\Witness Program\Screenshot"));
        }

        private Bitmap? GetImageFromFile(string location)
        {
            var files = Directory.GetFiles(location);
            var images = new List<Bitmap>();
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    Bitmap bitmap = (Bitmap)System.Drawing.Image.FromFile(file);
                    int quarterWidth = (int)(bitmap.Width / 4.0);
                    int quarterHeight = (int)(bitmap.Height / 6.0);
                    var rect = new Rectangle(quarterWidth, quarterHeight, quarterWidth * 2, quarterHeight * 4);
                    var cropped = bitmap.Clone(rect, bitmap.PixelFormat);
                    images.Add(cropped);
                }
            }

            foreach (Bitmap bitmap in images)
            {
                return bitmap;
            }
            return null;
        }


        private void ParseGreen(Bitmap? bitmap)
        {
            if (bitmap == null)
                return;

            OtherImage = (Bitmap)bitmap.Clone();

            var boardImage = FilterGreenPuzzleBoard(bitmap);

            var boardBlobs = GetGreenBoard(boardImage, 50);
            var bigArea = boardBlobs.Max(b => b.Area);
            var boardRect = new Rectangle(0, 0, boardImage.Width, boardImage.Height);
            foreach (var blob in boardBlobs)
            {
                if (blob.Area == bigArea)
                    boardRect = blob.Rectangle;
            }

            var squaresImage = FilterGreenPuzzleSquares(bitmap, boardRect);
            var lineBlobs = GetGreenBoard(squaresImage, 50);
            var maxSquare = lineBlobs.Max(b => b.Area);
            var squareRects = new List<Rectangle>();
            foreach (var blob in lineBlobs)
            {
                if (blob.Area != maxSquare)
                    squareRects.Add(blob.Rectangle);
            }
            var squares = GetSquares(squareRects);

            var colorsImage = FilterGreenColorSquares(OtherImage, squares);
            OtherImage = colorsImage;
            //var colorBlobs = GetGreenBoard(colorsImage, 20);
            var colors = GetColors(squares, colorsImage);
            DrawBlobs(lineBlobs, colorsImage);


            //DrawBlobs(colorBlobs, colorsImage);
            ImageBox.Image = colorsImage;

            Board = Board.GetColorBoard(colors);
            Player = new Player(Board);
            var solution = Player.BeginSolve();
            Solutions = Player.BeginSolveAll();
            DrawSolution();
        }

        private Color[,] GetColors(Rectangle[,] squares, Bitmap image)
        {
            var output = new Color[squares.GetLength(0), squares.GetLength(1)];
            for (int r = 0; r < squares.GetLength(0); r++)
            {
                for (int w = 0; w < squares.GetLength(1); w++)
                {
                    var curRect = squares[r, w];
                    var redTotal = 0;
                    var greenTotal = 0;
                    var blueTotal = 0;
                    var count = 0;
                    for (int x = curRect.X; x < curRect.Width + curRect.X; x++)
                    {
                        for (int y = curRect.Y; y < curRect.Height + curRect.Y; y++)
                        {

                            var color = image.GetPixel(x, y);
                            if (color.A == 0)
                                continue;
                            ++count;
                            redTotal += color.R;
                            greenTotal += color.G;
                            blueTotal += color.B;
                        }
                    }
                    if (count != 0)
                    {
                        var avgColor = Color.FromArgb(redTotal / count, greenTotal / count, blueTotal / count);
                        output[r, w] = avgColor;
                    }
                }
            }
            ClampColors(output);
            return output;
        }

        private void ClampColors(Color[,] colors)
        {
            var foundColors = new List<Color>();
            for (int r = 0; r < colors.GetLength(0); r++)
            {
                for (int w = 0; w < colors.GetLength(1); w++)
                {
                    var curColor = colors[r, w];
                    var didFindColor = false;
                    foreach (var color in foundColors)
                    {
                        if (ColorsAreClose(color, curColor))
                        {
                            colors[r, w] = color;
                            didFindColor = true;
                            break;
                        }
                    }
                    if (!didFindColor)
                    {
                        foundColors.Add(curColor);
                    }
                }
            }
        }

        private bool ColorsAreClose(Color a, Color z, int threshold = 10)
        {
            int r = (int)a.R - z.R,
                g = (int)a.G - z.G,
                b = (int)a.B - z.B;
            return (r * r + g * g + b * b) <= threshold * threshold;
        }

        private Bitmap FilterGreenColorSquares(Bitmap image, Rectangle[,] squares)
        {
            var board = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    if (!PointInSquares(x, y, squares))
                        continue;
                    var color = image.GetPixel(x, y);
                    if (color.R < 30 && color.G < 30 && color.B < 30)
                        board.SetPixel(x, y, color);
                    if (color.R > 80)
                        board.SetPixel(x, y, color);
                }
            }
            return board;
        }

        private bool PointInSquares(int x, int y, Rectangle[,] squares)
        {
            foreach (var rect in squares)
            {
                if (rect.Contains(x, y))
                    return true;
            }
            return false;
        }

        private Bitmap FilterGreenPuzzleBoard(Bitmap image)
        {
            var board = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var color = image.GetPixel(x, y);
                    if (color.R < 10 && color.G > 10)
                        board.SetPixel(x, y, Color.White);
                }
            }
            return board;
        }

        private Bitmap FilterGreenPuzzleSquares(Bitmap image, Rectangle boardRect)
        {
            var board = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    if (!boardRect.Contains(x, y)) continue;
                    var color = image.GetPixel(x, y);
                    var max = Math.Max(Math.Max(color.R, color.G), color.B);
                    if (color.B > 240 && color.R > 240 && color.G > 240)
                        board.SetPixel(x, y, Color.White);
                    else if (color.B == max && color.B <= 60)
                        board.SetPixel(x, y, Color.Black);
                    else
                        board.SetPixel(x, y, Color.White);
                }
            }
            return board;
        }

        private Blob[] GetGreenBoard(Bitmap image, int minSize)
        {
            var blobCounter = new BlobCounter
            {
                FilterBlobs = true,
                MinHeight = minSize,
                MinWidth = minSize,
            };
            blobCounter.ProcessImage(image);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            return blobs;
        }

        private bool ParseTriangles(Bitmap? bitmap)
        {
            if (bitmap == null)
                return false;
            OtherImage = (Bitmap)bitmap.Clone();

            var filters = FilterTrianglePuzzle(bitmap);

            var triangles = GetTriangles(filters[0]);
            if (triangles.Count < 2)
                return false;
            var squares = GetSquares(GetSquares(filters[1]));
            var board = Board.GetTrianlgeBoard(squares, triangles);
            var moves = new List<Direction>() { Direction.Right, Direction.Right, Direction.Right, Direction.Right };
            board.DoMoves(moves, board.Walls[0, 0], new Line(Color.Pink));
            var player = new Player(board);
            var solution = player.BeginSolve();
            if (solution != null && player.StartLocation != null)
                board.DoMoves(solution, player.StartLocation, player.Line);
            ImageBox.Image = board.DrawBoard();
            return true;
        }

        private Rectangle[,] GetSquares(List<Rectangle> rects)
        {
            var size = (int)Math.Sqrt(rects.Count);
            var output = new Rectangle[size, size];
            var minX = rects.Min(r => r.X);
            var minY = rects.Min(r => r.Y);
            var maxX = rects.Max(r => r.X);
            var maxY = rects.Max(r => r.Y);
            var widthPer = (maxX - minX) / size + 1;
            var heightPer = (maxY - minY) / size + 1;

            foreach (var rect in rects)
            {
                var x = (rect.X - minX) / widthPer;
                var y = size - (rect.Y - minY) / heightPer - 1;
                if (output[x, y] != new Rectangle())
                    return output;
                output[x, y] = rect;
            }

            return output;
        }

        private List<Triangle> GetTriangles(Bitmap image)
        {
            var blobCounter = new BlobCounter
            {
                FilterBlobs = true,
                MinHeight = 10,
                MinWidth = 10,
            };
            blobCounter.ProcessImage(image);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            return Triangle.GetTriangles(blobs);
        }


        private List<Rectangle> GetSquares(Bitmap image)
        {
            //ImageBox.Image = image;
            //var boardFinder = new BlobCounter
            //{
            //    FilterBlobs = true,
            //    MinHeight = 10,
            //    MinWidth = 10,
            //};
            //boardFinder.ProcessImage(image);
            //Blob[] blobs = boardFinder.GetObjectsInformation();

            //DrawRectangle(blobs[0].Rectangle, image);
            //var cropped = image.Clone(blobs[0].Rectangle, image.PixelFormat);
            Invert(image);


            ImageBox.Image = image;
            var squareCounter = new BlobCounter
            {
                FilterBlobs = true,
                MinHeight = 10,
                MinWidth = 10,
            };
            squareCounter.ProcessImage(image);
            Blob[] squares = squareCounter.GetObjectsInformation();
            var maxSize = squares.Max(square => square.Area);
            var output = new List<Rectangle>();
            foreach (var square in squares)
            {
                if (square.Area < maxSize)
                    output.Add(square.Rectangle);
            }
            return output;

        }

        private void Invert(Bitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var color = image.GetPixel(x, y);
                    if (color.R != 0)
                        image.SetPixel(x, y, Color.Black);
                    else
                        image.SetPixel(x, y, Color.White);
                }
            }
        }

        private Rectangle ShrinkBounding(Rectangle bounding, Bitmap image)
        {
            for (int x = bounding.Left; x < bounding.Right; x++)
            {
                var curY = bounding.Top + bounding.Height / 2;
                var curColor = image.GetPixel(x, curY);
                image.SetPixel(x, curY, Color.Purple);
                if (curColor.Equals(Color.White))
                {
                    var newWidth = bounding.Width - (x - bounding.Left);
                    bounding = new Rectangle(x, bounding.Y, newWidth, bounding.Height);
                }
            }

            for (int y = bounding.Top; y > bounding.Bottom; y--)
            {
                var curColor = image.GetPixel(bounding.Left + bounding.Width / 2, y);
                if (curColor == Color.White)
                {
                    var newHeight = bounding.Height - (bounding.Top - y);
                    bounding = new Rectangle(bounding.X, y, bounding.Width, newHeight);
                }
            }
            return bounding;
        }


        private List<Bitmap> FilterTrianglePuzzle(Bitmap image)
        {
            var triangles = new Bitmap(image.Width, image.Height);
            var lines = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    lines.SetPixel(x, y, Color.Black);
                    var color = image.GetPixel(x, y);
                    if (color.R >= 240)
                        triangles.SetPixel(x, y, Color.White);
                    else if (color.R + color.G + color.B >= 200 && color.R <= 150)
                        lines.SetPixel(x, y, Color.White);
                }
            }
            return new List<Bitmap> { triangles, lines };
        }

        private void DrawBlobs(Blob[] blobs, Bitmap image)
        {
            using Graphics g = Graphics.FromImage(image);
            var pen = new Pen(Color.Red, 5);

            foreach (var blob in blobs)
            {

                g.DrawRectangle(pen, blob.Rectangle);
            }
        }

        private void DrawRectangle(Rectangle rect, Bitmap image)
        {
            using Graphics g = Graphics.FromImage(image);
            var pen = new Pen(Color.Blue, 5);

            g.DrawRectangle(pen, rect);

        }

        private void DrawTriangles(List<Triangle> rects, Bitmap image)
        {
            using Graphics g = Graphics.FromImage(image);
            var pen = new Pen(Color.Red, 5);

            foreach (var rect in rects)
            {

                g.DrawRectangle(pen, rect.Rectangle);
                var font = new Font(FontFamily.GenericSansSerif, rect.Rectangle.Height, FontStyle.Regular);
                var sf = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                };
                g.DrawString(rect.Count.ToString(), font, Brushes.Red, rect.Rectangle, sf);
            }
        }

        private void SwappedClicked(object sender, EventArgs e)
        {
            Bitmap image = (Bitmap)ImageBox.Image;
            ImageBox.Image = OtherImage;
            OtherImage = image;
        }

        public Bitmap CaptureApplication(string procName)
        {
            var proc = Process.GetProcessesByName(procName)[0];
            var rect = new User32.Rect();
            User32.GetWindowRect(proc.MainWindowHandle, ref rect);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            }

            return bmp;
        }

        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        }

        private void NextSolutionClicked(object sender, EventArgs e)
        {
            ++CurSolution;
            if (CurSolution >= Solutions.Count)
                CurSolution = 0;
            DrawSolution();
        }

        private void PreviousSolutionClicked(object sender, EventArgs e)
        {
            --CurSolution;
            if (CurSolution < 0)
                CurSolution = Solutions.Count - 1;
            DrawSolution();
        }

        private void DrawSolution()
        {
            if (Solutions.Count == 0 || Board == null || Player == null)
                return;
            Text = $"Solution {CurSolution + 1} of {Solutions.Count}";
            Board.ClearLines();
            Board.DoMoves(Solutions[CurSolution], Player);
            ImageBox.Image.Dispose();
            ImageBox.Image = Board.DrawBoard();
        }
    }
}