using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

                foreach (var face in visibleFaces)
                {
                    DrawFace(ref bmp, face, transformedVertices, ind, z_buff);
                    
                }
            }
        }

        internal void DrawFace(ref Bitmap bmp, List<int> face, List<Vertex> all_vertices, int poly_ind, double[,] z_buff)
        {
            //var triangulated_face = DrawHelpers.Triangulate(vertices, face);

            for (int i = 2; i < face.Count; i++)
            {
                var trinag_verts = new List<Vertex> { all_vertices[face[0]], all_vertices[face[i - 1]], all_vertices[face[i]] };
                //  var triang_verts_inds = new List<int> { face[0], face[i - 1], face[i] };
                var triang_verts_inds = new List<int> { 0, i - 1, i };

                var triang = new List<Tuple<Vertex, int>> { new Tuple<Vertex, int> (all_vertices[face[0]], 0),
                                                            new Tuple<Vertex, int> (all_vertices[face[i-1]], i-1),
                                                            new Tuple<Vertex, int> (all_vertices[face[i]], i)
                };
                DrawTriang(ref bmp, triang, poly_ind, z_buff);
            }

            

            // здесь может быть ветка с реализацией для текстурок или же ещё глубже
        }

        internal void DrawTriang(ref Bitmap bmp, List<Tuple<Vertex, int>> triang, int poly_ind, double[,] z_buff)
        {
            Color base_color = colors[poly_ind];

            triang = triang.OrderBy(elem => elem.Item1.y).ToList();
            
            Vector3 v1 = new Vector3(triang[0].Item1);
            Vector3 v2 = new Vector3(triang[1].Item1);
            Vector3 v3 = new Vector3(triang[2].Item1);

            Vector3 n1 = new Vector3(all_polyhedrons[poly_ind].normals[triang[0].Item2]).Normalized();
            Vector3 n2 = new Vector3(all_polyhedrons[poly_ind].normals[triang[1].Item2]).Normalized();
            Vector3 n3 = new Vector3(all_polyhedrons[poly_ind].normals[triang[2].Item2]).Normalized();

            Color c1 = DrawHelpers.MultColor(3 * Math.Max(n1.Dot(lightPosition - v1), 0.0), base_color);
            Color c2 = DrawHelpers.MultColor(3 * Math.Max(n2.Dot(lightPosition - v2), 0.0), base_color);
            Color c3 = DrawHelpers.MultColor(3 * Math.Max(n3.Dot(lightPosition - v3), 0.0), base_color);

            var up = v1; var mid = v2; var bot = v3;

            double x1, y1, z1, x2, y2, z2;
            for (var cur_y = up.y; cur_y <= mid.y; cur_y += 0.5)
            {
                x1 = DrawHelpers.FindXbyY(cur_y, up.x, up.y, mid.x, mid.y);
                z1 = DrawHelpers.FindZbyY(cur_y, up.y, up.z, mid.y, mid.z);

                x2 = DrawHelpers.FindXbyY(cur_y, up.x, up.y, bot.x, bot.y);
                z2 = DrawHelpers.FindZbyY(cur_y, up.y, up.z, bot.y, bot.z);

                var min_x = Math.Min(x1, x2);
                var max_x = Math.Max(x1, x2);

                for (double cur_x = min_x; cur_x <= max_x; cur_x += 0.5)
                {
                    double cur_z = DrawHelpers.FindZbyX(cur_x, x1, z1, x2, z2);
                    if (DrawHelpers.InLimits((int)cur_x, (int)cur_y, bmp.Width, bmp.Height) && cur_z > z_buff[(int)cur_x, (int)cur_y])
                    {
                       
                        z_buff[(int)cur_x, (int)cur_y] = cur_z;
                        Vector3 p = new Vector3(cur_x, cur_y, cur_z);

                        Color pixelColor;
                        switch (light_mode)
                        {
                            case LightingMode.Phong:
                                pixelColor = Shading.ToonModel(p, lightPosition, base_color, v1, n1, v2, n2, v3, n3, c1, c2, c3);
                                break;
                            case LightingMode.Guro:
                                pixelColor = Shading.LambertModel(p, lightPosition, base_color, v1, n1, v2, n2, v3, n3, c1, c2, c3);
                                break;
                            default:
                                pixelColor = base_color; 
                                break;
                        }
                        
                        
                        bmp.SetPixel((int)cur_x, (int)cur_y, pixelColor);
                    }
                }

                
            }
            for (var cur_y = mid.y; cur_y <= bot.y; cur_y += 0.5)
            {
                x1 = DrawHelpers.FindXbyY(cur_y, mid.x, mid.y, bot.x, bot.y);
                z1 = DrawHelpers.FindZbyY(cur_y, mid.y, mid.z, bot.y, bot.z);

                x2 = DrawHelpers.FindXbyY(cur_y, up.x, up.y, bot.x, bot.y);
                z2 = DrawHelpers.FindZbyY(cur_y, up.y, up.z, bot.y, bot.z);

                var min_x = Math.Min(x1, x2);
                var max_x = Math.Max(x1, x2);

                for (double cur_x = min_x; cur_x <= max_x; cur_x += 0.5)
                {
                    double cur_z = DrawHelpers.FindZbyX(cur_x, x1, z1, x2, z2);
                    if (DrawHelpers.InLimits((int)cur_x, (int)cur_y, bmp.Width, bmp.Height) && cur_z > z_buff[(int)cur_x, (int)cur_y])
                    {
                        z_buff[(int)cur_x, (int)cur_y] = cur_z;
                        Vector3 p = new Vector3(cur_x, cur_y, cur_z);

                        Color pixelColor;
                        switch (light_mode)
                        {
                            case LightingMode.Phong:
                                pixelColor = Shading.ToonModel(p,lightPosition,base_color, v1,n1,v2,n2,v3,n3,c1,c2,c3);
                                break;
                            case LightingMode.Guro:
                                pixelColor = Shading.LambertModel(p, lightPosition, base_color, v1, n1, v2, n2, v3, n3, c1, c2, c3);
                                break;
                            default:
                                pixelColor = base_color;
                                break;
                        }


                        bmp.SetPixel((int)cur_x, (int)cur_y, pixelColor);
                    }
                }
            }

        }

        class Shading
        {
            public static Color ToonModel(Vector3 p, Vector3 lightpos, Color base_color, Vector3 v1, Vector3 n1, Vector3 v2, Vector3 n2, Vector3 v3, Vector3 n3, Color c1, Color c2, Color c3)
            {
                
                double w1 = 0, w2 = 0, w3 = 0;

                w1 = ((v2.y - v3.y) * (p.x - v3.x) + (v3.x - v2.x) * (p.y - v3.y)) /
                     ((v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y));

                w2 = ((v3.y - v1.y) * (p.x - v3.x) + (v1.x - v3.x) * (p.y - v3.y)) /
                     ((v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y));

                w3 = 1 - w1 - w2;

                var norm = ((w1 * n1 + w2 * n2 + w3 * n3).Normalize()).Normalized();

                var l = (lightpos - p).Normalized();
                double diff = 0.2 + Math.Max(norm.Dot(l), 0.0);
                Color clr;

                if (diff < 0.4)
                {
                    clr = DrawHelpers.MultColor(0.3, base_color);
                }
                else if (diff < 0.7)
                {
                    clr = base_color;
                }
                else //if (diff < 1.15)
                {
                    clr = DrawHelpers.MultColor(1.3, base_color);
                }
                //else  // Блики
                //{
                //    clr = Color.White;
                //}

                return clr;

            }

            public static Color LambertModel(Vector3 p, Vector3 lightpos, Color base_color, Vector3 v1, Vector3 n1, Vector3 v2, Vector3 n2, Vector3 v3, Vector3 n3, Color c1, Color c2, Color c3)
            {
                
                double w1 = 0, w2 = 0, w3 = 0;

                w1 = ((v2.y - v3.y) * (p.x - v3.x) + (v3.x - v2.x) * (p.y - v3.y)) /
                     ((v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y));

                w2 = ((v3.y - v1.y) * (p.x - v3.x) + (v1.x - v3.x) * (p.y - v3.y)) /
                     ((v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y));

                w3 = 1 - w1 - w2;
                return DrawHelpers.SumColor(DrawHelpers.MultColor(w1, c1), DrawHelpers.MultColor(w2, c2), DrawHelpers.MultColor(w3, c3));

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
                        bmp.SetPixel((int)x, (int)y, Color.Black);
                    }
                }
            }

            public static List<List<Vertex>> Triangulate(List<Vertex> all_verts, List<int> picked_verts)
            {
                List<List<Vertex>> triangulations = new List<List<Vertex>> { };
                for (int i = 2; i < picked_verts.Count; i++)
                {
                    triangulations.Add(new List<Vertex> { all_verts[picked_verts[0]], all_verts[picked_verts[i - 1]], all_verts[picked_verts[i]] });
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

       

        /// <summary>
        /// Отрисовывает поле в зависимости от выбранного типа проекции
        /// </summary>
        //public void RedrawField()
        //{
        //    if (cur_polyhedron == null) return;

        //    double[,] matrix = GetProjectionMatrix(projectionModeSelector.SelectedIndex);

        //    label32.Text = cur_polyhedron.Center().ToString();

        //    Bitmap bmp = null;
            
        //    // Освещение
        //    switch (lightningComboBox.SelectedIndex)
        //    {
        //        // Отключено
        //        case 0:
        //            break;
        //        // Шейдинг Гуро для модели Ламберта
        //        case 1:
        //            bmp = GouraudShading.UseShading(matrix, objects_list.Items, Width, Height, lightPosition);
        //            //g.Clear(Color.White);
        //            if (pictureBox1.Image != null)
        //                pictureBox1.Image.Dispose();
        //            pictureBox1.Image = bmp;
        //            return;
        //            break;
        //            break;
        //        // Шейдинг Фонга для модели туншейдинг
        //        case 2: 
        //            bmp = PhongShading.UseShading(matrix, objects_list.Items, Width, Height, lightPosition);
        //            //g.Clear(Color.White);
        //            if (pictureBox1.Image != null)
        //                pictureBox1.Image.Dispose();
        //            pictureBox1.Image = bmp;
        //            return;
        //            break;
        //        default:
        //            break;
        //    }

        //    switch (DrawModeSelector.SelectedIndex)
        //    { 
        //        // Только рёбра 
        //        case 0:
        //            g.Clear(Color.White);
        //            DrawEdges(matrix);
        //            break;
        //        // Отсечение + Z-buff
        //        case 1:
        //            bmp = ZBuff_AfterFacesDelete(matrix);
        //            g.Clear(Color.White);
        //            if (pictureBox1.Image != null)
        //                pictureBox1.Image.Dispose();
        //            pictureBox1.Image = bmp;
        //            break;
        //        case 2:
        //            bmp = BackfaceCulling.Cull(matrix, objects_list.Items, Width, Height);
        //            g.Clear(Color.White);
        //            if (pictureBox1.Image != null)
        //                pictureBox1.Image.Dispose();
        //            pictureBox1.Image = bmp;
        //            break;
        //        case 3:
        //            bmp = ZBuffer.ZBuff(matrix, objects_list.Items, Width, Height);
        //            g.Clear(Color.White);
        //            if (pictureBox1.Image != null)
        //                pictureBox1.Image.Dispose();
        //            pictureBox1.Image = bmp;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        /// <summary>
        /// Возвращает проекционную матрицу
        /// </summary>
        /// <param name="index">0 - перспективная; 1 - изометрическая </param>
        /// <returns></returns>
        internal static double[,] GetProjectionMatrix(int index)
        {
            switch (index)
            {
                case 0: // перспективная
                    double c = 1000;
                    return new double[,] {
                                { 1, 0, 0, 0 },
                                { 0, 1, 0, 0 },
                                { 0, 0, 0, -1 / c },
                                { 0, 0, 0, 1 }
                            };

                case 1: //аксонометрическая - изометрическая
                    var psi = AffineTransformations.DegreesToRadians(45);
                    var phi = AffineTransformations.DegreesToRadians(35);
                    return new double[,] {
                                { Math.Cos(psi), Math.Sin(phi)*Math.Cos(psi), 0, 0 },
                                { 0,             Math.Cos(phi),               0, 0 },
                                { Math.Sin(psi),-Math.Sin(phi)*Math.Cos(psi), 0, 0 },
                                { 0,                                       0, 0, 1 }
                            };

                default:
                    return new double[,] { };
            }
        }

        // Создаёт изображение многогранников только с рёбрами
        internal void DrawEdges(double[,] matrix)
        {
            double[,] cur_m;
            Vertex line_start;
            Vertex line_end;

            foreach(var obj in objects_list.Items)
            {
                var cur_poly = obj as Polyhedron;
                for (int i = 0; i < cur_poly.vertices.Count; i++)
                {
                    cur_m = AffineTransformations.Multiply(new double[,] {{ cur_poly.vertices[i].x,
                                                                                    cur_poly.vertices[i].y,
                                                                                    cur_poly.vertices[i].z,
                                                                                    1 }}, matrix);
                    //line_start = new Vertex(cur_m[0,0]/ cur_m[0,3], cur_m[0, 1] / cur_m[0, 3], 0);
                    line_start = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);

                    //пробегает по всем граничным точкам и рисует линию
                    for (int j = 0; j < cur_poly.edges[i].Count; j++)
                    {
                        var ind = cur_poly.edges[i][j];
                        cur_m = AffineTransformations.Multiply(new double[,] {{ cur_poly.vertices[ind].x,
                                                                                cur_poly.vertices[ind].y,
                                                                                cur_poly.vertices[ind].z,
                                                                                1 }}, matrix);
                        //line_end = new Vertex(cur_m[0, 0] / cur_m[0, 3], cur_m[0, 1] / cur_m[0, 3], 0);
                        line_end = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);
                        g.DrawLine(pen, (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);
                    }
                }
            }

           g.DrawEllipse(pen, (float)camera.cameraPos.x, (float)camera.cameraPos.y, 4, 4);

        }

        internal Bitmap ZBuff_AfterFacesDelete(double[,] matrix)
        {
            var bmp = new Bitmap(Width, Height);

            double[,] z_buff = new double[Width, Height];

            for (int i = 0; i < Width; i++) //x
                for (int j = 0; j < Height; j++) //y
                {
                    z_buff[i, j] = double.MinValue;
                }

            // Вектор обзора, направленный из экрана к пользователю
            var viewDirection = new Vertex(0, 0, 1);

            var rand = new Random(42);

            for (var i = 0; i < objects_list.Items.Count; ++i)
            {
                var polyhedron = objects_list.Items[i] as Polyhedron;
                var transformedVertices = new List<Vertex>();

                // Преобразование вершин с использованием матрицы трансформации
                foreach (var v in polyhedron.vertices)
                {
                    var result = AffineTransformations.Multiply(new double[,] { { v.x, v.y, v.z, 1 } }, matrix);
                    transformedVertices.Add(new Vertex(result[0, 0], result[0, 1], v.z));
                }

                var visibleFaces = new List<List<int>>();

                // Определение видимых граней по нормалям
                foreach (var face in polyhedron.faces)
                {
                    var normal = BackfaceCulling.Normalize(face, transformedVertices);
                    var scalar = normal.x * viewDirection.x + normal.y * viewDirection.y + normal.z * viewDirection.z;
                    if (scalar < 0) visibleFaces.Add(face); // Грань видима, если нормаль направлена к наблюдателю (по оси Z)
                }

                // Отрисовка видимых граней
                var triangulated_faces = new List<List<Vertex>>();
                foreach (var face in visibleFaces)
                {
                    triangulated_faces = ZBuffer.Triangulate(transformedVertices, face);

                    Color c = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));


                    foreach (var triangg in triangulated_faces)
                    {
                        ZBuffer.DrawTriang(triangg, ref bmp, ref z_buff, c);
                    }
                }
            }

            return bmp;
        }
    
    
    }

    
    

    
}
