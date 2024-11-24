// Смена рисуемой фигуры
var selectFigure = document.querySelector('#figure');
selectFigure.addEventListener('change', function() {
  if (selectFigure.options[0].selected === true) {
    vertices = quadrilateral;
    primitive = gl.TRIANGLE_STRIP;
  }
  else if (selectFigure.options[1].selected === true) {
    vertices = fan;
    primitive = gl.TRIANGLE_FAN;
  }
  else {
    vertices = pentagon;
    primitive = gl.TRIANGLE_FAN;
  }
  main();
});

// Смена режима закрашивания
var selectPaintMode = document.querySelector('#paintMode');
selectPaintMode.addEventListener('change', function() {
  if (selectPaintMode.options[0].selected === true) {
    vertexShaderSource = vertexShaderSource1;
    fragmentShaderSource = fragmentShaderSource1;
  }
  else if (selectPaintMode.options[1].selected === true) {
    vertexShaderSource = vertexShaderSource2;
    fragmentShaderSource = fragmentShaderSource2;
  }
  else {
    vertexShaderSource = vertexShaderSource3;
    fragmentShaderSource = fragmentShaderSource3;
  }
  main();
});

// ID шейдерной программы
var program;
// ID атрибута
var attrib_vertex;
// ID Vertex Buffer Object
var VBO;

var gl;
var canvas = document.getElementById("canvasWebGL");
gl = canvas.getContext("webgl2");


// ---------------------- Шейдеры ------------------

var vertexShaderSource1 = 
`#version 300 es
in vec2 coord;
void main() {
  gl_Position = vec4(coord, 0.0, 1.0);
}
`

var fragmentShaderSource1 = 
`#version 300 es
precision highp float;
out vec4 color;
void main() {
  color = vec4(0, 1, 0, 1);
}
`

// TODO: закрашивание 2
var vertexShaderSource2 = 
`#version 300 es
in vec2 coord;
void main() {
  gl_Position = vec4(coord, 0.0, 1.0);
}
`

var fragmentShaderSource2 = 
`#version 300 es
precision highp float;
out vec4 color;
void main() {
  color = vec4(0, 1, 0, 1);
}
`

// TODO: закрашивание 3
var vertexShaderSource3 = 
`#version 300 es
in vec2 coord;
void main() {
  gl_Position = vec4(coord, 0.0, 1.0);
}
`

var fragmentShaderSource3 = 
`#version 300 es
precision highp float;
out vec4 color;
void main() {
  color = vec4(0, 1, 0, 1);
}
`
// --------------------- Фигуры ---------------------

var quadrilateral = [
  -0.7, -0.5,
  -0.7, 0.5,
  0.8, -0.5,
  0.7, 0.3
];

var fan = [
  0.0, -0.8,
  -0.8, 0.6,
  -0.5, 0.7,
  -0.2, 0.8,
  0.2, 0.8,
  0.6, 0.4,
];

var pentagon = [
  0.0, 1.0,
  0.951, 0.309,
  0.588, -0.809,
  -0.588, -0.809,
  -0.951, 0.309
];

// --------------------------------------------------

var vertexShaderSource = vertexShaderSource1;
var fragmentShaderSource = fragmentShaderSource1;
var vertices = quadrilateral;
var primitive = gl.TRIANGLE_STRIP;

// Иниациализация ресурсов
function init() {
  // Шейдеры
  initShaders();
  // Вершинный буфер
  initVBO(vertices);
}


// Инициализация буфера вершин
function initVBO(vertices) {
  VBO = gl.createBuffer();
  // Передаем вершины в буфер
  gl.bindBuffer(gl.ARRAY_BUFFER, VBO);
  gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
}


function initShaders() {
  // Создаем вершинный шейдер
  var vertexShader = getShader(gl, gl.VERTEX_SHADER, vertexShaderSource);
  // Создаем фрагментный шейдер
  var fragmentShader = getShader(gl, gl.FRAGMENT_SHADER, fragmentShaderSource);

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

function getShader(gl, type, source) {
  // создаем шейдер по типу
  var shader = gl.createShader(type);
  // Установка источника шейдера
  gl.shaderSource(shader, source);
  // Компилируем шейдер
  gl.compileShader(shader);
   
  if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
    alert("Ошибка компиляции шейдера: " + gl.getShaderInfoLog(shader));
    gl.deleteShader(shader);   
    return null;
  }
  return shader;  
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
  gl.drawArrays(primitive, 0, vertices.length / 2); // Делим на кол-во параметров вершины (у нас сейчас это 2 координаты)
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


function main() {
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

window.onload = main()