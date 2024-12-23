
const projectionMatrix = mat4.create();
let viewMatrix = mat4.create();

const camera = {
    position: vec3.fromValues(0, 0, -35),
    rotation: vec3.fromValues(0, 0, 0),
    move_speed: 0.1,
    rot_speed: 0.02,
  };

function cameraMatrixSetUp()
{
    mat4.perspective(projectionMatrix, 45 * Math.PI / 180, canvas.width / canvas.height, 0.1, 1000.0);

    viewMatrix = mat4.create();
    mat4.translate(viewMatrix, viewMatrix, camera.cameraPosition);
    mat4.rotateX(viewMatrix, viewMatrix, camera.cameraRotation[0]);
    mat4.rotateY(viewMatrix, viewMatrix, camera.cameraRotation[1]);
    mat4.rotateZ(viewMatrix, viewMatrix, camera.cameraRotation[2]);
}


document.addEventListener('keydown', (event) => {
    switch (event.key) {
      case 'a': 
        camera.position[0] += camera.move_speed; 
        break; // Вправо по X
      case 'd': 
        camera.position[0] -= camera.move_speed; 
        break; // Влево по X
      case 'w': 
        camera.position[1] -= camera.move_speed; 
        break; // Вниз по Y
      case 's': 
        camera.position[1] += camera.move_speed; 
        break; // Вверх по Y
      case 'q': 
        camera.position[2] += camera.move_speed; 
        break; // Вверх по Z
      case 'e': 
        camera.position[2] -= camera.move_speed; 
        break; // Вниз по Z

      case 'ArrowUp': 
        camera.rotation[0] += camera.rot_speed; 
        break;
      case 'ArrowDown': 
        camera.rotation[0] -= camera.rot_speed; 
        break;
      case 'ArrowLeft': 
        camera.rotation[1] += camera.rot_speed; 
        break;
      case 'ArrowRight': 
        camera.rotation[1] -= camera.rot_speed; 
        break;
    }
  });