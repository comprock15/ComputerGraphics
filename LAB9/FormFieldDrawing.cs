using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.FSharp.Core.ByRefKinds;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace LAB9
{
    public partial class Form1 : Form
    {

        public void UltimateFieldRedraw()
        {
            if (cur_polyhedron == null)
                return;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            if (draw_mode == DrawingMode.EdgesOnly)
            {
                DrawEdgesOnly(ref bmp);
            }
            else
            {
                DrawZbuff(ref bmp);
            }


            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();
            pictureBox1.Image = bmp;

        }


        public void DrawEdgesOnly(ref Bitmap bmp)
        {
            foreach (var poly in all_polyhedrons)
            {
                var new_verts = poly.vertices.Select((v) => camera.to2D(v, proj_mode)).ToList();
                for (int i = 0; i < poly.edges.Count; i++)
                {
                    var v1 = new_verts[i];
                    for (int j = 0; j < poly.edges[i].Count; j++)
                    {
                        var v2 = new_verts[poly.edges[i][j]];
                        DrawHelpers.DrawLine(ref bmp, v1, v2);
                    }
                }
            }
        }



        public void DrawZbuff(ref Bitmap bmp)
        {
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;

            //
            double[,] z_buff = new double[width, height];
            for (int i = 0; i < width; i++) //x
                for (int j = 0; j < height; j++) //y
                {
                    z_buff[i, j] = double.MinValue;
                }


            for (int ind = 0; ind < all_polyhedrons.Count; ind++)
            {
                var transformedVertices = all_polyhedrons[ind].vertices.Select((v) => camera.to2D(v, proj_mode)).ToList();

                List<List<int>> visibleFaces;

                // Zbuff + Отсечение нелицевых граней 
                if (draw_mode == DrawingMode.InvisFacesCutZBUFF)
                {
                    visibleFaces = new List<List<int>> { };
                    var viewDirection = camera.cameraDir;
                    foreach (var face in all_polyhedrons[ind].faces)
                    {
                        var normal = BackfaceCulling.Normalize(face, transformedVertices);
                        var scalar = normal.x * viewDirection.x + normal.y * viewDirection.y + normal.z * viewDirection.z;
                        if (scalar < 0) // Грань видима, если нормаль направлена к наблюдателю (по оси Z)
                            visibleFaces.Add(face);
                    }
                }
                // Zbuff
                else
                    visibleFaces = all_polyhedrons[ind].faces;


                switch (light_mode)
                {
                    case LightingMode.Phong:
                        foreach (var face in visibleFaces)
                            PhongShading.ZBufferForPhong.DrawFace(face, ref bmp, ref z_buff, colors[ind], all_polyhedrons[ind], transformedVertices, lightPosition);
                        break;
                    case LightingMode.Guro:
                        foreach (var face in visibleFaces)
                            GouraudShading.ZBufferForGourard.DrawFace(face, ref bmp, ref z_buff, colors[ind], all_polyhedrons[ind], transformedVertices, lightPosition);

                        break;
                    default:
                        // if ТЕКСТУРИРОВАНИЕ ВКЛЮЧЕНО 
                        // метод
                        //else
                        // foreach (var face in visibleFaces)
                        foreach (var face in visibleFaces)
                            DrawFacePlain(face, ref bmp, ref z_buff, colors[ind], transformedVertices);
                        break;
                }


            }
        }

        internal static void DrawFacePlain(List<int> face, ref Bitmap bmp, ref double[,] z_buff, Color c, List<Vertex> new_verts)
        {
            var triangulated_faces = ZBuffer.Triangulate(new_verts, face);

            foreach (var triangg in triangulated_faces)
            {
                ZBuffer.DrawTriang(triangg, ref bmp, ref z_buff, c);
            }

        }



        class DrawHelpers
        {

            public static double FindXbyY(double cur_y, double x1, double y1, double x2, double y2)
            {
                if (y2 - y1 != 0)
                    return (cur_y - y1) * (x2 - x1) / (y2 - y1) + x1;
                else
                    return (cur_y - y1) * (x2 - x1) / 0.0001 + x1;
            }
            public static double FindYbyX(double cur_x, double x1, double y1, double x2, double y2)
            {
                if (x2 - x1 != 0)
                    return (cur_x - x1) * (y2 - y1) / (x2 - x1) + y1;
                else
                    return (cur_x - x1) * (y2 - y1) / 0.0001 + y1;
            }

            public static double FindZbyY(double cur_y, double y1, double z1, double y2, double z2)
            {
                if (y2 - y1 != 0)
                    return (cur_y - y1) * (z2 - z1) / (y2 - y1) + z1;
                else
                    return (cur_y - y1) * (z2 - z1) / 0.0001 + z1;
            }
            public static double FindZbyX(double cur_x, double x1, double z1, double x2, double z2)
            {
                if (x2 - x1 != 0)
                    return (cur_x - x1) * (z2 - z1) / (x2 - x1) + z1;
                else
                    return (cur_x - x1) * (z2 - z1) / 0.0001 + z1;
            }

            public static bool InLimits(int x, int y, int lim_w, int lim_h)
            {
                return x >= 0 && y >= 0 && x < lim_w && y < lim_h;
            }

            public static void DrawLine(ref Bitmap bmp, Vertex v1, Vertex v2)
            {
                var x1 = v1.x; var y1 = v1.y;
                var x2 = v2.x; var y2 = v2.y;

                if (Math.Abs(x1 - x2) > Math.Abs(y1 - y2)) // расстояние между х больше чем между у
                {
                    var max_x = Math.Max(x1, x2);
                    var min_x = Math.Min(x1, x2);
                    for (var x = min_x; x <= max_x; x++)
                    {
                        var y = FindYbyX(x, x1, y1, x2, y2);
                        if (DrawHelpers.InLimits((int)x, (int)y, bmp.Width, bmp.Height))
                            bmp.SetPixel((int)x, (int)y, Color.Black);
                    }
                }
                else
                {
                    var max_y = Math.Max(y1, y2);
                    var min_y = Math.Min(y1, y2);
                    for (var y = min_y; y <= max_y; y++)
                    {
                        var x = FindXbyY(y, x1, y1, x2, y2);
                        if (DrawHelpers.InLimits((int)x, (int)y, bmp.Width, bmp.Height))
                            bmp.SetPixel((int)x, (int)y, Color.Black);
                    }
                }
            }


            public static List<List<int>> Triangulate(List<Vertex> all_verts, List<int> picked_verts)
            {
                List<List<int>> triangulations = new List<List<int>> { };
                for (int i = 2; i < picked_verts.Count; i++)
                {
                    triangulations.Add(new List<int> { picked_verts[0], picked_verts[i - 1], picked_verts[i] });
                }
                return triangulations;
            }

            public static Color MultColor(double c, Color color)
            {
                return Color.FromArgb(Math.Max(0, Math.Min((int)(c * color.R), 255)),
                                      Math.Max(0, Math.Min((int)(c * color.G), 255)),
                                      Math.Max(0, Math.Min((int)(c * color.B), 255)));
            }

            public static Color SumColor(params Color[] colors)
            {
                int r = 0, g = 0, b = 0;
                foreach (Color c in colors)
                {
                    r += c.R;
                    g += c.G;
                    b += c.B;
                }
                return Color.FromArgb(Math.Max(0, Math.Min(r, 255)),
                                      Math.Max(0, Math.Min(g, 255)),
                                      Math.Max(0, Math.Min(b, 255)));
            }

        }

    }
}

