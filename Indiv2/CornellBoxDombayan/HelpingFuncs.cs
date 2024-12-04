using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indiv2
{
    class Geometry
    {
        // возвращает точку спроецированную на вектор
        public static Point getPointProjection(Point origin, Vector direction, Point projected)
        {
            double parameter = (direction.x * (projected.x - origin.x) + direction.y * (projected.y - origin.y) + direction.z * (projected.z - origin.z)) 
                                / (Math.Pow(direction.x, 2) + Math.Pow(direction.y, 2) + Math.Pow(direction.z, 2));
            return pointOnLine(origin, direction, parameter);
        }

        public static double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2) + Math.Pow(p2.z - p1.z, 2));
        }

        // возвращает точку на векторе
        public static Point pointOnLine(Point origin, Vector direction, double parameter)
        {
            return new Point(origin.x + direction.x * parameter, origin.y + direction.y * parameter, origin.z + direction.z * parameter);
        }

        public static double degreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }


        public static double Clamp(double val, double min, double max)
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

    }
}
