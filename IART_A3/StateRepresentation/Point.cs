using System;

namespace IART_A3.StateRepresentation
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public double Distance(Point other)
        {
            return Math.Sqrt((other.X - X) * (other.X - X) + (other.Y - Y) * (other.Y - Y));
        }
    }
}
