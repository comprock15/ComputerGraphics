let canvas = document.getElementById("canvas");
let ctx = canvas.getContext("2d");

// Настройки канваса
let width = 200;
let height = 200;
ctx.canvas.width = width;
ctx.canvas.height = height;

// Заполняем канвас черным цветом
ctx.fillStyle = 'black';
ctx.fillRect(0, 0, width, height);

// Заполняем матрицу пикселей черным цветом
let pixelGrid = Array(width).fill(0).map(() => new Array(height).fill(0).map(() => { return new Color(0, 0, 0); }));

let camera = new Camera(new Vector(0, 0, 0), Math.PI/2);

let scene = new Scene([
    new Sphere(new Vector(0, 0, 10), 1, {color: new Color(100, 40, 5)}),
    new Sphere(new Vector(2, -1, 5), 1, {color: new Color(0, 100, 50)}),
    new Sphere(new Vector(2, 1, 9), 2, {color: new Color(50, 0, 180)}),
], new Color(0,0,90));


render();