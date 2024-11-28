using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB9
{
    public partial class Form1 : Form
    { 
        /// <summary>
        /// Отрисовывает поле в зависимости от выбранного типа проекции
        /// </summary>
        public void RedrawField()
        {
            if (cur_polyhedron == null) return;

            double[,] matrix = GetProjectionMatrix(comboBox2.SelectedIndex);

            label32.Text = cur_polyhedron.Center().ToString();

            Bitmap bmp = null;
            
            // Освещение
            switch (lightningComboBox.SelectedIndex)
            {
                // Отключено
                case 0:
                    break;
                // Шейдинг Гуро для модели Ламберта
                case 1:
                    break;
                // Шейдинг Фонга для модели туншейдинг
                case 2: 
                    bmp = PhongShading.UseShading(matrix, objects_list.Items, Width, Height, lightPosition);
                    //g.Clear(Color.White);
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    pictureBox1.Image = bmp;
                    return;
                    break;
                default:
                    break;
            }

            switch (comboBox6.SelectedIndex)
            { 
                // Только рёбра 
                case 0:
                    g.Clear(Color.White);
                    DrawEdges(matrix);
                    break;
                // Отсечение + Z-buff
                case 1:
                    bmp = ZBuff_AfterFacesDelete(matrix);
                    g.Clear(Color.White);
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    pictureBox1.Image = bmp;
                    break;
                case 2:
                    bmp = BackfaceCulling.Cull(matrix, objects_list.Items, Width, Height);
                    g.Clear(Color.White);
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    pictureBox1.Image = bmp;
                    break;
                case 3:
                    bmp = ZBuffer.ZBuff(matrix, objects_list.Items, Width, Height);
                    g.Clear(Color.White);
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    pictureBox1.Image = bmp;
                    break;
                default:
                    break;
            }
        }

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
                        g.DrawLine(p, (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);
                    }
                }
            }

            g.DrawEllipse(p, (float)camry.cameraPosition.x, (float)camry.cameraPosition.y, 4, 4);

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
