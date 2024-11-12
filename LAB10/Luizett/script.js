"use strict";

// Исходный код вершинного шейдера
// in - обознаечение входного параметра, необходим для вершинного шейдера 
var vertexShaderSource = 
`#version 300 es
in vec2 coord;
void main() {
  gl_Position = vec4(coord, 0.0, 1.0);
}
`;

// Исходный код фрагментного шейдера 
// фрагментный шейдер не имеет точности по умолчанию, потому нужно задать её вручную. В данном сдучае выбрали "high precision" 
var fragmentShaderSource = 
`#version 300 es
precision highp float;
out vec4 outColor;
void main() {
  outColor = vec4(0, 1, 0, 1);
}
`;

function start()
{
    var canvas = document.getElementById("glcanvas");
    var gl = canvas.getContext("webgl2");
    if (!gl) {
        alert("Browser do not support webgl2");
        return;
    }

    // Инициализируем шейдеры
    var vertexShader = initShader(gl, gl.VERTEX_SHADER, vertexShaderSource);
    var fragmentShader = initShader(gl, gl.FRAGMENT_SHADER, fragmentShaderSource);

    // Инициализируем шейдерную программу
    var program = initProgram(gl, vertexShader, fragmentShader );

    // Получить id аттрибутов из шейдерной программы
    var vertexAttributeLocation = gl.getAttribLocation(program, "coord");
    if (vertexAttributeLocation  === -1) {
        alert("Error in get attributeLocation");
        return;
    }

    // Инициализация VBO - массива данных в видеопамяти
    var VBO = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, VBO);
    // Координаты вершин теругольника
    var triangle = [
        -1.0, -1.0,
        0.0, 1.0,
        1.0, -1.0
    ];
    // Заполняем данными
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(triangle), gl.STATIC_DRAW);

    // Установим шейдерную программу текущей
    gl.useProgram(program);
    // Вызовем демонов 
    gl.enableVertexAttribArray(vertexAttributeLocation);
    gl.bindBuffer(gl.ARRAY_BUFFER, VBO);
    // Привяжем атрибут к VBO
    gl.vertexAttribPointer(vertexAttributeLocation , 2, gl.FLOAT, false, 0, 0);


    // Отрисовываем сцену
    gl.clearColor(0,0,0,1);
    gl.clear(gl.COLOR_BUFFER_BIT);
    gl.viewport(0, 0, gl.canvas.width, gl.canvas.height);
   
    // Рисование
    gl.drawArrays(gl.TRIANGLES, 0, 3);


    //Удаление
    // gl.deleteProgram();
    // gl.deleteBuffer();
}

//Инициализировать шейдер
function initShader(gl, type, source) {
    // Создать объект шейдера
    var shader = gl.createShader(type);
    // Инициализируем шейдер исходным кодом
    gl.shaderSource(shader, source);
    // Компилируем шейдер
    gl.compileShader(shader);
    // Обработка ошибок
    if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
        alert("Shader compile error: " + gl.getShaderInfoLog(shader));
        return null;
    }
    return shader;
}

// Инициализировать шейдерную программу
function initProgram(gl, vertexShader, fragmentShader) {
    //Создаём шейдерную программу
    var program = gl.createProgram();
    //Присоединяем каждый шейдер
    gl.attachShader(program, vertexShader);
    gl.attachShader(program, fragmentShader);
    //Линкуем программу
    gl.linkProgram(program);
    // Обработка ошибок
    var success = gl.getProgramParameter(program, gl.LINK_STATUS);
    if (success) {
        return program;
      }
    
      console.log(gl.getProgramInfoLog(program));
      gl.deleteProgram(program);
      return undefined;
}

start();