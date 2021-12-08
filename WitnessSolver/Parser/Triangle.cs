using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessSolver.Parser
{
    internal class Triangle
    {
        public int Count { get; private set; }
        public Rectangle Rectangle { get; private set; }

        public Triangle(Rectangle rect)
        {
            Count = 1;
            Rectangle = rect;
        }

        public void Add(Rectangle rect)
        {
            var minX = Math.Min(Rectangle.X, rect.X);
            var minY = Math.Min(Rectangle.Y, rect.Y);
            var maxWidth = Math.Max(Rectangle.Right - minX, rect.Right - minX);
            var maxHeight = Math.Max(Rectangle.Bottom - minY, rect.Bottom - minY);
            Rectangle = new Rectangle(minX, minY, maxWidth, maxHeight);
            Count++;
        }

        public bool CloseEnough(Rectangle other)
        {
            var left = Rectangle.Right < other.Right ? Rectangle : other;
            var right = Rectangle.Right > other.Right ? Rectangle : other;

            var x1 = left.Right;
            var y1 = left.Top;
            var x2 = right.Left;
            var y2 = right.Top;

            var dist = Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));

            return dist < Math.Min(Rectangle.Width, other.Width);
        }

        public static List<Triangle> GetTriangles(Blob[] blobs)
        {
            var output = new List<Triangle>();
            foreach (Blob blob in blobs)
            {
                var curRect = blob.Rectangle;
                var foundBigger = false;
                for (int i = 0; i < output.Count; i++)
                {
                    var curTriangle = output[i];
                    if (curTriangle.CloseEnough(curRect))
                    {
                        curTriangle.Add(curRect);
                        foundBigger = true;
                        break;
                    }
                }
                if (!foundBigger)
                    output.Add(new Triangle(curRect));
            }
            return output;
        }

        public (int X, int Y) GetMiddle()
        {
            return (Rectangle.X + Rectangle.Width/2, Rectangle.Y + Rectangle.Height/2);
        }
    }
}
