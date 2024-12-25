
// const projectionMatrix = mat4.create();
// let viewMatrix = mat4.create();

const camera = {
    position: vec3.fromValues(0, -40, -50),
    rotation: vec3.fromValues(0, 0, 0),
    move_speed: 0.5,
    rot_speed: 0.02,
  };

// function cameraMatrixSetUp()
// {
//     mat4.perspective(projectionMatrix, 45 * Math.PI / 180, canvas.width / canvas.height, 0.1, 1000.0);

//     viewMatrix = mat4.create();
//     mat4.translate(viewMatrix, viewMatrix, camera.cameraPosition);
//     mat4.rotateX(viewMatrix, viewMatrix, camera.cameraRotation[0]);
//     mat4.rotateY(viewMatrix, viewMatrix, camera.cameraRotation[1]);
//     mat4.rotateZ(viewMatrix, viewMatrix, camera.cameraRotation[2]);
// }


function getCameraViewMatrix() {
  let matrix = mat4.create();

  // Применяем сдвиг камеры в позицию (cameraPosition)
  mat4.translate(matrix, matrix, camera.position);

  // Поворачиваем камеру вокруг осей X, Y, Z на указанные углы (cameraRotation)
  mat4.rotateX(matrix, matrix, camera.rotation[0]);
  mat4.rotateY(matrix, matrix, camera.rotation[1]);
  mat4.rotateZ(matrix, matrix, camera.rotation[2]);

  return matrix;
};

// Получение проекционной матрицы камеры
function getCameraProjectionMatrix() {
  let matrix = mat4.create();

  // Устанавливаем перспективную проекцию
  mat4.perspective(matrix, 45 * Math.PI / 180, gl.canvas.clientWidth / gl.canvas.clientHeight, 0.1, 1000.0);

  return matrix;
};

// Получение матрицы вида для объектов с учётом камеры
function getObjViewMatrix(camera_viewMatr, translation, rotation, scale) {
  let matrix = mat4.create();
  matrix = camera_viewMatr;

  // Сдвиг
  mat4.translate(matrix, matrix, translation);

  // Вращение
  mat4.rotateX(matrix, matrix, rotation[0]);
  mat4.rotateY(matrix, matrix, rotation[1]);
  mat4.rotateZ(matrix, matrix, rotation[2]);

  mat4.scale(matrix, matrix, scale);

  return matrix;
};


