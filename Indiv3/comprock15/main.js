// Импорт вспомогательных функций для работы с шейдерами и моделями
import { loadShader, loadOBJ, loadTexture, createProgram } from './utils.js';

// Точка входа после полной загрузки Document Object Model
document.addEventListener('DOMContentLoaded', main);

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
  
    const camera = {
        cameraPosition: vec3.fromValues(0, -10, -40),
        // Углы поворота камеры вокруг осей
        cameraRotation: vec3.fromValues(20 * Math.PI/180, Math.PI, 0),
        cameraSpeed: 0.5,
        cameraRotationSpeed: 0.02,
    };

    document.addEventListener('keydown', (event) => {
        switch (event.key) {
            case 'ц':
            case 'w': scene.objects[0].position[2] += camera.cameraSpeed; break; // Вперед по Z
            case 'ы':
            case 's': scene.objects[0].position[2] -= camera.cameraSpeed; break; // Назад по Z
            case 'ф':
            case 'a': scene.objects[0].position[0] += camera.cameraSpeed; break; // Вправо по X
            case 'в':
            case 'd': scene.objects[0].position[0] -= camera.cameraSpeed; break; // Влево по X
            
            case 'ArrowUp': camera.cameraRotation[0] += camera.cameraRotationSpeed; break;
            case 'ArrowDown': camera.cameraRotation[0] -= camera.cameraRotationSpeed; break;
            case 'ArrowLeft': camera.cameraRotation[1] += camera.cameraRotationSpeed; break;
            case 'ArrowRight': camera.cameraRotation[1] -= camera.cameraRotationSpeed; break;
        }
        
        switch (event.code) {
            case 'Space': scene.objects[0].position[1] += camera.cameraSpeed; break; // Вверх по Y
            case 'ControlLeft': scene.objects[0].position[1] -= camera.cameraSpeed; break; // Вниз по Y
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
        
        drawScene(gl, scene, viewMatrix, projectionMatrix);

        requestAnimationFrame(render);
    }

    render();
}

async function setScene(gl) {
    const program = await createProgram(gl, 'shaders/vertexShader.vert', 'shaders/fragmentShader.frag');

    const zeppelin = await loadOBJ(gl, "./models/zeppelin/untitled.obj");
    zeppelin.texture = await loadTexture(gl, './models/zeppelin/zeppelin.png');
    const terrain = await loadOBJ(gl, "./models/zeppelin/zeppelin.obj");
    terrain.texture = await loadTexture(gl, './models/zeppelin/zeppelin.png');
    const cloud = await loadOBJ(gl, "./models/zeppelin/untitled.obj");
    cloud.texture = await loadTexture(gl, './models/zeppelin/zeppelin.png');
    const balloon = await loadOBJ(gl, "./models/zeppelin/zeppelin.obj");
    balloon.texture = await loadTexture(gl, './models/zeppelin/zeppelin.png');

    const scene = {
        objects: [
            {
                model: zeppelin,
                texture: zeppelin.texture,
                positions: [vec3.fromValues(0, 0, 0)],
                rotation: vec3.create(),
                scale: vec3.fromValues(1.0, 1.0, 1.0),
                program: program,
                material: {
                    ambient: [1, 1, 1],
                    diffuse: [1, 1, 1],
                    specular: [1, 1, 1],
                    shininess: 1.0,
                    roughness: 0.3,
                },
                numberOfInstances: 1
            },
            {
                model: terrain,
                texture: terrain.texture,
                positions: [vec3.fromValues(0, -40, 0)],
                rotation: vec3.create(),
                scale: vec3.fromValues(100.0, 1.0, 100.0),
                program: program,
                material: {
                    ambient: [0.2, 0.2, 0.2],
                    diffuse: [0.8, 0.8, 0.8],
                    specular: [0.5, 0.5, 0.5],
                    shininess: 32.0,
                    roughness: 0.9,
                },
                numberOfInstances: 1
            },
            {
                model: cloud,
                texture: cloud.texture,
                positions: [vec3.fromValues(0, 30, 0)],
                rotation: vec3.create(),
                scale: vec3.fromValues(1.0, 1.0, 1.0),
                program: program,
                material: {
                    ambient: [1, 1, 1],
                    diffuse: [1, 1, 1],
                    specular: [1, 1, 1],
                    shininess: 1.0,
                    roughness: 1.0,
                },
                numberOfInstances: 10,
                isCloud: true
            },
            {
                model: balloon,
                texture: balloon.texture,
                positions: [vec3.fromValues(0, 15, 0)],
                rotation: vec3.fromValues(0, 0.5, 0),
                scale: vec3.fromValues(1.0, 1.0, 1.0),
                program: program,
                material: {
                    ambient: [1, 1, 1],
                    diffuse: [1, 1, 1],
                    specular: [1, 1, 1],
                    shininess: 1.0,
                    roughness: 1.0,
                },
                numberOfInstances: 5
            }
        ],
        lights: {
            ambientLight: {
                color: [0.1, 0.1, 0.1]
            },
            directionalLight: {
                direction: vec3.fromValues(20, -20, -20),
                color: vec3.fromValues(1, 1, 1),
                intensity: 1.0
            }
        }
    }

    scene.objects.forEach((obj) => {
        if (obj.numberOfInstances > 1) {
            obj.angles = [];
            for (let i = 0; i < obj.numberOfInstances; ++i) {
                let pos = vec3.fromValues(...obj.positions[0]);
                pos[0] += 30 * (Math.random() - 0.5);
                pos[1] += 10 * (Math.random() - 0.5);
                pos[2] += 30 * (Math.random() - 0.5);
                obj.positions[i + 1] = pos;
                obj.angles[i] = 2 * Math.PI * Math.random();
            }
            obj.positions.shift();
        }
    })

    return scene;
}

async function drawScene(gl, scene, viewMatrix, projectionMatrix) {
    // Отрисовка объектов сцены
    const time = Date.now() * 0.000025;
    scene.objects.forEach(obj => {
        gl.useProgram(obj.program);
        
        const modelMatrices = new Float32Array(16 * obj.numberOfInstances);
        const normalMatrices = new Float32Array(16 * obj.numberOfInstances);

        for (let i = 0; i < obj.numberOfInstances; ++i) {
            // Создание и настройка матрицы модели
            const modelMatrix = mat4.create();
            let pos =  vec3.fromValues(...obj.positions[i]);
            if (obj.numberOfInstances > 1 && obj.isCloud) {
                
                const r = 20;
                const r1 = 10;
                //const angle = 2 * i * Math.PI / obj.numberOfInstances;
                const angle = obj.angles[i];
                pos[0] = pos[0] + r * Math.sin((3*time + Math.PI/2) + angle);
                pos[2] = pos[2] + r * Math.sin(2*time + angle);
                //pos[1] = pos[0];
                //x=13(cos(t)−cos(6,5t)/6,5),y=13(sin(t)−sin(6,5t)/6,5) t∈[0;4π]
                //pos[0] += r * (Math.cos(time + angle) - Math.cos(r1 * time + angle)/r1);
                //pos[2] += r * (Math.sin(time + angle) - Math.sin(r1 * time + angle)/r1);
                const shimmer = Math.max(1.0, 10 * Math.sin(100*time + angle));
                obj.material.ambient = vec3.fromValues(shimmer, shimmer, shimmer);
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

        // Передача матриц в шейдер
        const uViewMatrix = gl.getUniformLocation(obj.program, 'uViewMatrix');
        const uProjectionMatrix = gl.getUniformLocation(obj.program, 'uProjectionMatrix');
        const aNormalMatrix = gl.getAttribLocation(obj.program, 'aNormalMatrix');
        const aModelMatrix = gl.getAttribLocation(obj.program, 'aModelMatrix');
  
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