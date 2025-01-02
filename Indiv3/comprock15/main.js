// Импорт вспомогательных функций для работы с шейдерами и моделями
import { loadShader, loadOBJ, loadTexture, createProgram } from './utils.js';

// Точка входа после полной загрузки Document Object Model
document.addEventListener('DOMContentLoaded', main);

const startTime = Date.now();

// Основная функция программы
async function main() {
    const canvas = document.getElementById('glcanvas');
    const gl = canvas.getContext('webgl2');

    // Проверка поддержки WebGL2
    if (!gl) {
    alert('Ваш браузер не поддерживает WebGL2!');
    throw new Error('WebGL2 not supported');
    }

    const scene = await setScene(gl);
  
    const camera = [{
            position: vec3.fromValues(0, -10, -50),
            // Углы поворота камеры вокруг осей
            rotation: vec3.fromValues(10 * Math.PI/180, Math.PI, 0),
            speed: 0.5,
            rotationSpeed: 0.02,
        }, {
            position: vec3.fromValues(scene.objects[0].positions[0][0], scene.objects[0].positions[0][1] - 7, scene.objects[0].positions[0][2]),
            // Углы поворота камеры вокруг осей
            rotation: vec3.fromValues(Math.PI / 2, Math.PI, 0),
            speed: 1.0,
            rotationSpeed: 0.02,
        },
];

    let selectedCameraIndex = 0;

    document.addEventListener('keydown', (event) => {
        switch (event.key) {
            case 'ц':
            case 'w': 
                scene.objects[0].positions[0][2] += camera[1].speed; 
                camera[1].position[1] -= camera[1].speed; 
                break; // Вперед по Z
            case 'ы':
            case 's': 
                scene.objects[0].positions[0][2] -= camera[1].speed;
                camera[1].position[1] += camera[1].speed; 
                break; // Назад по Z
            case 'ф':
            case 'a': 
                scene.objects[0].positions[0][0] += camera[1].speed;
                camera[1].position[0] += camera[1].speed; 
                break; // Влево по X
            case 'в':
            case 'd': 
                scene.objects[0].positions[0][0] -= camera[1].speed;
                camera[1].position[0] -= camera[1].speed;
                break; // Вправо по X
            
            case 'ArrowUp': camera[0].rotation[0] += camera[0].rotationSpeed; break;
            case 'ArrowDown': camera[0].rotation[0] -= camera[0].rotationSpeed; break;
            case 'ArrowLeft': camera[0].rotation[1] += camera[0].rotationSpeed; break;
            case 'ArrowRight': camera[0].rotation[1] -= camera[0].rotationSpeed; break;

            case 'с':
            case 'c': selectedCameraIndex = (selectedCameraIndex + 1) % camera.length; break;
        }
        
        switch (event.code) {
            case 'Space': 
                scene.objects[0].positions[0][1] += camera[1].speed;
                camera[1].position[2] -= camera[1].speed;
                break; // Вверх по Y
            case 'ControlLeft': 
                scene.objects[0].positions[0][1] -= camera[1].speed;
                camera[1].position[2] += camera[1].speed; 
                break; // Вниз по Y
        }
    });

    let viewMatrix = mat4.create();
    const projectionMatrix = mat4.create();
  
    //Основной цикл отрисовки
    function render() {
        // Очистка экрана и буфера глубины
        gl.clearColor(0.3, 0.3, 0.5, 1.0);
        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
        gl.enable(gl.DEPTH_TEST);

        // Установка матриц камеры и проекции
        mat4.perspective(projectionMatrix, 45 * Math.PI / 180, canvas.width / canvas.height, 0.1, 1000.0);

        viewMatrix = mat4.create();
        mat4.translate(viewMatrix, viewMatrix, camera[selectedCameraIndex].position);
        mat4.rotateX(viewMatrix, viewMatrix, camera[selectedCameraIndex].rotation[0]);
        mat4.rotateY(viewMatrix, viewMatrix, camera[selectedCameraIndex].rotation[1]);
        mat4.rotateZ(viewMatrix, viewMatrix, camera[selectedCameraIndex].rotation[2]);
        
        drawScene(gl, scene, viewMatrix, projectionMatrix);

        requestAnimationFrame(render);
    }

    render();
}

async function setScene(gl) {
    const program = await createProgram(gl, 'shaders/vertexShader.vert', 'shaders/fragmentShader.frag');
    const programWaving = await createProgram(gl, 'shaders/waving.vert', 'shaders/fragmentShader.frag');
    const programNormalMap = await createProgram(gl, 'shaders/vertexShader.vert', 'shaders/normalmap.frag');

    const zeppelin = await loadOBJ(gl, "./models/zeppelin/zeppelin.obj");
    zeppelin.texture = await loadTexture(gl, './models/zeppelin/zeppelin.png');
    zeppelin.normalmap = await loadTexture(gl, './models/zeppelin/zeppelin_normal_map.png');

    const terrain = await loadOBJ(gl, "./models/terrain/terrain.obj");
    terrain.texture = await loadTexture(gl, './models/terrain/terrain.jpg');

    const cloud = await loadOBJ(gl, "./models/cloud/cloud.obj");
    cloud.texture = await loadTexture(gl, './models/cloud/cloud.png');

    const balloon = await loadOBJ(gl, "./models/balloon/balloon.obj");
    balloon.texture = await loadTexture(gl, './models/balloon/balloon.png');

    const tree = await loadOBJ(gl, "./models/christmas-tree/christmas-tree.obj");
    tree.texture = await loadTexture(gl, './models/christmas-tree/christmas-tree.png');

    const scene = {
        objects: [
            {
                model: zeppelin,
                texture: zeppelin.texture,
                normalmap: zeppelin.normalmap,
                positions: [vec3.fromValues(0, 0, 0)],
                rotation: vec3.create(),
                scale: vec3.fromValues(2.0, 2.0, 2.0),
                program: programNormalMap,
                material: {
                    ambient: [1, 1, 1],
                    diffuse: [1, 1, 1],
                    specular: [0.1, 0.1, 0.1],
                    shininess: 1.0,
                    roughness: 0.3,
                },
                numberOfInstances: 1
            },
            {
                model: terrain,
                texture: terrain.texture,
                positions: [vec3.fromValues(0, -15, 0)],
                rotation: vec3.create(),
                scale: vec3.fromValues(100.0, 1.0, 100.0),
                program: program,
                material: {
                    ambient: [1, 1, 1],
                    diffuse: [0.8, 0.8, 0.8],
                    specular: [0.1, 0.1, 0.1],
                    shininess: 1.0,
                    roughness: 0.9,
                },
                numberOfInstances: 1
            },
            {
                model: cloud,
                texture: cloud.texture,
                positions: [vec3.fromValues(0, 30, 0)],
                rotation: vec3.create(),
                scale: vec3.fromValues(2.0, 2.0, 2.0),
                program: program,
                material: {
                    ambient: [1, 1, 1],
                    diffuse: [1, 1, 1],
                    specular: [0.1, 0.1, 0.1],
                    shininess: 1.0,
                    roughness: 1.0,
                },
                numberOfInstances: 100,
                isCloud: true
            },
            {
                model: balloon,
                texture: balloon.texture,
                positions: [vec3.fromValues(0, 5, 0)],
                rotation: vec3.fromValues(0, 0.5, 0),
                scale: vec3.fromValues(1.5, 1.5, 1.5),
                program: programWaving,
                material: {
                    ambient: [1, 1, 1],
                    diffuse: [1, 1, 1],
                    specular: [0.1, 0.1, 0.1],
                    shininess: 1,
                    roughness: 0,
                },
                numberOfInstances: 5
            },
            {
                model: tree,
                texture: tree.texture,
                positions: [vec3.fromValues(10, -15, 30)],
                rotation: vec3.fromValues(0, 0, 0),
                scale: vec3.fromValues(5.0, 5.0, 5.0),
                program: programWaving,
                material: {
                    ambient: [1, 1, 1],
                    diffuse: [1, 1, 1],
                    specular: [0.1, 0.1, 0.1],
                    shininess: 1.0,
                    roughness: 1.0,
                },
                numberOfInstances: 1
            }
        ],
        lights: {
            ambientLight: {
                color: [0.3, 0.3, 0.4]
            },
            directionalLight: {
                direction: vec3.fromValues(15, -10, 10),
                color: vec3.fromValues(1, 1, 1),
                intensity: 1.0
            }
        }
    }
    const sceneSize = 100;
    scene.objects.forEach((obj) => {
        if (obj.numberOfInstances > 1) {
            obj.angles = [];
            for (let i = 0; i < obj.numberOfInstances; ++i) {
                let pos = vec3.fromValues(...obj.positions[0]);
                pos[0] += sceneSize * (Math.random() - 0.5);
                pos[1] += 10 * (Math.random() - 0.5);
                pos[2] += sceneSize * (Math.random() - 0.5);
                obj.positions[i + 1] = pos;
                obj.angles[i] = 2 * Math.PI * Math.random();
            }
            obj.positions.shift();
        }
        obj.ambientBuffer = gl.createBuffer();
    })

    return scene;
}

async function drawScene(gl, scene, viewMatrix, projectionMatrix) {
    // Отрисовка объектов сцены
    const time = (Date.now() - startTime) / 50000;
    scene.objects.forEach(obj => {
        gl.useProgram(obj.program);
        
        const modelMatrices = new Float32Array(16 * obj.numberOfInstances);
        const normalMatrices = new Float32Array(16 * obj.numberOfInstances);
        const ambients = new Float32Array(3 * obj.numberOfInstances);

        for (let i = 0; i < obj.numberOfInstances; ++i) {
            ambients.set(obj.material.ambient, i * 3);
            // Создание и настройка матрицы модели
            const modelMatrix = mat4.create();
            let pos =  vec3.fromValues(...obj.positions[i]);
            if (obj.numberOfInstances > 1 && obj.isCloud) {
                const r = 20;

                pos[0] = pos[0] + r * Math.sin((3*time + Math.PI/2) + obj.angles[i]);
                pos[2] = pos[2] + r * Math.sin(2*time + obj.angles[i]);

                const shimmer = Math.max(1.0, 3 * Math.sin(100*time + obj.angles[i]));
                ambients.set(vec3.fromValues(shimmer, shimmer, shimmer), i * 3);
            }
            mat4.translate(modelMatrix, modelMatrix, pos);
            mat4.rotateX(modelMatrix, modelMatrix, obj.rotation[0]);
            mat4.rotateY(modelMatrix, modelMatrix, obj.rotation[1]);
            mat4.rotateZ(modelMatrix, modelMatrix, obj.rotation[2]);
            mat4.scale(modelMatrix, modelMatrix, obj.scale);
            modelMatrices.set(modelMatrix, i * 16);

            // Вычисление нормальной матрицы
            const normalMatrix = mat4.create();
            mat4.invert(normalMatrix, modelMatrix);
            mat4.transpose(normalMatrix, normalMatrix);
            normalMatrices.set(normalMatrix, i * 16);
        }
        //console.log(ambients);

        // Передача матриц в шейдер
        const uViewMatrix = gl.getUniformLocation(obj.program, 'uViewMatrix');
        const uProjectionMatrix = gl.getUniformLocation(obj.program, 'uProjectionMatrix');
        const aNormalMatrix = gl.getAttribLocation(obj.program, 'aNormalMatrix');
        const aModelMatrix = gl.getAttribLocation(obj.program, 'aModelMatrix');

        // Передача текущего времени для колыханий
        const uTime = gl.getUniformLocation(obj.program, 'uTime');
        if (uTime) {
            gl.uniform1f(uTime, time);
        }
  
        gl.uniformMatrix4fv(uViewMatrix, false, viewMatrix);
        gl.uniformMatrix4fv(uProjectionMatrix, false, projectionMatrix);

        gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.modelMatrixBuffer);
        gl.bufferData(gl.ARRAY_BUFFER, modelMatrices, gl.DYNAMIC_DRAW);
        for (let i = 0; i < 4; ++i) {
            gl.enableVertexAttribArray(aModelMatrix + i);
            gl.vertexAttribPointer(
                aModelMatrix + i,
                4, // кол-во элементов в столбце
                gl.FLOAT,
                false,
                16 * Float32Array.BYTES_PER_ELEMENT, // шаг между последовательными элементами
                i * 4 * Float32Array.BYTES_PER_ELEMENT // начальный оффсет
            );
            gl.vertexAttribDivisor(aModelMatrix + i, 1); // Обновляем один раз в экземпляр
        }

        gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.modelNormalBuffer);
        gl.bufferData(gl.ARRAY_BUFFER, normalMatrices, gl.DYNAMIC_DRAW);
        for (let i = 0; i < 4; ++i) {
            gl.enableVertexAttribArray(aNormalMatrix + i);
            gl.vertexAttribPointer(
                aNormalMatrix + i,
                4, // кол-во элементов в столбце
                gl.FLOAT,
                false,
                16 * Float32Array.BYTES_PER_ELEMENT, // шаг между последовательными элементами
                i * 4 * Float32Array.BYTES_PER_ELEMENT // начальный оффсет
            );
            gl.vertexAttribDivisor(aNormalMatrix + i, 1); // Обновляем один раз в экземпляр
        }
  
        // Привязка текстуры и передача в шейдер
        const uTexture = gl.getUniformLocation(obj.program, 'uTexture');
        gl.activeTexture(gl.TEXTURE0);
        gl.bindTexture(gl.TEXTURE_2D, obj.texture);
        gl.uniform1i(uTexture, 0);

        // Привязка карты нормалей и передача в шейдер
        const uNormalMap = gl.getUniformLocation(obj.program, 'uNormalMap');
        if (uNormalMap) {
            gl.activeTexture(gl.TEXTURE1);
            gl.bindTexture(gl.TEXTURE_2D, obj.normalmap);
            gl.uniform1i(uNormalMap, 1);
        }
  
        // Передача материала в шейдер
        if (obj.material) {
          const aAmbient = gl.getAttribLocation(obj.program, 'aAmbient');
          gl.bindBuffer(gl.ARRAY_BUFFER, obj.ambientBuffer);
          gl.bufferData(gl.ARRAY_BUFFER, ambients, gl.DYNAMIC_DRAW);
          gl.vertexAttribPointer(aAmbient, 3, gl.FLOAT, false, 0, 0);
          gl.vertexAttribDivisor(aAmbient, 1); // Обновляем один раз в экземпляр
          gl.enableVertexAttribArray(aAmbient);

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
        }
  
        // Передача источников света
        const uAmbientLightColor = gl.getUniformLocation(obj.program, 'uAmbientLight.color');
        gl.uniform3fv(uAmbientLightColor, scene.lights.ambientLight.color);
  
        const uDirectionalLightDirection = gl.getUniformLocation(obj.program, 'uDirectionalLight.direction');
        gl.uniform3fv(uDirectionalLightDirection, scene.lights.directionalLight.direction);
        const uDirectionalLightColor = gl.getUniformLocation(obj.program, 'uDirectionalLight.color');
        gl.uniform3fv(uDirectionalLightColor, scene.lights.directionalLight.color);
        const uDirectionalLightIntensity = gl.getUniformLocation(obj.program, 'uDirectionalLight.intensity');
        gl.uniform1f(uDirectionalLightIntensity, scene.lights.directionalLight.intensity);
  
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
        gl.drawElementsInstanced(gl.TRIANGLES, obj.model.indices.length, gl.UNSIGNED_SHORT, 0, obj.numberOfInstances);
      });
}