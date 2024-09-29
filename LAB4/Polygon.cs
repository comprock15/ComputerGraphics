using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4
{
    internal class Polygon
    {
        public List<System.Drawing.Point> vertices;
        public int vertices_count;
        public Polygon(Point first_point)
        {
            vertices = new List<Point>() { first_point };
            vertices_count = 1;
        }
        public void AddVertex(Point new_vert)
        {
            vertices.Add(new_vert);
            vertices_count++;
        }
    }
}
