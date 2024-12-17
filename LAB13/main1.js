// Инициализация шейдерной программы
const program = initShaderProgram(gl, vsSource, fsSource);

// Получение атрибутов вершин и текстур из шейдерной программы
const attrib_vertexPosition = gl.getAttribLocation(program, 'aVertexPosition');
const attrib_vertexNormal = gl.getAttribLocation(program, 'aVertexNormal');
const attrib_textureCoord = gl.getAttribLocation(program, 'aTextureCoord');

// Получение uniform-переменных для матриц и текстур
const uniform_projectionMatrix = gl.getUniformLocation(program, 'uProjectionMatrix');
const uniform_modelViewMatrix = gl.getUniformLocation(program, 'uModelViewMatrix');
const uniform_lightDirection = gl.getUniformLocation(program, 'uLightDirection');
const uniform_uSampler = gl.getUniformLocation(program, 'uSampler');

// Точка входа после полной загрузки Document Object Model
document.addEventListener('DOMContentLoaded', main);

function main() {
    let frame = 0;
    // Рекурсивный рендеринг с использованием requestAnimationFrame
    function render() {
        if (objectsData[0] && objectsData[1] && textures[0] && textures[1])
            draw(gl, frame);
        requestAnimationFrame(render);
        frame += 0.01;
    }
    render();
}

function draw(gl, frame) {
    // Очистка буфера цвета и глубины для нового кадра
    gl.clearColor(0.0, 0.0, 0.1, 1.0);
    gl.enable(gl.DEPTH_TEST);
    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

    gl.useProgram(program);

    // Получение матрицы проекции и камеры
    const projectionMatrix = getCameraProjectionMatrix();
    const viewMatrix = getCameraViewMatrix();

    // Отрисовка двух объектов с разными параметрами
    drawObjInstanced(gl, viewMatrix, projectionMatrix, frame, 0, [0, 0, 0], [0.5, 0.5, 0.5], 0.5, 0, 10);
    drawObj(gl, viewMatrix, projectionMatrix, frame, 1, [0, 0, 0], [0.0, 1.0, 0.0], 1.3);
}

function drawObj(gl, camViewMatr, camProjMatr, frame, objIndex, position, rotation, scale) {
    // Создание локальной матрицы объекта на основе камеры
    let objViewMatrix = mat4.clone(camViewMatr);

    // Трансформации: позиция, вращение и масштаб объекта
    mat4.translate(objViewMatrix, objViewMatrix, position);
    mat4.rotateX(objViewMatrix, objViewMatrix, frame * rotation[0]);
    mat4.rotateY(objViewMatrix, objViewMatrix, frame * rotation[1]);
    mat4.rotateZ(objViewMatrix, objViewMatrix, frame * rotation[2]);
    mat4.scale(objViewMatrix, objViewMatrix, [scale, scale, scale]);

    // Передача матриц проекции и модели-вида в шейдеры
    gl.uniformMatrix4fv(uniform_projectionMatrix, false, camProjMatr);
    gl.uniformMatrix4fv(uniform_modelViewMatrix, false, objViewMatrix);

    // Подключение буфера позиций вершин
    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].positionBuffer);
    gl.vertexAttribPointer(attrib_vertexPosition, 3, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(attrib_vertexPosition);

    // Подключение буфера нормалей
    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].normalBuffer);
    gl.vertexAttribPointer(attrib_vertexNormal, 3, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(attrib_vertexNormal);

    // Подключение буфера текстурных координат
    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].textureCoordBuffer);
    gl.vertexAttribPointer(attrib_textureCoord, 2, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(attrib_textureCoord);

    // Активация и привязка текстуры для текущего объекта
    gl.activeTexture(texture_active[objIndex]);
    gl.bindTexture(gl.TEXTURE_2D, textures[objIndex]);
    gl.uniform1i(uniform_uSampler, objIndex);

    // Подключение буфера индексов и отрисовка элементов (треугольников)
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, objectsData[objIndex].indexBuffer);
    gl.drawElements(gl.TRIANGLES, objectsData[objIndex].indices.length, gl.UNSIGNED_INT, 0);
}

// Вывод множества объектов, используя один вызов функции отрисовки
function drawObjInstanced(gl, camViewMatr, camProjMatr, frame, objIndex, position, rotation, scale, initialRotation=0, radius=10) {
    // Создание локальной матрицы объекта на основе камеры
    let objViewMatrix = mat4.clone(camViewMatr);

    // Трансформации: позиция, вращение и масштаб объекта
    // Движение по окружности задаётся формулой x = x0 + R * cos t, y = y0 + R * sin t, где t — время.
    let x = position[0] + radius * Math.cos(initialRotation + frame);
    let y = position[1] + radius * Math.sin(initialRotation + frame);
    //mat4.translate(objViewMatrix, objViewMatrix, position);
    mat4.translate(objViewMatrix, objViewMatrix, [x, y, position[2]]);
    mat4.rotateX(objViewMatrix, objViewMatrix, frame * rotation[0]);
    mat4.rotateY(objViewMatrix, objViewMatrix, frame * rotation[1]);
    mat4.rotateZ(objViewMatrix, objViewMatrix, frame * rotation[2]);
    mat4.scale(objViewMatrix, objViewMatrix, [scale, scale, scale]);

    // Передача матриц проекции и модели-вида в шейдеры
    gl.uniformMatrix4fv(uniform_projectionMatrix, false, camProjMatr);
    gl.uniformMatrix4fv(uniform_modelViewMatrix, false, objViewMatrix);

    // Подключение буфера позиций вершин
    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].positionBuffer);
    gl.vertexAttribPointer(attrib_vertexPosition, 3, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(attrib_vertexPosition);

    // Подключение буфера нормалей
    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].normalBuffer);
    gl.vertexAttribPointer(attrib_vertexNormal, 3, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(attrib_vertexNormal);

    // Подключение буфера текстурных координат
    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].textureCoordBuffer);
    gl.vertexAttribPointer(attrib_textureCoord, 2, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(attrib_textureCoord);

    // Активация и привязка текстуры для текущего объекта
    gl.activeTexture(texture_active[objIndex]);
    gl.bindTexture(gl.TEXTURE_2D, textures[objIndex]);
    gl.uniform1i(uniform_uSampler, objIndex);

    // Подключение буфера индексов и отрисовка элементов (треугольников)
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, objectsData[objIndex].indexBuffer);
    gl.drawElements(gl.TRIANGLES, objectsData[objIndex].indices.length, gl.UNSIGNED_INT, 0);
}