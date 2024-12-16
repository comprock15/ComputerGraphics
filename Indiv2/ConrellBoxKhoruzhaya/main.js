let canvas = document.getElementById("canvas");
let ctx = canvas.getContext("2d");

// Настройки канваса
let width = 300;
let height = 300;
ctx.canvas.width = width;
ctx.canvas.height = height;

// Заполняем канвас черным цветом
ctx.fillStyle = 'black';
ctx.fillRect(0, 0, width, height);

// Заполняем матрицу пикселей черным цветом
let pixelGrid = Array(width).fill(0).map(() => new Array(height).fill(0).map(() => { return new Color(0, 0, 0); }));

let camera = new Camera(new Vector(0, 0, -6), Math.PI/1.2);

let scene = new Scene([
    // Комната
    // Передняя стена
    new Plane([new Vector(-10, -10, 10), new Vector(-10, 10, 10), new Vector(10, 10, 10), new Vector(10, -10, 10)], {color: new Color(255, 255, 255)}),
    // Левая стена
    new Plane([new Vector(-10, -10, 10), new Vector(-10, -10, -10), new Vector(-10, 10, -10), new Vector(-10, 10, 10),], {color: new Color(255, 0, 0)}),
    // Правая стена
    new Plane([new Vector(10, -10, 10), new Vector(10, 10, 10), new Vector(10, 10, -10), new Vector(10, -10, -10)], {color: new Color(0, 0, 255)}),
    // Верхняя стена
    new Plane([new Vector(-10, 10, 10), new Vector(-10, 10, -10), new Vector(10, 10, -10), new Vector(10, 10, 10)], {color: new Color(255, 255, 255)}),
    // Нижняя стена
    new Plane([new Vector(-10, -10, 10), new Vector(10, -10, 10), new Vector(10, -10, -10), new Vector(-10, -10, -10)], {color: new Color(255, 255, 255)}),
    // Задняя стена
    new Plane([new Vector(-10, -10, -10), new Vector(10, -10, -10), new Vector(10, 10, -10), new Vector(-10, 10, -10)], {color: new Color(255, 255, 255)}),
    
    // Фигуры на сцене
    new Sphere(new Vector(-5, 0, 9), 2, {color: new Color(200, 40, 5)}),
    new Sphere(new Vector(5, -2, 2), 2, {color: new Color(0, 200, 50)}),
    new Sphere(new Vector(2, 1, 7), 3, {color: new Color(50, 0, 200)}),
    new Box(new Vector(-2, -6, 2), 3, 1, 1, {color: new Color(100, 0, 100)})
], [ // Источники света
    new Light(new Vector(0, 9, 3), 1)
], // Цвет фона
new Color(0, 0, 0));


document.getElementById("render").onclick = () => render();