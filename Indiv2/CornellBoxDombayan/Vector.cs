using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indiv2
{
    class Point
    {
        public double x, y, z;
        public Point(double x, double y, double z)
        {
            this.x = x; this.y = y; this.z = z;
        }
    }

    class Vector
    {
        public double x, y, z;

        public Vector(double x, double y, double z, bool isNeedNormalize = false)
        {
            double normalization = isNeedNormalize ? Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2)) : 1;
            this.x = x / normalization;
            this.y = y / normalization;
            this.z = z / normalization;
        }

        public Vector(Point start, Point end, bool isVectorNeededToBeNormalized = false) : this(end.x - start.x, end.y - start.y, end.z - start.z, isVectorNeededToBeNormalized) { }

        // Нормализовать вектор
        public Vector Normalize()
        {
            double normalization = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
            x = x / normalization;
            y = y / normalization;
            z = z / normalization;
            return this;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        
        // Умножение всех координат вектора на число
        public static Vector operator *(double k, Vector b)
        {
            return new Vector(k * b.x, k * b.y, k * b.z);
        }

        // Векторное произведение
        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        // Скалярное произведение
        public static double Dot(Vector a, Vector b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public double Length()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }
    }
}
