using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public partial class Form3 : Form
    {
        MainForm menu;
        bool add_points_mode;
        int points_count;
        
        public Form3(MainForm menu)
        {
            InitializeComponent();
            this.menu = menu;
            add_points_mode = false;
            points_count = 0;
        }
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Show();
        }

        private void pictureBox_Main_MouseClick(object sender, MouseEventArgs e)
        {
            if (add_points_mode && points_count < 3)
            {
                points_count++;
                switch (points_count) {
                    case 1:
                        pictureBox4.Location = new Point(e.X, e.Y);
                        pictureBox4.Visible = true;
                        break;
                    case 2:
                        pictureBox5.Location = new Point(e.X, e.Y);
                        pictureBox5.Visible = true;
                        break;
                    case 3:
                        pictureBox6.Location = new Point(e.X, e.Y);
                        pictureBox6.Visible = true;
                        break;
                }
                if (points_count == 3)
                {
                    add_points_mode = false;
                    button3.Enabled = true;
                }
            }
        }

        // Окошки выбора цвета для вершин
        private void pictureBox1_Click(object sender, EventArgs e) {
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                pictureBox1.BackColor = colorDialog1.Color;
                pictureBox4.BackColor = colorDialog1.Color;
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e) {
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                pictureBox2.BackColor = colorDialog1.Color;
                pictureBox5.BackColor = colorDialog1.Color;
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e) {
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                pictureBox3.BackColor = colorDialog1.Color;
                pictureBox6.BackColor = colorDialog1.Color;
            }
        }

        // Кнопка "Добавить вершины"
        private void button1_Click(object sender, EventArgs e)
        {
            add_points_mode = true;
            button1.Enabled = false;
        }
        // Кнопка "Сброс"
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            pictureBox4.Visible= false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            button3.Enabled = false;
            add_points_mode=false;
            points_count = 0;
            pictureBox_Main.Image = new Bitmap(pictureBox_Main.Width, pictureBox_Main.Height);
        }


        // Сортировка точек и цветов, связанных с ними, по Y координате (по высоте)
        // p1 - upper point, p3 - lower point
        List<(Point, Color)> sortPointsByY(Point pp1, Point pp2, Point pp3, Color cc1, Color cc2, Color cc3)
        {
            var l = new List<(Point, Color)>() { (pp1, cc1), (pp2, cc2), (pp3, cc3) };
            l = l.OrderBy(p => p.Item1.Y).ToList();
            return l;
        }

        // Кнопка "Создать треугольник"
        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            button3.Enabled = false;

            var elems = sortPointsByY(pictureBox4.Location, pictureBox5.Location, pictureBox6.Location, 
                                      pictureBox4.BackColor, pictureBox5.BackColor, pictureBox6.BackColor);
            pictureBox_Main.Image = FillGradient(elems[0].Item1, elems[1].Item1, elems[2].Item1, 
                                                 elems[0].Item2, elems[1].Item2, elems[2].Item2);
        }

        // Расчёт расстояния между двумя точками
        private double PointsDistance(Point p1, Point p2)
        { 
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        // Расчёт цвета точки на отрезке по цветам начала и конца 
        private Color GetIntermediateColor(Point start, Point fin, Point p, Color st, Color fn)
        {
            var dist = PointsDistance(start, p) / PointsDistance(start, fin);

            var a = (int)(st.A + dist * (fn.A - st.A));
            a = a <= 255 ? a : 255; a = a >= 0 ? a : 0;

            var r = (int)(st.R + dist * (fn.R - st.R));
            r = r <= 255 ? r : 255; r = r >= 0 ? r : 0;

            var g = (int)(st.G + dist * (fn.G - st.G));
            g = g <= 255 ? g : 255; g = g >= 0 ? g : 0;

            var b = (int)(st.B + dist * (fn.B - st.B));
            b = b <= 255 ? b : 255; b = b >= 0 ? b : 0;

            return Color.FromArgb(a, r, g, b);
        }

        // Функция рисования треугольника с градиентами
        private Bitmap FillGradient(Point p1, Point p2, Point p3, Color c1, Color c2, Color c3)
        {
            Bitmap bmp = new Bitmap(pictureBox_Main.Width, pictureBox_Main.Height);

            // Уравнения прямых
            //   p1-p2
            var f1 = (int y) => { return (y - p1.Y) * (p2.X - p1.X) / (float)(p2.Y - p1.Y) + p1.X; };
            //   p1-p3
            var f2 = (int y) => { return (y - p1.Y) * (p3.X - p1.X) / (float)(p3.Y - p1.Y) + p1.X; };
            //   p2-p3
            var f3 = (int y) => { return (y - p2.Y) * (p3.X - p2.X) / (float)(p3.Y - p2.Y) + p2.X; };

            // Вспомогательные пременные
            var left_right = (int y) => { return (f1(y), f2(y)); }; // если p2 левее чем р3
            var left_point  = p2; var left_point_color  = c2;
            var right_point = p3; var right_point_color = c3;

            if (p3.X < p2.X) { 
                left_right = (int y) => { return (f2(y), f1(y)); }; // если p3 левее чем р2
                left_point = p3;
                left_point_color = c3;
                right_point = p2;
                right_point_color = c2;
            }

            //     заливка

            bmp.SetPixel(p1.X, p1.Y, c1);

            //заливка до средней точки (р2)
            for (int y = p1.Y+1; y <= p2.Y; y++)
            {
                //граничные по x точки
                var p_left = new Point((int)left_right(y).Item1, y);
                var p_right = new Point((int)left_right(y).Item2, y);

                //определение цвета граничных x
                Color c_left = GetIntermediateColor(p1, left_point, p_left, c1, left_point_color);
                Color c_right = GetIntermediateColor(p1, right_point, p_right, c1, right_point_color);

                bmp.SetPixel(p_left.X, p_left.Y, c_left);
                bmp.SetPixel(p_right.X, p_right.Y, c_right);

                for (int x = p_left.X; x <= p_right.X; x++)
                    bmp.SetPixel(x, y, GetIntermediateColor(p_left, p_right, new Point(x,y), c_left, c_right));
            }

            /////////

            left_right = (int y) => { return (f3(y), f2(y)); }; // если p2 левее чем р3
            right_point = p1;
            right_point_color = c1;
            left_point = p2;
            left_point_color = c2;
            if (p3.X < p2.X)
            {
                left_right = (int y) => { return (f2(y), f3(y)); }; // если p3 левее чем р2
                right_point = p2;
                right_point_color = c2;
                left_point = p1;
                left_point_color = c1;
            }

            // Заливка после средней точки
            bmp.SetPixel(p3.X, p3.Y, c3);
            for (int y = p3.Y-1; y > p2.Y; y--)
            {
                //граничные по x точки
                var p_left = new Point((int)left_right(y).Item1, y);
                var p_right = new Point((int)left_right(y).Item2, y);

                //определение цвета граничных x
                Color c_left = GetIntermediateColor(p3, left_point, p_left, c3, left_point_color);
                Color c_right = GetIntermediateColor(p3, right_point, p_right, c3, right_point_color);

                bmp.SetPixel(p_left.X, p_left.Y, c_left);
                bmp.SetPixel(p_right.X, p_right.Y, c_right);

                for (int x = p_left.X; x <= p_right.X; x++)
                    bmp.SetPixel(x, y, GetIntermediateColor(p_left, p_right, new Point(x, y), c_left, c_right));
            }

            return bmp;
        }
    }
}
