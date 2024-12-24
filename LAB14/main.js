// Импорт вспомогательных функций для работы с шейдерами и моделями
import { loadShader, loadOBJ } from './utils.js';

// Основная функция программы
async function main() {
  const canvas = document.getElementById('glCanvas');
  const gl = canvas.getContext('webgl2');

  // Проверка поддержки WebGL2
  if (!gl) {
    alert('Ваш браузер не поддерживает WebGL2!');
    throw new Error('WebGL2 not supported');
  }

  // Функция для настройки размера канваса и области отображения
  const setCanvasSize = () => {
    // canvas.width = window.innerWidth;
    // canvas.height = window.innerHeight;
    gl.viewport(0, 0, canvas.width, canvas.height);
  };

  // Установка начального размера канваса и обработчик изменения размера окна
  setCanvasSize();
  window.addEventListener('resize', setCanvasSize);

  // Загрузка и компиляция шейдерных программ
  const phongProgram = await createProgram(gl, 'shaders/phong.vert', 'shaders/phong.frag');
  const toonProgram = await createProgram(gl, 'shaders/toon.vert', 'shaders/toon.frag');
  const orenNayar = await createProgram(gl, 'shaders/orenNayar.vert', 'shaders/orenNayar.frag');

  // Загрузка моделей из OBJ-файлов
  const models = {};
  models.model1 = await loadOBJ(gl, 'models/model1.obj');
  models.model2 = await loadOBJ(gl, 'models/model2.obj');
  models.model3 = await loadOBJ(gl, 'models/model3.obj');
  models.model4 = await loadOBJ(gl, 'models/model4.obj');
  models.model5 = await loadOBJ(gl, 'models/model5.obj');
  models.model6 = await loadOBJ(gl, 'models/bowl.obj');
  models.model7 = await loadOBJ(gl, 'models/teapot.obj');

  // Загрузка текстур для моделей
  const textures = {};
  textures.texture1 = await loadTexture(gl, 'textures/texture1.jpg');
  textures.texture2 = await loadTexture(gl, 'textures/texture2.jpg');
  textures.texture3 = await loadTexture(gl, 'textures/texture3.jpg');
  textures.texture4 = await loadTexture(gl, 'textures/texture4.jpg');
  textures.texture5 = await loadTexture(gl, 'textures/texture5.jpg');
  textures.texture6 = await loadTexture(gl, 'textures/bowl.jpg');
  textures.texture7 = await loadTexture(gl, 'textures/teapot.jpg');

  // Настройка объектов сцены (модель, текстура, трансформации, материал, шейдер)
  const sceneObjects = [
    {
      model: models.model6,
      texture: textures.texture6,
      position: vec3.fromValues(25, -30, -20),
      rotation: vec3.create(),
      scale: vec3.fromValues(5.0, 5.0, 5.0),
      program: phongProgram, // чашка с Фонгом
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.8, 0.8],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0,
        roughness: 0.3,
      }
    },
    {
      model: models.model6,
      texture: textures.texture6,
      position: vec3.fromValues(-25, -30, -20),
      rotation: vec3.create(),
      scale: vec3.fromValues(5.0, 5.0, 5.0),
      program: toonProgram, // чашка с туншейдингом
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.8, 0.8],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0,
        roughness: 0.3,
      }
    },
    {
      model: models.model6,
      texture: textures.texture6,
      position: vec3.fromValues(0, -30, -20),
      rotation: vec3.create(),
      scale: vec3.fromValues(5.0, 5.0, 5.0),
      program: orenNayar, // чашка с КФ
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.5, 0.3],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0,
        roughness: 0.5,
      }
    },
    {
      model: models.model7,
      texture: textures.texture7,
      position: vec3.fromValues(30, -30, -50),
      rotation: vec3.create(),
      scale: vec3.fromValues(20.0, 20.0, 20.0),
      program: phongProgram, // чайник с Фонгом
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.8, 0.8],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0,
        roughness: 0.3,
      }
    },
    {
      model: models.model7,
      texture: textures.texture7,
      position: vec3.fromValues(-30, -30, -50),
      rotation: vec3.create(),
      scale: vec3.fromValues(20.0, 20.0, 20.0),
      program: toonProgram, // чайник с туншейдингом
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.8, 0.8],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0,
        roughness: 0.3,
      }
    },
    {
      model: models.model7,
      texture: textures.texture7,
      position: vec3.fromValues(0, -30, -50),
      rotation: vec3.create(),
      scale: vec3.fromValues(20.0, 20.0, 20.0),
      program: orenNayar, // чайник с КФ
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.5, 0.3],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0,
        roughness: 0.5,
      }
    },
    {
      model: models.model1,
      texture: textures.texture1,
      position: vec3.fromValues(0, -10, -50),
      rotation: vec3.create(),
      scale: vec3.fromValues(2.0, 2.0, 2.0),
      program: toonProgram, // слон
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.8, 0.8],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0,
        roughness: 0.3,
      }
    },
    {
      model: models.model2,
      texture: textures.texture2,
      position: vec3.fromValues(-10, 0, -15),
      rotation: vec3.create(),
      scale: vec3.fromValues(2, 2, 2),
      program: phongProgram, // 
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.8, 0.8],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0,
        roughness: 0.3,
      }
    },
    {
      model: models.model3,
      texture: textures.texture3,
      position: vec3.fromValues(10, 0, -15),
      rotation: vec3.create(),
      scale: vec3.fromValues(1, 1, 1),
      program: phongProgram,
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.8, 0.8],
        specular: [0.5, 0.5, 0.5],
        roughness: 0.3,
        shininess: 32.0,
        //F0: [0.9, 0.9, 0.9]
      }
    },
    {
      model: models.model4,
      texture: textures.texture4,
      position: vec3.fromValues(-20, -1.5, -15),
      rotation: vec3.create(),
      scale: vec3.fromValues(2, 2, 2),
      program: phongProgram,
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.8, 0.8],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0
      }
    },
    {
      model: models.model5,
      texture: textures.texture5,
      position: vec3.fromValues(20, -5, -15),
      rotation: vec3.create(),
      scale: vec3.fromValues(1, 1, 1),
      program: phongProgram,
      material: {
        ambient: [0.2, 0.2, 0.2],
        diffuse: [0.8, 0.8, 0.8],
        specular: [0.5, 0.5, 0.5],
        shininess: 32.0,
        roughness: 0.3,
      }
    }
  ];

  // Источники света
  const lights = {
    ambientLight: {
      color: [0.1, 0.1, 0.1]
    },
    pointLight: {
      position: vec3.fromValues(18, -22, -46),
      color: vec3.fromValues(1, 1, 1),
      intensity: 1.0
    },
    directionalLight: {
      direction: vec3.fromValues(0, -20, -1),
      color: vec3.fromValues(1, 1, 1),
      intensity: 1.0
    },
    spotLight: {
      position: vec3.fromValues(0, 0, -35),
      direction: vec3.fromValues(0, 0, -1),
      color: vec3.fromValues(1, 1, 1),
      intensity: 5.0,
      coneAngle: 30 * Math.PI / 180,
      cutoffAngle: 40 * Math.PI / 180
    },
  };

  document.getElementById("global_light").addEventListener('change', (e) => {lights.directionalLight.intensity = document.getElementById("global_light").valueAsNumber; });
  document.getElementById("point_light").addEventListener('change', (e) => {lights.pointLight.intensity = document.getElementById("point_light").valueAsNumber;});
  document.getElementById("proj_light").addEventListener('change', (e) => {lights.spotLight.intensity = document.getElementById("proj_light").valueAsNumber;});

  document.getElementById("point_light_posX").addEventListener('change', (e) => {lights.pointLight.position[0] = document.getElementById("point_light_posX").valueAsNumber;});
  document.getElementById("point_light_posY").addEventListener('change', (e) => {lights.pointLight.position[1] = document.getElementById("point_light_posY").valueAsNumber;});
  document.getElementById("point_light_posZ").addEventListener('change', (e) => {lights.pointLight.position[2] = document.getElementById("point_light_posZ").valueAsNumber;});

  document.getElementById("proj_light_posX").addEventListener('change', (e) => {lights.spotLight.position[0] = document.getElementById("proj_light_posX").valueAsNumber;});
  document.getElementById("proj_light_posY").addEventListener('change', (e) => {lights.spotLight.position[1] = document.getElementById("proj_light_posY").valueAsNumber;});
  document.getElementById("proj_light_posZ").addEventListener('change', (e) => {lights.spotLight.position[2] = document.getElementById("proj_light_posZ").valueAsNumber;});

  document.getElementById("proj_light_rotX").addEventListener('change', (e) => {lights.spotLight.direction[0] = document.getElementById("proj_light_rotX").valueAsNumber;});
  document.getElementById("proj_light_rotY").addEventListener('change', (e) => {lights.spotLight.direction[1] = document.getElementById("proj_light_rotY").valueAsNumber;});
  document.getElementById("proj_light_rotZ").addEventListener('change', (e) => {lights.spotLight.direction[2] = document.getElementById("proj_light_rotZ").valueAsNumber;});

  document.getElementById("proj_light_width").addEventListener('change', (e) => {lights.spotLight.cutoffAngle = document.getElementById("proj_light_width").valueAsNumber * Math.PI / 180;})

  const camera = {
    cameraPosition: vec3.fromValues(0, 5, -40),
    // Углы поворота камеры вокруг осей
    cameraRotation: vec3.fromValues(20 * Math.PI/180, 0, 0),
    cameraSpeed: 0.5,
    cameraRotationSpeed: 0.02,
  };

  // Управление камерой с помощью клавиш
  document.addEventListener('keydown', (event) => {
    switch (event.key) {
      case 'ц':
      case 'w': camera.cameraPosition[1] -= camera.cameraSpeed; break; // Вниз по Y
      case 'ы':
      case 's': camera.cameraPosition[1] += camera.cameraSpeed; break; // Вверх по Y
      case 'ф':
      case 'a': camera.cameraPosition[0] += camera.cameraSpeed; break; // Вправо по X
      case 'в':
      case 'd': camera.cameraPosition[0] -= camera.cameraSpeed; break; // Влево по X
      case 'й':
      case 'q': camera.cameraPosition[2] += camera.cameraSpeed; break; // Вверх по Z
      case 'у':
      case 'e': camera.cameraPosition[2] -= camera.cameraSpeed; break; // Вниз по Z
      case 'ArrowUp': camera.cameraRotation[0] += camera.cameraRotationSpeed; break;
      case 'ArrowDown': camera.cameraRotation[0] -= camera.cameraRotationSpeed; break;
      case 'ArrowLeft': camera.cameraRotation[1] += camera.cameraRotationSpeed; break;
      case 'ArrowRight': camera.cameraRotation[1] -= camera.cameraRotationSpeed; break;
    }
  });

  let viewMatrix = mat4.create();
  const projectionMatrix = mat4.create();
  
  //Основной цикл отрисовки
  function render() {
    // Очистка экрана и буфера глубины
    gl.clearColor(0.1, 0.1, 0.1, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
    gl.enable(gl.DEPTH_TEST);

    // Установка матриц камеры и проекции
    mat4.perspective(projectionMatrix, 45 * Math.PI / 180, canvas.width / canvas.height, 0.1, 1000.0);

    viewMatrix = mat4.create();
    mat4.translate(viewMatrix, viewMatrix, camera.cameraPosition);
    mat4.rotateX(viewMatrix, viewMatrix, camera.cameraRotation[0]);
    mat4.rotateY(viewMatrix, viewMatrix, camera.cameraRotation[1]);
    mat4.rotateZ(viewMatrix, viewMatrix, camera.cameraRotation[2]);
    

    // Отрисовка объектов сцены
    sceneObjects.forEach(obj => {
      gl.useProgram(obj.program);

      // Создание и настройка матрицы модели
      const modelMatrix = mat4.create();
      mat4.translate(modelMatrix, modelMatrix, obj.position);
      mat4.rotateX(modelMatrix, modelMatrix, obj.rotation[0]);
      mat4.rotateY(modelMatrix, modelMatrix, obj.rotation[1]);
      mat4.rotateZ(modelMatrix, modelMatrix, obj.rotation[2]);
      mat4.scale(modelMatrix, modelMatrix, obj.scale);

      // Вычисление нормальной матрицы
      const normalMatrix = mat4.create();
      mat4.invert(normalMatrix, modelMatrix);
      mat4.transpose(normalMatrix, normalMatrix);

      // Передача матриц в шейдер
      const uModelMatrix = gl.getUniformLocation(obj.program, 'uModelMatrix');
      const uViewMatrix = gl.getUniformLocation(obj.program, 'uViewMatrix');
      const uProjectionMatrix = gl.getUniformLocation(obj.program, 'uProjectionMatrix');
      const uNormalMatrix = gl.getUniformLocation(obj.program, 'uNormalMatrix');

      gl.uniformMatrix4fv(uModelMatrix, false, modelMatrix);
      gl.uniformMatrix4fv(uViewMatrix, false, viewMatrix);
      gl.uniformMatrix4fv(uProjectionMatrix, false, projectionMatrix);
      gl.uniformMatrix4fv(uNormalMatrix, false, normalMatrix);

      // Привязка текстуры и передача в шейдер
      const uTexture = gl.getUniformLocation(obj.program, 'uTexture');
      gl.activeTexture(gl.TEXTURE0);
      gl.bindTexture(gl.TEXTURE_2D, obj.texture);
      gl.uniform1i(uTexture, 0);

      // Передача материала в шейдер
      if (obj.material) {
        const uAmbientColor = gl.getUniformLocation(obj.program, 'uMaterial.ambient');
        gl.uniform3fv(uAmbientColor, obj.material.ambient);
        const uDiffuseColor = gl.getUniformLocation(obj.program, 'uMaterial.diffuse');
        gl.uniform3fv(uDiffuseColor, obj.material.diffuse);
        const uSpecularColor = gl.getUniformLocation(obj.program, 'uMaterial.specular');
        if (uSpecularColor)
          gl.uniform3fv(uSpecularColor, obj.material.specular);
        const uShininess = gl.getUniformLocation(obj.program, 'uMaterial.shininess');
        if (uShininess)
          gl.uniform1f(uShininess, obj.material.shininess)
        const uRoughness = gl.getUniformLocation(obj.program, 'uMaterial.roughness');
        if (uRoughness)
          gl.uniform1f(uRoughness, obj.material.roughness)
        const uF0 = gl.getUniformLocation(obj.program, 'uMaterial.F0');
        if (uF0)
          gl.uniform3fv(uF0, obj.material.F0);
      }

      // Передача источников света
      const uAmbientLightColor = gl.getUniformLocation(obj.program, 'uAmbientLight.color');
      gl.uniform3fv(uAmbientLightColor, lights.ambientLight.color);

      const uPointLightPosition = gl.getUniformLocation(obj.program, 'uPointLight.position');
      gl.uniform3fv(uPointLightPosition, lights.pointLight.position);
      const uPointLightColor = gl.getUniformLocation(obj.program, 'uPointLight.color');
      gl.uniform3fv(uPointLightColor, lights.pointLight.color);
      const uPointLightIntensity = gl.getUniformLocation(obj.program, 'uPointLight.intensity');
      gl.uniform1f(uPointLightIntensity, lights.pointLight.intensity);

      const uDirectionalLightDirection = gl.getUniformLocation(obj.program, 'uDirectionalLight.direction');
      gl.uniform3fv(uDirectionalLightDirection, lights.directionalLight.direction);
      const uDirectionalLightColor = gl.getUniformLocation(obj.program, 'uDirectionalLight.color');
      gl.uniform3fv(uDirectionalLightColor, lights.directionalLight.color);
      const uDirectionalLightIntensity = gl.getUniformLocation(obj.program, 'uDirectionalLight.intensity');
      gl.uniform1f(uDirectionalLightIntensity, lights.directionalLight.intensity);

      const uSpotLightPosition = gl.getUniformLocation(obj.program, 'uSpotLight.position');
      gl.uniform3fv(uSpotLightPosition, lights.spotLight.position);
      const uSpotLightDirection = gl.getUniformLocation(obj.program, 'uSpotLight.direction');
      gl.uniform3fv(uSpotLightDirection, lights.spotLight.direction);
      const uSpotLightColor = gl.getUniformLocation(obj.program, 'uSpotLight.color');
      gl.uniform3fv(uSpotLightColor, lights.spotLight.color);
      const uSpotLightIntensity = gl.getUniformLocation(obj.program, 'uSpotLight.intensity');
      gl.uniform1f(uSpotLightIntensity, lights.spotLight.intensity);
      const uSpotLightConeAngle = gl.getUniformLocation(obj.program, 'uSpotLight.coneAngle');
      gl.uniform1f(uSpotLightConeAngle, lights.spotLight.coneAngle);
      const uSpotLightCutoffAngle = gl.getUniformLocation(obj.program, 'uSpotLight.cutoffAngle');
      gl.uniform1f(uSpotLightCutoffAngle, lights.spotLight.cutoffAngle);

      // Привязка атрибутов вершин
      const positionAttribute = gl.getAttribLocation(obj.program, 'aPosition');
      const normalAttribute = gl.getAttribLocation(obj.program, 'aNormal');
      const uvAttribute = gl.getAttribLocation(obj.program, 'aTexCoord');

      gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.vertexBuffer);
      gl.vertexAttribPointer(positionAttribute, 3, gl.FLOAT, false, 0, 0);
      gl.enableVertexAttribArray(positionAttribute);

      gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.normalBuffer);
      gl.vertexAttribPointer(normalAttribute, 3, gl.FLOAT, false, 0, 0);
      gl.enableVertexAttribArray(normalAttribute);

      gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.uvBuffer);
      gl.vertexAttribPointer(uvAttribute, 2, gl.FLOAT, false, 0, 0);
      gl.enableVertexAttribArray(uvAttribute);

      // Отрисовка модели
      gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, obj.model.indexBuffer);
      gl.drawElements(gl.TRIANGLES, obj.model.indices.length, gl.UNSIGNED_SHORT, 0);
    });
    requestAnimationFrame(render);
  }
  render();
}

// Создание шейдерной программы
async function createProgram(gl, vertexShaderPath, fragmentShaderPath) {
  const vertexShader = await loadShader(gl, vertexShaderPath, gl.VERTEX_SHADER);
  const fragmentShader = await loadShader(gl, fragmentShaderPath, gl.FRAGMENT_SHADER);

  const program = gl.createProgram();
  gl.attachShader(program, vertexShader);
  gl.attachShader(program, fragmentShader);
  gl.linkProgram(program);

  if (!gl.getProgramParameter(program, gl.LINK_STATUS)) {
    console.error('Error linking program:', gl.getProgramInfoLog(program));
    gl.deleteProgram(program);
    return null;
  }
  gl.deleteShader(vertexShader)
  gl.deleteShader(fragmentShader)

  return program;
}

// Загрузка текстуры
async function loadTexture(gl, url) {
  return new Promise((resolve, reject) => {
    const image = new Image();
    image.src = url;
    image.onload = () => {
      const texture = gl.createTexture();
      gl.bindTexture(gl.TEXTURE_2D, texture);
      gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);
      gl.generateMipmap(gl.TEXTURE_2D);
      gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_LINEAR);
      gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR);

      resolve(texture);
    };

    image.onerror = () => {
      reject(`Could not load texture at ${url}`);
    };
  });
}

main();