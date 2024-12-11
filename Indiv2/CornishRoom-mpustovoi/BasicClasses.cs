using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CornishRoom_mpustovoi {
    /// <summary>
    /// Вершина/вектор
    /// </summary>
    public class Vertor {
        /// <summary>
        /// Координата по X
        /// </summary>
        public double x = 0;
        /// <summary>
        /// Координата по Y
        /// </summary>
        public double y = 0;
        /// <summary>
        /// Координата по Z
        /// </summary>
        public double z = 0;

        /// <summary>
        /// Конструктор вершины/вектора
        /// </summary>
        public Vertor() { }
        /// <summary>
        /// Конструктор вершины/вектора
        /// </summary>
        /// <param name="x">Координата по X</param>
        /// <param name="y">Координата по Y</param>
        /// <param name="z">Координата по Z</param>
        public Vertor(double x, double y, double z) { this.x = x; this.y = y; this.z = z; }
        /// <summary>
        /// Конструктор вершины/вектора
        /// </summary>
        /// <param name="v">Другая вершина/вектора</param>
        public Vertor(Vertor v) { x = v.x; y = v.y; z = v.z; }

        /// <summary>
        /// Возвращает длину вектора
        /// </summary>
        /// <returns>Длина вектора</returns>
        public double Length() => Math.Sqrt(x * x + y * y + z * z);

        /// <summary>
        /// Нормализация вектора
        /// </summary>
        /// <param name="v">Вектор для нормализации</param>
        /// <returns>Нормализованный вектор</returns>
        public static Vertor Normalize(Vertor v) {
            double z = v.Length();
            return z == 0 ? new Vertor(v) : new Vertor(v.x / z, v.y / z, v.z / z);
        }

        /// <summary>
        /// Скалярное произведение двух векторов
        /// </summary>
        /// <param name="v1">Первый вектор</param>
        /// <param name="v2">Второй вектор</param>
        /// <returns>Результат скалярного произведения</returns>
        public static double Scalar(Vertor v1, Vertor v2) => v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;

        /// <summary>
        /// Вычитание одного вектора из другого
        /// </summary>
        /// <param name="v1">Первый вектор</param>
        /// <param name="v2">Второй вектор</param>
        /// <returns>Разность векторов</returns>
        public static Vertor operator -(Vertor v1, Vertor v2) => new Vertor(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        /// <summary>
        /// Сложение двух векторов
        /// </summary>
        /// <param name="v1">Первый вектор</param>
        /// <param name="v2">Второй вектор</param>
        /// <returns>Сумма векторов</returns>
        public static Vertor operator +(Vertor v1, Vertor v2) => new Vertor(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        /// <summary>
        /// Векторное произведение двух векторов
        /// </summary>
        /// <param name="v1">Первый вектор</param>
        /// <param name="v2">Второй вектор</param>
        /// <returns>Результат векторного произведения</returns>
        public static Vertor operator *(Vertor v1, Vertor v2) => new Vertor(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
        /// <summary>
        /// Умножение вектора на скаляр
        /// </summary>
        /// <param name="c">Скаляр</param>
        /// <param name="v">Вектор</param>
        /// <returns>Результат умножения</returns>
        public static Vertor operator *(double c, Vertor v) => new Vertor(v.x * c, v.y * c, v.z * c);
        /// <summary>
        /// Умножение вектора на скаляр
        /// </summary>
        /// <param name="v">Вектор</param>
        /// <param name="c">Скаляр</param>
        /// <returns>Результат умножения</returns>
        public static Vertor operator *(Vertor v, double c) => new Vertor(v.x * c, v.y * c, v.z * c);
        /// <summary>
        /// Вычитание скаляра из вектора
        /// </summary>
        /// <param name="v">Вектор</param>
        /// <param name="c">Скаляр</param>
        /// <returns>Результат вычитания</returns>
        public static Vertor operator -(Vertor v, double c) => new Vertor(v.x - c, v.y - c, v.z - c);
        /// <summary>
        /// Вычитание вектора из скаляра
        /// </summary>
        /// <param name="c">Скаляр</param>
        /// <param name="v">Вектор</param>
        /// <returns>Результат вычитания</returns>
        public static Vertor operator -(double c, Vertor v) => new Vertor(c - v.x, c - v.y, c - v.z);
        /// <summary>
        /// Сложение скаляра с вектором
        /// </summary>
        /// <param name="v">Вектор</param>
        /// <param name="c">Скаляр</param>
        /// <returns>Результат сложения</returns>
        public static Vertor operator +(Vertor v, double c) => new Vertor(v.x + c, v.y + c, v.z + c);
        /// <summary>
        /// Сложение скаляра с вектором
        /// </summary>
        /// <param name="c">Скаляр</param>
        /// <param name="v">Вектор</param>
        /// <returns>Результат сложения</returns>
        public static Vertor operator +(double c, Vertor v) => new Vertor(v.x + c, v.y + c, v.z + c);
        /// <summary>
        /// Деление вектора на скаляр
        /// </summary>
        /// <param name="v">Вектор</param>
        /// <param name="c">Скаляр</param>
        /// <returns>Результат деления</returns>
        public static Vertor operator /(Vertor v, double c) => new Vertor(v.x / c, v.y / c, v.z / c);
        /// <summary>
        /// Деление скаляра на вектор
        /// </summary>
        /// <param name="c">Скаляр</param>
        /// <param name="v">Вектор</param>
        /// <returns>Результат деления</returns>
        public static Vertor operator /(double c, Vertor v) => new Vertor(c / v.x, c / v.y, c / v.z);
    }

    /// <summary>
    /// Ребро
    /// </summary>
    public class Edge {
        /// <summary>
        /// Вершины
        /// </summary>
        public List<Vertor> vertices = new List<Vertor> { };
        /// <summary>
        /// Материал
        /// </summary>
        public double[] material = new double[5];
        /// <summary>
        /// Цвет
        /// </summary>
        public Color color = Color.Black;

        /// <summary>
        /// Конструктор ребра
        /// </summary>
        public Edge() { }
        /// <summary>
        /// Конструктор ребра
        /// </summary>
        /// <param name="vertices">Вершины</param>
        public Edge(List<Vertor> vertices) => this.vertices = vertices.Select(v => new Vertor(v)).ToList();
        /// <summary>
        /// Конструктор ребра
        /// </summary>
        /// <param name="edge">Ребро</param>
        public Edge(Edge edge) {
            vertices = edge.vertices.Select(v => new Vertor(v)).ToList();
            material = (double[])edge.material.Clone();
            color = edge.color;
        }
    }

    /// <summary>
    /// Многогранник
    /// </summary>
    public class Polyhedron {
        /// <summary>
        /// Рёбра
        /// </summary>
        public List<Edge> edges = new List<Edge>();
        /// <summary>
        /// Материал
        /// </summary>
        public double[] material = new double[5];
        /// <summary>
        /// Цвет материала
        /// </summary>
        public Color materialColor = new Color();

        /// <summary>
        /// Конструктор многогранника
        /// </summary>
        public Polyhedron() { }
        /// <summary>
        /// Конструктор многогранника
        /// </summary>
        /// <param name="edges">Рёбра</param>
        public Polyhedron(List<Edge> edges) => this.edges = new List<Edge>(edges.Select(e => new Edge(e)));
        /// <summary>
        /// Конструктор многогранника
        /// </summary>
        /// <param name="polyhedron">Другой многогранник</param>
        public Polyhedron(Polyhedron polyhedron) {
            edges = new List<Edge>(polyhedron.edges.Select(e => new Edge(e)));
            material = (double[])polyhedron.material.Clone();
            materialColor = polyhedron.materialColor;
        }

        /// <summary>
        /// Устанавливает материал для рёбер
        /// </summary>
        /// <param name="material">Материал</param>
        public void SetMaterial(double[] material) => edges.ForEach(x => x.material = material);

        /// <summary>
        /// Пересечение луча с треугольником
        /// </summary>
        /// <param name="ray">Луч</param>
        /// <param name="v0">Первая вершина треугольника</param>
        /// <param name="v1">Вторая вершина треугольника</param>
        /// <param name="v2">Третья вершина треугольника</param>
        /// <param name="intersect">Параметр пересечения</param>
        /// <returns>Истина, если пересечение найдено</returns>
        public bool IntersectRayWithTriangle(Ray ray, Vertor v0, Vertor v1, Vertor v2, out double intersect) {
            var eps = 0.0001;
            Vertor edge1 = v1 - v0;
            Vertor edge2 = v2 - v0;
            Vertor e2cross = ray.direction * edge2;
            double det = Vertor.Scalar(edge1, e2cross);
            intersect = -1;
            if (-eps < det && det < eps)
                return false;

            Vertor rayPosv0 = ray.position - v0;
            double u = Vertor.Scalar(rayPosv0, e2cross) / det;
            if (u < 0 || 1 < u)
                return false;

            Vertor e1cross = rayPosv0 * edge1;
            double v = Vertor.Scalar(ray.direction, e1cross) / det;
            if (v < 0 || 1 < v + u)
                return false;

            double inters = Vertor.Scalar(edge2, e1cross) / det;
            if (inters < eps)
                return false;

            intersect = inters;
            return true;
        }

        /// <summary>
        /// Проверяет пересечение луча с многогранником
        /// </summary>
        /// <param name="ray">Луч</param>
        /// <param name="intersect">Параметр пересечения</param>
        /// <param name="normalVector">Нормаль в точке пересечения</param>
        /// <returns>Истина, если пересечение найдено</returns>
        public virtual bool IntersectPolyhedrons(Ray ray, out double intersect, out Vertor normalVector) {
            intersect = 0;
            normalVector = null;
            Edge side = null;
            foreach (var polyhedronSide in edges) {
                if (polyhedronSide.vertices.Count == 3) {
                    if (IntersectRayWithTriangle(ray, polyhedronSide.vertices[0], polyhedronSide.vertices[1], polyhedronSide.vertices[2], out double t) && (intersect == 0 || t < intersect)) {
                        intersect = t;
                        side = polyhedronSide;
                    }
                }
                else if (polyhedronSide.vertices.Count == 4) {
                    if (IntersectRayWithTriangle(ray, polyhedronSide.vertices[0], polyhedronSide.vertices[1], polyhedronSide.vertices[3], out double t) && (intersect == 0 || t < intersect)) {
                        intersect = t;
                        side = polyhedronSide;
                    }
                    else if (IntersectRayWithTriangle(ray, polyhedronSide.vertices[1], polyhedronSide.vertices[2], polyhedronSide.vertices[3], out t) && (intersect == 0 || t < intersect)) {
                        intersect = t;
                        side = polyhedronSide;
                    }
                }
            }
            if (intersect != 0) {
                normalVector = Vertor.Normalize((side.vertices[1] - side.vertices[0]) * (side.vertices[side.vertices.Count - 1] - side.vertices[0]));
                materialColor = side.color;
                material = side.material;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Создаёт многогранник в форме куба
        /// </summary>
        /// <param name="radius">Размер куба</param>
        /// <returns>Многогранник-куб</returns>
        public static Polyhedron Hexahedron(int radius) {
            var hSize = radius / 2;
            var vertices = new List<Vertor> {
                new Vertor(-hSize, hSize, -hSize),
                new Vertor(hSize, hSize, -hSize),
                new Vertor(hSize, -hSize, -hSize),
                new Vertor(-hSize, -hSize, -hSize),
                new Vertor(-hSize, hSize, hSize),
                new Vertor(hSize, hSize, hSize),
                new Vertor(hSize, -hSize, hSize),
                new Vertor(-hSize, -hSize, hSize)
            };

            var faces = new int[][] {
                new[] { 0, 1, 2, 3 },
                new[] { 0, 4, 5, 1 },
                new[] { 4, 7, 6, 5 },
                new[] { 5, 6, 2, 1 },
                new[] { 4, 0, 3, 7 },
                new[] { 3, 2, 6, 7 }
            };

            var polyhedron = new Polyhedron();
            foreach (var face in faces)
                polyhedron.edges.Add(new Edge { vertices = face.Select(index => vertices[index]).ToList() });

            return polyhedron;
        }
    }

    /// <summary>
    /// Шар
    /// </summary>
    public class Sphere : Polyhedron {
        /// <summary>
        /// Радиус шара
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Погрешность для проверки пересечений
        /// </summary>
        private static readonly double eps = 0.001;

        /// <summary>
        /// Конструктор шара
        /// </summary>
        /// <param name="v">Позиция центра шара</param>
        /// <param name="radius">Радиус шара</param>
        public Sphere(Vertor v, double radius) {
            edges.Add(new Edge(new List<Vertor> { v }));
            Radius = radius;
        }
        /// <summary>
        /// Конструктор шара
        /// </summary>
        /// <param name="sphere">Исходный шар</param>
        public Sphere(Sphere sphere) {
            edges = new List<Edge>();
            edges.Add(new Edge(sphere.edges[0]));
            Radius = sphere.Radius;
            materialColor = sphere.materialColor;
            material = (double[])sphere.material.Clone();
        }

        /// <summary>
        /// Проверка пересечения луча с шаром
        /// </summary>
        /// <param name="ray">Луч</param>
        /// <param name="intersection">Точка пересечения</param>
        /// <param name="normalVector">Нормаль на поверхности шара в точке пересечения</param>
        /// <returns>Возвращает true, если пересечение найдено</returns>
        public override bool IntersectPolyhedrons(Ray ray, out double intersection, out Vertor normalVector) {
            normalVector = null;
            Vertor position = edges[0].vertices[0];
            Vertor cv = ray.position - position;
            double sc1 = Vertor.Scalar(cv, ray.direction);
            double sc2 = Vertor.Scalar(cv, cv) - Radius * Radius;
            double d = sc1 * sc1 - sc2;
            intersection = 0;
            if (d >= 0) {
                double sqrtd = Math.Sqrt(d);
                double t1 = -sc1 + sqrtd;
                double t2 = -sc1 - sqrtd;

                double tMin = Math.Min(t1, t2);
                double tMax = Math.Max(t1, t2);

                intersection = (tMin > eps) ? tMin : tMax;
            }
            if (intersection > eps) {
                normalVector = (ray.position + ray.direction * intersection) - position;
                normalVector = Vertor.Normalize(normalVector);

                materialColor = edges[0].color;
                material = edges[0].material;
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Луч
    /// </summary>
    public class Ray {
        /// <summary>
        /// Начальная точка луча
        /// </summary>
        public Vertor position = null;
        /// <summary>
        /// Направление луча
        /// </summary>
        public Vertor direction = null;

        /// <summary>
        /// Конструктор луча
        /// </summary>
        public Ray() { }
        /// <summary>
        /// Конструктор луча
        /// </summary>
        /// <param name="v1">Начальная точка</param>
        /// <param name="v2">Конечная точка</param>
        public Ray(Vertor v1, Vertor v2) {
            position = new Vertor(v1);
            direction = Vertor.Normalize(v2 - v1);
        }
        /// <summary>
        /// Конструктор луча
        /// </summary>
        /// <param name="r">Другой луч</param>
        public Ray(Ray r) {
            position = new Vertor(r.position);
            direction = new Vertor(r.direction);
        }

        /// <summary>
        /// Вычисляет преломлённый луч
        /// </summary>
        /// <param name="lightIntersectionPosition">Точка пересечения луча с поверхностью</param>
        /// <param name="normalVector">Нормаль к поверхности в точке пересечения</param>
        /// <param name="refraction">Индекс преломления среды</param>
        /// <param name="refractVal">Индекс преломления среды, в которую входит луч</param>
        /// <returns>Преломленный луч или null, если преломления не происходит</returns>
        public Ray Refract(Vertor lightIntersectionPosition, Vertor normalVector, double refraction, double refractVal) {
            Ray ray = new Ray { position = new Vertor(lightIntersectionPosition) };
            double scalar = Vertor.Scalar(normalVector, direction);
            double refracted = refraction / refractVal;
            double theta = 1 - refracted * refracted * (1 - scalar * scalar);

            if (theta >= 0) {
                double cosTheta = Math.Sqrt(theta);
                ray.direction = Vertor.Normalize(direction * refracted - (cosTheta + refracted * scalar) * normalVector);
                return ray;
            }
            else
                return null;
        }

        /// <summary>
        /// Вычисляет отраженный луч
        /// </summary>
        /// <param name="lightIntersectionPosition">Точка пересечения луча с поверхностью</param>
        /// <param name="normalVector">Нормаль к поверхности в точке пересечения</param>
        /// <returns>Отраженный луч</returns>
        public Ray Reflect(Vertor lightIntersectionPosition, Vertor normalVector) => new Ray(lightIntersectionPosition, lightIntersectionPosition + direction - 2 * normalVector * Vertor.Scalar(direction, normalVector));
    }

    /// <summary>
    /// Источник света
    /// </summary>
    public class LightSource {
        /// <summary>
        /// Позиция
        /// </summary>
        public Vertor position = null;

        /// <summary>
        /// Конструктор источника света
        /// </summary>
        public LightSource() { }
        /// <summary>
        /// Конструктор источника света
        /// </summary>
        /// <param name="p">Позиция источника света</param>
        public LightSource(Vertor p) => position = new Vertor(p);
        /// <summary>
        /// Конструктор источника света
        /// </summary>
        /// <param name="light">Другой источник света</param>
        public LightSource(LightSource light) => position = new Vertor(light.position);

        /// <summary>
        /// Вычисление освещения
        /// </summary>
        /// <param name="lightIntersectionPosition">Точка пересечения луча с поверхностью</param>
        /// <param name="normalVector">Нормаль к поверхности в точке пересечения</param>
        /// <param name="materialColor">Цвет материала поверхности</param>
        /// <param name="diffuse">Интенсивность рассеянного света</param>
        /// <returns>Цвет освещения в виде вектора</returns>
        public Vertor Shading(Vertor lightIntersectionPosition, Vertor normalVector, Color materialColor, double diffuse) {
            Vertor dir = position - lightIntersectionPosition;
            dir = Vertor.Normalize(dir);

            Vertor diff = diffuse * new Vertor(1f, 1f, 1f) * Math.Max(Vertor.Scalar(normalVector, dir), 0.1);

            return new Vertor(
                diff.x * materialColor.R / 255.0,
                diff.y * materialColor.G / 255.0,
                diff.z * materialColor.B / 255.0
            );
        }
    }
}