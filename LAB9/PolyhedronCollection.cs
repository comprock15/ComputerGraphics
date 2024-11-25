using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB9 {
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
            Vertex start = new Vertex(-75, -75, -75);  //=(250, 150, 200)?
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

            List<List<int>> faces = new List<List<int>>()
            {
                new List<int> { 0, 1, 2 },
                new List<int> { 0, 1, 3 },
                new List<int> { 0, 2, 3 },
                new List<int> { 1, 2, 3 }
            };

            Polyhedron polyhedron = new Polyhedron(vertices, edges, faces);
            polyhedron.SetName("Тетраэдр");
            return polyhedron;            
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

            List<List<int>> faces = new List<List<int>>()
            {
                new List<int> { 0, 1, 4, 2 },
                new List<int> { 0, 1, 6, 3 },
                new List<int> { 0, 2, 5, 3 },
                new List<int> { 7, 4, 1, 6 },
                new List<int> { 7, 4, 2, 5 },
                new List<int> { 7, 5, 3, 6 }
            };

            Polyhedron polyhedron = new Polyhedron(vertices, edges, faces);
            polyhedron.SetName("Гексаэдр");
            return polyhedron;
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

            List<List<int>> faces = new List<List<int>>()
            {
                new List<int> { 0, 2, 4 },
                new List<int> { 0, 4, 3 },
                new List<int> { 0, 3, 5 },
                new List<int> { 0, 5, 2 },
                new List<int> { 1, 2, 4 },
                new List<int> { 1, 4, 3 },
                new List<int> { 1, 3, 5 },
                new List<int> { 1, 5, 2 }
            };

            Polyhedron polyhedron = new Polyhedron(vertices, edges, faces);
            polyhedron.SetName("Октаэдр");
            return polyhedron;
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

            List<List<int>> faces = new List<List<int>>()
            {
                 new List<int> { 0, 1, 3 },
                 new List<int> { 0, 3, 4 },
                 new List<int> { 0, 4, 5},
                 new List<int> { 0, 5, 6 },
                 new List<int> { 0, 6, 1 },

                 new List<int> { 1, 2, 3 },
                 new List<int> { 2, 3, 8 },
                 new List<int> { 3, 8, 4 },
                 new List<int> { 8, 4, 10 },
                 new List<int> { 4, 10, 5 },
                 new List<int> { 10, 5, 9 },
                 new List<int> { 5, 9, 6 },
                 new List<int> { 9, 6, 7 },
                 new List<int> { 6, 7, 1 },
                 new List<int> { 7, 1, 2 },

                 new List<int> { 11, 10, 8 },
                 new List<int> { 11, 8, 2 },
                 new List<int> { 11, 2, 7 },
                 new List<int> { 11, 7, 9 },
                 new List<int> { 11, 9, 10 },
            };

            Polyhedron polyhedron = new Polyhedron(vertices, edges, faces);
            polyhedron.SetName("Икосаэдр");
            return polyhedron;
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

            List<List<int>> faces = new List<List<int>>()
            {
                new List<int> { 0, 1, 2, 3, 4 },

                new List<int> { 0, 1, 9, 8, 7 },
                new List<int> { 1, 2, 11, 10, 9 },
                new List<int> { 2, 3, 19, 17, 11 },
                new List<int> { 3, 4, 5, 14, 19 },
                new List<int> { 4, 0, 7, 6, 5 },

                new List<int> { 7, 6, 12, 18, 8 },
                new List<int> { 5, 6, 12, 13, 14 },
                new List<int> { 19, 14, 13, 15, 17 },
                new List<int> { 11, 17, 15, 16, 10 },
                new List<int> { 9, 10, 16, 18, 8 },

                new List<int> { 12, 13, 15, 16, 18 }
            };

            Polyhedron polyhedron = new Polyhedron(vertices, edges, faces);
            polyhedron.SetName("Додекаэдр");
            return polyhedron;
        }

        public static Polyhedron MakeRotationFigure(string axis, int partitions_count, List<Vertex> forming_verts)
        {
            int forming_count = forming_verts.Count;
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

            for (int i = 0; i < edgs.Count - forming_count; i++)
            {
                edgs[i].Add(i + forming_count);
            }

            for (int i = 0; i < forming_count; i++)
            {
                edgs[edgs.Count - i - 1].Add(forming_count - i - 1);
            }

            var faces = new List<List<int>> { new List<int> { }, new List<int> { } };

            // шапочки фигуры
            for (int i = 0; i < partitions_count; i++)
            {
                faces[0].Add(i * forming_count);
                faces[1].Add((i+1) * forming_count - 1 );
            }

            // боковые грани
            for (int i = 0; i < partitions_count - 1; i++)
            {
                for (int j = 0; j < forming_count - 1; j++)
                {
                    int init = j + i * forming_count;
                    faces.Add(new List<int> { init, init + 1, init + 1 + forming_count, init + forming_count }); 
                }
            }

            // боковые грани между 1 и последними точками
            for (int i = 0; i < forming_count - 1; i++)
            {
                faces.Add(new List<int> { i, i + 1, (i + 1) + forming_count * (partitions_count - 1), i + forming_count * (partitions_count - 1) });
            }


            var res_poly = new Polyhedron(all_verts, edgs, faces);
            res_poly.SetName("Фигура вращения");
            return res_poly;
        }
    }
}
