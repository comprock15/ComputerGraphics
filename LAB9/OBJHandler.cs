using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


namespace LAB9 {
    /// <summary>
    /// Обработчик obj-файлов
    /// </summary>
    internal class OBJHandler {
        /// <summary>
        /// Загрузка многогранника
        /// </summary>
        /// <param name="filePath">Путь к obj-файлу</param>
        /// <returns>Многогранник</returns>
        public static Polyhedron Load(string filePath) {
            var vertices = new List<Vertex>();
            var edges = new List<List<int>>();
            var faces = new List<List<int>>();
            var normals = new List<Vertex>();
            var textureCoordinates = new List<Vertex>();

            foreach (var line in File.ReadLines(filePath)) {
                string[] parts = line.Split(' ');

                if (parts[0] == "v") { // вершина
                    double x = double.Parse(parts[1].Replace('.', ','));
                    double y = double.Parse(parts[2].Replace('.', ','));
                    double z = double.Parse(parts[3].Replace('.', ','));
                    vertices.Add(new Vertex(x, y, z));
                    edges.Add(new List<int>());
                }
                else if (parts[0] == "vn") { // нормаль
                    double nx = double.Parse(parts[1].Replace('.', ','));
                    double ny = double.Parse(parts[2].Replace('.', ','));
                    double nz = double.Parse(parts[3].Replace('.', ','));
                    normals.Add(new Vertex(nx, ny, nz));
                }
                else if (parts[0] == "vt") { // текстурные координаты
                    var us = parts[1].Replace('.', ',');
                    var uv = parts[2].Replace('.', ',');
                    double u = double.Parse(parts[1].Replace('.', ','));
                    double v = double.Parse(parts[2].Replace('.', ','));
                    textureCoordinates.Add(new Vertex(0, 0, 0, u, v));
                }
                else if (parts[0] == "f") { // Грань
                    var faceVertices = parts.Skip(1)
                        .Select(p => {
                            var vertexIndices = p.Split('/');
                            int vertexIndex = int.Parse(vertexIndices[0]) - 1;
                            int textureIndex = vertexIndices.Length > 1 && !string.IsNullOrEmpty(vertexIndices[1])
                                ? int.Parse(vertexIndices[1]) - 1 : -1;
                            int normalIndex = vertexIndices.Length > 2 && !string.IsNullOrEmpty(vertexIndices[2])
                                ? int.Parse(vertexIndices[2]) - 1 : -1;

                            return (vertexIndex, textureIndex, normalIndex);
                        })
                        .Where(t => t.vertexIndex >= 0 && t.vertexIndex < vertices.Count)
                        .ToList();

                    for (int i = 0; i < faceVertices.Count; ++i) {
                        int start = faceVertices[i].vertexIndex;
                        int end = faceVertices[(i + 1) % faceVertices.Count].vertexIndex;
                        if (!edges[start].Contains(end)) edges[start].Add(end);
                        if (!edges[end].Contains(start)) edges[end].Add(start);
                    }

                    faces.Add(faceVertices.Select(fv => fv.vertexIndex).ToList());
                }
            }
            Polyhedron polyhedron = normals.Count > 0
                ? new Polyhedron(vertices, edges, faces, normals, textureCoordinates)
                : new Polyhedron(vertices, edges, faces);

            foreach (var face in faces) {
                Console.WriteLine(string.Join(", ", face));
            }

            polyhedron.SetName(Path.GetFileNameWithoutExtension(filePath));
            return polyhedron;
        }

        /// <summary>
        /// Сохранение многогранника
        /// </summary>
        /// <param name="polyhedron">Многогранник</param>
        /// <param name="filePath">Путь к obj-файлу</param>
        public static void Save(Polyhedron polyhedron, string filePath) {
            using (StreamWriter writer = new StreamWriter(filePath)) {
                // Сохраняем вершины
                foreach (var vertex in polyhedron.vertices)
                    writer.WriteLine(vertex.ToOBJString());

                // Сохраняем нормали
                if (polyhedron.normals != null && polyhedron.normals.Count > 0)
                    foreach (var normal in polyhedron.normals)
                        writer.WriteLine(normal.ToOBJString("vn"));

                // Сохраняем текстурные координаты
                if (polyhedron.textureCoordinates != null && polyhedron.textureCoordinates.Count > 0)
                    foreach (var texCoord in polyhedron.textureCoordinates)
                        writer.WriteLine(texCoord.ToOBJTextureString());

                // Сохраняем грани (пока неверно, заглушка)
                foreach (var face in polyhedron.faces) {
                    var faceLine = "f";
                    foreach (var vertexIndex in face) {
                        int textureIndex = (polyhedron.textureCoordinates?.Count > vertexIndex) ? vertexIndex + 1 : 0;
                        int normalIndex = (polyhedron.normals?.Count > vertexIndex) ? vertexIndex + 1 : 0;

                        if (textureIndex > 0 && normalIndex > 0)
                            faceLine += $" {vertexIndex + 1}/{textureIndex}/{normalIndex}";
                        else if (textureIndex > 0)
                            faceLine += $" {vertexIndex + 1}/{textureIndex}";
                        else if (normalIndex > 0)
                            faceLine += $" {vertexIndex + 1}//{normalIndex}";
                        else
                            faceLine += $" {vertexIndex + 1}";
                    }
                    writer.WriteLine(faceLine);
                }
            }
        }
    }
}