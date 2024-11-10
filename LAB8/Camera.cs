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
    public Vector3 position;

    /// <summary>
    /// Направление обзора камеры
    /// </summary>
    public Vector3 rotation;

    public class Vector3
    {
        public double x;
        public double y;
        public double z;

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public Camera(Vector3 position, Vector3 rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }

    public Camera()
    {
        this.position = new Vector3(0, 0, 0);
        this.rotation = new Vector3(0, 0, 0);
    }

    public List<Polyhedron> GetPolyhedronsInCameraCoordinates(List<Polyhedron> polyhedrons)
    {
        //List<Polyhedron> newPolyhedrons = new List<Polyhedron>(polyhedrons);
        List<Polyhedron> newPolyhedrons = polyhedrons.Select(p => new Polyhedron(p)).ToList();

        double[,] xRotationMatrix = new double[4, 4] {
            { 1,       0,               0,          0 },
            { 0,  Math.Cos(rotation.x), Math.Sin(rotation.x), 0 },
            { 0, -Math.Sin(rotation.x), Math.Cos(rotation.x), 0 },
            { 0,       0,               0,          1 }
        };

        double[,] yRotationMatrix = new double[4, 4] {
            { Math.Cos(rotation.y), 0, -Math.Sin(rotation.y), 0 },
            {      0,          1,       0,          0 },
            { Math.Sin(rotation.y), 0,  Math.Cos(rotation.y), 0 },
            {      0,          0,       0,          1 }
        };

        double[,] zRotationMatrix = new double[4, 4] {
                { Math.Cos(rotation.z), Math.Sin(rotation.z), 0, 0 },
                {-Math.Sin(rotation.z), Math.Cos(rotation.z), 0, 0 },
                {      0,               0,          1, 0 },
                {      0,               0,          0, 1 }
            };

        double[,] translationMatrix = new double[4, 4] {
            {  1,   0,   0,   0 },
            {  0,   1,   0,   0 },
            {  0,   0,   1,   0 },
            { -position.x,  -position.y, -position.z,   1 }
        };

        double[,] viewMatrix = AffineTransformations.Multiply(
            AffineTransformations.Multiply(
                xRotationMatrix,
                yRotationMatrix),
            zRotationMatrix);

        viewMatrix = AffineTransformations.Multiply(viewMatrix, translationMatrix);

        foreach (Polyhedron polyhedron in newPolyhedrons)
        {
            polyhedron.vertices = AffineTransformations.RecalculatedCoords(polyhedron, viewMatrix);
        }

        return newPolyhedrons;
    }    
}