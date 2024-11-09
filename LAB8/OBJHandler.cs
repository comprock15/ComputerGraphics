using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var vertices = new List<Vertex>();
            var edges = new List<List<int>>();
            var faces = new List<List<int>>();

            foreach (var line in File.ReadLines(filePath)) {
                string[] parts = line.Split(' ');

                if (parts[0] == "v") {
                    double x = double.Parse(parts[1].Replace('.', ','));
                    double y = double.Parse(parts[2].Replace('.', ','));
                    double z = double.Parse(parts[3].Replace('.', ','));
                    vertices.Add(new Vertex(x, y, z));
                    edges.Add(new List<int>());
                }
                else if (parts[0] == "f") {
                    var faceVertices = parts.Skip(1)
                        .Select(p => int.Parse(p.Split('/')[0]) - 1)
                        .Where(index => index >= 0 && index < vertices.Count)
                        .ToList();

                    for (int i = 0; i < faceVertices.Count; ++i) {
                        int start = faceVertices[i];
                        int end = faceVertices[(i + 1) % faceVertices.Count];
                        if (!edges[start].Contains(end)) edges[start].Add(end);
                        if (!edges[end].Contains(start)) edges[end].Add(start);
                    }
                    faces.Add(faceVertices);
                }
            }
            Polyhedron polyhedron = new Polyhedron(vertices, edges, faces);
            polyhedron.SetName(Path.GetFileNameWithoutExtension(filePath));
            return polyhedron;
        }

        /// <summary>
        /// Сохранение многогранника
        /// </summary>
        /// <param name="polyhedron">Многогранник</param>
        /// <param name="filePath">Путь к файлу</param>
        public static void Save(Polyhedron polyhedron, string filePath) {
            using (StreamWriter writer = new StreamWriter(filePath)) {
                foreach (var vertex in polyhedron.vertices)
                    writer.WriteLine($"v {vertex.x} {vertex.y} {vertex.z}");

                var addedFaces = new HashSet<string>();

                foreach (var face in polyhedron.faces) {
                    var faceLine = "f";
                    foreach (var vertexIndex in face)
                        if (vertexIndex < polyhedron.vertices.Count)
                            faceLine += $" {vertexIndex + 1}";
                    if (!addedFaces.Contains(faceLine)) {
                        writer.WriteLine(faceLine);
                        addedFaces.Add(faceLine);
                    }
                }
            }

            return;
        }
    }
}