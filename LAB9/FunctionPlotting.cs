using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;

static class FunctionPlotting
{
    public static Polyhedron GetPlot(string expressionString, double x0, double x1, double y0, double y1, int step)
    {
        Expression expression = Infix.Parse(expressionString).ResultValue;
        var symbols = new Dictionary<string, FloatingPoint>() { { "x", 0 }, { "y", 0 } };

        Polyhedron polyhedron = new Polyhedron();

        // Вычисление координат вершин
        double dx = (x1 - x0) / step;
        double dy = (y1 - y0) / step;
        for (int i = 0; i <= step; ++i)
        {
            for (int j = 0; j <= step; ++j)
            {
                var x = x0 + i * dx;
                var y = y0 + j * dy;
                symbols["x"] = x;
                symbols["y"] = y;
                double z = Evaluate.Evaluate(symbols, expression).RealValue;
                polyhedron.vertices.Add(new Vertex(x, y, z));
            }
        }

        // Соединение вершин ребрами
        for (int i = 0; i <= step; ++i)
        {
            for (int j = 0; j <= step; ++j)
            {
                int index = i * (step + 1) + j;
                polyhedron.edges.Add(new List<int>());

                if (i < step)
                    polyhedron.edges[index].Add(index + step + 1);
                if (j < step)
                    polyhedron.edges[index].Add(index + 1);

                // Добавление грани
                if (i < step && j < step)
                    polyhedron.faces.Add(new List<int>() { index, index + 1, index + step + 2, index + step + 1 });
            }
        }

        polyhedron.SetName(expressionString);
        return polyhedron;
    }
}