using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form2 : Form
    {
        MainForm menu;
        Graphics g;
        List<PointF> points = new List<PointF>();
        Random random = new Random();

        public Form2(MainForm menu)
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);

            numericUpDown1.Maximum = pictureBox1.Height - 1;
            numericUpDown2.Maximum = pictureBox1.Height - 1;
            numericUpDown1.Value = numericUpDown1.Maximum / 2;
            numericUpDown2.Value = numericUpDown2.Maximum / 2;

            this.menu = menu;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            menu.Show();
        }

        private void nextStepButton_Click(object sender, EventArgs e)
        {
            // Initial state
            if (points.Count == 0)
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;

                points.Add(new PointF(0, (float)numericUpDown1.Value));
                points.Add(new PointF(pictureBox1.Width - 1, (float)numericUpDown2.Value));
            }
            if (points.Count < pictureBox1.Width)
            {
                List<PointF> newPoints = new List<PointF>(points);

                // Check all pairs of points
                for (int i = 0; i < points.Count - 1; i++)
                {
                    PointF pl = points[i];
                    PointF pr = points[i + 1];
                    float R = (float)numericUpDown3.Value;
                    float l = (float)Math.Sqrt(Math.Pow(pl.X - pr.X, 2) + Math.Pow(pl.Y - pr.Y, 2));
                    PointF point = new PointF((pl.X + pr.X) / 2,
                                                (pl.Y + pr.Y) / 2 + ((float)random.NextDouble() * 2 - 1) * R * l);
                    newPoints.Insert(i * 2 + 1, point);
                }

                // Save new points
                points = newPoints;
            }

            DrawLines(points);
        }

        private void DrawLines(List<PointF> points)
        {
            g.Clear(pictureBox1.BackColor);
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(new Pen(Color.Black), points[i], points[i + 1]);
                if (checkBox1.Checked)
                    g.FillEllipse(new SolidBrush(Color.Red), points[i].X-3, points[i].Y-3, 6, 6);
            }
            if (checkBox1.Checked)
                g.FillEllipse(new SolidBrush(Color.Red), points[points.Count - 1].X - 3, points[points.Count - 1].Y - 3, 6, 6);
            pictureBox1.Invalidate();
        }

        private void heightChanged(object sender, EventArgs e)
        {
            clearButton_Click(sender, e);    
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            g.Clear(pictureBox1.BackColor);
            g.DrawLine(new Pen(Color.Black), 0, (int)numericUpDown1.Value, pictureBox1.Width - 1, (int)numericUpDown2.Value);
            pictureBox1.Invalidate();

            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            points.Clear();
        }
    }
}
