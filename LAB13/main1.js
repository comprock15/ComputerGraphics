// Инициализация шейдерной программы
const program = initShaderProgram(gl, vsSource, fsSource);

// Получение атрибутов вершин и текстур из шейдерной программы
const attrib_vertexPosition = gl.getAttribLocation(program, 'aVertexPosition');
const attrib_vertexNormal = gl.getAttribLocation(program, 'aVertexNormal');
const attrib_textureCoord = gl.getAttribLocation(program, 'aTextureCoord');
const attrib_modelViewMatrix = gl.getAttribLocation(program, 'aModelViewMatrix');

// Получение uniform-переменных для матриц и текстур
const uniform_projectionMatrix = gl.getUniformLocation(program, 'uProjectionMatrix');
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
    drawObjInstanced(gl, viewMatrix, projectionMatrix, frame, 0, [0, 0, 0], [0.5, 0.5, 0.5], scale=0.7, radius=10, nClones=10);
    drawObj(gl, viewMatrix, projectionMatrix, frame, 1, [0, -2, 0], [0.0, 1.0, 0.0], 1.5);
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

    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].modelViewMatrixBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, objViewMatrix, gl.DYNAMIC_DRAW);
    // Матрицу в атрибут так просто передать нельзя, поэтому страдаем и передаем её как 4 вектора по 4 элемента
    for (let i = 0; i < 4; ++i) {
        gl.enableVertexAttribArray(attrib_modelViewMatrix + i);
        gl.vertexAttribPointer(
            attrib_modelViewMatrix + i,
            4, // кол-во элементов в столбце
            gl.FLOAT,
            false,
            16 * Float32Array.BYTES_PER_ELEMENT, // шаг между последовательными элементами
            i * 4 * Float32Array.BYTES_PER_ELEMENT // начальный оффсет
        );
        gl.vertexAttribDivisor(attrib_modelViewMatrix + i, 1); // Обновляем один раз в экземпляр
    }


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
    //gl.drawElements(gl.TRIANGLES, objectsData[objIndex].indices.length, gl.UNSIGNED_INT, 0);
    gl.drawElementsInstanced(gl.TRIANGLES, objectsData[objIndex].indices.length, gl.UNSIGNED_INT, 0, 1);
}

// Вывод множества объектов, используя один вызов функции отрисовки
function drawObjInstanced(gl, camViewMatr, camProjMatr, frame, objIndex, position, rotation, scale, radius, nClones) {
    let modelViewMatrices = new Float32Array(16 * nClones);
    for (let i = 0; i < nClones; ++i) {
        // Создание локальной матрицы объекта на основе камеры
        let objViewMatrix = mat4.clone(camViewMatr);

        // Трансформации: позиция, вращение и масштаб объекта
        // Движение по окружности задаётся формулой x = x0 + R * cos t, y = y0 + R * sin t, где t — время
        let angle = 2 * Math.PI * i / nClones; 
        let x = position[0] + (radius + 5) * Math.cos(angle + frame);
        let y = position[1] + radius * Math.sin(angle + frame);
        mat4.translate(objViewMatrix, objViewMatrix, [x, 0, y]);
        mat4.rotateX(objViewMatrix, objViewMatrix, frame * rotation[0] * (i + 1));
        mat4.rotateY(objViewMatrix, objViewMatrix, frame * rotation[1] * (i + 1));
        mat4.rotateZ(objViewMatrix, objViewMatrix, frame * rotation[2] * (i + 1));
        let scale1 = 2*scale / (i+2);
        mat4.scale(objViewMatrix, objViewMatrix, [scale1, scale1, scale1]);

        modelViewMatrices.set(objViewMatrix, i * 16);
    }

    // Передача матриц проекции и модели-вида в шейдеры
    gl.uniformMatrix4fv(uniform_projectionMatrix, false, camProjMatr);

    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].modelViewMatrixBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, modelViewMatrices, gl.DYNAMIC_DRAW);
    for (let i = 0; i < 4; ++i) {
        gl.enableVertexAttribArray(attrib_modelViewMatrix + i);
        gl.vertexAttribPointer(
            attrib_modelViewMatrix + i,
            4, // кол-во элементов в столбце
            gl.FLOAT,
            false,
            16 * Float32Array.BYTES_PER_ELEMENT, // шаг между последовательными элементами
            i * 4 * Float32Array.BYTES_PER_ELEMENT // начальный оффсет
        );
        gl.vertexAttribDivisor(attrib_modelViewMatrix + i, 1); // Обновляем один раз в экземпляр
    }

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
    gl.drawElementsInstanced(gl.TRIANGLES, objectsData[objIndex].indices.length, gl.UNSIGNED_INT, 0, nClones);
}