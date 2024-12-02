using System.Collections.Generic;
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

            var faceTextureIndices = new List<List<int>>();
            var faceNormalIndices = new List<List<int>>();

            foreach (var line in File.ReadLines(filePath)) {
                string[] parts = line.Split(' ');

                switch (parts[0]) {
                    case "v": // Вершина
                        double x = double.Parse(parts[1].Replace('.', ','));
                        double y = double.Parse(parts[2].Replace('.', ','));
                        double z = double.Parse(parts[3].Replace('.', ','));
                        vertices.Add(new Vertex(x, y, z));
                        edges.Add(new List<int>());
                        break;

                    case "vn": // Нормаль
                        double nx = double.Parse(parts[1].Replace('.', ','));
                        double ny = double.Parse(parts[2].Replace('.', ','));
                        double nz = double.Parse(parts[3].Replace('.', ','));
                        normals.Add(new Vertex(nx, ny, nz));
                        break;

                    case "vt": // Текстурные координаты
                        double u = double.Parse(parts[1].Replace('.', ','));
                        double v = double.Parse(parts[2].Replace('.', ','));
                        textureCoordinates.Add(new Vertex(0, 0, 0, u, v));
                        break;

                    case "f": // Грань
                        var faceVertices = new List<int>();
                        var faceTexIndices = new List<int>();
                        var faceNormIndices = new List<int>();

                        foreach (var p in parts.Skip(1)) {
                            var vertexIndices = p.Split('/');
                            int vertexIndex = int.Parse(vertexIndices[0]) - 1;
                            int textureIndex = vertexIndices.Length > 1 && !string.IsNullOrEmpty(vertexIndices[1])
                                ? int.Parse(vertexIndices[1]) - 1 : -1;
                            int normalIndex = vertexIndices.Length > 2 && !string.IsNullOrEmpty(vertexIndices[2])
                                ? int.Parse(vertexIndices[2]) - 1 : -1;

                            faceVertices.Add(vertexIndex);
                            faceTexIndices.Add(textureIndex);
                            faceNormIndices.Add(normalIndex);
                        }

                        faces.Add(faceVertices);
                        faceTextureIndices.Add(faceTexIndices);
                        faceNormalIndices.Add(faceNormIndices);

                        for (int i = 0; i < faceVertices.Count; ++i) {
                            int start = faceVertices[i];
                            int end = faceVertices[(i + 1) % faceVertices.Count];
                            if (!edges[start].Contains(end)) edges[start].Add(end);
                            if (!edges[end].Contains(start)) edges[end].Add(start);
                        }
                        break;
                }
            }
            var polyhedron = new Polyhedron(vertices, edges, faces, normals, textureCoordinates, faceTextureIndices, faceNormalIndices);

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

                // Сохраняем грани
                for (int i = 0; i < polyhedron.faces.Count; ++i) {
                    var face = polyhedron.faces[i];
                    var textureIndices = polyhedron.GetTextureIndices(i);
                    var normalIndices = polyhedron.GetNormalIndices(i);

                    var faceLine = "f";
                    for (int j = 0; j < face.Count; ++j) {
                        int vertexIndex = face[j] + 1;
                        int textureIndex = (textureIndices.Count > j && textureIndices[j] >= 0) ? textureIndices[j] + 1 : 0;
                        int normalIndex = (normalIndices.Count > j && normalIndices[j] >= 0) ? normalIndices[j] + 1 : 0;

                        if (textureIndex > 0 && normalIndex > 0)
                            faceLine += $" {vertexIndex}/{textureIndex}/{normalIndex}";
                        else if (textureIndex > 0)
                            faceLine += $" {vertexIndex}/{textureIndex}";
                        else if (normalIndex > 0)
                            faceLine += $" {vertexIndex}//{normalIndex}";
                        else
                            faceLine += $" {vertexIndex}";
                    }
                    writer.WriteLine(faceLine);
                }
            }
        }
    }
}