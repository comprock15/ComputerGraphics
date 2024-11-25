﻿using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace LAB7
{
    internal class ZBuffer
    {
        private static int h, w;
        public static Bitmap ZBuff(double[,] matrix, ListBox.ObjectCollection polyhedrons, int width, int height)
        {
            //var polyhedrons = polyhedronss as List<Polyhedron>;
            h = height; w = width;
            var bmp = new Bitmap(width, height);

            double[,] z_buff = new double[width, height];
            for (int i = 0; i < width; i++) //x
                for (int j = 0; j < height; j++) //y
                {
                    z_buff[i, j] = double.MinValue;
                }

            double[,] cur_m;
            Random rand = new Random(42);
            for (var ind = 0; ind < polyhedrons.Count; ind++)
            {
                // Вычислим координаты вершин в зависимости от выбранной проекции
                var orig_verts = (polyhedrons[ind] as Polyhedron).vertices;
                List<Vertex> new_verts = new List<Vertex> { };
                foreach (var vert in orig_verts)
                {
                    cur_m = AffineTransformations.Multiply(new double[,] { { vert.x, vert.y, vert.z, 1 } }, matrix);
                    //new_verts.Add(new Vertex(cur_m[0, 0], cur_m[0, 1], cur_m[0, 2]));
                    new_verts.Add(new Vertex(cur_m[0, 0], cur_m[0, 1], vert.z));
                }
                
                // Разобъём грани на треугольники для более удобной растеризации
                var triangulated_faces = new List<List<Vertex>>();
                foreach (var face in (polyhedrons[ind] as Polyhedron).faces)
                {
                    
                    
                    //triangulated_faces = triangulated_faces.Concat(Triangulate(new_verts, face)).ToList();
                    triangulated_faces = Triangulate(new_verts, face);

                     Color c = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));


                    foreach (var triangg in triangulated_faces)
                    {
                        var triang = triangg.OrderBy(v => v.y).ToList();
                        var up = triang[0]; var mid = triang[1]; var bot = triang[2];
                       

                        double x1, y1, z1, x2, y2, z2;
                        for (var cur_y = up.y; cur_y <= mid.y; cur_y += 0.5)
                        {
                            x1 = FindXbyY(cur_y, up.x, up.y, mid.x, mid.y);
                            z1 = FindZbyY(cur_y, up.y, up.z, mid.y, mid.z);

                            x2 = FindXbyY(cur_y, up.x, up.y, bot.x, bot.y);
                            z2 = FindZbyY(cur_y, up.y, up.z, bot.y, bot.z);

                            if (x1 < x2)
                            {
                                for (double cur_x = x1; cur_x <= x2; cur_x += 0.5)
                                {
                                    double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
                                    if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
                                    {
                                        z_buff[(int)cur_x, (int)cur_y] = cur_z;
                                        bmp.SetPixel((int)cur_x, (int)cur_y, c);
                                    }
                                }
                            }
                            else
                            {
                                for (double cur_x = x1; cur_x >= x2; cur_x -= 0.5)
                                {
                                    double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
                                    if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
                                    {
                                        z_buff[(int)cur_x, (int)cur_y] = cur_z;
                                        bmp.SetPixel((int)cur_x, (int)cur_y, c);
                                    }
                                }
                            }
                        }
                        for (var cur_y = mid.y; cur_y <= bot.y; cur_y += 0.5)
                        {
                            x1 = FindXbyY(cur_y, mid.x, mid.y, bot.x, bot.y);
                            z1 = FindZbyY(cur_y, mid.y, mid.z, bot.y, bot.z);

                            x2 = FindXbyY(cur_y, up.x, up.y, bot.x, bot.y);
                            z2 = FindZbyY(cur_y, up.y, up.z, bot.y, bot.z);

                            if (x1 < x2)
                            {
                                for (double cur_x = x1; cur_x <= x2; cur_x += 0.5)
                                {
                                    double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
                                    if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
                                    {
                                        z_buff[(int)cur_x, (int)cur_y] = cur_z;
                                        bmp.SetPixel((int)cur_x, (int)cur_y, c);
                                    }
                                }
                            }
                            else
                            {
                                for (double cur_x = x1; cur_x >= x2; cur_x -= 0.5)
                                {
                                    double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
                                    if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
                                    {
                                        z_buff[(int)cur_x, (int)cur_y] = cur_z;
                                        bmp.SetPixel((int)cur_x, (int)cur_y, c);
                                    }
                                }
                            }

                        }


                    }
                }

                // Нарисуем рёбра 
                var edges = (polyhedrons[ind] as Polyhedron).edges;
                double x, y, z;
                for (var j = 0; j < edges.Count; j++)
                {
                    var v1 = new_verts[j];
                    foreach (var v_index in edges[j])
                    {
                        var v2 = new_verts[v_index];
                        if (v1.y > v2.y)
                        {
                            for (y = v1.y; y >= v2.y; y-= 0.01)
                            {
                                x = FindXbyY(y, v1.x, v1.y, v2.x, v2.y);
                                z = FindZbyY(y, v1.y, v1.z, v2.y, v2.z);
                                if (InLimits((int)x, (int)y) && z + 1 > z_buff[(int)x, (int)y])
                                {
                                    z_buff[(int)x, (int)y] = z;
                                    bmp.SetPixel((int)x, (int)y, Color.Black);
                                }
                            }
                        }
                        else
                        {
                            for (y = v1.y; y <= v2.y; y += 0.01)
                            {
                                x = FindXbyY(y, v1.x, v1.y, v2.x, v2.y);
                                z = FindZbyY(y, v1.y, v1.z, v2.y, v2.z);
                                if (InLimits((int)x, (int)y) && z + 1 > z_buff[(int)x, (int)y])
                                {
                                    z_buff[(int)x, (int)y] = z;
                                    bmp.SetPixel((int)x, (int)y, Color.Black);
                                }
                            }
                        }

                    }
                }
            }

            // Нарисуем координатные оси
            

            return bmp;
        }

        private static List<List<Vertex>> Triangulate(List<Vertex> all_verts, List<int> picked_verts)
        {
            List<List<Vertex>> triangulations = new List<List<Vertex>> { };
            for (int i = 2; i < picked_verts.Count; i++)
            {
                triangulations.Add(new List<Vertex> { all_verts[picked_verts[0]], all_verts[picked_verts[i - 1]], all_verts[picked_verts[i]] });
            }
            return triangulations;
        }

        private static List<Vertex> Rasterization(List<Vertex> triag)
        {
            
            triag.OrderBy(v => v.y);
            var up = triag[0]; var mid = triag[1]; var bot = triag[2];
            List<Vertex> all_triag_pix = new List<Vertex>();

            double y = up.y;
            double x1, y1, z1, x2, y2, z2;
            while (y <= mid.y)
            {
                // Граничные точки
                x1 = FindXbyY(y, up.x, up.y, mid.x, mid.y);
                z1 = FindZbyY(y, up.y, up.z, mid.y, mid.z);

                x2 = FindXbyY(y, up.x, up.y, bot.x, bot.y);
                z2 = FindZbyY(y, up.y, up.z, bot.y, bot.z);

                // Внутренние

                y += 0.5;
            }


            return all_triag_pix;
        }

        private static double FindXbyY(double cur_y, double x1, double y1, double x2, double y2)
        {
            if (y2 - y1 != 0)
                return (cur_y - y1) * (x2 - x1) / (y2 - y1) + x1;
            else
                return (cur_y - y1) * (x2 - x1) / 0.0001 + x1;
        }

        private static double FindZbyY(double cur_y,  double y1, double z1,  double y2, double z2)
        {
            if (y2 - y1 != 0)
                return (cur_y - y1) * (z2 - z1) / (y2 - y1) + z1;
            else
                return (cur_y - y1) * (z2 - z1) / 0.0001 + z1;
        }
        private static double FindZbyX(double cur_x, double x1, double z1, double x2, double z2)
        {
            if (x2 - x1 != 0)
                return (cur_x - x1) * (z2 - z1) / (x2 - x1) + z1;
            else
                return (cur_x - x1) * (z2 - z1) / 0.0001 + z1;
        }

        private static bool InLimits(int x, int y)
        {
            return x >= 0 && y >= 0 && x < w && y < h;
        }

    }
}
