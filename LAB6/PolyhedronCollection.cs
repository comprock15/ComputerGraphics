using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB6
{
    internal static class PolyhedronCollection
    {
        public static Polyhedron MakeTetrahedron() 
        {
            Vertex start = new Vertex(0, 0, 0);  //=(250, 150, 200)?
            double side_size = 150;

            List<Vertex> vertices = new List<Vertex>
            {
                start,
                new Vertex(start.x + side_size, start.y + 0,         start.z + side_size),
                new Vertex(start.x + side_size, start.y + side_size, start.z + 0),
                new Vertex(start.x + 0,         start.y + side_size, start.z + side_size),
            };

            List<List<int>> edges = new List<List<int>> {
                new List<int> { 1, 2, 3 }, //0
                new List<int> { 2, 3},     //1
                new List<int> { 3 },       //2
                new List<int> { }
            };
            return new Polyhedron(vertices, edges);            
        }

        public static Polyhedron MakeHexahedron()
        {
            return null;
        }

        public static Polyhedron MakeOctahedron()
        {
            return null;
        }

        public static Polyhedron MakeIcosahedron()
        {
            return null;
        }

        public static Polyhedron MakeDodecahedron()
        {
            return null;
        }

    }
}
