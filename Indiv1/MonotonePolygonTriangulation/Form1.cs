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
    public partial class Form1 : Form
    {
        Polygon polygon = new Polygon();
        Graphics graphics;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                polygon.vertices.Add(e.Location);
                graphics.Clear(Color.White);

                DrawPolygon(polygon);
            }
        }

        private void DrawPolygon(Polygon polygon)
        {
            if (polygon.vertices.Count > 1)
            {
                graphics.DrawLines(new Pen(Color.Black), polygon.vertices.ToArray());
                graphics.DrawLine(new Pen(Color.Black), polygon.vertices.First(), polygon.vertices.Last());
            }

            foreach (PointF p in polygon.vertices)
                graphics.FillEllipse(new SolidBrush(Color.Black), p.X - 2, p.Y - 2, 4, 4);

            pictureBox1.Invalidate();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            polygon.vertices.Clear();
            graphics.Clear(Color.White);
            pictureBox1.Enabled = true;
            pictureBox1.Invalidate();
        }

        private void triangulateButton_Click(object sender, EventArgs e)
        {
            if (polygon.vertices.Count < 4)
                return;

            graphics.Clear(SystemColors.Control);

            List<Polygon> triangles = Triangulate(polygon);
            foreach (Polygon triangle in triangles)
                DrawPolygon(triangle);

            pictureBox1.Invalidate();
            pictureBox1.Enabled = false;
        }

        private List<Polygon> Triangulate(Polygon polygon)
        {
            List<Polygon> triangles = new List<Polygon>();

            // Сортируем вершины по возрастанию координаты X
            List<int> sortedVertices = Enumerable.Range(0, polygon.vertices.Count).OrderBy(i => polygon.vertices[i].X).ToList();

            //int index_left = sortedVertices.First();
            //int index_right = sortedVertices.Last();

            // Разделяем вершины на верхнюю и нижнюю цепочки
            List<bool> isUpper = FindUpperVertices(polygon, sortedVertices.First(), sortedVertices.Last());

            LinkedList<int> stack = new LinkedList<int>();
            stack.AddLast(sortedVertices.First()); // Добавляем самую левую вершину

            int vi = 1;
            while (vi < sortedVertices.Count)
            {
                if (stack.Count == 1)
                {
                    stack.AddLast(sortedVertices[vi]);
                    vi++;
                }
                else
                {
                    // Ситуация 1
                    if (AreNeighbors(polygon, vi, stack.Last()) && !AreNeighbors(polygon, vi, stack.First()))
                    {
                        if (Angle(
                                polygon.vertices[stack.Last.Previous.Value],
                                polygon.vertices[stack.Last.Value],
                                polygon.vertices[vi]
                            ) < Math.PI)
                        {
                            Polygon t = new Polygon();
                            t.vertices.Add(polygon.vertices[stack.Last.Previous.Value]);
                            t.vertices.Add(polygon.vertices[stack.Last.Value]);
                            t.vertices.Add(polygon.vertices[vi]);
                            triangles.Add(t);

                            stack.RemoveLast();
                        }
                        else
                        {
                            stack.AddLast(vi);
                            vi++;
                        }
                    }
                    // Ситуация 2
                    else if (AreNeighbors(polygon, vi, stack.First()) && !AreNeighbors(polygon, vi, stack.Last()))
                    {
                        int st = stack.Last();

                        while (stack.Count > 1)
                        {
                            Polygon t = new Polygon();
                            t.vertices.Add(polygon.vertices[stack.Last.Previous.Value]);
                            t.vertices.Add(polygon.vertices[stack.Last.Value]);
                            t.vertices.Add(polygon.vertices[vi]);
                            triangles.Add(t);
                            stack.RemoveLast();
                        }
                        stack.RemoveLast();
                        stack.AddLast(st);
                        stack.AddLast(vi);
                        vi++;
                    }
                    else if (AreNeighbors(polygon, vi, stack.First()) && AreNeighbors(polygon, vi, stack.Last()))
                    {
                        while (stack.Count > 1)
                        {
                            Polygon t = new Polygon();
                            t.vertices.Add(polygon.vertices[stack.Last.Previous.Value]);
                            t.vertices.Add(polygon.vertices[stack.Last.Value]);
                            t.vertices.Add(polygon.vertices[vi]);
                            triangles.Add(t);
                            stack.RemoveLast();
                        }
                        break;
                    }
                    else
                    {
                        stack.AddLast(vi);
                        vi++;
                    }
                }
            }

            return triangles;
        }

        private List<bool> FindUpperVertices(Polygon polygon, int index_left, int index_right)
        {
            List<bool> isUpper = new bool[polygon.vertices.Count].ToList();

            if (index_right < index_left)
                index_right += polygon.vertices.Count;

            for (int i = index_left + 1; i < index_right; i++)
            {
                var t = i % polygon.vertices.Count;
                isUpper[t] = true;
            }

            return isUpper;
        }

        private bool AreNeighbors(Polygon polygon, int v1, int v2)
        {
            return Math.Abs(v2 - v1) == 1 || Math.Abs(v2 - v1) == polygon.vertices.Count;
        }

        private double Angle(PointF p1, PointF p2, PointF p3)
        {
            double a = Distance(p1, p2);
            double b = Distance(p2, p3);
            double c = Distance(p1, p3);

            return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
        }

        private double Distance(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}
