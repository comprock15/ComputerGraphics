using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LAB4
{
    static class AffineTransformations
    {
        /// <summary>
        /// Перемножение координат на матрицу преобразования
        /// </summary>
        /// <param name="coords">Координаты точки</param>
        /// <param name="matrix">Матрица преобразования</param>
        /// <returns></returns>
        private static double[] Multiply(double[] coords, double[,] matrix)
        {
            double[] newCoords = new double[coords.Length];
            for (int j = 0; j < matrix.GetLength(1); j++)
                for (int i = 0; i < matrix.GetLength(0); i++)
                    newCoords[j] += coords[i] * matrix[i, j];

            return newCoords;
        }

        /// <summary>
        /// Пересчитывает новые координаты вершин полигона
        /// </summary>
        /// <param name="polygon">Полигон</param>
        /// <param name="matrix">Матрица аффинного преобразования</param>
        private static void RecalculateCoords(ref Polygon polygon, double[,] matrix)
        {
            for (int i = 0; i < polygon.vertices.Count; i++)
            {
                double[] oldCoords = new double[] { polygon.vertices[i].X, polygon.vertices[i].Y, 1 };
                double[] newCoords = Multiply(oldCoords, matrix);
                polygon.vertices[i] = new PointF((float)newCoords[0], (float)newCoords[1]);
            }
        }

        /// <summary>
        /// Считает координаты центра полигона
        /// </summary>
        private static (double, double) CalculateCenterCoords(ref Polygon polygon)
        {
            double xMin = Double.MaxValue;
            double xMax = Double.MinValue;
            double yMin = Double.MaxValue;
            double yMax = Double.MinValue;

            foreach (PointF point in polygon.vertices)
            {
                xMin = Math.Min(xMin, point.X);
                xMax = Math.Max(xMax, point.X);
                yMin = Math.Min(yMin, point.Y);
                yMax = Math.Max(yMax, point.Y);
            }

            return ((xMin + xMax) / 2, (yMin + yMax) / 2);
        }

        /// <summary>
        /// Переводит угол из градусов в радианы
        /// </summary>
        private static double DegreesToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }

        /// <summary>
        /// Смещение на dx, dy
        /// </summary>
        /// <param name="polygon">Полигон для преобразования</param>
        /// <param name="dx">Смещение по x</param>
        /// <param name="dy">Смещение по y</param>
        public static void TransformOffset(ref Polygon polygon, double dx, double dy)
        {
            // Задаём матрицу преобразования
            double[,] matrix = new double[3, 3] {
                {  1,   0,   0 },
                {  0,   1,   0 },
                { dx,  -dy,   1 }
            };

            // Пересчитываем координаты всех точек
            RecalculateCoords(ref polygon, matrix);
        }

        /// <summary>
        /// Поворот вокруг заданной пользователем точки
        /// </summary>
        /// <param name="polygon">Полигон для преобразования</param>
        /// <param name="angle">Угол поворота в градусах</param>
        /// <param name="x">Координата x точки</param>
        /// <param name="y">Координата y точки</param>
        public static void TransformRotationPoint(ref Polygon polygon, double angle, double x, double y)
        {
            angle = DegreesToRadians(angle);

            // Задаём матрицу преобразования
            double[,] matrix = new double[3, 3] {
                {                               Math.Cos(angle),                                 Math.Sin(angle), 0 },
                {                              -Math.Sin(angle),                                 Math.Cos(angle), 0 },
                {-x * Math.Cos(angle) + y * Math.Sin(angle) + x,  -x * Math.Sin(angle) - y * Math.Cos(angle) + y, 1 }
            };

            // Пересчитываем координаты всех точек
            RecalculateCoords(ref polygon, matrix);
        }

        /// <summary>
        /// Поворот вокруг своего центра
        /// </summary>
        /// <param name="polygon">Полигон</param>
        /// <param name="angle">Угол поворота в градусах</param>
        public static void TransformRotationCenter(ref Polygon polygon, double angle)
        {
            // Считаем координаты центра
            (double xCenter, double yCenter) = CalculateCenterCoords(ref polygon);
            // Поворачиваем относительно центра
            TransformRotationPoint(ref polygon, angle, xCenter, yCenter);

        }

        /// <summary>
        /// Масштабирование относительно заданной пользователем точки
        /// </summary>
        /// <param name="polygon">Полигон</param>
        /// <param name="scale">Масштаб</param>
        /// <param name="x">Координата x точки</param>
        /// <param name="y">Координата y точки</param>
        public static void TransformScalePoint(ref Polygon polygon, double scale, double x, double y)
        {
            // Задаём матрицу преобразования
            double[,] matrix = new double[3, 3] {
                {          scale,               0,  0 },
                {              0,           scale,  0 },
                {(1 - scale) * x, (1 - scale) * y,  1 }
            };

            // Пересчитываем координаты всех точек
            RecalculateCoords(ref polygon, matrix);
        }

        /// <summary>
        /// Масштабирование относительно своего центра
        /// </summary>
        /// <param name="polygon">Полигон</param>
        /// <param name="scale">Масштаб</param>
        public static void TransformScaleCenter(ref Polygon polygon, double scale)
        {
            // Считаем координаты центра
            (double xCenter, double yCenter) = CalculateCenterCoords(ref polygon);
            // Масштабируем относительно центра
            TransformScalePoint(ref polygon, scale, xCenter, yCenter);
        }
    }
}
