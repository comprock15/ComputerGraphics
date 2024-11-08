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
}