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

    // Система координат камеры
    private Vector3 forward = new Vector3(0, 0, 1); // Направление взгляда
    private Vector3 right = new Vector3(1, 0, 0);
    private Vector3 up = new Vector3(0, 1, 0);

    // Углы Тейта-Брайана (Повороты)
    private double yaw;   // рыскание
    private double pitch; // тангаж

    // Настройки перспективной проекции
    private double fov = Math.PI / 2; // Угол обзора в радианах
    private double aspect = 800.0/600; // Соотношение сторон
    private double zNear = 1;  // Расстояние до ближней плоскости отсечения
    private double zFar = 1000; // Расстояние до дальней плоскости отсечения

    public Camera(Vector3 position, Vector3 target)
    {
        this.position = position;
        this.target = target;
        forward = (position - target).Normalize();
        RecalculateAxes();

        pitch = Math.Asin(forward.y);
        yaw = Math.Acos(forward.x / Math.Cos(pitch));
    }

    public Camera()
    {
        this.position = new Vector3(0, 0, -100);
        this.target = new Vector3(0, 0, 0);
        forward = (position - target).Normalize();
        RecalculateAxes();
    }

    public void Move(double dx, double dy, double dz)
    {
        this.position += new Vector3(dx, dy, dz);
        this.target += new Vector3(dx, dy, dz);
    }

    public void RecalculateAxes()
    {
        right = new Vector3(0,1,0).Cross(forward).Normalize();
        up = forward.Cross(right).Normalize();
    }

    public void Rotate(double dyaw, double dpitch)
    {
        pitch += dpitch;
        yaw += dyaw;
        if (pitch > 2 * Math.PI)
            pitch -= 2 * Math.PI;
        if (yaw > 2 * Math.PI)
            yaw -= 2 * Math.PI;

        forward = new Vector3(Math.Cos(yaw) * Math.Cos(pitch),
                                Math.Sin(pitch), 
                                Math.Sin(yaw) * Math.Cos(pitch)).Normalize();
        RecalculateAxes();
    }

    public double[,] GetViewMatrix()
    {
        forward = (position - target).Normalize();
        RecalculateAxes();

        double[,] translationMatrix = new double[4, 4] {
            {  1,   0,   0,   0 },
            {  0,   1,   0,   0 },
            {  0,   0,   1,   0 },
            { -position.x,  -position.y,  -position.z,   1 }
        };

        double[,] viewMatrix = new double[4, 4] {
            { right.x,     right.y,     right.z,     0 },
            { up.x,        up.y,        up.z,        0 },
            { forward.x,   forward.y,   forward.z,   0 },
            { 0,           0,           0,           1 }
        };

        double[,] projectionMatrix = new double[4, 4] {
            {  1.0 / Math.Tan(fov/2) / aspect,   0,   0,   0 },
            {  0,   1.0 / Math.Tan(fov/2),   0,   0 },
            {  0,   0,   (zFar + zNear) / (zFar - zNear),   -2 * zFar * zNear / (zFar - zNear) },
            { 0,  0,  1,   0 }
        };

        //double[,] projectionMatrix = new double[4, 4] {
        //        { 1, 0, 0, 0 },
        //        { 0, 1, 0, 0 },
        //        { 0, 0, 0, -1/1000 },
        //        { 800 / 2, 600 / 2, 0, 1 }
        //    };

        viewMatrix = AffineTransformations.Multiply(translationMatrix, viewMatrix);
        //viewMatrix = AffineTransformations.Multiply(viewMatrix, projectionMatrix);

        return viewMatrix;
    }

    public List<Polyhedron> GetPolyhedronsInCameraCoordinates(List<Polyhedron> polyhedrons)
    {
        List<Polyhedron> newPolyhedrons = polyhedrons.Select(p => new Polyhedron(p)).ToList();


        var viewMatrix = GetViewMatrix();


        foreach (Polyhedron polyhedron in newPolyhedrons)
        {
            polyhedron.vertices = AffineTransformations.RecalculatedCoords(polyhedron, viewMatrix);
        }

        return newPolyhedrons;
    }    
}