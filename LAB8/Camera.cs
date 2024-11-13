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
    /// Точка, в которую направлен взгляд камеры
    /// </summary>
    public Vector3 target;

    private Vector3 direction; // Направление взгляда
    private double pitch; // Поворот вверх
    private double yaw; // Поворот влево-вправо

    private Vector3 right = new Vector3(1, 0, 0);
    private Vector3 up = new Vector3(0, 1, 0);

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

        /// <summary>
        /// Нормирование вектора
        /// </summary>
        public Vector3 Normalize()
        {
            double vlength = Math.Sqrt(x * x + y * y + z * z);
            x /= vlength;
            y /= vlength;
            z /= vlength;
            return this;
        }

        /// <summary>
        /// Векторное произведение
        /// </summary>
        public Vector3 Cross(Vector3 v)
        {
            return new Vector3(y * v.z - z * v.y, z * v.x - x * v.z, x * v.y - y * v.x);
        }

        public double ScalarProduct(Vector3 v)
        {
            return x * v.x + y * v.y + z * v.z;
        }

        static public Vector3 operator+ (Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        static public Vector3 operator- (Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }
    }

    public Camera(Vector3 position, Vector3 target)
    {
        this.position = position;
        this.target = target;
        direction = (position - target).Normalize();
        RecalculateAxes();
        pitch = Math.Asin(direction.y);
        yaw = Math.Acos(direction.x / Math.Cos(pitch));
    }

    public Camera()
    {
        this.position = new Vector3(0, 0, 0);
        this.target = new Vector3(0, 0, -100);
        direction = (position - target).Normalize();
        RecalculateAxes();
    }

    public void Move(double dx, double dy, double dz)
    {
        this.position += new Vector3(dx, dy, dz);
        this.target += new Vector3(dx, dy, 0);
    }

    public void RecalculateAxes()
    {
        right = new Vector3(0,1,0).Cross(direction).Normalize();
        up = direction.Cross(right);
    }

    public void Rotate(double dyaw, double dpitch)
    {
        pitch += dpitch;
        yaw += dyaw;
        if (pitch > 2 * Math.PI)
            pitch -= 2 * Math.PI;
        if (yaw > 2 * Math.PI)
            yaw -= 2 * Math.PI;

        direction = new Vector3(Math.Cos(yaw) * Math.Cos(pitch),
                                Math.Sin(pitch), 
                                Math.Sin(yaw) * Math.Cos(pitch)).Normalize();
        RecalculateAxes();
    }

    public List<Polyhedron> GetPolyhedronsInCameraCoordinates(List<Polyhedron> polyhedrons)
    {
        RecalculateAxes();

        List<Polyhedron> newPolyhedrons = polyhedrons.Select(p => new Polyhedron(p)).ToList();

        double[,] translationMatrix = new double[4, 4] {
            {  1,   0,   0,   0 },
            {  0,   1,   0,   0 },
            {  0,   0,   1,   0 },
            { -position.x,  -position.y,  -position.z,   1 }
        };

        double[,] viewMatrix = new double[4, 4] {
            { right.x,     right.y,     right.z,     0 },
            { up.x,        up.y,        up.z,        0 },
            { direction.x, direction.y, direction.z, 0 },
            { 0,           0,           0,           1 }
        };

        viewMatrix = AffineTransformations.Multiply(translationMatrix, viewMatrix);

        foreach (Polyhedron polyhedron in newPolyhedrons)
        {
            polyhedron.vertices = AffineTransformations.RecalculatedCoords(polyhedron, viewMatrix);
        }

        return newPolyhedrons;
    }    
}