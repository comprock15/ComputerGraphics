﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB7 {
    /// <summary>
    /// Коллекция многогранников
    /// </summary>
    internal static class PolyhedronCollection
    {
        /// <summary>
        /// Построение тетраэдра
        /// </summary>
        /// <returns>Тетраэдр</returns>
        public static Polyhedron MakeTetrahedron() 
        {
            Vertex start = new Vertex(300, 300, 0);  //=(250, 150, 200)?
            double side_size = 150;

            List<Vertex> vertices = new List<Vertex>() {
                start,
                new Vertex(start.x + side_size, start.y + 0,         start.z + side_size),
                new Vertex(start.x + side_size, start.y + side_size, start.z + 0),
                new Vertex(start.x + 0,         start.y + side_size, start.z + side_size),
            };

            List<List<int>> edges = new List<List<int>>() {
                new List<int> { 1, 2, 3 }, //0
                new List<int> { 2, 3},     //1
                new List<int> { 3 },       //2
                new List<int> { }
            };
            return new Polyhedron(vertices, edges);            
        }

        /// <summary>
        /// Построение гексаэдра
        /// </summary>
        /// <returns>Гексаэдр</returns>
        public static Polyhedron MakeHexahedron()
        {
            Vertex start = new Vertex(300, 300, 0);  
            double side_size = 150;

            List<Vertex> vertices = new List<Vertex>() {
                start,
                new Vertex(start.x + side_size, start.y + 0,         start.z + 0),
                new Vertex(start.x + 0,         start.y + side_size, start.z + 0),
                new Vertex(start.x + 0,         start.y + 0,         start.z + side_size),

                new Vertex(start.x + side_size, start.y + side_size, start.z + 0),
                new Vertex(start.x + 0,         start.y + side_size, start.z + side_size),
                new Vertex(start.x + side_size, start.y + 0,         start.z + side_size),

                new Vertex(start.x + side_size, start.y + side_size, start.z + side_size)
            };

            List<List<int>> edges = new List<List<int>>() {
                new List<int> { 1, 2, 3 }, //0
                new List<int> { 4, 6 },    //1
                new List<int> { 4, 5 },    //2
                new List<int> { 5, 6 },    //3
                new List<int> { 7 },       //4
                new List<int> { 7 },       //5
                new List<int> { 7 },       //6
                new List<int> { }          //7
            };
            return new Polyhedron(vertices, edges);
        }

        /// <summary>
        /// Построение октаэдра
        /// </summary>
        /// <returns>Октаэдр</returns>
        public static Polyhedron MakeOctahedron()
        {
            Vertex start = new Vertex(300, 300, 0);
            double side_size = 150;

            List<Vertex> vertices = new List<Vertex>() {
                new Vertex(start.x + 0, start.y + side_size, start.z + 0),
                new Vertex(start.x + 0, start.y - side_size, start.z + 0),
                new Vertex(start.x + side_size, start.y + 0, start.z + 0),
                new Vertex(start.x - side_size, start.y + 0, start.z + 0),
                new Vertex(start.x + 0, start.y + 0, start.z + side_size),
                new Vertex(start.x + 0, start.y + 0, start.z - side_size)
            };

            List<List<int>> edges = new List<List<int>>() {
                new List<int> { 2, 3, 4, 5 }, //0
                new List<int> { 2, 3, 4, 5 }, //1
                new List<int> { 4, 5 },       //2
                new List<int> { 4, 5 },       //3
                new List<int> {   },          //4
                new List<int> {   },          //5
            };
            return new Polyhedron(vertices, edges);
        }

        /// <summary>
        /// Построение икосаэдра
        /// </summary>
        /// <returns>Икосаэдр</returns>
        public static Polyhedron MakeIcosahedron()
        {            
            Vertex start = new Vertex(300, 300, 0);
            double r = 100 * (1 + Math.Sqrt(5)) / 4; // радиус полувписанной окружности 
            double side_size = 50;

            List<Vertex> vertices = new List<Vertex>() {
                new Vertex(start.x + 0,         start.y - side_size, start.z - r),
                new Vertex(start.x + 0,         start.y + side_size, start.z - r),
                new Vertex(start.x + side_size, start.y + r,         start.z + 0),
                new Vertex(start.x + r,         start.y + 0,         start.z - side_size),
                new Vertex(start.x + side_size, start.y - r,         start.z + 0),
                new Vertex(start.x - side_size, start.y - r,         start.z + 0),
                new Vertex(start.x - r,         start.y + 0,         start.z - side_size),
                new Vertex(start.x - side_size, start.y + r,         start.z + 0),
                new Vertex(start.x + r,         start.y + 0,         start.z + side_size),
                new Vertex(start.x - r,         start.y + 0,         start.z + side_size),
                new Vertex(start.x + 0,         start.y - side_size, start.z + r),
                new Vertex(start.x + 0,         start.y + side_size, start.z + r),
            };

            List<List<int>> edges = new List<List<int>>() {
                new List<int> { 1, 3, 4, 5, 6 },
                new List<int> { 2, 3, 6, 7 },
                new List<int> { 3, 7, 8, 11 },
                new List<int> { 4, 8 },
                new List<int> { 5, 8, 10 },
                new List<int> { 6, 9, 10 },
                new List<int> { 7, 9 },
                new List<int> { 9, 11 },
                new List<int> { 10, 11 },
                new List<int> { 10, 11 },
                new List<int> { 11 },
                new List<int> {    },
            };
            return new Polyhedron(vertices, edges);
        }

        /// <summary>
        /// Построение додекаэдра
        /// </summary>
        /// <returns>Додекаэдр</returns>
        public static Polyhedron MakeDodecahedron() {
            Vertex start = new Vertex(300, 300, 0);
            double r = 100 * (3 + Math.Sqrt(5)) / 4; // радиус полувписанной окружности 
            double x = 100 * (1 + Math.Sqrt(5)) / 4; // половина стороны пятиугольника в сечении 
            double side_size = 50;

            List<Vertex> vertices = new List<Vertex>() {
                new Vertex(start.x + 0,         start.y - side_size, start.z - r),
                new Vertex(start.x + 0,         start.y + side_size, start.z - r),
                new Vertex(start.x + x,         start.y + x,         start.z - x),
                new Vertex(start.x + r,         start.y + 0,         start.z - side_size),
                new Vertex(start.x + x,         start.y - x,         start.z - x),
                new Vertex(start.x + side_size, start.y - r,         start.z + 0),
                new Vertex(start.x - side_size, start.y - r,         start.z + 0),
                new Vertex(start.x - x,         start.y - x,         start.z - x),
                new Vertex(start.x - r,         start.y + 0,         start.z - side_size),
                new Vertex(start.x - x,         start.y + x,         start.z - x),
                new Vertex(start.x - side_size, start.y + r,         start.z + 0),
                new Vertex(start.x + side_size, start.y + r,         start.z + 0),
                new Vertex(start.x - x,         start.y - x,         start.z + x),
                new Vertex(start.x + 0,         start.y - side_size, start.z + r),
                new Vertex(start.x + x,         start.y - x,         start.z + x),
                new Vertex(start.x + 0,         start.y + side_size, start.z + r),
                new Vertex(start.x - x,         start.y + x,         start.z + x),
                new Vertex(start.x + x,         start.y + x,         start.z + x),
                new Vertex(start.x - r,         start.y + 0,         start.z + side_size),
                new Vertex(start.x + r,         start.y + 0,         start.z + side_size)

            };

            List<List<int>> edges = new List<List<int>>() {
                new List<int> { 1, 4, 7 },
                new List<int> { 2, 9 },
                new List<int> { 3, 11 },
                new List<int> { 4, 19 },
                new List<int> { 5 },
                new List<int> { 6, 14 },
                new List<int> { 7, 12 },
                new List<int> { 8 },
                new List<int> { 9, 18 },
                new List<int> { 10 },
                new List<int> { 11, 16 },
                new List<int> { 17 },
                new List<int> { 13, 18 },
                new List<int> { 14, 15 },
                new List<int> { 19 },
                new List<int> { 16, 17 },
                new List<int> { 18 },
                new List<int> { 19 },
                new List<int> {    },
                new List<int> {    },
            };
            return new Polyhedron(vertices, edges);
        }

        public static Polyhedron MakeRotationFigure(string axis, int partitions_count, List<Vertex> forming_verts)
        {
            double angle = 360.0 / partitions_count;
            
            var all_verts = new List<Vertex>(forming_verts);
            var temp_poly = new Polyhedron(forming_verts, new List<List<int>> { });

            for (int i = 0; i < partitions_count - 1; i++)
            {
                switch (axis)
                {
                    case "OX":
                        AffineTransformations.RotationAboutXAxis(ref temp_poly, angle);
                        break;
                    case "OY":
                        AffineTransformations.RotationAboutYAxis(ref temp_poly, angle);
                        break;
                    case "OZ":
                        AffineTransformations.RotationAboutZAxis(ref temp_poly, angle);
                        break;
                    default:
                        break;
                }

                foreach (var vert in temp_poly.vertices)
                {
                    all_verts.Add(new Vertex(vert));
                }
            }

            var edgs = new List<List<int>> { new List<int> { } };
            for (int i = 1; i < all_verts.Count; i++)
            {
                if (i % forming_verts.Count != 0)
                {
                    edgs.Add(new List<int> { i-1 });
                }
                else
                {
                    edgs.Add(new List<int> { });
                }
            }

            for (int i = 0; i < edgs.Count - forming_verts.Count; i++)
            {
                edgs[i].Add(i + forming_verts.Count);
            }

            for (int i = 0; i < forming_verts.Count; i++)
            {
                edgs[edgs.Count - i - 1].Add(forming_verts.Count - i - 1);
            }

            var res_poly = new Polyhedron(all_verts, edgs);
            return res_poly;
        }
    }
}
