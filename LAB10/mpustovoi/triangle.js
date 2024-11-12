function main() {
    const canvas = document.getElementById('webglCanvas');
    const gl = canvas.getContext('webgl');

    if (!gl) {
        console.error("Ваш браузер не поддерживает WebGL");
        return;
    }

    // вершинный шейдер
    const vertexShaderSource = `
        attribute vec2 a_Position;
        void main() {
            gl_Position = vec4(a_Position, 0.0, 1.0);
        }
    `;

    // фрагментный шейдер
    const fragmentShaderSource = `
        precision mediump float;
        void main() {
            gl_FragColor = vec4(0.0, 1.0, 0.0, 1.0);
        }
    `;

    // создание и компиляция
    const vertexShader = createShader(gl, gl.VERTEX_SHADER, vertexShaderSource);
    const fragmentShader = createShader(gl, gl.FRAGMENT_SHADER, fragmentShaderSource);

    // создание программы
    const program = createProgram(gl, vertexShader, fragmentShader);
    gl.useProgram(program);

    // координаты треугольника
    const vertices = new Float32Array([
       -1.0, -1.0,
        1.0, -1.0,
        0.0,  1.0
    ]);

    // создание VBO
    const vbo = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, vbo);
    gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);

    // позиции аттрибута и настройка
    const a_Position = gl.getAttribLocation(program, 'a_Position');
    gl.vertexAttribPointer(a_Position, 2, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(a_Position);

    // чёрный фон и отрисовка треугольника
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT);
    gl.drawArrays(gl.TRIANGLES, 0, 3);
}

// ссоздания шейдера
function createShader(gl, type, source) {
    const shader = gl.createShader(type);
    // добавление шейдеров
    gl.shaderSource(shader, source);
    gl.compileShader(shader);

    if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
        console.error('Ошибка компиляции шейдера:', gl.getShaderInfoLog(shader));
        gl.deleteShader(shader);
        return null;
    }
    return shader;
}

// Вспомогательная функция для создания программы
function createProgram(gl, vertexShader, fragmentShader) {
    const program = gl.createProgram();
    gl.attachShader(program, vertexShader);
    gl.attachShader(program, fragmentShader);
    gl.linkProgram(program);

    if (!gl.getProgramParameter(program, gl.LINK_STATUS)) {
        console.error('Ошибка линковки программы:', gl.getProgramInfoLog(program));
        gl.deleteProgram(program);
        return null;
    }
    return program;
}

main();