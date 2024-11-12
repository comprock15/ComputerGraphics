// ID шейдерной программы
var program;
// ID атрибута
var attrib_vertex;
// ID Vertex Buffer Object
var VBO;

var gl;
var vertexShaderSource = document.getElementById("vertexShader").innerText;
var fragmentShaderSource = document.getElementById("fragmentShader").innerText;


// Иниациализация ресурсов
function init() {
    // Шейдеры
    initShader();
    // Вершинный буфер
    initVBO();
}


// Инициализация буфера вершин
function initVBO() {
    VBO = gl.createBuffer();
    // Вершины нашего треугольника
    var triangle = [
        -1, -1,
        0, 1,
        1, -1
    ];
    // Передаем вершины в буфер
    gl.bindBuffer(gl.ARRAY_BUFFER, VBO);
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(triangle), gl.STATIC_DRAW);
}


function initShader() {
    // Создаем вершинный шейдер
    var vertexShader = gl.createShader(gl.VERTEX_SHADER);
    // Передаем исходный код
    gl.shaderSource(vertexShader, vertexShaderSource);
    // Компилируем шейдер
    gl.compileShader(vertexShader);
    if (!gl.getShaderParameter(vertexShader, gl.COMPILE_STATUS)) {
        console.error("Вершинный шейдер: " + gl.getShaderInfoLog(vertexShader));
    }

    // Создаем фрагментный шейдер
    var fragmentShader = gl.createShader(gl.FRAGMENT_SHADER);
    // Передаем исходный код
    gl.shaderSource(fragmentShader, fragmentShaderSource);
    // Компилируем шейдер
    gl.compileShader(fragmentShader);
    if (!gl.getShaderParameter(vertexShader, gl.COMPILE_STATUS)) {
        console.log("Фрагментный шейдер: " + gl.getShaderInfoLog(fragmentShader));
    }

    // Создаем программу и прикрепляем шейдеры к ней
    program = gl.createProgram();
    gl.attachShader(program, vertexShader);
    gl.attachShader(program, fragmentShader);

    // Линкуем шейдерную программу
    gl.linkProgram(program);

    // Проверяем статус сборки
    if (!gl.getProgramParameter(program, gl.LINK_STATUS)) {
        console.error("Не удалсь установить шейдеры");
        return;
    }

    // Вытягиваем ID атрибута из собранной программы
    attrib_vertex = gl.getAttribLocation(program, "coord");

    if (attrib_vertex === -1) {
        console.error("Не получилось связать атрибут");
        return;
    }
}


function draw() {
    // Покрасим фон в черный цвет
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT);

    // Устанавливаем шейдерную программу текущей
    gl.useProgram(program);
    // Включаем массив атрибутов
    gl.enableVertexAttribArray(attrib_vertex);
    // Подключаем VBO
    gl.bindBuffer(gl.ARRAY_BUFFER, VBO);
    // Указываем, что каждая вершина имеет по 2 координаты
    gl.vertexAttribPointer(attrib_vertex, 2, gl.FLOAT, gl.FALSE, 0, 0);
    // Передаем данные на видеокарту (рисуем)
    gl.drawArrays(gl.TRIANGLES, 0, 3);
    // Отключаем массив атрибутов
    gl.disableVertexAttribArray(attrib_vertex);
}


// Освобождение ресурсов
function release() {
    // Шейдеры
    releaseShader();
    // Вершинный буфер
    releaseVBO();
}


function releaseShader() {
    gl.useProgram(null);
    // Удаляем шейдерную программу
    gl.deleteProgram(program);
}


function releaseVBO() {
    gl.bindBuffer(gl.ARRAY_BUFFER, null);
    // Удаляем VBO
    gl.deleteBuffer(VBO);
}


window.onload = function() {
    var canvas = document.getElementById("canvasWebGL");
    gl = canvas.getContext("webgl2");
    if (!gl) {
        console.error("Ваш браузер не поддерживает WebGL");
        return;
    }

    init();

    draw();

    release();
}