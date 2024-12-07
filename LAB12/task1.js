function task1() {
    let canvas = document.getElementById('canvas1');
    let gl = canvas.getContext('webgl2');

    if (!gl) {
        alert('Ваш браузер не поддерживает WebGL2!');
        throw new Error('WebGL2 not supported');
    }

    // Вершинный шейдер
    const vertexShaderSource = `#version 300 es
    in vec3 aPosition;
    in vec3 aColor;
    uniform mat4 uModelViewMatrix;
    out vec3 vColor;
    void main() {
        gl_Position = uModelViewMatrix * vec4(aPosition, 1.0);
        vColor = aColor;
    }`;

    // Фрагментный шейдер
    const fragmentShaderSource = `#version 300 es
    precision mediump float;
    in vec3 vColor;
    out vec4 fragColor;
    void main() {
        fragColor = vec4(vColor, 1.0);
    }`;

    // Компиляция шейдера
    function compileShader(source, type) {
        const shader = gl.createShader(type);
        gl.shaderSource(shader, source);
        gl.compileShader(shader);
        if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
            console.error('Ошибка компиляции шейдера:', gl.getShaderInfoLog(shader));
            gl.deleteShader(shader);
            throw new Error('Ошибка компиляции шейдера');
        }
        return shader;
    }

    // Создание шейдерной программы
    const vertexShader = compileShader(vertexShaderSource, gl.VERTEX_SHADER);
    const fragmentShader = compileShader(fragmentShaderSource, gl.FRAGMENT_SHADER);
    const program = gl.createProgram();
    gl.attachShader(program, vertexShader);
    gl.attachShader(program, fragmentShader);
    gl.linkProgram(program);
    if (!gl.getProgramParameter(program, gl.LINK_STATUS)) {
        console.error('Ошибка линковки программы:', gl.getProgramInfoLog(program));
        gl.deleteProgram(program);
        throw new Error('Ошибка линковки программы');
    }
    gl.useProgram(program);

    // Данные тетраэдра (X, Y, Z, R, G, B)
    const vertices = new Float32Array([
        0.0, 1.0, 0.0,   1.0, 0.0, 0.0, // Вершина 1
        -1.0, -1.0, 1.0, 0.0, 1.0, 0.0, // Вершина 2
        1.0, -1.0, 1.0,  0.0, 0.0, 1.0, // Вершина 3
        0.0, -1.0, -1.0, 1.0, 1.0, 1.0  // Вершина 4
    ]);
    const faces = new Uint16Array([
        0, 1, 2, // Лицевая грань
        0, 2, 3, // Правая грань
        0, 3, 1, // Левая грань
        1, 3, 2  // Нижняя грань
    ]);

    // Создание VBO и IBO
    const vbo = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, vbo);
    gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);

    const ibo = gl.createBuffer();
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, ibo);
    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, faces, gl.STATIC_DRAW);

    // Подключение атрибутов
    const aPosition = gl.getAttribLocation(program, 'aPosition');
    const aColor = gl.getAttribLocation(program, 'aColor');
    gl.vertexAttribPointer(aPosition, 3, gl.FLOAT, false, 6 * Float32Array.BYTES_PER_ELEMENT, 0);
    gl.enableVertexAttribArray(aPosition);
    gl.vertexAttribPointer(aColor, 3, gl.FLOAT, false, 6 * Float32Array.BYTES_PER_ELEMENT, 3 * Float32Array.BYTES_PER_ELEMENT);
    gl.enableVertexAttribArray(aColor);

    // Матрицы преобразований
    const uModelViewMatrix = gl.getUniformLocation(program, 'uModelViewMatrix');
    let modelViewMatrix = mat4.create();
    let projectionMatrix = mat4.create();
    let modelViewProjectionMatrix = mat4.create();

    // Корректировка камеры и положения
    mat4.perspective(projectionMatrix, Math.PI / 4, canvas.width / canvas.height, 0.1, 100.0);
    mat4.scale(projectionMatrix, projectionMatrix, [1, 1, -1]);
    let translation = [0.0, 0.25, 2.5];
    let rotationX = Math.PI / 6;
    let rotationY = 0;
    let rotationZ = 0;

    function updateModelViewMatrix() {
        mat4.identity(modelViewMatrix);
        mat4.translate(modelViewMatrix, modelViewMatrix, translation);
        mat4.rotateX(modelViewMatrix, modelViewMatrix, rotationX);
        mat4.rotateY(modelViewMatrix, modelViewMatrix, rotationY);
        mat4.rotateZ(modelViewMatrix, modelViewMatrix, rotationZ);
        mat4.multiply(modelViewProjectionMatrix, projectionMatrix, modelViewMatrix);
        gl.uniformMatrix4fv(uModelViewMatrix, false, modelViewProjectionMatrix);
    }
    updateModelViewMatrix();

    // Обработчики кнопок
    document.getElementById('moveX+').onclick = () => { translation[0] += 0.1; updateModelViewMatrix(); render(); };
    document.getElementById('moveX-').onclick = () => { translation[0] -= 0.1; updateModelViewMatrix(); render(); };
    document.getElementById('moveY+').onclick = () => { translation[1] += 0.1; updateModelViewMatrix(); render(); };
    document.getElementById('moveY-').onclick = () => { translation[1] -= 0.1; updateModelViewMatrix(); render(); };
    document.getElementById('moveZ+').onclick = () => { translation[2] += 0.1; updateModelViewMatrix(); render(); };
    document.getElementById('moveZ-').onclick = () => { translation[2] -= 0.1; updateModelViewMatrix(); render(); };

    // Отрисовка
    function render() {
        gl.clearColor(0.0, 0.0, 0.0, 1.0);
        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
        gl.enable(gl.DEPTH_TEST);

        gl.drawElements(gl.TRIANGLES, faces.length, gl.UNSIGNED_SHORT, 0);
    }
    render();
}
