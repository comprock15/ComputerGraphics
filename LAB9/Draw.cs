//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Drawing;
//using System.Windows.Forms;

//static class DrawingShading
//{
//    public static Bitmap UseShading(double[,] matrix, ListBox.ObjectCollection polyhedrons, int width, int height, Vector3 lightpos)
//    {
//        return ZBufferForPhong.ZBuff(matrix, polyhedrons, width, height, lightpos);
//    }

//    public static Color ToonShadingModel(Vector3 n, Vector3 l, Color diffColor)
//    {
//        Vector3 n2 = n.Normalized();
//        Vector3 l2 = l.Normalized();
//        double diff = 0.2 + Math.Max(n2.Dot(l2), 0.0);
//        Color clr;

//        if (diff < 0.4)
//        {
//            clr = MultColor(0.3, diffColor);
//        }
//        else if (diff < 0.7)
//        {
//            clr = diffColor;
//        }
//        else //if (diff < 1.15)
//        {
//            clr = MultColor(1.3, diffColor);
//        }
//        //else  // Блики
//        //{
//        //    clr = Color.White;
//        //}

//        return clr;
//    }

//    private static Color MultColor(double c, Color color)
//    {
//        return Color.FromArgb(Math.Min((int)(c * color.R), 255), Math.Min((int)(c * color.G), 255), Math.Min((int)(c * color.R), 255));
//    }

//    private static Vector3 Interpolate(Vector3 p, Vector3 v1, Vector3 n1, Vector3 v2, Vector3 n2, Vector3 v3, Vector3 n3)
//    {
//        double w1 = 0, w2 = 0, w3 = 0;

//        w1 = ((v2.y - v3.y) * (p.x - v3.x) + (v3.x - v2.x) * (p.y - v3.y)) /
//             ((v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y));

//        w2 = ((v3.y - v1.y) * (p.x - v3.x) + (v1.x - v3.x) * (p.y - v3.y)) /
//             ((v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y));

//        w3 = 1 - w1 - w2;
//        return (w1 * n1 + w2 * n2 + w3 * n3).Normalize();
//    }

//    private static class ZBufferForPhong
//    {
//        private static int h, w;
//        public static Bitmap ZBuff(double[,] matrix, ListBox.ObjectCollection polyhedrons, int width, int height, Vector3 lightpos)
//        {
//            //var polyhedrons = polyhedronss as List<Polyhedron>;
//            h = height; w = width;
//            var bmp = new Bitmap(width, height);

//            double[,] z_buff = new double[width, height];
//            for (int i = 0; i < width; i++) //x
//                for (int j = 0; j < height; j++) //y
//                {
//                    z_buff[i, j] = double.MinValue;
//                }

//            double[,] cur_m;
//            Random rand = new Random(42);
//            for (var ind = 0; ind < polyhedrons.Count; ind++)
//            {
//                // Вычислим координаты вершин в зависимости от выбранной проекции
//                var orig_verts = (polyhedrons[ind] as Polyhedron).vertices;
//                List<Vertex> new_verts = new List<Vertex> { };
//                foreach (var vert in orig_verts)
//                {
//                    cur_m = AffineTransformations.Multiply(new double[,] { { vert.x, vert.y, vert.z, 1 } }, matrix);
//                    //new_verts.Add(new Vertex(cur_m[0, 0], cur_m[0, 1], cur_m[0, 2]));
//                    new_verts.Add(new Vertex(cur_m[0, 0], cur_m[0, 1], vert.z));
//                }
//                Color c = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));

//                //Color c = Color.FromArgb(150, 150, 150);
//                // Разобъём грани на треугольники для более удобной растеризации
//                var triangulated_faces = new List<List<int>>();
//                foreach (var face in (polyhedrons[ind] as Polyhedron).faces)
//                {
//                    triangulated_faces = Triangulate(new_verts, face);




//                    foreach (var triangg in triangulated_faces)
//                    {
//                        DrawTriang(triangg, ref bmp, ref z_buff, c, polyhedrons[ind] as Polyhedron, new_verts, lightpos);
//                    }
//                }
//            }

//            return bmp;
//        }

//        public static void DrawTriang(List<int> triangg, ref Bitmap bmp, ref double[,] z_buff, Color c, Polyhedron poly, List<Vertex> new_verts, Vector3 lightpos)
//        {
//            var triang = triangg.Select(v => new_verts[v]).OrderBy(v => v.y).ToList();
//            var up = triang[0]; var mid = triang[1]; var bot = triang[2];

//            Vector3 v1 = new Vector3(new_verts[triangg[0]].x, new_verts[triangg[0]].y, new_verts[triangg[0]].z);
//            Vector3 n1 = new Vector3(poly.normals[triangg[0]].x, poly.normals[triangg[0]].y, poly.normals[triangg[0]].z).Normalized();
//            Vector3 v2 = new Vector3(new_verts[triangg[1]].x, new_verts[triangg[1]].y, new_verts[triangg[1]].z);
//            Vector3 n2 = new Vector3(poly.normals[triangg[1]].x, poly.normals[triangg[1]].y, poly.normals[triangg[1]].z).Normalized();
//            Vector3 v3 = new Vector3(new_verts[triangg[2]].x, new_verts[triangg[2]].y, new_verts[triangg[2]].z);
//            Vector3 n3 = new Vector3(poly.normals[triangg[2]].x, poly.normals[triangg[2]].y, poly.normals[triangg[2]].z).Normalized();

//            double x1, y1, z1, x2, y2, z2;
//            for (var cur_y = up.y; cur_y <= mid.y; cur_y += 0.5)
//            {
//                x1 = FindXbyY(cur_y, up.x, up.y, mid.x, mid.y);
//                z1 = FindZbyY(cur_y, up.y, up.z, mid.y, mid.z);

//                x2 = FindXbyY(cur_y, up.x, up.y, bot.x, bot.y);
//                z2 = FindZbyY(cur_y, up.y, up.z, bot.y, bot.z);

//                if (x1 < x2)
//                {
//                    for (double cur_x = x1; cur_x <= x2; cur_x += 0.5)
//                    {
//                        double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
//                        if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
//                        {
//                            z_buff[(int)cur_x, (int)cur_y] = cur_z;
//                            Vector3 p = new Vector3(cur_x, cur_y, cur_z);
//                            Vector3 norm = Interpolate(p, v1, n1, v2, n2, v3, n3);
//                            bmp.SetPixel((int)cur_x, (int)cur_y, ToonShadingModel(norm, (lightpos - p), c));
//                        }
//                    }
//                }
//                else
//                {
//                    for (double cur_x = x1; cur_x >= x2; cur_x -= 0.5)
//                    {
//                        double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
//                        if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
//                        {
//                            z_buff[(int)cur_x, (int)cur_y] = cur_z;
//                            Vector3 p = new Vector3(cur_x, cur_y, cur_z);
//                            Vector3 norm = Interpolate(p, v1, n1, v2, n2, v3, n3);
//                            bmp.SetPixel((int)cur_x, (int)cur_y, ToonShadingModel(norm, (lightpos - p), c));
//                        }
//                    }
//                }
//            }
//            for (var cur_y = mid.y; cur_y <= bot.y; cur_y += 0.5)
//            {
//                x1 = FindXbyY(cur_y, mid.x, mid.y, bot.x, bot.y);
//                z1 = FindZbyY(cur_y, mid.y, mid.z, bot.y, bot.z);

//                x2 = FindXbyY(cur_y, up.x, up.y, bot.x, bot.y);
//                z2 = FindZbyY(cur_y, up.y, up.z, bot.y, bot.z);

//                if (x1 < x2)
//                {
//                    for (double cur_x = x1; cur_x <= x2; cur_x += 0.5)
//                    {
//                        double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
//                        if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
//                        {
//                            z_buff[(int)cur_x, (int)cur_y] = cur_z;
//                            Vector3 p = new Vector3(cur_x, cur_y, cur_z);
//                            Vector3 norm = Interpolate(p, v1, n1, v2, n2, v3, n3);
//                            bmp.SetPixel((int)cur_x, (int)cur_y, ToonShadingModel(norm, (lightpos - p), c));
//                        }
//                    }
//                }
//                else
//                {
//                    for (double cur_x = x1; cur_x >= x2; cur_x -= 0.5)
//                    {
//                        double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
//                        if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
//                        {
//                            z_buff[(int)cur_x, (int)cur_y] = cur_z;
//                            Vector3 p = new Vector3(cur_x, cur_y, cur_z);
//                            Vector3 norm = Interpolate(p, v1, n1, v2, n2, v3, n3);
//                            bmp.SetPixel((int)cur_x, (int)cur_y, ToonShadingModel(norm, (lightpos - p), c));
//                        }
//                    }
//                }

//            }


//        }

//        public static List<List<int>> Triangulate(List<Vertex> all_verts, List<int> picked_verts)
//        {
//            List<List<int>> triangulations = new List<List<int>> { };
//            for (int i = 2; i < picked_verts.Count; i++)
//            {
//                triangulations.Add(new List<int> { picked_verts[0], picked_verts[i - 1], picked_verts[i] });
//            }
//            return triangulations;
//        }

//        private static List<Vertex> Rasterization(List<Vertex> triag)
//        {

//            triag.OrderBy(v => v.y);
//            var up = triag[0]; var mid = triag[1]; var bot = triag[2];
//            List<Vertex> all_triag_pix = new List<Vertex>();

//            double y = up.y;
//            double x1, y1, z1, x2, y2, z2;
//            while (y <= mid.y)
//            {
//                // Граничные точки
//                x1 = FindXbyY(y, up.x, up.y, mid.x, mid.y);
//                z1 = FindZbyY(y, up.y, up.z, mid.y, mid.z);

//                x2 = FindXbyY(y, up.x, up.y, bot.x, bot.y);
//                z2 = FindZbyY(y, up.y, up.z, bot.y, bot.z);

//                // Внутренние

//                y += 0.5;
//            }


//            return all_triag_pix;
//        }

//        private static double FindXbyY(double cur_y, double x1, double y1, double x2, double y2)
//        {
//            if (y2 - y1 != 0)
//                return (cur_y - y1) * (x2 - x1) / (y2 - y1) + x1;
//            else
//                return (cur_y - y1) * (x2 - x1) / 0.0001 + x1;
//        }

//        private static double FindZbyY(double cur_y, double y1, double z1, double y2, double z2)
//        {
//            if (y2 - y1 != 0)
//                return (cur_y - y1) * (z2 - z1) / (y2 - y1) + z1;
//            else
//                return (cur_y - y1) * (z2 - z1) / 0.0001 + z1;
//        }
//        private static double FindZbyX(double cur_x, double x1, double z1, double x2, double z2)
//        {
//            if (x2 - x1 != 0)
//                return (cur_x - x1) * (z2 - z1) / (x2 - x1) + z1;
//            else
//                return (cur_x - x1) * (z2 - z1) / 0.0001 + z1;
//        }

//        private static bool InLimits(int x, int y)
//        {
//            return x >= 0 && y >= 0 && x < w && y < h;
//        }

//    }
//}

//static class GouraudShading
//{
//    public static Bitmap UseShading(double[,] matrix, ListBox.ObjectCollection polyhedrons, int width, int height, Vector3 lightpos)
//    {
//        return ZBufferForGourard.ZBuff(matrix, polyhedrons, width, height, lightpos);
//    }

//    public static Color LambertModel(Vector3 n, Vector3 l, Color diffColor)
//    {
//        Vector3 n2 = n.Normalized();
//        Vector3 l2 = l.Normalized();
//        double diff = 3 * Math.Max(n2.Dot(l2), 0.0);
//        Color clr;

//        clr = MultColor(diff, diffColor);

//        return clr;
//    }

//    private static Color MultColor(double c, Color color)
//    {
//        return Color.FromArgb(Math.Max(0, Math.Min((int)(c * color.R), 255)),
//                              Math.Max(0, Math.Min((int)(c * color.G), 255)),
//                              Math.Max(0, Math.Min((int)(c * color.B), 255)));
//    }

//    private static Color SumColor(params Color[] colors)
//    {
//        int r = 0, g = 0, b = 0;
//        foreach (Color c in colors)
//        {
//            r += c.R;
//            g += c.G;
//            b += c.B;
//        }
//        return Color.FromArgb(Math.Max(0, Math.Min(r, 255)),
//                              Math.Max(0, Math.Min(g, 255)),
//                              Math.Max(0, Math.Min(b, 255)));
//    }

//    private static Color Interpolate(Vector3 p, Vector3 v1, Color c1, Vector3 v2, Color c2, Vector3 v3, Color c3)
//    {
//        double w1 = 0, w2 = 0, w3 = 0;

//        w1 = ((v2.y - v3.y) * (p.x - v3.x) + (v3.x - v2.x) * (p.y - v3.y)) /
//             ((v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y));

//        w2 = ((v3.y - v1.y) * (p.x - v3.x) + (v1.x - v3.x) * (p.y - v3.y)) /
//             ((v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y));

//        w3 = 1 - w1 - w2;
//        return SumColor(MultColor(w1, c1), MultColor(w2, c2), MultColor(w3, c3));
//    }

   
//        private static int h, w;
//        public static Bitmap ZBuff(double[,] matrix, ListBox.ObjectCollection polyhedrons, int width, int height, Vector3 lightpos)
//        {
//            //var polyhedrons = polyhedronss as List<Polyhedron>;
//            h = height; w = width;
//            var bmp = new Bitmap(width, height);

//            double[,] z_buff = new double[width, height];
//            for (int i = 0; i < width; i++) //x
//                for (int j = 0; j < height; j++) //y
//                {
//                    z_buff[i, j] = double.MinValue;
//                }

//            double[,] cur_m;
//            Random rand = new Random(42);
//            for (var ind = 0; ind < polyhedrons.Count; ind++)
//            {
//                Color c = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));

//                // Вычисляем освещенность в вершинах
//                Polyhedron polyhedron = polyhedrons[ind] as Polyhedron;
//                List<Color> verticesColors = new List<Color>(polyhedron.vertices.Count);
//                for (int i = 0; i < polyhedron.vertices.Count; ++i)
//                {
//                    Vector3 v = new Vector3(polyhedron.vertices[i].x, polyhedron.vertices[i].y, polyhedron.vertices[i].z);
//                    Vector3 n = new Vector3(polyhedron.normals[i].x, polyhedron.normals[i].y, polyhedron.normals[i].z);
//                    verticesColors.Add(LambertModel(n, (lightpos - v), c));
//                }

//                // Вычислим координаты вершин в зависимости от выбранной проекции
//                var orig_verts = (polyhedrons[ind] as Polyhedron).vertices;
//                List<Vertex> new_verts = new List<Vertex> { };
//                foreach (var vert in orig_verts)
//                {
//                    cur_m = AffineTransformations.Multiply(new double[,] { { vert.x, vert.y, vert.z, 1 } }, matrix);
//                    //new_verts.Add(new Vertex(cur_m[0, 0], cur_m[0, 1], cur_m[0, 2]));
//                    new_verts.Add(new Vertex(cur_m[0, 0], cur_m[0, 1], vert.z));
//                }

//                //Color c = Color.FromArgb(150, 150, 150);
//                // Разобъём грани на треугольники для более удобной растеризации
//                var triangulated_faces = new List<List<int>>();
//                foreach (var face in (polyhedrons[ind] as Polyhedron).faces)
//                {
//                    triangulated_faces = Triangulate(new_verts, face);




//                    foreach (var triangg in triangulated_faces)
//                    {
//                        DrawTriang(triangg, ref bmp, ref z_buff, polyhedrons[ind] as Polyhedron, new_verts, verticesColors, lightpos);
//                    }
//                }
//            }

//            return bmp;
//        }

//        public static void DrawTriang(List<int> triangg, ref Bitmap bmp, ref double[,] z_buff, Polyhedron poly, List<Vertex> new_verts, List<Color> verticesColors, Vector3 lightpos)
//        {
//            var triang = triangg.Select(v => new_verts[v]).OrderBy(v => v.y).ToList();
//            var up = triang[0]; var mid = triang[1]; var bot = triang[2];

//            Vector3 v1 = new Vector3(new_verts[triangg[0]].x, new_verts[triangg[0]].y, new_verts[triangg[0]].z);
//            Color c1 = verticesColors[triangg[0]];
//            Vector3 v2 = new Vector3(new_verts[triangg[1]].x, new_verts[triangg[1]].y, new_verts[triangg[1]].z);
//            Color c2 = verticesColors[triangg[1]];
//            Vector3 v3 = new Vector3(new_verts[triangg[2]].x, new_verts[triangg[2]].y, new_verts[triangg[2]].z);
//            Color c3 = verticesColors[triangg[2]];

//            double x1, y1, z1, x2, y2, z2;
//            for (var cur_y = up.y; cur_y <= mid.y; cur_y += 0.5)
//            {
//                x1 = FindXbyY(cur_y, up.x, up.y, mid.x, mid.y);
//                z1 = FindZbyY(cur_y, up.y, up.z, mid.y, mid.z);

//                x2 = FindXbyY(cur_y, up.x, up.y, bot.x, bot.y);
//                z2 = FindZbyY(cur_y, up.y, up.z, bot.y, bot.z);

//                if (x1 < x2)
//                {
//                    for (double cur_x = x1; cur_x <= x2; cur_x += 0.5)
//                    {
//                        double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
//                        if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
//                        {
//                            z_buff[(int)cur_x, (int)cur_y] = cur_z;
//                            Vector3 p = new Vector3(cur_x, cur_y, cur_z);
//                            Color pixelColor = Interpolate(p, v1, c1, v2, c2, v3, c3);
//                            bmp.SetPixel((int)cur_x, (int)cur_y, pixelColor);
//                        }
//                    }
//                }
//                else
//                {
//                    for (double cur_x = x1; cur_x >= x2; cur_x -= 0.5)
//                    {
//                        double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
//                        if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
//                        {
//                            z_buff[(int)cur_x, (int)cur_y] = cur_z;
//                            Vector3 p = new Vector3(cur_x, cur_y, cur_z);
//                            Color pixelColor = Interpolate(p, v1, c1, v2, c2, v3, c3);
//                            bmp.SetPixel((int)cur_x, (int)cur_y, pixelColor);
//                        }
//                    }
//                }
//            }
//            for (var cur_y = mid.y; cur_y <= bot.y; cur_y += 0.5)
//            {
//                x1 = FindXbyY(cur_y, mid.x, mid.y, bot.x, bot.y);
//                z1 = FindZbyY(cur_y, mid.y, mid.z, bot.y, bot.z);

//                x2 = FindXbyY(cur_y, up.x, up.y, bot.x, bot.y);
//                z2 = FindZbyY(cur_y, up.y, up.z, bot.y, bot.z);

//                if (x1 < x2)
//                {
//                    for (double cur_x = x1; cur_x <= x2; cur_x += 0.5)
//                    {
//                        double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
//                        if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
//                        {
//                            z_buff[(int)cur_x, (int)cur_y] = cur_z;
//                            Vector3 p = new Vector3(cur_x, cur_y, cur_z);
//                            Color pixelColor = Interpolate(p, v1, c1, v2, c2, v3, c3);
//                            bmp.SetPixel((int)cur_x, (int)cur_y, pixelColor);
//                        }
//                    }
//                }
//                else
//                {
//                    for (double cur_x = x1; cur_x >= x2; cur_x -= 0.5)
//                    {
//                        double cur_z = FindZbyX(cur_x, x1, z1, x2, z2);
//                        if (InLimits((int)cur_x, (int)cur_y) && cur_z > z_buff[(int)cur_x, (int)cur_y])
//                        {
//                            z_buff[(int)cur_x, (int)cur_y] = cur_z;
//                            Vector3 p = new Vector3(cur_x, cur_y, cur_z);
//                            Color pixelColor = Interpolate(p, v1, c1, v2, c2, v3, c3);
//                            bmp.SetPixel((int)cur_x, (int)cur_y, pixelColor);
//                        }
//                    }
//                }

//            }


//        }

        

//    }
//}