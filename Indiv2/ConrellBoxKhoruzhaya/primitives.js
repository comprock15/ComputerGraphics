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
        // let point =  Vector.scale(ray.direction,distToSurface)

        return { collide: collide, dist: distToSurface, point: point, normal: Vector.subtract(point, this.position).normalize(), obj: this }
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
