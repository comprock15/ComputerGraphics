using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Indiv2
{
    class LightSource
    {
        public Point location;
        public double intensity;

        public LightSource(Point location, double intensivity)
        {
            this.location = location;
            this.intensity = intensivity;
        }
    }


    // все константы для Фонга https://en.wikipedia.org/wiki/Phong_reflection_model
    class Material
    {
        //Константа альфа для бликов
        // a shininess constant for this material, which is larger for surfaces that are smoother and more mirror-like. When this constant is large the specular highlight is small.
        // 
        public double shininess;
        // specular reflection constant, the ratio of reflection of the specular term of incoming light
        public double specular;
        //  diffuse reflection constant, the ratio of reflection of the diffuse term of incoming light
        public double diffuse;
        // an ambient reflection constant, the ratio of reflection of the ambient term present in all points in the scene rendered
        public double ambient;

        public double reflectivity = 0.0;
        

        public Material(double shininess, double specular, double diffuse, double ambient)
        {
            this.shininess = shininess;
            this.specular = specular;
            this.diffuse = diffuse;
            this.ambient = ambient;
        }
    }


    abstract class Shape
    {
        public Point center;
        public Color color;
        public Material material;

        /// <summary>
        /// по направлениювектора и началу вектора выдаёт точку перечения с объектом и нормаль плоскости которую пересекли  
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public abstract Tuple<Point, Vector> getIntersect(Vector direction, Point origin);
    }

    
    class Face : Shape
    {
        double width, height;
        public Vector normale;
        public Vector heightVector, widthVector;

        public Face(Point center, Vector normale, Vector heightVector, double width, double height)
        {
            this.center = center;
            this.width = width;
            this.height = height;
            this.normale = normale;
            this.heightVector = heightVector.Normalize();
            this.widthVector = (normale * heightVector).Normalize();
        }

        public Face(Point center, Vector normale, Vector heightVector, double width, double height, Color color, Material material)
        {
            this.color = color;
            this.center = center;
            this.width = width;
            this.height = height;
            this.normale = normale;
            this.material = material;
            this.heightVector = heightVector.Normalize();
            this.widthVector = (normale * heightVector).Normalize();
        }

        public Point worldToFaceBasis(Point p)
        {
            return new Point(
                widthVector.x * (p.x - center.x) + widthVector.y * (p.y - center.y) +
                widthVector.z * (p.z - center.z),
                heightVector.x * (p.x - center.x) + heightVector.y * (p.y - center.y) +
                heightVector.z * (p.z - center.z),
                normale.x * (p.x - center.x) + normale.y * (p.y - center.y) +
                normale.z * (p.z - center.z));
        }

        public override Tuple<Point, Vector> getIntersect(Vector direction, Point origin)
        {
            if (Math.Abs(normale.x * (origin.x - center.x) + normale.y * (origin.y - center.y) + normale.z * (origin.z - center.z)) < 0.00001) // точка выпуска луча лежит в плоскости
            {
                return null;
            }

            double tn = -(normale.x * center.x) - (normale.y * center.y) - (normale.z * center.z) + (normale.x * origin.x) + (normale.y * origin.y) + (normale.z * origin.z);
            if (Math.Abs(tn) < 0.00001)          // прямая лежит на плоскости, если знаменатель тоже 0
            {
                return null;
            }
            double td = normale.x * direction.x + normale.y * direction.y + normale.z * direction.z;
            if (Math.Abs(td) < 0.00001)          // прямая параллельна плоскости
            {
                return null;
            }
            var pointInWorld = Geometry.pointOnLine(origin, direction, -tn / td);
            if (new Vector(origin, pointInWorld).z / direction.z < 0)     // вектор из полученной точки в ориджин всегда коллинеарен направлению, 
            {                                                            // но если х1/x2=y1/у2=z1/z2 - отрицательны, они противонаправлены
                return null;
            }

            var pointOnSurface = worldToFaceBasis(pointInWorld);

            // точка пересечения не за пределами панели 
            if (Math.Abs(pointOnSurface.x) <= width / 2 && Math.Abs(pointOnSurface.y) <= height / 2)
            {
                return Tuple.Create(pointInWorld, normale);
            }
            return null;
        }
    }

    class Cube : Shape
    {
        double side;
        private List<Face> faces;
        public Cube(Point center, double side, Color color, Material material)
        {
            this.center = center;
            this.side = side;
            this.color = color;
            this.material = material;
            faces = new List<Face>
            {
                new Face(new Point(center.x, center.y, center.z - side / 2), new Vector(0, 0, -1), new Vector(0, 1, 0), side, side),
                new Face(new Point(center.x, center.y, center.z + side / 2), new Vector(0, 0, 1), new Vector(0, 1, 0), side, side),
                new Face(new Point(center.x, center.y + side / 2, center.z), new Vector(0, 1, 0), new Vector(0, 0, 1), side, side),
                new Face(new Point(center.x, center.y - side / 2, center.z), new Vector(0, -1, 0), new Vector(0, 0, 1), side, side),
                new Face(new Point(center.x + side / 2, center.y, center.z), new Vector(1, 0, 0), new Vector(0, 1, 0), side, side),
                new Face(new Point(center.x - side / 2, center.y, center.z), new Vector(-1, 0, 0), new Vector(0, 1, 0), side, side)
            };
        }

        public override Tuple<Point, Vector> getIntersect(Vector direction, Point origin)
        {
            double nearestPoint = double.MaxValue;
            Tuple<Point, Vector> res = null;
            foreach (var face in faces)
            {
                Tuple<Point, Vector> intersectionAndNormale;
                if ((intersectionAndNormale = face.getIntersect(direction, origin)) != null && Geometry.distance(origin, intersectionAndNormale.Item1) < nearestPoint)
                {
                    nearestPoint = Geometry.distance(origin, intersectionAndNormale.Item1);
                    res = intersectionAndNormale;
                }
            }
            return res;
        }
    }

    
    class Sphere : Shape
    {
        double radius;

        public Sphere(Point center, double radius, Color color, Material material)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.material = material;
        }


        
        public override Tuple<Point, Vector> getIntersect(Vector direction, Point origin)
        {
            direction = direction.Normalize();
            Vector sourceToCenter = new Vector(origin, center);
              //определяем угол между векторами
            if (Vector.Dot(sourceToCenter, direction) < 0)   // Если меньше нуля значит векторы напралены в разные стороны и вектор направления направелен не в сторону центар сферы
                                                             // Центр сферы за точкой выпуска луча
            {
                if (Geometry.distance(origin, center) > radius)                       //расстояние от точки начала ветора от центра сферы больше чем радиус значит
                                                                                      //Пересечения нет
                {
                    return null;
                }
                else if (Geometry.distance(origin, center) - radius < 0.000001)       // расстояние от точки начала вектора до центра сферы равно радиусу
                                                                                      // Мы на сфере
                {
                    
                    return null;
                }
                else                                                                // Мы внутри сферы
                {
                    Point projection = Geometry.getPointProjection(origin, direction, center);
                    double distance = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(Geometry.distance(center, projection), 2)) - Geometry.distance(origin, projection);
                    var intersection = Geometry.pointOnLine(origin, direction, distance);
                    return Tuple.Create(intersection, new Vector(center, intersection, true));
                }
            }
            else     // луч направлен на сферу
            {
                //проекция центра сферы на вектор
                Point projection = Geometry.getPointProjection(origin, direction, center);
                // если радиус меньше чем расстояние от точки проекции от центра сферы значит точка проекции лежит не на сфере значит она не является точкой пересечения значит она не нужна
                if (Geometry.distance(center, projection) > radius)
                {
                    return null;
                }
                else
                {
                    // рассчёт расстояния до точки пересечения (теорема пифагора)
                    double distance = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(Geometry.distance(center, projection), 2));
                    if (Geometry.distance(origin, center) > radius) // начало луча за сферой
                    {
                        distance = Geometry.distance(origin, projection) - distance;
                    }
                    else // начало луча в сфере
                    {
                        distance = Geometry.distance(origin, projection) + distance;
                    }
                    var intersection = Geometry.pointOnLine(origin, direction, distance);
                    return Tuple.Create(intersection, new Vector(center, intersection, true));
                }
            }
        }
    }
}
