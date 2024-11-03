using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LAB7 {
    /// <summary>
    /// Обработчик OBJHandler
    /// </summary>
    internal class OBJHandler {
        /// <summary>
        /// Загрузка многогранника
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns>Многогранник</returns>
        public static Polyhedron Load(string filePath) {
            List<Vertex> vertices = new List<Vertex>();
            List<List<int>> edges = new List<List<int>>();
            HashSet<string> edgesHash = new HashSet<string>();

            foreach (var line in File.ReadLines(filePath)) {
                string[] parts = line.Split(' ');

                if (parts[0] == "v") {
                    double x = double.Parse(parts[1]);
                    double y = double.Parse(parts[2]);
                    double z = double.Parse(parts[3]);
                    vertices.Add(new Vertex(x, y, z));
                    edges.Add(new List<int>());
                }
                else if (parts[0] == "f") {
                    int start = int.Parse(parts[1]) - 1;
                    int end = int.Parse(parts[2]) - 1;

                    string edgeKey = $"{start}-{end}";
                    string reverseEdgeKey = $"{end}-{start}";

                    if (start >= 0 && end >= 0 && start < vertices.Count && end < vertices.Count
                        && edgesHash.Add(edgeKey) && edgesHash.Add(reverseEdgeKey)) {
                        edges[start].Add(end);
                        edges[end].Add(start);
                    }
                }
            }

            return new Polyhedron(vertices, edges);
        }


        /// <summary>
        /// Сохранение многогранника
        /// </summary>
        /// <param name="polyhedron">Многогранник</param>
        /// <param name="filePath">Путь к файлу</param>
        public static void Save(Polyhedron polyhedron, string filePath) {
            using (var writer = new StreamWriter(filePath))
            {

                foreach (var vertex in polyhedron.vertices)
                    writer.WriteLine($"v {vertex.x} {vertex.y} {vertex.z}");

                var addedEdges = new HashSet<string>();

                for (int i = 0; i < polyhedron.edges.Count; ++i)
                {
                    foreach (var edge in polyhedron.edges[i])
                    {
                        string edgeKey = $"{i}-{edge}";
                        string reverseEdgeKey = $"{edge}-{i}";

                        if (i < polyhedron.vertices.Count && edge < polyhedron.vertices.Count
                            && addedEdges.Add(edgeKey) && addedEdges.Add(reverseEdgeKey))
                        {
                            writer.WriteLine($"f {i + 1} {edge + 1}");
                        }
                    }
                }
            }
        }
    }
}