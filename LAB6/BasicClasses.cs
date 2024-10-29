using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Vertex
{
    public double x;
    public double y;
    public double z;
    public Vertex(double x, double y, double z)
    {
        this.x = x; this.y = y; this.z = z;
    }
}

internal class Polygon
{
    public List<Vertex> vertices;
    public Polygon(List<Vertex> vertices)
    {
        this.vertices = vertices;
    }
}


internal class Polyhedron
{
    public List<Vertex> vertices;
    //матрица смежности -
    // кол-во элементов листа = колву вершин, каждой вершине сорответствует лист в котором перечислены номера вершин с которыми она смежна 
    public List<List<int>> edges;
    
    // Конструктор по умолчанию
    public Polyhedron()
    {
        vertices.Add(new Vertex(0,0,0));
        edges.Add(new List<int>() { });
    }

    public Polyhedron(List<Vertex> vertices, List<List<int>> edges)
    {
        this.vertices = vertices;
        this.edges = edges;
    }
}