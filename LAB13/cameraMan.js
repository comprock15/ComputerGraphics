const cameraPosition = [0, 0, -5];
const cameraRotation = [0, 0, 0];
const cameraSpeed = 0.1;
const rotationSpeed = 0.02;

// Обработка событий клавиатуры
document.addEventListener('keydown', (event) => {
    switch (event.key) {
        case 'w': cameraPosition[2] -= cameraSpeed; break;
        case 's': cameraPosition[2] += cameraSpeed; break;
        case 'a': cameraPosition[0] -= cameraSpeed; break;
        case 'd': cameraPosition[0] += cameraSpeed; break;
        case 'ArrowUp': cameraRotation[1] += rotationSpeed; break;
        case 'ArrowDown': cameraRotation[1] -= rotationSpeed; break;
        case 'ArrowLeft': cameraRotation[2] += rotationSpeed; break;
        case 'ArrowRight': cameraRotation[2] -= rotationSpeed; break;
    }
});

function getCameraViewMatrix()
{
    const modelViewMatrix = mat4.create();
    mat4.translate(modelViewMatrix, modelViewMatrix, cameraPosition);
    mat4.rotateX(modelViewMatrix, modelViewMatrix, cameraRotation[0]);
    mat4.rotateY(modelViewMatrix, modelViewMatrix, cameraRotation[1]);
    mat4.rotateZ(modelViewMatrix, modelViewMatrix, cameraRotation[2]);
    return modelViewMatrix;
};

function getCameraProjectionMatrix()
{
    const projectionMatrix = mat4.create();
    mat4.perspective(projectionMatrix,
                            45 * Math.PI / 180, // fovy
                            gl.canvas.clientWidth / gl.canvas.clientHeight, // aspect
                            0.1, // zNear
                            1000.0 // zFar
                        );
    return projectionMatrix;
};

function getObjViewMatrix(camera_viewMatr, translation, rotation)
{
    const modelViewMatrix = camera_viewMatr;
    mat4.translate(modelViewMatrix, modelViewMatrix, translation);
    mat4.rotateX(modelViewMatrix, modelViewMatrix, rotation[0]);
    mat4.rotateY(modelViewMatrix, modelViewMatrix, rotation[1]);
    mat4.rotateZ(modelViewMatrix, modelViewMatrix, rotation[2]);
    return modelViewMatrix;
};

console.debug('camera.js compile');
