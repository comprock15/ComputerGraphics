/// <summary>
/// Аффинные преобразования
/// </summary>
internal class AffineTransformations
{
    /// <summary>
    /// Функция для перемножения двух матриц
    /// </summary>
    public static double[,] Multiply(double[,] matrix1, double[,] matrix2)
    {
        int rows1 = matrix1.GetLength(0);
        int columns1 = matrix1.GetLength(1);
        int rows2 = matrix2.GetLength(0);
        int columns2 = matrix2.GetLength(1);

        if (columns1 != rows2)
            throw new ArgumentException($"Невозможно умножить матрицу {rows1}x{columns1} на матрицу {rows2}x{columns2}");

        double[,] matrix = new double[rows1, columns2];

        for (int i = 0; i < rows1; ++i)
            for (int j = 0; j < columns2; ++j)
                for (int k = 0; k < columns1; ++k)
                    matrix[i, j] += matrix1[i, k] * matrix2[k, j];

        return matrix;
    }

    /// <summary>
    /// Возвращает новые координаты вершин многогранника после применения аффинного преобразования
    /// </summary>
    /// <param name="polyhedron">Многогранник</param>
    /// <param name="matrix">Матрица аффинного преобразования</param>
    public static List<Vertex> RecalculatedCoords(Polyhedron polyhedron, double[,] matrix)
    {
        List<Vertex> newVertices = polyhedron.vertices;
        for (int i = 0; i < polyhedron.vertices.Count; i++)
        {
            double[,] oldCoords = new double[,] { { polyhedron.vertices[i].x, polyhedron.vertices[i].y, polyhedron.vertices[i].z, 1 } };
            double[,] newCoords = Multiply(oldCoords, matrix);
            newVertices[i] = new Vertex(newCoords[0, 0], newCoords[0, 1], newCoords[0, 2]);
        }
        return newVertices;
    }

    /// <summary>
    /// Пересчитывает новые координаты многогранника и изменяет их
    /// </summary>
    /// <param name="polyhedron">Многогранник</param>
    /// <param name="matrix">Матрица аффинного преобразования</param>
    private static void RecalculateCoords(ref Polyhedron polyhedron, double[,] matrix) => polyhedron.vertices = RecalculatedCoords(polyhedron, matrix);

    /// <summary>
    /// Считает координаты центра многогранника
    /// </summary>
    private static double[,] CalculateCenterCoords(Polyhedron polyhedron)
    {
        double xMin = Double.MaxValue;
        double xMax = Double.MinValue;
        double yMin = Double.MaxValue;
        double yMax = Double.MinValue;
        double zMin = Double.MaxValue;
        double zMax = Double.MinValue;

        foreach (Vertex vertex in polyhedron.vertices)
        {
            xMin = Math.Min(xMin, vertex.x);
            xMax = Math.Max(xMax, vertex.x);
            yMin = Math.Min(yMin, vertex.y);
            yMax = Math.Max(yMax, vertex.y);
            zMin = Math.Min(zMin, vertex.z);
            zMax = Math.Max(zMax, vertex.z);
        }

        return new double[1, 3] { { (xMin + xMax) / 2, (yMin + yMax) / 2, (zMin + zMax) / 2 } };
    }

    /// <summary>
    /// Перевод угла из градусов в радианы
    /// </summary>
    /// <param name="angle">Угол в градусах</param>
    /// <returns>Угол в радианах</returns>
    public static double DegreesToRadians(double angle) => angle * (Math.PI / 180);

    /// <summary>
    /// Смещение на dx, dy, dz
    /// </summary>
    /// <param name="polyhedron">Многогранник для преобразования</param>
    /// <param name="dx">Смещение по x</param>
    /// <param name="dy">Смещение по y</param>
    /// <param name="dz">Смещение по z</param>
    public static void Translation(ref Polyhedron polyhedron, double dx, double dy, double dz)
    {
        // Задаём матрицу преобразования
        double[,] matrix = new double[4, 4] {
            {  1,   0,   0,   0 },
            {  0,   1,   0,   0 },
            {  0,   0,   1,   0 },
            { dx,  dy,  dz,   1 }
        };

        // Пересчитываем координаты всех точек
        RecalculateCoords(ref polyhedron, matrix);
    }

    /// <summary>
    /// Поворот вокруr оси X
    /// </summary>
    /// <param name="polyhedron">Многогранник для преобразования</param>
    /// <param name="angle">Угол поворота в градусах</param>
    public static void RotationAboutXAxis(ref Polyhedron polyhedron, double angle)
    {
        angle = DegreesToRadians(angle);

        // Задаём матрицу преобразования
        double[,] matrix = new double[4, 4] {
            { 1,       0,               0,          0 },
            { 0,  Math.Cos(angle), Math.Sin(angle), 0 },
            { 0, -Math.Sin(angle), Math.Cos(angle), 0 },
            { 0,       0,               0,          1 }
        };

        // Пересчитываем координаты всех точек
        RecalculateCoords(ref polyhedron, matrix);
    }

    /// <summary>
    /// Поворот вокруr оси Y
    /// </summary>
    /// <param name="polyhedron">Многогранник для преобразования</param>
    /// <param name="angle">Угол поворота в градусах</param>
    public static void RotationAboutYAxis(ref Polyhedron polyhedron, double angle)
    {
        angle = DegreesToRadians(angle);

        // Задаём матрицу преобразования
        double[,] matrix = new double[4, 4] {
            { Math.Cos(angle), 0, -Math.Sin(angle), 0 },
            {      0,          1,       0,          0 },
            { Math.Sin(angle), 0,  Math.Cos(angle), 0 },
            {      0,          0,       0,          1 }
        };

        // Пересчитываем координаты всех точек
        RecalculateCoords(ref polyhedron, matrix);
    }

    /// <summary>
    /// Поворот вокруr оси Z
    /// </summary>
    /// <param name="polyhedron">Многогранник для преобразования</param>
    /// <param name="angle">Угол поворота в градусах</param>
    public static void RotationAboutZAxis(ref Polyhedron polyhedron, double angle)
    {
        angle = DegreesToRadians(angle);

        // Задаём матрицу преобразования
        double[,] matrix = new double[4, 4] {
                { Math.Cos(angle), Math.Sin(angle), 0, 0 },
                {-Math.Sin(angle), Math.Cos(angle), 0, 0 },
                {      0,               0,          1, 0 },
                {      0,               0,          0, 1 }
            };

        // Пересчитываем координаты всех точек
        RecalculateCoords(ref polyhedron, matrix);
    }

    /// <summary>
    /// Масштабирование относительно центра
    /// </summary>
    /// <param name="polyhedron">Многогранник</param>
    /// <param name="scale">Масштаб</param>
    public static void Scale(ref Polyhedron polyhedron, double scale)
    {
        double[,] center = CalculateCenterCoords(polyhedron);
        // Задаём матрицу преобразования
        double[,] matrix = new double[4, 4] {
            { scale, 0,   0,   0 },
            {   0, scale, 0,   0 },
            {   0,   0, scale, 0 },
            {(1-scale)*center[0,0], (1-scale)*center[0,1], (1-scale)*center[0,2],  1 }
        };

        // Пересчитываем координаты всех точек
        RecalculateCoords(ref polyhedron, matrix);
    }

    /// <summary>
    /// Масштабирование относительно координат
    /// </summary>
    /// <param name="polyhedron">Многогранник</param>
    /// <param name="scaleX">Масштаб по X</param>
    /// <param name="scaleY">Масштаб по Y</param>
    /// <param name="scaleZ">Масштаб по Z</param>
    public static void Scale(ref Polyhedron polyhedron, double scaleX, double scaleY, double scaleZ) {
        double[,] matrix = new double[4, 4] {
            { scaleX, 0,     0,     0 },
            { 0,     scaleY, 0,     0 },
            { 0,     0,     scaleZ, 0 },
            { 0,     0,     0,      1 }
        };

        RecalculateCoords(ref polyhedron, matrix);
    }

    /// <summary>
    /// Поворот относительно прямой
    /// </summary>
    /// <param name="polyhedron">Многогранник</param>
    public static void RotateAboutLine(ref Polyhedron polyhedron, double angle, double x1, double y1, double z1, double x2, double y2, double z2)
    {
        double l = x2 - x1;
        double m = y2 - y1;
        double n = z2 - z1;

        // Нормируем вектор
        double vlength = Math.Sqrt(l * l + m * m + n * n);
        l /= vlength;
        m /= vlength;
        n /= vlength;

        angle = DegreesToRadians(angle);
        double cos = Math.Cos(angle);
        double sin = Math.Sin(angle);

        Translation(ref polyhedron, -x1, -y1, -z1);

        // Задаём матрицу преобразования
        double[,] matrix = new double[4, 4] {
            { l*l + cos*(1-l*l),   l*(1-cos)*m + n*sin, l*(1-cos)*n-m*sin, 0 },
            { l*(1-cos)*m - n*sin, m*m + cos*(1-m*m),   m*(1-cos)*n+l*sin, 0 },
            { l*(1-cos)*n+m*sin,   m*(1-cos)*n-l*sin,   n*n + cos*(1-n*n), 0 },
            {0,0,0,1}
        };

        // Пересчитываем координаты всех точек
        RecalculateCoords(ref polyhedron, matrix);

        Translation(ref polyhedron, x1, y1, z1);
    }

    /// <summary>
    /// Поворот относительно оси
    /// </summary>
    /// <param name="polyhedron">Многогранник</param>
    /// <param name="angle">Угол</param>
    /// <param name="axis">Код оси</param>
    public static void RotateAroundCenter(ref Polyhedron polyhedron, double angle, int axis) {
        double[,] centerCoords = CalculateCenterCoords(polyhedron);
        double centerX = centerCoords[0, 0];
        double centerY = centerCoords[0, 1];
        double centerZ = centerCoords[0, 2];

        Translation(ref polyhedron, -centerX, -centerY, -centerZ);

        switch (axis) {
            case 0:
                RotationAboutXAxis(ref polyhedron, angle);
                break;
            case 1:
                RotationAboutYAxis(ref polyhedron, angle);
                break;
            case 2:
                RotationAboutZAxis(ref polyhedron, angle);
                break;
        }

        Translation(ref polyhedron, centerX, centerY, centerZ);
    }

    /// <summary>
    /// Отражение относительно плоскости
    /// </summary>
    /// <param name="polyhedron">Многогранник</param>
    /// <param name="matrix">Матрица аффинного преобразования</param>
    public static void Reflect(ref Polyhedron polyhedron, double[,] matrix) => RecalculateCoords(ref polyhedron, matrix);
}