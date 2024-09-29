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
        /// <param name="matr">Матрица преобразования</param>
        /// <returns></returns>
        private static double[] Multiply(double[] coords, double[,] matr)
        {
            double[] newCoords = new double[coords.Length];
            for (int j = 0; j < matr.GetLength(1); j++)
                for (int i = 0; i < matr.GetLength(0); i++)
                    newCoords[j] += coords[i] * matr[i, j];

            return newCoords;
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
                {-dx, -dy,   1 }
            };

            // Пересчитываем координаты всех точек
            for (int i = 0; i < polygon.vertices.Count; i++)
            {
                double[] oldCoords = new double[] { polygon.vertices[i].X, polygon.vertices[i].Y, 1 };
                double[] newCoords = Multiply(oldCoords, matrix);
                polygon.vertices[i] = new Point((int)newCoords[0], (int)newCoords[1]);
            }

        }

        public static void TransformRotationPoint(ref Polygon polygon, double angle, double x, double y)
        {
            // TODO: Поворот вокруг заданной пользователем точки
        }

        public static void TransformRotationCenter(ref Polygon polygon, double angle)
        {
            // TODO: Поворот вокруг своего центра
            // 1) Посчитать координаты центра
            // 2) TransformRotationPoint(ref polygon, angle, center_x, center_y)
        }

        public static void TransformScalePoint(ref Polygon polygon, double scale, double x, double y)
        {
            // TODO: Масштабирование относительно заданной пользователем точки
        }

        public static void TransformScaleCenter(ref Polygon polygon, double scale)
        {
            // TODO: Масштабирование относительно своего центра
            // 1) Посчитать координаты центра
            // 2) TransformScalePoint(ref polygon, scale, center_x, center_y)
        }
    }
}
