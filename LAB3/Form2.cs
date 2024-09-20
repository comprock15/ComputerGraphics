using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LAB3 {
    public partial class Form2 : Form {
        MainForm menu;
        private Point? startPoint = null;
        private Point? endPoint = null;
        private bool useBresenham = true;
        private List<LineInfo> lines = new List<LineInfo>();

        private class LineInfo {
            public Point Start { get; set; }
            public Point End { get; set; }
            public bool UseBresenham { get; set; }
        }

        public Form2(MainForm menu) {
            InitializeComponent();
            this.menu = menu;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e) => menu.Show();

        private void Button1_Click(object sender, EventArgs e) {
            menu.Show();
            Close();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e) {
            e.Graphics.ScaleTransform(5.0f, 5.0f);

            foreach (var line in lines) {
                if (line.UseBresenham)
                    BresenhamLine(e.Graphics, line.Start, line.End, Color.Black);
                else
                    WuLine(e.Graphics, line.Start, line.End, Color.Black);
            }

            if (startPoint != null && endPoint != null) {
                Point correctedStart = new Point(startPoint.Value.X / 5, startPoint.Value.Y / 5);
                Point correctedEnd = new Point(endPoint.Value.X / 5, endPoint.Value.Y / 5);

                if (useBresenham)
                    BresenhamLine(e.Graphics, correctedStart, correctedEnd, Color.Black);
                else
                    WuLine(e.Graphics, correctedStart, correctedEnd, Color.Black);
            }
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e) {
            if (startPoint == null)
                startPoint = e.Location;
            else if (endPoint == null) {
                endPoint = e.Location;

                lines.Add(new LineInfo {
                    Start = new Point(startPoint.Value.X / 5, startPoint.Value.Y / 5),
                    End = new Point(endPoint.Value.X / 5, endPoint.Value.Y / 5),
                    UseBresenham = useBresenham
                });

                startPoint = null;
                endPoint = null;

                pictureBox1.Invalidate();
            }
        }

        private void RadioButtonBresenham_CheckedChanged(object sender, EventArgs e) => useBresenham = radioButtonBresenham.Checked;

        private void ButtonReset_Click(object sender, EventArgs e) {
            startPoint = null;
            endPoint = null;
            lines.Clear();
            pictureBox1.Invalidate();
        }

        private void ButtonUndo_Click(object sender, EventArgs e) {
            if (lines.Count > 0) {
                lines.RemoveAt(lines.Count - 1);
                pictureBox1.Invalidate();
            }
        }

        private void BresenhamLine(Graphics g, Point p0, Point p1, Color color) {
            int dx = Math.Abs(p1.X - p0.X);
            int dy = Math.Abs(p1.Y - p0.Y);
            int sx = p0.X < p1.X ? 1 : -1;
            int sy = p0.Y < p1.Y ? 1 : -1;
            int err = dx - dy;

            while (true) {
                g.FillRectangle(new SolidBrush(color), p0.X, p0.Y, 1, 1);
                if (p0.X == p1.X && p0.Y == p1.Y) break;
                int e2 = 2 * err;
                if (-dy < e2) {
                    err -= dy;
                    p0.X += sx;
                }
                if (e2 < dx) {
                    err += dx;
                    p0.Y += sy;
                }
            }
        }

        private void WuLine(Graphics g, Point p0, Point p1, Color color) {
            bool steep = Math.Abs(p1.Y - p0.Y) > Math.Abs(p1.X - p0.X);

            int x0 = p0.X;
            int y0 = p0.Y;
            int x1 = p1.X;
            int y1 = p1.Y;

            if (steep) {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }

            if (x0 > x1) {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            int dx = x1 - x0;
            int dy = y1 - y0;
            float grad = dy / (float)dx;

            float y = y0 + grad;

            for (int x = x0; x <= x1; ++x) {
                if (steep) {
                    g.FillRectangle(new SolidBrush(Color.FromArgb((int)(255 * (1 - Frac(y))), color)), (int)y, x, 1, 1);
                    g.FillRectangle(new SolidBrush(Color.FromArgb((int)(255 * Frac(y)), color)), (int)y + 1, x, 1, 1);
                }
                else {
                    g.FillRectangle(new SolidBrush(Color.FromArgb((int)(255 * (1 - Frac(y))), color)), x, (int)y, 1, 1);
                    g.FillRectangle(new SolidBrush(Color.FromArgb((int)(255 * Frac(y)), color)), x, (int)y + 1, 1, 1);
                }
                y += grad;
            }
        }

        private void Swap(ref int a, ref int b) => (b, a) = (a, b);

        private float Frac(float x) => x - (float)Math.Floor(x);
    }
}
