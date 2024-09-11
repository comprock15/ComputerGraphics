﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB2
{
    public partial class Form1 : Form
    {
        MainForm menu;
        public Form1(MainForm menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu.Show();
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            menu.Close();
        }

        // Загружает и обрабатывает изображение
        private void loadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Загрузка изображения";
                dlg.Filter = "Файлы изображений (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(dlg.FileName);

                    Bitmap bitmap1 = new Bitmap(pictureBox1.Image);
                    pictureBox2.Image = bitmap1;
                    grayscaleNTSC(bitmap1);

                    Bitmap bitmap2 = new Bitmap(pictureBox1.Image);
                    pictureBox3.Image = bitmap2;
                    grayscaleSRGB(bitmap2);

                    Bitmap bitmap3 = new Bitmap(pictureBox1.Image);
                    pictureBox4.Image = bitmap3;
                    imageDifference(bitmap1, bitmap2, bitmap3);
                }
            }
        }

        // Переводит изображение в оттенки серого NTSC
        private void grayscaleNTSC(Bitmap bitmap)
        {
            // Проходимся по всем пикселям
            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y); // Получаем исходный цвет
                    int gray = (int)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B); // Пересчитываем новое значение
                    bitmap.SetPixel(x, y, Color.FromArgb(gray, gray, gray)); // Обновляем
                }
            }
        }

        // Переводит изображение в оттенки серого NTSC
        private void grayscaleSRGB(Bitmap bitmap)
        {
            // Проходимся по всем пикселям
            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y); // Получаем исходный цвет
                    int gray = (int)(0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B); // Пересчитываем новое значение
                    bitmap.SetPixel(x, y, Color.FromArgb(gray, gray, gray)); // Обновляем
                }
            }
        }

        // Считает разницу полутоновых изображений
        private void imageDifference(Bitmap bitmap1, Bitmap bitmap2, Bitmap result)
        {
            // Проходимся по всем пикселям
            for (var x = 0; x < result.Width; x++)
            {
                for (var y = 0; y < result.Height; y++)
                {
                    Color color1 = bitmap1.GetPixel(x, y); // Цвет 1го изображения
                    Color color2 = bitmap2.GetPixel(x, y); // Цвет 2го изображения
                    int diff = 255 - Math.Abs(color1.R - color2.R); // Считаем абсолютную разницу (вычитаем из белого, так как на черном фоне ужасно видно)
                    result.SetPixel(x, y, Color.FromArgb(diff, diff, diff)); // Обновляем результирующий пиксель
                }
            }
        }
    }
}
