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
            pictureBox1.Invalidate();
        }
    }
}
