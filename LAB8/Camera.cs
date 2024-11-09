using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Camera
{
    /// <summary>
    /// Положение камеры
    /// </summary>
    public Point3 position;

    /// <summary>
    /// Направление обзора камеры
    /// </summary>
    public Point3 direction;

    /// <summary>
    /// Матрица проекционного преобразования
    /// </summary>
    private double[,] projectionMatrix = new double[,] {
                        { 1, 0, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 0, 1, 1 / 1000 },
                        { 0, 0, 0, 1 }
                    };

    public class Point3
    {
        public double x;
        public double y;
        public double z;

        public Point3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public Camera(Point3 position, Point3 direction)
    {
        this.position = position;
        this.direction = direction;
    }

    public (List<Polyhedron>, string) GetPolyhedronsInCameraCoordinates(List<Polyhedron> polyhedrons)
    {
        //List<Polyhedron> newPolyhedrons = new List<Polyhedron>(polyhedrons);
        List<Polyhedron> newPolyhedrons = polyhedrons.Select(p => new Polyhedron(p)).ToList();

        double[] angles = GetAngles();

        double[,] xRotationMatrix = new double[4, 4] {
            { 1,       0,               0,          0 },
            { 0,  Math.Cos(angles[0]), Math.Sin(angles[0]), 0 },
            { 0, -Math.Sin(angles[0]), Math.Cos(angles[0]), 0 },
            { 0,       0,               0,          1 }
        };

        double[,] yRotationMatrix = new double[4, 4] {
            { Math.Cos(angles[1]), 0, -Math.Sin(angles[1]), 0 },
            {      0,          1,       0,          0 },
            { Math.Sin(angles[1]), 0,  Math.Cos(angles[1]), 0 },
            {      0,          0,       0,          1 }
        };

        double[,] zRotationMatrix = new double[4, 4] {
                { Math.Cos(angles[2]), Math.Sin(angles[2]), 0, 0 },
                {-Math.Sin(angles[2]), Math.Cos(angles[2]), 0, 0 },
                {      0,               0,          1, 0 },
                {      0,               0,          0, 1 }
            };

        double[,] translationMatrix = new double[4, 4] {
            {  1,   0,   0,   0 },
            {  0,   1,   0,   0 },
            {  0,   0,   1,   0 },
            { -position.x,  -position.y,  -position.z,   1 }
        };

        double[,] viewMatrix = AffineTransformations.Multiply(
            AffineTransformations.Multiply(
                AffineTransformations.Multiply(
                    AffineTransformations.Multiply(
                        xRotationMatrix,
                        yRotationMatrix),
                    zRotationMatrix),
                translationMatrix),
            projectionMatrix);

        foreach (Polyhedron polyhedron in newPolyhedrons)
        {
            polyhedron.vertices = AffineTransformations.RecalculatedCoords(polyhedron, viewMatrix);
        }

        return (newPolyhedrons, AnglesString(angles));
    }

    private double[] GetAngles()
    {
        double[] angles = new double[3];
        angles[0] = GetAngleBetweenVectors(direction, new Point3(1, 0, 0));
        angles[1] = GetAngleBetweenVectors(direction, new Point3(0, 1, 0));
        angles[2] = GetAngleBetweenVectors(direction, new Point3(0, 0, 1));
        return angles;
    }

    public string AnglesString(double[] angles)
    {
        return string.Join(", ", angles);
    }

    private double GetAngleBetweenVectors(Point3 v1, Point3 v2)
    {
        return Math.Acos((v1.x*v2.x + v1.y*v2.y + v1.z*v2.z) / 
                         (Math.Sqrt(v1.x*v1.x + v1.y * v1.y + v1.z * v1.z) * Math.Sqrt(v2.x * v2.x + v2.y * v2.y + v2.z * v2.z)));
    }
    
}