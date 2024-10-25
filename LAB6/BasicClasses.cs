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

//internal class Edge
//{
//    public Vertex v1;
//    public Vertex v2;
//    public Edge(Vertex v1, Vertex v2)
//    {
//        this.v1 = v1; this.v2 = v2;
//    }
//}

internal class Polyhedron
{
    public List<Vertex> vertices;
    //матрица смежности -
    // кол-во элементов листа = колву вершин, каждой вершине сорответствует лист в котором перечислены номера вершин с которыми она смежна 
    public List<SortedSet<int>> edges;
    
    public Polyhedron(Vertex v)
    {
        vertices.Add(v);
        edges.Add(new SortedSet<int>() { });
    }
    public void AddVertex(Vertex new_v, SortedSet<int> edgs)
    {
        //TODO: добавить добапвление в списки старых вершин новую вершину если её там ещё нет
        vertices.Add(new_v);
        edges.Add(edgs);
    }
}

