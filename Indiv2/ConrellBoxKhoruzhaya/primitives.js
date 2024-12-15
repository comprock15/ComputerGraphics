class Scene {
    constructor(objects = [], lights = [], bgcolor = new Color(0,0,0)) {
        this.objects = objects;
        this.bgcolor = bgcolor;
        this.lights = lights;
    }

    add(object) {
        this.objects.push(object);
    }

    remove(object) {
        this.objects = this.objects.filter(o => o != object);
    }
}

// Источник света
class Light {
    constructor(position, intensity) {
        this.position = position;
        this.intensity = intensity;
    }
}

// Камера
class Camera {
    constructor(position, fieldOfView) {
        this.position = position;
        this.fieldOfView = fieldOfView;
    }
}

// Луч
class Ray {
    constructor(origin, direction) {
        this.origin = origin;
        this.direction = direction;
    }
}

// ----------------------------------------------

class Shape {
    constructor(properties) {
        this.properties = properties;
    }
}

// Сфера
class Sphere extends Shape {
    constructor(position, radius, properties) {
        super(properties);
        this.position = position;
        this.radius = radius;
    }

    // Находит пересечение с лучом
    collision(ray) {
        let distRay = Vector.subtract(this.position, ray.origin);
        let distToCenter = distRay.length;
        let rayDistToCenter = Vector.dot(distRay, ray.direction);
        let rayDistFromCenterSquared = distToCenter ** 2 - rayDistToCenter ** 2;

        let radiusSquared = this.radius ** 2

        let distToSurface = rayDistToCenter - Math.sqrt(Math.abs(radiusSquared - rayDistFromCenterSquared));

        let collide = true;

        if (rayDistToCenter < 0) { collide = false; }
        if (rayDistFromCenterSquared > radiusSquared) { collide = false; }

        if (!collide) { distToSurface = Infinity; }

        let point = Vector.add(Vector.scale(ray.direction, distToSurface), ray.origin)
        //let point =  Vector.scale(ray.direction,distToSurface)

        return { collide: collide, dist: distToSurface, point: point, normal: Vector.subtract(point, this.position).normalize(), obj: this }
    }
}

// Треугольник
class Triangle extends Shape {
    constructor(points, properties) {
        super(properties)
        this.points = points
    }

    collision(ray) {
        let planeVector = Vector.cross(Vector.subtract(this.points[1], this.points[0]), Vector.subtract(this.points[2], this.points[0])).normalize()
        let planeOffset = Vector.dot(planeVector, this.points[0])
        let distToSurface = (planeOffset - Vector.dot(planeVector, ray.origin)) / Vector.dot(planeVector, ray.direction)
        let point = Vector.add(Vector.scale(ray.direction, distToSurface), ray.origin)
        let c1 = Vector.dot(Vector.cross(Vector.subtract(this.points[1], this.points[0]), Vector.subtract(point, this.points[0])), planeVector) >= 0
        let c2 = Vector.dot(Vector.cross(Vector.subtract(this.points[2], this.points[1]), Vector.subtract(point, this.points[1])), planeVector) >= 0
        let c3 = Vector.dot(Vector.cross(Vector.subtract(this.points[0], this.points[2]), Vector.subtract(point, this.points[2])), planeVector) >= 0
        let collide = c1 && c2 && c3

        if (!collide || distToSurface <= 0) { distToSurface = Infinity }

        return { collide: collide, dist: distToSurface, point: point, normal: planeVector, obj: this }
    }
}

// Прямоугольничек
class Plane extends Shape {
    constructor(points, properties) {
        super(properties);
        this.points = points;
        this.set_triangles();
    }

    set_triangles() {
        this.triangles = [
            new Triangle([this.points[0], this.points[1], this.points[2]], this.properties),
            new Triangle([this.points[2], this.points[3], this.points[0]], this.properties),
        ]
    }

    collision(ray) {
        let col = this.triangles[0].collision(ray);
        for (let i = 1; i < this.triangles.length; ++i) {
            let col1 = this.triangles[i].collision(ray);
            if (col1.dist < col.dist)
                col = col1;
        }
        return col;
    }
}

// Класс параллелепипеда
class Box extends Shape {
    constructor(location, scale_x, scale_y, scale_z, properties) {
        super(properties)
        this.location = location
        this.scale_x = scale_x
        this.scale_y = scale_y
        this.scale_z = scale_z
        this.set_triangles();
    }

    set_triangles() {
        let p1, p2, p3, p4, p5, p6, p7, p8;
        p1 = new Vector(-this.scale_x/2, -this.scale_y/2, -this.scale_z/2).add(this.location);
        p2 = new Vector(this.scale_x/2, -this.scale_y/2, -this.scale_z/2).add(this.location);
        p3 = new Vector(-this.scale_x/2, this.scale_y/2, -this.scale_z/2).add(this.location);
        p4 = new Vector(-this.scale_x/2, -this.scale_y/2, this.scale_z/2).add(this.location);
        p5 = new Vector(this.scale_x/2, this.scale_y/2, -this.scale_z/2).add(this.location);
        p6 = new Vector(-this.scale_x/2, this.scale_y/2, this.scale_z/2).add(this.location);
        p7 = new Vector(this.scale_x/2, -this.scale_y/2, this.scale_z/2).add(this.location);
        p8 = new Vector(this.scale_x/2, this.scale_y/2, this.scale_z/2).add(this.location);

        this.triangles = [
            new Triangle([p1, p2, p3], this.properties),
            new Triangle([p5, p2, p3], this.properties),
            new Triangle([p1, p2, p4], this.properties), 
            new Triangle([p7, p2, p4], this.properties), 
            new Triangle([p1, p4, p3], this.properties),
            new Triangle([p6, p4, p3], this.properties),
            new Triangle([p8, p4, p6], this.properties),
            new Triangle([p8, p4, p7], this.properties),
            new Triangle([p8, p2, p7], this.properties),
            new Triangle([p8, p2, p5], this.properties),
            new Triangle([p8, p3, p6], this.properties),
            new Triangle([p8, p3, p5], this.properties)
        ];
    }

    collision(ray) {
        let col = this.triangles[0].collision(ray);
        for (let i = 1; i < this.triangles.length; ++i) {
            let col1 = this.triangles[i].collision(ray);
            if (col1.dist < col.dist)
                col = col1;
        }
        return col;
    }
}

// Цвет
class Color {
    constructor(r, g, b) {
        this.r = Math.max(0, Math.min(r, 255));
        this.g = Math.max(0, Math.min(g, 255));
        this.b = Math.max(0, Math.min(b, 255));
    }

    scale(s) {
        this.r *= s;
        this.g *= s;
        this.b *= s;
        return this;
    }

    add(c) {
        this.r += c.r;
        this.g += c.g;
        this.b += c.b;
        return this;
    }

    static add(c1, c2) {
        return new Color(c1.r + c2.r, c1.g + c2.g, c1.b + c2.b);
    }

    static scale(c, s) {
        return new Color(c.r * s, c.g * s, c.b * s);
    }
}

// Вектор
class Vector {
    constructor(x, y, z) {
        this.x = x
        this.y = y
        this.z = z
    }

    get length() {
        return Math.sqrt(this.x ** 2 + this.y ** 2 + this.z ** 2);
    }

    set length(len) {
        let scaleFactor = len / this.length;
        this.scale(scaleFactor);
    }

    copy() {
        return new Vector(this.x, this.y, this.z);
    }

    normalize() {
        this.length = 1;
        return this;
    }

    scale(s) {
        this.x *= s;
        this.y *= s;
        this.z *= s;
        return this;
    }

    add(v) {
        this.x += v.x;
        this.y += v.y;
        this.z += v.z;
        return this;
    }

    subtract(v) {
        this.x -= v.x;
        this.y -= v.y;
        this.z -= v.z;
        return this;
    }

    static normalize(v) {
        return v.copy().normalize();
    }

    static add(v1, v2) {
        return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    static subtract(v1, v2) {
        return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    static scale(v, s) {
        return new Vector(v.x * s, v.y * s, v.z * s);
    }

    static dot(v1, v2) {
        return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
    }

    static cross(v1, v2) {
        return new Vector(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
    }
}
