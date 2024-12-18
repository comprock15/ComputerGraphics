// Позиция камеры в пространстве
const cameraPosition = [0, 0, -35];
// Углы поворота камеры вокруг осей
const cameraRotation = [0, 0, 0];
// Скорость перемещения камеры
const cameraSpeed = 0.1;
// Скорость вращения камеры
const rotationSpeed = 0.02;

// Обработка событий клавиатуры для управления позицией и вращением камеры
document.addEventListener('keydown', (event) => {
    switch (event.key) {
        case 'w': cameraPosition[1] -= cameraSpeed; break; // Вниз по Y
        case 's': cameraPosition[1] += cameraSpeed; break; // Вверх по Y
        case 'a': cameraPosition[0] += cameraSpeed; break; // Вправо по X
        case 'd': cameraPosition[0] -= cameraSpeed; break; // Влево по X
        case 'q': cameraPosition[2] += cameraSpeed; break; // Вверх по Z
        case 'e': cameraPosition[2] -= cameraSpeed; break; // Вниз по Z
        case 'ArrowUp': cameraRotation[0] += rotationSpeed; break;
        case 'ArrowDown': cameraRotation[0] -= rotationSpeed; break;
        case 'ArrowLeft': cameraRotation[1] += rotationSpeed; break;
        case 'ArrowRight': cameraRotation[1] -= rotationSpeed; break;
    }
});

// Получение матрицы вида камеры
function getCameraViewMatrix() {
    const modelViewMatrix = mat4.create();

    // Применяем сдвиг камеры в позицию (cameraPosition)
    mat4.translate(modelViewMatrix, modelViewMatrix, cameraPosition);

    // Поворачиваем камеру вокруг осей X, Y, Z на указанные углы (cameraRotation)
    mat4.rotateX(modelViewMatrix, modelViewMatrix, cameraRotation[0]);
    mat4.rotateY(modelViewMatrix, modelViewMatrix, cameraRotation[1]);
    mat4.rotateZ(modelViewMatrix, modelViewMatrix, cameraRotation[2]);

    return modelViewMatrix;
};

// Получение проекционной матрицы камеры
function getCameraProjectionMatrix() {
    const projectionMatrix = mat4.create();

    // Устанавливаем перспективную проекцию
    mat4.perspective(projectionMatrix, 45 * Math.PI / 180, gl.canvas.clientWidth / gl.canvas.clientHeight, 0.1, 1000.0);

    return projectionMatrix;
};

// Получение матрицы вида для объектов с учётом камеры
function getObjViewMatrix(camera_viewMatr, translation, rotation) {
    const modelViewMatrix = camera_viewMatr;

    // Сдвиг
    mat4.translate(modelViewMatrix, modelViewMatrix, translation);

    // Вращение
    mat4.rotateX(modelViewMatrix, modelViewMatrix, rotation[0]);
    mat4.rotateY(modelViewMatrix, modelViewMatrix, rotation[1]);
    mat4.rotateZ(modelViewMatrix, modelViewMatrix, rotation[2]);

    return modelViewMatrix;
};

console.debug('camera.js compile');
