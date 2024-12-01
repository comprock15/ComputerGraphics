using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public Vector3(Vector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    internal Vector3(Vertex v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    static public Vector3 operator +(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    static public Vector3 operator -(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }
    public static Vector3 operator *(Vector3 a, Vector3 b)
    {
        return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
    }

    static public Vector3 operator -(Vector3 v)
    {
        return new Vector3(-v.x, -v.y, -v.z);
    }

    static public Vector3 operator *(double c, Vector3 v) => new Vector3(c * v.x, c * v.y, c * v.z);
    static public Vector3 operator *(Vector3 v, double c) => c * v;

    /// <summary>
    /// Длина вектора
    /// </summary>
    public double Length()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public Vector3 Normalized()
    {
        Vector3 v = new Vector3(this);
        double vlength = Length();
        v.x /= vlength;
        v.y /= vlength;
        v.z /= vlength;
        return v;
    }

    /// <summary>
    /// Нормирование вектора
    /// </summary>
    public Vector3 Normalize()
    {
        double vlength = Length();
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

    /// <summary>
    /// Скалярное произведение
    /// </summary>
    public double Dot(Vector3 v)
    {
        return x * v.x + y * v.y + z * v.z;
    }

}