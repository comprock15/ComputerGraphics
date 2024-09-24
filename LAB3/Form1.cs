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
        Bitmap bitmap, imageToFillFrom;
        Pen pen;
        Point previousMouseLocation;

        public Form1(MainForm menu)
        {
            InitializeComponent();
            this.menu = menu;

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            graphics = Graphics.FromImage(bitmap);
            pen = new Pen(colorChooser.BackColor, 1);
            imageToFillFrom = (Bitmap)pictureBox2.Image;
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
        /// Выбрать изображение для заливки
        /// </summary>
        private void loadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Загрузка изображения";
                dialog.Filter = "Файлы изображений (*.jpg;*.jpeg;*.png;;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageToFillFrom = new Bitmap(dialog.FileName);
                    pictureBox2.Image = imageToFillFrom;
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
            graphics.DrawLine(pen, previousMouseLocation, e.Location);
            // Обновляем последние координаты мыши
            previousMouseLocation = e.Location;
            // Обновляем отображаемое изображение
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Обновляем последние координаты мыши
            previousMouseLocation = e.Location;

            // Включен режим заливки цветом
            if (radioButton2.Checked)
            {
                FillByColor(e.X, e.Y, bitmap.GetPixel(e.X, e.Y));
            }
            // Включен режим заливки изображением
            else if (radioButton3.Checked)
            {
                // Если изображение для заливки отсутствует - выходим
                if (imageToFillFrom == null)
                {
                    loadImageButton.Select();
                    return;
                }
                // Заливаем область, считая, что в месте клика будет центр изображения-заливки
                FillByImage(e.X, e.Y, imageToFillFrom.Width/2, imageToFillFrom.Height/2, bitmap.GetPixel(e.X, e.Y));
            }
            // Включен режим выделения границы связной области
            else if (radioButton4.Checked)
            {
                FindBorder(e.X, e.Y);
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
            int leftBoundary = x - 1;
            while (leftBoundary >= 0 && bitmap.GetPixel(leftBoundary, y) == oldColor)
            {
                --leftBoundary;
            }

            // Ищем правую границу
            int rightBoundary = x + 1;
            while (rightBoundary < bitmap.Width && bitmap.GetPixel(rightBoundary, y) == oldColor)
            {
                ++rightBoundary;
            }

            // Рисуем линию от левой границы до правой границы, не включая саму границу
            graphics.DrawLine(pen, leftBoundary + 1, y, rightBoundary - 1, y);

            // Вызываем эту же функцию рекурсивно для всех точек, лежащих выше текущей на один пиксель
            if (y + 1 < bitmap.Height)
            {
                for (int xi = leftBoundary + 1; xi < rightBoundary; ++xi)
                {
                    FillByColor(xi, y + 1, oldColor);
                }
            }

            // Вызываем эту же функцию рекурсивно для всех точек, лежащих ниже текущей на один пиксель
            if (y - 1 >= 0)
            {
                for (int xi = leftBoundary + 1; xi < rightBoundary; ++xi)
                {
                    FillByColor(xi, y - 1, oldColor);
                }
            }

            // Обновляем отображаемое изображение
            pictureBox1.Invalidate();
        }

        private void FillByImage(int x, int y, int imageX, int imageY, Color oldColor)
        {
            // Выходим, если пиксель цвета, который не нужно перекрашивать
            if (bitmap.GetPixel(x, y) != oldColor)
                return;

            // Ищем левую границу
            int leftBoundary = x - 1;
            while (leftBoundary >= 0 && bitmap.GetPixel(leftBoundary, y) == oldColor)
            {
                --leftBoundary;
            }

            // Ищем правую границу
            int rightBoundary = x + 1;
            while (rightBoundary < bitmap.Width && bitmap.GetPixel(rightBoundary, y) == oldColor)
            {
                ++rightBoundary;
            }

            // Рисуем линию от левой границы до правой границы, не включая саму границу
            DrawLineFromImage(imageToFillFrom, bitmap, leftBoundary + 1, rightBoundary - 1, y, imageX - x + leftBoundary + 1, imageY);

            // Вызываем эту же функцию рекурсивно для всех точек, лежащих выше текущей на один пиксель
            if (y + 1 < bitmap.Height)
            {
                for (int xi = leftBoundary + 1; xi < rightBoundary; ++xi)
                {
                    FillByImage(xi, y + 1, imageX - x + xi, imageY + 1, oldColor);
                }
            }

            // Вызываем эту же функцию рекурсивно для всех точек, лежащих ниже текущей на один пиксель
            if (y - 1 >= 0)
            {
                for (int xi = leftBoundary + 1; xi < rightBoundary; ++xi)
                {
                    FillByImage(xi, y - 1, imageX - x + xi, imageY - 1, oldColor);
                }
            }

            // Обновляем отображаемое изображение
            pictureBox1.Invalidate();
        }

        /// <summary>
        /// Пересчитывает координату X так, чтобы она была больше нуля и меньше ширины изображения
        /// </summary>
        /// <param name="imageX">Координата X для преобразования</param>
        /// <param name="image">Изображение</param>
        /// <returns>Координата X в пределах изображения</returns>
        private int CalculateCyclicX(int imageX, Bitmap image)
        {
            if (imageX < 0)
            {
                ++imageX;
                imageX *= -1;
                imageX = image.Width - imageX % image.Width - 1;
            }
            else if (imageX >= image.Width)
            {
                imageX = imageX % image.Width;
            }
            return imageX;
        }

        /// <summary>
        /// Пересчитывает координату Y так, чтобы она была больше нуля и меньше ширины изображения
        /// </summary>
        /// <param name="imageY">Координата Y для преобразования</param>
        /// <param name="image">Изображение</param>
        /// <returns>Координата Y в пределах изображения</returns>
        private int CalculateCyclicY(int imageY, Bitmap image)
        {
            if (imageY < 0)
            {
                ++imageY;
                imageY *= -1;
                imageY = image.Height - imageY % image.Height - 1;
            }
            else if (imageY >= image.Height)
            {
                imageY = imageY % image.Height;
            }
            return imageY;
        }

        /// <summary>
        /// Рисует линию на основе изображения-заливки
        /// </summary>
        /// <param name="imageFrom">Изображение, которым заливаем</param>
        /// <param name="imageTo">Изображение, которое заливаем</param>
        /// <param name="x1">X самого левого пикселя, который нужно перекрасить</param>
        /// <param name="x2">X самого правого пикселя, который нужно перекрасить</param>
        /// <param name="y">Y пикселя, который нужно перекрасить</param>
        /// <param name="ix">X пикселя на изображении-заливке, соответствующий x1</param>
        /// <param name="iy">Y пикселя на изображении-заливке, соответствующий y</param>
        private void DrawLineFromImage(Bitmap imageFrom, Bitmap imageTo, int x1, int x2, int y, int ix, int iy)
        {
            iy = CalculateCyclicY(iy, imageFrom);
            for (int x = x1; x <= x2; ++x)
            {
                imageTo.SetPixel(x, y, imageFrom.GetPixel(CalculateCyclicX(ix + x - x1, imageFrom), iy));
            }
        }

        /// <summary>
        /// Возможные направления движения при обходе границы
        /// </summary>
        enum Direction { RIGHT, UP_RIGHT, UP, UP_LEFT, LEFT, DOWN_LEFT, DOWN, DOWN_RIGHT };

        /// <summary>
        /// Получить следующий пиксель по направлению
        /// </summary>
        /// <param name="x">Текущий X</param>
        /// <param name="y">Текущий Y</param>
        /// <param name="dir">Текущее направление</param>
        /// <returns>Следующий пиксель</returns>
        Point GetNextPixel(int x, int y, Direction dir)
        {
            switch (dir)
            {
                case Direction.RIGHT:
                    x += 1;
                    break;
                case Direction.UP_RIGHT:
                    x += 1;
                    y -= 1;
                    break;
                case Direction.UP:
                    y -= 1;
                    break;
                case Direction.UP_LEFT:
                    x -= 1;
                    y -= 1;
                    break;
                case Direction.LEFT:
                    x -= 1;
                    break;
                case Direction.DOWN_LEFT:
                    x -= 1;
                    y += 1;
                    break;
                case Direction.DOWN:
                    y += 1;
                    break;
                case Direction.DOWN_RIGHT:
                    x += 1;
                    y += 1;
                    break;
            }
            return new Point(x, y);
        }

        /// <summary>
        /// Найти границу связной области
        /// </summary>
        /// <param name="x">Координата X точки внутри области</param>
        /// <param name="y">Координата Y точки внутри области</param>
        private void FindBorder(int x, int y)
        {
            // Сохраняем цвет внутренней области
            Color innerColor = bitmap.GetPixel(x, y);

            // Идём вправо до пикселя границы, его будем считать начальным
            ++x;
            for (; x < bitmap.Width && bitmap.GetPixel(x, y) == innerColor; ++x);

            // Если не нашли границу - выходим
            if (x >= bitmap.Width)
                return;

            // Сохраняем цвет границы
            Color borderColor = bitmap.GetPixel(x, y);

            // Запомним координаты начальной точки границы
            int startX = x;
            int startY = y;

            // Список точек границы
            LinkedList<Point> border = new LinkedList<Point>();
            border.AddLast(new Point(x, y));

            // Множество пройденных точек границы
            HashSet<Point> checkedPixels = new HashSet<Point>();
            checkedPixels.Add(new Point(x, y));

            // Первое направление - вниз
            Direction direction = Direction.DOWN;

            // Встретили ли начальную точку границы
            bool startPixelFound = false;

            while (true)
            {
                startPixelFound = false;

                // Нашли ли следующий непроверенный пиксель границы
                bool nextPixelfound = false;

                // Просматриваем все направления
                for (int i = 0; i < 8; i++)
                {
                    // Координаты следующего пикселя
                    Point pixel = GetNextPixel(x, y, (Direction)(((int)direction + i) % 8));

                    // Вышли за границы - пропускаем
                    if (pixel.X < 0 || pixel.X >= bitmap.Width || pixel.Y < 0 || pixel.Y >= bitmap.Height)
                        continue;

                    // Если нашли пиксель границы, которого еще не было
                    if (bitmap.GetPixel(pixel.X, pixel.Y) == borderColor && !checkedPixels.Contains(pixel))
                    {
                        // Добавляем к списку точек границы
                        border.AddLast(pixel);
                        // Добавляем к множеству проверенных точек границы
                        checkedPixels.Add(pixel);
                        // Следующее направление: 90 градусов по часовой стрелке от направления, по которому мы пришли
                        direction = (Direction)(((int)direction + i + 6) % 8);
                        // Обновляем координаты текущего пикселя
                        x = pixel.X; y = pixel.Y;

                        nextPixelfound = true;
                        break;
                    }

                    // Если встретили стартовый пиксель
                    if (pixel.X == startX && pixel.Y == startY)
                        startPixelFound = true;
                }

                // Если не нашли новых пикселей границы - выходим
                if (!nextPixelfound)
                    break;

            }

            // Если граница замкнутая - рисуем
            if (startPixelFound)
            {
                foreach (Point p in border)
                {
                    bitmap.SetPixel(p.X, p.Y, Color.Red);
                }
                pictureBox1.Invalidate();
            }
        }

        /// <summary>
        /// Вывод подсказки к выделению границы
        /// </summary>
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = !label1.Visible;
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
