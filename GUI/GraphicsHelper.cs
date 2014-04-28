using System.Drawing;

namespace GUI
{
    public static class GraphicsHelper
    {
        public static void DrawGrid(this Graphics g, Pen pen, float x, float y, float width, float height, int xDivisions, int yDivisions)
        {
            var xSize = width/xDivisions;
            var ySize = height/yDivisions;

            for (var xx = 0; xx <= xDivisions; ++xx)
            {
                g.DrawLine(pen, x + xx * xSize, y, x + xx * xSize, y + height);
            }

            for (var yy = 0; yy <= yDivisions; ++yy)
            {
                g.DrawLine(pen, x, y + yy * ySize, x + width, y + yy * ySize);
            }
        }
    }
}
