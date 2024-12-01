using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LAB9 {
    /// <summary>
    /// Отсечение нелицевых граней
    /// </summary>
    internal class BackfaceCulling {
        /// <summary>
        /// Отсечение нелицевых граней и отрисовка
        /// </summary>
        /// <param name="matrix">Матрица трансформации для преобразования вершин</param>
        /// <param name="polyhedrons">Список многоугольников для обработки</param>
        /// <param name="width">Ширина изображения</param>
        /// <param name="height">Высота изображения</param>
        /// <returns>Bitmap с лицевыми гранями многоугольников</returns>
        public static Bitmap Cull(double[,] matrix, ListBox.ObjectCollection polyhedrons, int width, int height) {
            var bmp = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bmp);
            graphics.Clear(Color.White);

            // Вектор обзора, направленный из экрана к пользователю
            var viewDirection = new Vertex(0, 0, -1);

            var random = new Random(42);

            for (var i = 0; i < polyhedrons.Count; ++i) {
                var polyhedron = polyhedrons[i] as Polyhedron;
                var transformedVertices = new List<Vertex>();

                // Преобразование вершин с использованием матрицы трансформации
                foreach (var v in polyhedron.vertices) {
                    var result = AffineTransformations.Multiply(new double[,] { { v.x, v.y, v.z, 1 } }, matrix);
                    transformedVertices.Add(new Vertex(result[0, 0], result[0, 1], result[0, 2]));
                }

                var visibleFaces = new List<List<int>>();

                // Определение видимых граней по нормалям
                foreach (var face in polyhedron.faces) {
                    var normal = Normalize(face, transformedVertices);
                    var scalar = normal.x * viewDirection.x + normal.y * viewDirection.y + normal.z * viewDirection.z;
                    if (scalar < 0) visibleFaces.Add(face); // Грань видима, если нормаль направлена к наблюдателю (по оси Z)
                }

                // Отрисовка видимых граней
                foreach (var face in visibleFaces) {
                    var points = new List<Point>();
                    for (var j = 0; j < face.Count; ++j) {
                        var projectedPoint = ProjectTo2D(transformedVertices[face[j]]);
                        points.Add(new Point((int)projectedPoint.x, (int)projectedPoint.y));
                    }

                    // Генерация случайного цвета для каждой грани
                    var color = Color.FromArgb(random.Next(100, 256), random.Next(100, 256), random.Next(100, 256));

                    // Закрашивание грани случайным цветом
                    graphics.FillPolygon(new SolidBrush(color), points.ToArray());
                    graphics.DrawPolygon(Pens.Black, points.ToArray());
                }
            }

            graphics.Dispose();
            return bmp;
        }

        /// <summary>
        /// Вычисляет вектор нормали для грани
        /// </summary>
        /// <param name="face">Вершины грани</param>
        /// <param name="vertices">Вершины многоугольника</param>
        /// <returns>Вектор нормали</returns>
        public static Vertex Normalize(List<int> face, List<Vertex> vertices) => Vertex.CrossProduct(vertices[face[1]] - vertices[face[0]], vertices[face[2]] - vertices[face[0]]);

        /// <summary>
        /// Параллельная проекция вершины на 2D экран
        /// </summary>
        /// <param name="v">Вершина</param>
        /// <returns>Параллельная проекция вершины</returns>
        public static Vertex ProjectTo2D(Vertex v) => new Vertex(v.x, v.y, 0);
    }
}
