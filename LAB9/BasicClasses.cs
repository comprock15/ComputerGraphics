using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Вершина
/// </summary>
internal class Vertex
{
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
}

/// <summary>
/// Полигон
/// </summary>
internal class Polygon
{
    /// <summary>
    /// Список вершин
    /// </summary>
    public List<Vertex> vertices = new List<Vertex>();
    public Polygon(List<Vertex> vertices) => this.vertices = vertices;
}

/// <summary>
/// Многогранник
/// </summary>
internal class Polyhedron
{
    /// <summary>
    /// Список вершин
    /// </summary>
    public List<Vertex> vertices = new List<Vertex>();
    //матрица смежности -
    // кол-во элементов листа = колву вершин, каждой вершине сорответствует лист в котором перечислены номера вершин с которыми она смежна 
    /// <summary>
    /// Список рёбер
    /// </summary>
    public List<List<int>> edges = new List<List<int>>();

    /// <summary>
    /// Список граней
    /// </summary>
    public List<List<int>> faces = new List<List<int>>();

    private string name;
    /// <summary>
    /// Задать имя многогранника
    /// </summary>
    public void SetName(string name) => this.name = name;
    
    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public Polyhedron()
    {
        //vertices.Add(new Vertex(0,0,0));
        //edges.Add(new List<int>() { });
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertices">Список вершин</param>
    /// <param name="edges">Список рёбер</param>
    /// <param name="faces">Список граней</param>
    public Polyhedron(List<Vertex> vertices, List<List<int>> edges, List<List<int>> faces = null)
    {
        this.vertices = vertices;
        this.edges = edges;
        this.faces = faces;
    }

    public Polyhedron(Polyhedron polyhedron)
    {
        this.vertices = polyhedron.vertices;
        this.edges = polyhedron.edges;
        this.faces = polyhedron.faces;
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

    public override string ToString()
    {
        if (name == null)
            return base.ToString();
        else
            return name;
    }
}