using LAB9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

static class Texturing {
    public static void DrawTexturedFace(List<int> face, ref Bitmap bmp, ref double[,] zBuffer, Bitmap texture, List<Vertex> transformedVertices, List<Vertex> uvs) {
        var triangles = ZBuffer.Triangulate(transformedVertices, face);
        foreach (var triangle in triangles) {
            var texCoords = triangle.Select(v => uvs[transformedVertices.IndexOf(v)]).ToList();
            DrawTexturedTriangle(triangle, ref bmp, ref zBuffer, texture, texCoords);
        }
    }

    private static void DrawTexturedTriangle(List<Vertex> triangle, ref Bitmap bmp, ref double[,] zBuffer, Bitmap texture, List<Vertex> texCoords) {
        if (texture == null)
            return;

        if (triangle.Count != 3 || texCoords.Count != 3)
            throw new ArgumentException("Треугольник или текстурные координаты заданы некорректно.");

        var (v1, v2, v3) = (triangle[0], triangle[1], triangle[2]);
        var (uv1, uv2, uv3) = (texCoords[0], texCoords[1], texCoords[2]);

        var bounds = new {
            minX = Math.Max(0, (int)Math.Min(v1.x, Math.Min(v2.x, v3.x))),
            maxX = Math.Min(bmp.Width - 1, (int)Math.Max(v1.x, Math.Max(v2.x, v3.x))),
            minY = Math.Max(0, (int)Math.Min(v1.y, Math.Min(v2.y, v3.y))),
            maxY = Math.Min(bmp.Height - 1, (int)Math.Max(v1.y, Math.Max(v2.y, v3.y)))
        };

        for (int y = bounds.minY; y <= bounds.maxY; ++y) {
            for (int x = bounds.minX; x <= bounds.maxX; ++x) {
                var baryCoords = CalculateBarycentricCoordinates(x, y, v1, v2, v3);
                if (baryCoords.alpha >= 0 && baryCoords.beta >= 0 && baryCoords.gamma >= 0) {
                    double z = baryCoords.alpha * v1.z + baryCoords.beta * v2.z + baryCoords.gamma * v3.z;
                    if (z > zBuffer[x, y]) {
                        zBuffer[x, y] = z;

                        double u = baryCoords.alpha * uv1.u + baryCoords.beta * uv2.u + baryCoords.gamma * uv3.u;
                        double v = baryCoords.alpha * uv1.v + baryCoords.beta * uv2.v + baryCoords.gamma * uv3.v;

                        int texX = Math.Max(0, Math.Min(texture.Width - 1, (int)(u * texture.Width)));
                        int texY = Math.Max(0, Math.Min(texture.Height - 1, (int)(v * texture.Height)));

                        bmp.SetPixel(x, y, texture.GetPixel(texX, texY));
                    }
                }
            }
        }
    }

    private static (double alpha, double beta, double gamma) CalculateBarycentricCoordinates(double px, double py, Vertex v1, Vertex v2, Vertex v3) {
        double detT = (v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y);
        double alpha = ((v2.y - v3.y) * (px - v3.x) + (v3.x - v2.x) * (py - v3.y)) / detT;
        double beta = ((v3.y - v1.y) * (px - v3.x) + (v1.x - v3.x) * (py - v3.y)) / detT;
        double gamma = 1 - alpha - beta;
        return (alpha, beta, gamma);
    }
}
