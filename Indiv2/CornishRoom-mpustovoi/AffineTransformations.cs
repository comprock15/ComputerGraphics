using System;

namespace CornishRoom_mpustovoi {
    /// <summary>
    /// Аффинные преобразования
    /// </summary>
    public static class AffineTransformations {
        /// <summary>
        /// Произведение матриц
        /// </summary>
        /// <param name="m1">Матрица 1</param>
        /// <param name="m2">Матрица 2</param>
        /// <returns>Произведение матриц m1 и m2</returns>
        public static double[,] MultiplyMatrices(double[,] m1, double[,] m2) {
            var m = new double[1, 4];

            for (int i = 0; i < 4; ++i) {
                var sum = 0.0;
                for (int j = 0; j < 4; ++j)
                    sum += m1[0, j] * m2[j, i];
                m[0, i] = sum;
            }
            return m;
        }

        /// <summary>
        /// Поворот многогранника
        /// </summary>
        /// <param name="polyhedron">Многогранник</param>
        /// <param name="angleX">Угол поворота по X</param>
        /// <param name="angleY">Угол поворота по Y</param>
        /// <param name="angleZ">Угол поворота по Z</param>
        /// <returns>Повёрнутый многогранник</returns>
        public static Polyhedron Rotate(Polyhedron polyhedron, double angleX, double angleY, double angleZ) {
            Polyhedron newPolyhedron = new Polyhedron();
            foreach (var edge in polyhedron.edges) {
                Edge newEdge = new Edge();
                foreach (var point in edge.vertices) {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.x;
                    m[0, 1] = point.y;
                    m[0, 2] = point.z;
                    m[0, 3] = 1;

                    var angle = angleX * Math.PI / 180;
                    double[,] matrx = new double[4, 4] {
                        { Math.Cos(angle), 0, Math.Sin(angle), 0},
                        { 0, 1, 0, 0 },
                        {-Math.Sin(angle), 0, Math.Cos(angle), 0 },
                        { 0, 0, 0, 1 }
                    };

                    angle = angleY * Math.PI / 180;
                    double[,] matry = new double[4, 4] {
                        { 1, 0, 0, 0 },
                        { 0, Math.Cos(angle), -Math.Sin(angle), 0},
                        {0, Math.Sin(angle), Math.Cos(angle), 0 },
                        { 0, 0, 0, 1 }
                    };

                    angle = angleZ * Math.PI / 180;
                    double[,] matrz = new double[4, 4] {
                        { Math.Cos(angle), -Math.Sin(angle), 0, 0},
                        { Math.Sin(angle), Math.Cos(angle), 0, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 0, 1 }
                    };

                    var mRes = MultiplyMatrices(m, matrx);
                    mRes = MultiplyMatrices(mRes, matry);
                    mRes = MultiplyMatrices(mRes, matrz);

                    newEdge.vertices.Add(new Vertor(mRes[0, 0], mRes[0, 1], mRes[0, 2]));
                }
                newPolyhedron.edges.Add(newEdge);
            }
            return newPolyhedron;
        }

        /// <summary>
        /// Смещение многогранника
        /// </summary>
        /// <param name="polyhedron"></param>
        /// <param name="dx">Смещение по X</param>
        /// <param name="dy">Смещение по Y</param>
        /// <param name="dz">Смещение по Z</param>
        /// <returns>Смещённый многогранник</returns>
        public static Polyhedron Translation(Polyhedron polyhedron, double dx, double dy, double dz) {
            Polyhedron newEdges = new Polyhedron();
            foreach (var edge in polyhedron.edges) {
                Edge newPoints = new Edge();
                foreach (var point in edge.vertices) {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.x;
                    m[0, 1] = point.y;
                    m[0, 2] = point.z;
                    m[0, 3] = 1;

                    double[,] matr = new double[4, 4] {
                        { 1, 0, 0, 0},
                        { 0, 1, 0, 0 },
                        {0, 0, 1, 0 },
                        { dx, -dy, dz, 1 }
                    };

                    var final_matrix = MultiplyMatrices(m, matr);

                    newPoints.vertices.Add(new Vertor(final_matrix[0, 0], final_matrix[0, 1], final_matrix[0, 2]));
                }
                newEdges.edges.Add(newPoints);
            }
            return newEdges;
        }
    }
}