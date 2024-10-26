using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonotonePolygonTriangulation
{
    // Класс полигона
    public class Polygon
    {
        public List<PointF> vertices = new List<PointF>();
    }

    public partial class Form1 : Form
    {
        Polygon polygon = new Polygon(); // Полигон для триангуляции
        Graphics graphics;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }

        // Добавление точек полигона по клику
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                polygon.vertices.Add(e.Location);
                graphics.Clear(Color.White);

                DrawPolygon(polygon, new Pen(Color.Gray, 2));
            }
        }

        // Рисование полигона
        private void DrawPolygon(Polygon polygon, Pen pen)
        {
            // Рисование рёбер
            if (polygon.vertices.Count > 1)
            {
                graphics.DrawLines(pen, polygon.vertices.ToArray());
                graphics.DrawLine(pen, polygon.vertices.First(), polygon.vertices.Last());
            }

            // Рисование вершин
            foreach (PointF p in polygon.vertices)
                graphics.FillEllipse(new SolidBrush(Color.Black), p.X - 2, p.Y - 2, 4, 4);

            pictureBox1.Invalidate();
        }

        // Очистка полигона
        private void clearButton_Click(object sender, EventArgs e)
        {
            polygon.vertices.Clear();
            graphics.Clear(Color.White);
            triangulateButton.Enabled = true;
            pictureBox1.Enabled = true;
            pictureBox1.Invalidate();
        }

        // Нажатие кнопки для триангуляции
        private void triangulateButton_Click(object sender, EventArgs e)
        {
            // Проверка, что в многоугольнике более 2 вершин
            if (polygon.vertices.Count < 3)
                return;

            graphics.Clear(SystemColors.Control);

            // Отрисовка исходного полигона
            DrawPolygon(polygon, new Pen(Color.Gray, 2));

            // Получение разбиения полигона на треугольники и их отрисовка
            List<Polygon> triangles = Triangulate(polygon);
            foreach (Polygon triangle in triangles)
                DrawPolygon(triangle, new Pen(Color.Blue));

            pictureBox1.Invalidate();
            pictureBox1.Enabled = false;
            triangulateButton.Enabled = false;
        }

        // Триангуляция
        private List<Polygon> Triangulate(Polygon polygon)
        {
            List<Polygon> triangles = new List<Polygon>();

            // Сортируем вершины по возрастанию координаты X
            List<int> sortedVertices = Enumerable.Range(0, polygon.vertices.Count).OrderBy(i => polygon.vertices[i].X).ToList();

            // Находим вершины в верхней и нижней цепочке
            List<bool> isUpper = FindUpperVertices(polygon, sortedVertices.First(), sortedVertices.Last());
            List<bool> isLower = isUpper.Select(x => x = !x).ToList();
            isLower[sortedVertices.First()] = true;
            isLower[sortedVertices.Last()] = true;

            LinkedList<int> stack = new LinkedList<int>();
            stack.AddLast(sortedVertices.First()); // Добавляем самую левую вершину в стек

            // Далее просматриваем все вершини в порядке увеличения координаты X
            int vi = 1;
            while (vi < sortedVertices.Count)
            {
                // Если в стеке только одна вершина, то просто добавляем новую вершину в стек
                if (stack.Count == 1)
                {
                    stack.AddLast(sortedVertices[vi]);
                    vi++;
                }
                // Если в стеке больше вершин, то проверяем, не образуют ли они треугольник или веерообразный полигон
                else
                {
                    // Ситуация 1: Вершина vi соседняя с st, но не с s1
                    if (AreNeighbors(polygon, sortedVertices[vi], stack.Last()) && !AreNeighbors(polygon, sortedVertices[vi], stack.First()))
                    {
                        // Внутренний угол vi st s_(t-1) меньше 180 градусов
                        if (InnerAngleLessThan180(polygon, sortedVertices[vi], stack.Last.Value, stack.Last.Previous.Value,
                                isUpper, isLower))
                        {
                            // Добавляем треугольник vi st s_(t-1)
                            Polygon t = new Polygon();
                            t.vertices.Add(polygon.vertices[stack.Last.Previous.Value]);
                            t.vertices.Add(polygon.vertices[stack.Last.Value]);
                            t.vertices.Add(polygon.vertices[sortedVertices[vi]]);
                            triangles.Add(t);

                            // st исключается из стека, снова анализируем vi
                            stack.RemoveLast();
                        }
                        // Иначе добавим вершину в стек
                        else
                        {
                            stack.AddLast(sortedVertices[vi]);
                            vi++;
                        }
                    }
                    // Ситуация 2: Вершина vi соседняя с s1, но не с st
                    else if (AreNeighbors(polygon, sortedVertices[vi], stack.First()) && !AreNeighbors(polygon, sortedVertices[vi], stack.Last()))
                    {
                        int st = stack.Last();

                        // Полигон vi, s1, s2,..., st - веерообразный с узлом в точке vi
                        // Разбиваем на треугольники и добавляем их в список
                        while (stack.Count > 1)
                        {
                            Polygon t = new Polygon();
                            t.vertices.Add(polygon.vertices[stack.Last.Previous.Value]);
                            t.vertices.Add(polygon.vertices[stack.Last.Value]);
                            t.vertices.Add(polygon.vertices[sortedVertices[vi]]);
                            triangles.Add(t);
                            stack.RemoveLast();
                        }

                        // Очищаем стек и затем заносим в него сначала st, потом vi
                        stack.RemoveLast();
                        stack.AddLast(st);
                        stack.AddLast(sortedVertices[vi]);
                        vi++;
                    }
                    // Ситуация 3: Вершина vi соседняя с обеими вершинами s1 и st
                    else if (AreNeighbors(polygon, sortedVertices[vi], stack.First()) && AreNeighbors(polygon, sortedVertices[vi], stack.Last()))
                    {
                        // vi = vn и полигон vi, s1, s2,..., st - веерообразный с узлом в точке vn
                        // Алгоритм выполняет триангуляцию этого полигона и заканчивается
                        while (stack.Count > 1)
                        {
                            Polygon t = new Polygon();
                            t.vertices.Add(polygon.vertices[stack.Last.Previous.Value]);
                            t.vertices.Add(polygon.vertices[stack.Last.Value]);
                            t.vertices.Add(polygon.vertices[sortedVertices[vi]]);
                            triangles.Add(t);
                            stack.RemoveLast();
                        }
                        break;
                    }
                    else // В монотонном многоугольнике не возникает
                    {
                        stack.AddLast(sortedVertices[vi]);
                        vi++;
                    }
                }
            }

            return triangles;
        }

        /// <summary>
        /// Нахождение вершин верхней цепочки
        /// </summary>
        /// <param name="polygon">Полигон</param>
        /// <param name="index_left">Индекс самой левой точки</param>
        /// <param name="index_right">Индекс самой правой точки</param>
        private List<bool> FindUpperVertices(Polygon polygon, int index_left, int index_right)
        {
            List<bool> isUpper = new bool[polygon.vertices.Count].ToList();

            // Крайние вершины принадлежат и верхней, и нижней цепочке
            isUpper[index_left] = true;
            isUpper[index_right] = true;

            // Если самая правая вершина нарисована раньше самой левой
            if (index_right < index_left)
                index_right += polygon.vertices.Count;

            // Отмечаем все вершины верхней цепочки от левой до правой точки
            for (int i = index_left + 1; i < index_right; i++)
            {
                var t = i % polygon.vertices.Count;
                isUpper[t] = true;
            }

            return isUpper;
        }

        // Проверка, являются ли вершины соседними
        private bool AreNeighbors(Polygon polygon, int v1, int v2)
        {
            return Math.Abs(v2 - v1) == 1 || Math.Abs(v2 - v1) == polygon.vertices.Count - 1;
        }

        /// <summary>
        /// Проверка, что внутренний угол меньше 180 градусов
        /// </summary>
        /// <param name="i1">v_i</param>
        /// <param name="i2">s_t</param>
        /// <param name="i3">s_(t-1)</param>
        private bool InnerAngleLessThan180(Polygon polygon, int i1, int i2, int i3, List<bool> isUpper, List<bool> isLower)
        {
            PointF p1 = polygon.vertices[i1];
            PointF p2 = polygon.vertices[i2];
            PointF p3 = polygon.vertices[i3];

            // Вычисление положения точки s_(t-1) относительно вектора vi st
            double d = (p3.X - p1.X) * (p2.Y - p1.Y) - (p3.Y -  p1.Y) * (p2.X - p1.X);

            return (isUpper[i1] && d > 0)  // s_(t-1) лежит слева  от вектора v_i s_t, если v_i из верхней цепочки
                || (isLower[i1] && d < 0); // s_(t-1) лежит справа от вектора v_i s_t, если v_i из нижней  цепочки
        }
    }
}
