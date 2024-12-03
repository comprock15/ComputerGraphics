using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

/// <summary>
/// Вершина
/// </summary>
internal class Vertex {
    /// <summary>
    /// Координата по X
    /// </summary>
    public double x;
    /// <summary>
    /// Координата по Y
    /// </summary>
    public double y;
    /// <summary>
    /// Координата по Z
    /// </summary>
    public double z;
    /// <summary>
    /// Координата U (по оси X текстуры)
    /// </summary>
    public double u;
    /// <summary>
    /// Координата V (по оси Y текстуры)
    /// </summary>
    public double v;
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="x">Координата по X</param>
    /// <param name="y">Координата по Y</param>
    /// <param name="z">Координата по Z</param>
    public Vertex(double x, double y, double z) { this.x = x; this.y = y; this.z = z; }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="x">Координата по X</param>
    /// <param name="y">Координата по Y</param>
    /// <param name="z">Координата по Z</param>
    /// <param name="u">Координата U (по оси X текстуры)</param>
    /// <param name="v">Координата V (по оси Y текстуры)</param>
    public Vertex(double x, double y, double z, double u, double v) { this.x = x; this.y = y; this.z = z; this.u = u; this.v = v; }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="other">Вершина</param>
    public Vertex(Vertex other) { x = other.x; y = other.y; z = other.z; u = other.u; v = other.v; }

    /// <summary>
    /// Проверка на равенство двух вершин
    /// </summary>
    /// <param name="vertex1">Вершина 1</param>
    /// <param name="vertex2">Вершина 1</param>
    /// <returns>Равны ли координаты двух вершин</returns>
    static public bool operator ==(Vertex vertex1, Vertex vertex2) => vertex1.x == vertex2.x && vertex1.y == vertex2.y && vertex1.z == vertex2.z;
    /// <summary>
    /// Проверка на неравенство двух вершин
    /// </summary>
    /// <param name="vertex1">Вершина 1</param>
    /// <param name="vertex2">Вершина 1</param>
    /// <returns>Не равны ли координаты двух вершин</returns>
    static public bool operator !=(Vertex vertex1, Vertex vertex2) => !(vertex1 == vertex2);
    /// <summary>
    /// Сложение двух вершин
    /// </summary>
    /// <param name="vertex1">Вершина 1</param>
    /// <param name="vertex2">Вершина 1</param>
    /// <returns>Вершина из суммы координат двух вершин</returns>
    static public Vertex operator +(Vertex vertex1, Vertex vertex2) => new Vertex(vertex1.x + vertex2.x, vertex1.y + vertex2.y, vertex1.z + vertex2.z);
    /// <summary>
    /// Разность двух вершин
    /// </summary>
    /// <param name="vertex1">Вершина 1</param>
    /// <param name="vertex2">Вершина 1</param>
    /// <returns>Вершина из разности координат двух вершин</returns>
    static public Vertex operator -(Vertex vertex1, Vertex vertex2) => new Vertex(vertex1.x - vertex2.x, vertex1.y - vertex2.y, vertex1.z - vertex2.z);

    /// <summary>
    /// Скалярное произведение векторов
    /// </summary>
    /// <param name="vec1">Вектор 1</param>
    /// <param name="vec2">Вектор 2</param>
    static public double Dot(Vertex vec1, Vertex vec2) => vec1.x * vec2.x + vec1.y * vec2.y + vec1.z * vec2.z;

    /// <summary>
    /// Векторное произведение двух векторов
    /// </summary>
    /// <param name="vec1">Первый вектор</param>
    /// <param name="vec2">Второй вектор</param>
    /// <returns>Векторное произведение</returns>
    static public Vertex CrossProduct(Vertex vec1, Vertex vec2) => new Vertex(
            vec1.y * vec2.z - vec1.z * vec2.y,
            vec1.z * vec2.x - vec1.x * vec2.z,
            vec1.x * vec2.y - vec1.y * vec2.x
        );

    /// <summary>
    /// Нормализация вектора
    /// </summary>
    /// <param name="vec">Вектор</param>
    /// <returns>Нормализованный вектор</returns>
    static public Vertex Normalize(Vertex vec) {
        double length = Math.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
        return new Vertex(vec.x / length, vec.y / length, vec.z / length);
    }
    /// <summary>
    /// Строковое представление вершины
    /// </summary>
    /// <returns>Координаты вершины</returns>
    public override string ToString() => $"X: {Math.Round(x, 2)}, Y: {Math.Round(y, 2)}, Z: {Math.Round(z, 2)}";
    /// <summary>
    /// Возвращает строковое представление вершины или нормали в формате OBJ
    /// </summary>
    /// <param name="prefix">Префикс строки (например, "v" для вершины или "vn" для нормали)</param>
    /// <returns>Строка в формате OBJ</returns>
    public string ToOBJString(string prefix = "v") => $"{prefix} {x.ToString("G", CultureInfo.InvariantCulture)} " +
        $"{y.ToString("G", CultureInfo.InvariantCulture)} " +
        $"{z.ToString("G", CultureInfo.InvariantCulture)}";
    /// <summary>
    /// Возвращает строковое представление текстурных координат в формате OBJ
    /// </summary>
    /// <returns>Строка в формате OBJ для текстурных координат</returns>
    public string ToOBJTextureString() => $"vt {u.ToString("G", CultureInfo.InvariantCulture)} {v.ToString("G", CultureInfo.InvariantCulture)}";
}

/// <summary>
/// Полигон
/// </summary>
internal class Polygon {
    /// <summary>
    /// Список вершин
    /// </summary>
    public List<Vertex> vertices = new List<Vertex>();
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertices">Список вершин</param>
    public Polygon(List<Vertex> vertices) => this.vertices = vertices;
}

/// <summary>
/// Многогранник
/// </summary>
internal class Polyhedron {
    /// <summary>
    /// Список вершин
    /// </summary>
    public List<Vertex> vertices = new List<Vertex>();
    /// <summary>
    /// Список рёбер
    /// </summary>
    public List<List<int>> edges = new List<List<int>>();
    /// <summary>
    /// Список граней
    /// </summary>
    public List<List<int>> faces = new List<List<int>>();
    /// <summary>
    /// Список нормалей для каждой вершины
    /// </summary>
    public List<Vertex> normals = new List<Vertex>();
    /// <summary>
    /// Список текстурных координат
    /// </summary>
    public List<Vertex> textureCoordinates = new List<Vertex>();
    /// <summary>
    /// Список индексов текстурных координат для каждой грани
    /// </summary>
    public List<List<int>> faceTextureIndices = new List<List<int>>();
    /// <summary>
    /// Список индексов нормалей для каждой грани
    /// </summary>
    public List<List<int>> faceNormalIndices = new List<List<int>>();
    /// <summary>
    /// Текстура, связанная с многогранником
    /// </summary>
    public System.Drawing.Bitmap texture { get; set; } = null;
    /// <summary>
    /// Имя многогранника
    /// </summary>
    private string name;
    /// <summary>
    /// Задать имя многогранника
    /// </summary>
    public void SetName(string name) => this.name = name;
    /// <summary>
    /// Вернуть имя многогранника
    /// </summary>
    /// <returns>Имя многогранника</returns>
    public string GetName() => name;
    /// <summary>
    /// Получить индексы текстурных координат для грани
    /// </summary>
    /// <param name="faceIndex">Индекс грани</param>
    /// <returns>Список индексов текстурных координат</returns>
    public List<int> GetTextureIndices(int faceIndex) {
        if (faceTextureIndices == null || faceIndex < 0 || faceTextureIndices.Count <= faceIndex)
            return new List<int>();
        return faceTextureIndices[faceIndex];
    }
    /// <summary>
    /// Получить индексы нормалей для грани
    /// </summary>
    /// <param name="faceIndex">Индекс грани</param>
    /// <returns>Список индексов нормалей</returns>
    public List<int> GetNormalIndices(int faceIndex) {
        if (faceNormalIndices == null || faceIndex < 0 || faceNormalIndices.Count <= faceIndex)
            return new List<int>();
        return faceNormalIndices[faceIndex];
    }
    /// <summary>
    /// Задать индексы текстурных координат для грани
    /// </summary>
    /// <param name="faceIndex">Индекс грани</param>
    /// <param name="indices">Список индексов текстурных координат</param>
    public void SetTextureIndices(int faceIndex, List<int> indices) {
        if (faceTextureIndices == null)
            faceTextureIndices = new List<List<int>>();
        while (faceTextureIndices.Count <= faceIndex)
            faceTextureIndices.Add(new List<int>());
        faceTextureIndices[faceIndex] = indices;
    }
    /// <summary>
    /// Задать индексы нормалей для грани
    /// </summary>
    /// <param name="faceIndex">Индекс грани</param>
    /// <param name="indices">Список индексов нормалей</param>
    public void SetNormalIndices(int faceIndex, List<int> indices) {
        if (faceNormalIndices == null)
            faceNormalIndices = new List<List<int>>();
        while (faceNormalIndices.Count <= faceIndex)
            faceNormalIndices.Add(new List<int>());
        faceNormalIndices[faceIndex] = indices;
    }
    /// <summary>
    /// Добавить текстурный индекс для вершины в грани
    /// </summary>
    /// <param name="faceIndex">Индекс грани</param>
    /// <param name="textureIndex">Индекс текстуры</param>
    public void AddTextureIndex(int faceIndex, int textureIndex) {
        if (faceTextureIndices == null)
            faceTextureIndices = new List<List<int>>();
        while (faceTextureIndices.Count <= faceIndex)
            faceTextureIndices.Add(new List<int>());
        faceTextureIndices[faceIndex].Add(textureIndex);
    }
    /// <summary>
    /// Добавить нормальный индекс для вершины в грани
    /// </summary>
    /// <param name="faceIndex">Индекс грани</param>
    /// <param name="normalIndex">Индекс нормали</param>
    public void AddNormalIndex(int faceIndex, int normalIndex) {
        if (faceNormalIndices == null)
            faceNormalIndices = new List<List<int>>();
        while (faceNormalIndices.Count <= faceIndex)
            faceNormalIndices.Add(new List<int>());
        faceNormalIndices[faceIndex].Add(normalIndex);
    }

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public Polyhedron() { }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertices">Список вершин</param>
    /// <param name="edges">Список рёбер</param>
    /// <param name="faces">Список граней</param>
    public Polyhedron(List<Vertex> vertices, List<List<int>> edges, List<List<int>> faces = null) {
        this.vertices = vertices;
        this.edges = edges;
        this.faces = faces;
        CalculateVertexNormals();
    }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertices">Список вершин</param>
    /// <param name="edges">Список рёбер</param>
    /// <param name="faces">Список граней</param>
    /// <param name="normals">Список нормалей для каждой вершины</param>
    public Polyhedron(List<Vertex> vertices, List<List<int>> edges, List<List<int>> faces, List<Vertex> normals) {
        this.vertices = vertices;
        this.edges = edges;
        this.faces = faces;
        this.normals = normals;
    }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertices">Список вершин</param>
    /// <param name="edges">Список рёбер</param>
    /// <param name="faces">Список граней</param>
    /// <param name="normals">Список нормалей для каждой вершины</param>
    /// <param name="textureCoordinates">Список текстурных координат</param>
    public Polyhedron(List<Vertex> vertices, List<List<int>> edges, List<List<int>> faces, List<Vertex> normals, List<Vertex> textureCoordinates) : this(vertices, edges, faces, normals) {
        this.textureCoordinates = textureCoordinates;
    }
    /// <summary>
    ///Конструктор
    /// </summary>
    /// <param name="vertices">Список вершин</param>
    /// <param name="edges">Список рёбер</param>
    /// <param name="faces">Список граней</param>
    /// <param name="normals">Список нормалей для каждой вершины</param>
    /// <param name="textureCoordinates">Список текстурных координат</param>
    /// <param name="faceTextureIndices">Список индексов текстурных координат для каждой грани</param>
    /// <param name="faceNormalIndices">Список индексов нормалей для каждой грани</param>
    public Polyhedron(List<Vertex> vertices, List<List<int>> edges, List<List<int>> faces, List<Vertex> normals, List<Vertex> textureCoordinates, List<List<int>> faceTextureIndices, List<List<int>> faceNormalIndices) : this(vertices, edges, faces, normals, textureCoordinates) {
        this.faceTextureIndices = faceTextureIndices;
        this.faceNormalIndices = faceNormalIndices;
    }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="polyhedron">Многогранник</param>
    public Polyhedron(Polyhedron polyhedron) {
        vertices = polyhedron.vertices;
        edges = polyhedron.edges;
        faces = polyhedron.faces;
        normals = polyhedron.normals;
        textureCoordinates = polyhedron.textureCoordinates;
        faceTextureIndices = polyhedron.faceTextureIndices;
        faceNormalIndices = polyhedron.faceNormalIndices;
    }

    /// <summary>
    /// Вычисляет нормали для каждой вершины многогранника
    /// </summary>
    public void CalculateVertexNormals() {
        normals.Clear();
        foreach (var vertex in vertices)
            normals.Add(new Vertex(0, 0, 0));

        foreach (var face in faces) {
            var v1 = vertices[face[0]];
            var v2 = vertices[face[1]];
            var v3 = vertices[face[2]];

            // нормаль для грани
            var normal = Vertex.CrossProduct(v2 - v1, v3 - v1);

            normal = Vertex.Normalize(normal);

            normals[face[0]] += normal;
            normals[face[1]] += normal;
            normals[face[2]] += normal;
        }

        for (int i = 0; i < normals.Count; ++i)
            normals[i] = Vertex.Normalize(normals[i]);
    }

    /// <summary>
    /// Возвращает точку центра многогранника
    /// </summary>
    /// <returns></returns>
    public Vertex Center() {
        double x = vertices.Average(v => v.x);
        double y = vertices.Average(v => v.y);
        double z = vertices.Average(v => v.z);
        return new Vertex(x, y, z);
    }

    /// <summary>
    /// Строковое представление
    /// </summary>
    /// <returns>Строковое представление</returns>
    public override string ToString() => name ?? base.ToString();
}