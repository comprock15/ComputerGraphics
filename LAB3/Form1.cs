using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public partial class Form1 : Form
    {
        MainForm menu;
        Graphics graphics;
        Bitmap bitmap;
        Pen pen;
        Point previousdMouseLocation;

        public Form1(MainForm menu)
        {
            InitializeComponent();
            this.menu = menu;

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            graphics = Graphics.FromImage(bitmap);
            pen = new Pen(colorChooser.BackColor, 1);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Show();
        }

        /// <summary>
        /// Выбрать цвет для рисования
        /// </summary>
        private void colorChooser_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Обновляем цвет
                    (sender as Label).BackColor = colorDialog.Color;
                    pen.Color = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// Рисование за курсором мыши
        /// </summary>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Если не нажата ЛКМ или не режим рисования - выходим
            if (e.Button != MouseButtons.Left || !radioButton1.Checked)
                return;

            // Рисуем линию от старых координат мыши к новым
            graphics.DrawLine(pen, previousdMouseLocation, e.Location);
            // Обновляем последние координаты мыши
            previousdMouseLocation = e.Location;
            // Обновляем отображаемое изображение
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Обновляем последние координаты мыши
            previousdMouseLocation = e.Location;

            // Если включен режим заливки - заливаем область
            if (radioButton2.Checked)
            {
                FillByColor(e.X, e.Y, bitmap.GetPixel(e.X, e.Y));
            }
        }

        /// <summary>
        /// Заливка линиями заданным цветом
        /// </summary>
        /// <param name="x">Координата X перекрашиваемого пикселя</param>
        /// <param name="y">Координата Y перекрашиваемого пикселя</param>
        /// <param name="oldColor">Перекрашиваемый цвет</param>
        private void FillByColor(int x, int y, Color oldColor)
        {
            // Выходим, если пиксель уже перекрашен или цвета, который не нужно перекрашивать
            if (bitmap.GetPixel(x, y) == pen.Color || bitmap.GetPixel(x, y) != oldColor)
                return;

            // Ищем левую границу
            int left_boundary = x - 1;
            while (left_boundary >= 0 && bitmap.GetPixel(left_boundary, y) == oldColor)
            {
                --left_boundary;
            }

            // Ищем правую границу
            int right_boundary = x + 1;
            while (right_boundary < bitmap.Width && bitmap.GetPixel(right_boundary, y) == oldColor)
            {
                ++right_boundary;
            }

            // Рисуем линию от левой границы до правой границы, не включая саму границу
            graphics.DrawLine(pen, left_boundary + 1, y, right_boundary - 1, y);

            // Вызываем эту же функцию рекурсивно для всех точек, лежащих выше текущей на один пиксель
            if (y + 1 < bitmap.Height)
            {
                for (int xi = left_boundary + 1; xi < right_boundary; ++xi)
                {
                    FillByColor(xi, y + 1, oldColor);
                }
            }

            // Вызываем эту же функцию рекурсивно для всех точек, лежащих ниже текущей на один пиксель
            if (y - 1 >= 0)
            {
                for (int xi = left_boundary + 1; xi < right_boundary; ++xi)
                {
                    FillByColor(xi, y - 1, oldColor);
                }
            }

            // Обновляем отображаемое изображение
            pictureBox1.Invalidate();
        }

        /// <summary>
        /// Очистить рисунок
        /// </summary>
        private void clearButton_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            pictureBox1.Invalidate();
        }
    }
}
