let canvas = document.getElementById("canvas");
let ctx = canvas.getContext("2d");

// Настройки канваса
let width = 500;
let height = 500;
ctx.canvas.width = width;
ctx.canvas.height = height;

// Заполняем канвас черным цветом
ctx.fillStyle = 'black';
ctx.fillRect(0, 0, width, height);

// Заполняем матрицу пикселей черным цветом
let pixelGrid = Array(width).fill(0).map(() => new Array(height).fill(0).map(() => { return new Color(0, 0, 0); }));

let camera = new Camera(new Vector(0, 0, -6), Math.PI/1.2);

let leftWall = new Plane([new Vector(-10, -10, 10), new Vector(-10, -10, -10), new Vector(-10, 10, -10), new Vector(-10, 10, 10),], {
    color: new Color(255, 0, 0), 
    reflectivity: document.querySelector('input[name=leftwall][value=reflective]').checked,
    transparency: false
});
let rightWall = new Plane([new Vector(10, -10, 10), new Vector(10, 10, 10), new Vector(10, 10, -10), new Vector(10, -10, -10)], {
    color: new Color(0, 0, 255), 
    reflectivity: document.querySelector('input[name=rightwall][value=reflective]').checked,
    transparency: false
});
let upWall = new Plane([new Vector(-10, 10, 10), new Vector(-10, 10, -10), new Vector(10, 10, -10), new Vector(10, 10, 10)], {
    color: new Color(255, 255, 255), 
    reflectivity: document.querySelector('input[name=upwall][value=reflective]').checked,
    transparency: false
});
let downWall = new Plane([new Vector(-10, -10, 10), new Vector(10, -10, 10), new Vector(10, -10, -10), new Vector(-10, -10, -10)], {
    color: new Color(255, 255, 255), 
    reflectivity: document.querySelector('input[name=downwall][value=reflective]').checked,
    transparency: false
});
let frontWall = new Plane([new Vector(-10, -10, 10), new Vector(-10, 10, 10), new Vector(10, 10, 10), new Vector(10, -10, 10)], {
    color: new Color(255, 255, 255), 
    reflectivity: document.querySelector('input[name=frontwall][value=reflective]').checked,
    transparency: false
});
let backWall = new Plane([new Vector(-10, -10, -10), new Vector(10, -10, -10), new Vector(10, 10, -10), new Vector(-10, 10, -10)], {
    color: new Color(255, 255, 255), 
    reflectivity: document.querySelector('input[name=backwall][value=reflective]').checked,
    transparency: false
});


let sphere1 = new Sphere(new Vector(-5, 0, 9), 2, {
    color: new Color(255, 100, 100), 
    reflectivity: document.querySelector('input[name=sphere1][value=reflective]').checked,
    transparency: document.querySelector('input[name=sphere1][value=transparent]').checked
});
let sphere2 = new Sphere(new Vector(5, -2, 5), 3, {
    color: new Color(0, 255, 100), 
    reflectivity: document.querySelector('input[name=sphere2][value=reflective]').checked,
    transparency: document.querySelector('input[name=sphere2][value=transparent]').checked
});
let box1 = new Box(new Vector(-2, -6, 2), 3, 2, 1, {
    color: new Color(255, 0, 255), 
    reflectivity: document.querySelector('input[name=cube1][value=reflective]').checked,
    transparency: document.querySelector('input[name=cube1][value=transparent]').checked
});
let box2 = new Box(new Vector(4, 3, 2), 2, 2, 2, {
    color: new Color(255, 255, 200), 
    reflectivity: document.querySelector('input[name=cube2][value=reflective]').checked,
    transparency: document.querySelector('input[name=cube2][value=transparent]').checked
});


let light1 = new Light(new Vector(-9, 0, 2), 1);

let scene = new Scene([
    // Комната
    leftWall,
    rightWall,
    upWall,
    downWall,
    frontWall,
    backWall,
    
    // Фигуры на сцене
    sphere1,
    sphere2,
    box1,
    box2
], [ // Источники света
    new Light(new Vector(0, 9, 2), 1)
], // Цвет фона
new Color(0, 0, 0));



document.getElementById("render").onclick = () => render();