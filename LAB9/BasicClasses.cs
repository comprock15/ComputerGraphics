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
    public Vertex(double x, double y, double z) { this.x = x; this.y = y; this.z = z; }
    public Vertex(Vertex other) { this.x = other.x; this.y = other.y; this.z = other.z; }

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
    /// Строковое представление вершины
    /// </summary>
    /// <returns>Координаты вершины</returns>
    public override string ToString() => $"X: {Math.Round(x, 2)}, Y: {Math.Round(y, 2)}, Z: {Math.Round(z, 2)}";
    /// <summary>
    /// Возвращает строковое представление вершины или нормали в формате OBJ.
    /// </summary>
    /// <param name="prefix">Префикс строки (например, "v" для вершины или "vn" для нормали).</param>
    /// <returns>Строка в формате OBJ</returns>
    public string ToObjString(string prefix = "v") => $"{prefix} {x.ToString("G", CultureInfo.InvariantCulture)} " +
        $"{y.ToString("G", CultureInfo.InvariantCulture)} " +
        $"{z.ToString("G", CultureInfo.InvariantCulture)}";
}

/// <summary>
/// Полигон
/// </summary>
internal class Polygon {
    /// <summary>
    /// Список вершин
    /// </summary>
    public List<Vertex> vertices = new List<Vertex>();
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
    /// Имя многогранника
    /// </summary>
    private string name;

    /// <summary>
    /// Задать имя многогранника
    /// </summary>
    public void SetName(string name) => this.name = name;

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
    /// <param name="normals">Список нормалей</param>
    public Polyhedron(List<Vertex> vertices, List<List<int>> edges, List<List<int>> faces, List<Vertex> normals) {
        this.vertices = vertices;
        this.edges = edges;
        this.faces = faces;
        this.normals = normals;
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
            var normal = CrossProduct(v2 - v1, v3 - v1);

            normal = Normalize(normal);

            normals[face[0]] += normal;
            normals[face[1]] += normal;
            normals[face[2]] += normal;
        }

        for (int i = 0; i < normals.Count; ++i)
            normals[i] = Normalize(normals[i]);
    }

    /// <summary>
    /// Нормализация вектора
    /// </summary>
    /// <param name="vec">Вектор</param>
    /// <returns>Нормализованный вектор</returns>
    public static Vertex Normalize(Vertex vec) {
        double length = Math.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
        return new Vertex(vec.x / length, vec.y / length, vec.z / length);
    }

    /// <summary>
    /// Векторное произведение двух векторов
    /// </summary>
    /// <param name="vec1">Первый вектор</param>
    /// <param name="vec2">Второй вектор</param>
    /// <returns>Векторное произведение</returns>
    public static Vertex CrossProduct(Vertex vec1, Vertex vec2) => new Vertex(
            vec1.y * vec2.z - vec1.z * vec2.y,
            vec1.z * vec2.x - vec1.x * vec2.z,
            vec1.x * vec2.y - vec1.y * vec2.x
        );

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
    public override string ToString() {
        if (name == null)
            return base.ToString();
        else
            return name;
    }
}