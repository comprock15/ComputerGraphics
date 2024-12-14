let canvas = document.getElementById("canvas");
let ctx = canvas.getContext("2d");

let width = 400;
let height = 400;
ctx.canvas.width = width;
ctx.canvas.height = height;

ctx.fillStyle = 'black';
ctx.fillRect(0, 0, width, height);

// Заполняем матрицу пикселей черным цветом
let pixelGrid = Array(width).fill(0).map(() => new Array(height).fill(0).map(() => { return { r: 0, g: 0, b: 0 } }));
//paintCanvas(pixelGrid);

let camera = new Camera(new Vector(0, 0, 0), Math.PI/2);

let sphere = new Sphere(new Vector(0, 0, 10), 1);

render();