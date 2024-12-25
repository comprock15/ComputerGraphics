async function main(){

    const program = await shadersSetUp();
    gl.useProgram(program);
    await loadSceneTextures();
    await loadSceneObjects();

    // Источники света
    const lights = {
        ambientLight: {
        color: [0.5, 0.5, 0.5]
        },

        directionalLight: {
        direction: vec3.fromValues(0, -20, -1),
        color: vec3.fromValues(1, 1, 1),
        intensity: 1.0
        },

        spotLight: {
            position: vec3.fromValues(0, 20, 0),
            direction: vec3.fromValues(0, -20, -1),
            color: vec3.fromValues(1, 1, 1),
            intensity: 0.5,
            cutoff: Math.cos(Math.PI / 12)
        } 
    };

    const sceneObjects = [
        // Дерево
        {
          model: models.tree,
          texture: textures.tree,
          position: vec3.fromValues(0, 15, 0),
          rotation: vec3.fromValues(0, 0, 0),
          scale: vec3.fromValues(3.0, 3.0, 3.0),
          material: {
            ambient: [0.2, 0.2, 0.2],
            diffuse: [0.8, 0.8, 0.8],
            specular: [0.5, 0.5, 0.5],
            shininess: 32.0,
            //roughness: 0.3,
          }
        },
        // Дирижабль
        {
          model: models.airship,
          texture: textures.airship,
          position: vec3.fromValues(0, 40, 0),
          rotation: vec3.fromValues(0, 3.2, 0),
          scale: vec3.fromValues(0.01, 0.01, 0.01),
          material: {
            ambient: [0.4, 0.4, 0.4],
            diffuse: [0.8, 0.8, 0.8],
            specular: [0.5, 0.5, 0.5],
            shininess: 32.0,
          }
        },
        // Облако
        {
          model: models.cloud,
          texture: textures.cloud,
          position: vec3.fromValues(0, 40, 0),
          rotation: vec3.fromValues(0, 0, 0),
          scale: vec3.fromValues(1.0, 1.0, 1.0),
          material: {
            ambient: [1.0, 1.0, 1.0],
            diffuse: [0.8, 0.8, 0.8],
            specular: [1.5, 1.5, 1.5],
            shininess: 50.0,
          }
        },
        // Воздушный шар
        {
          model: models.balloon,
          texture: textures.balloon,
          position: vec3.fromValues(20, 40, 0),
          rotation: vec3.fromValues(0, 0, 0),
          scale: vec3.fromValues(0.2, 0.2, 0.2),
          material: {
            ambient: [0.5, 0.5, 0.5],
            diffuse: [0.8, 0.8, 0.8],
            specular: [0.5, 0.5, 0.5],
            shininess: 32.0,
          }
        },
        // terrain
        {
          model: models.terrain,
          texture: textures.terrain,
          position: vec3.fromValues(0, 0, 0),
          rotation: vec3.fromValues(0, 0, 0),
          scale: vec3.fromValues(120.0, 120.0, 120.0),
          material: {
            ambient: [0.2, 0.2, 0.2],
            diffuse: [0.8, 0.8, 0.8],
            specular: [0.5, 0.5, 0.5],
            shininess: 32.0,
          }
        },
    ];
    //sceneSetUp();

    document.addEventListener('keydown', (event) => {
        switch (event.key) {
          case 'a': 
            camera.position[0] += camera.move_speed; 
            lights.spotLight.position[0] -= camera.move_speed;
            sceneObjects.at(1).position[0] -= camera.move_speed;
            break; // Вправо по X
          case 'd': 
            camera.position[0] -= camera.move_speed; 
            lights.spotLight.position[0] += camera.move_speed;
            sceneObjects.at(1).position[0] += camera.move_speed;
            break; // Влево по X
          case 'w': 
            camera.position[1] -= camera.move_speed; 
            lights.spotLight.position[1] -= camera.move_speed;
            sceneObjects.at(1).position[1] += camera.move_speed;
            break; // Вниз по Ya
          case 's': 
            camera.position[1] += camera.move_speed; 
            lights.spotLight.position[1] += camera.move_speed;
            sceneObjects.at(1).position[1] -= camera.move_speed;
            break; // Вверх по Y
          case 'q': 
            camera.position[2] += camera.move_speed; 
            lights.spotLight.position[2] -= camera.move_speed;
            sceneObjects.at(1).position[2] -= camera.move_speed;
            break; // Вверх по Z
          case 'e': 
            camera.position[2] -= camera.move_speed; 
            lights.spotLight.position[2] += camera.move_speed;
            sceneObjects.at(1).position[2] += camera.move_speed;
            break; // Вниз по Z
    
          case 'ArrowUp': 
            camera.rotation[0] += camera.rot_speed;
            //lights.spotLight.rotation[0] += camera.rot_speed; 
            break;
          case 'ArrowDown': 
            camera.rotation[0] -= camera.rot_speed; 
            //lights.spotLight.rotation[0] -= camera.rot_speed; 
            break;
          case 'ArrowLeft': 
            camera.rotation[1] += camera.rot_speed; 
           // lights.spotLight.rotation[1] += camera.rot_speed; 
            break;
          case 'ArrowRight': 
            camera.rotation[1] -= camera.rot_speed; 
            //qlights.spotLight.rotation[1] -= camera.rot_speed; 
            break;
  
          case 't':
            if (lights.spotLight.intensity > 0.1)
              lights.spotLight.intensity = 0.0;
            else
              lights.spotLight.intensity = 1.0;
        }});

    // === Получение locations uniform'ов ===

    const uModelMatrixLocation = gl.getUniformLocation(program, 'uModelMatrix');
    const uViewMatrixLocation = gl.getUniformLocation(program, 'uViewMatrix');
    const uProjectionMatrixLocation = gl.getUniformLocation(program, 'uProjectionMatrix');
    const uNormalMatrixLocation = gl.getUniformLocation(program, 'uNormalMatrix');

    const uTextureLocation = gl.getUniformLocation(program, 'uTexture');
   // const uMaterialLocation =  gl.getUniformLocation(program, 'uMaterial');

    const uIsTerrainLocation = gl.getUniformLocation(program, 'uIsTerrain');
    const uIsAirshipLocation = gl.getUniformLocation(program, 'uIsAirShip');

    const uHeightMapLocation = gl.getUniformLocation(program, 'uHeightMap');
    const uNormalMapLocation = gl.getUniformLocation(program, 'uNormalMap');

    const positionAttribute = gl.getAttribLocation(program, 'aPosition');
    const normalAttribute = gl.getAttribLocation(program, 'aNormal');
    const uvAttribute = gl.getAttribLocation(program, 'aTexCoord');

    const uMaterialAmbientLocation = gl.getUniformLocation(program, 'uMaterial.ambient');
    const uMaterialDiffuseLocation = gl.getUniformLocation(program, 'uMaterial.diffuse');
    const uMaterialSpecularLocation = gl.getUniformLocation(program, 'uMaterial.specular');
    const uMaterialShininessLocation = gl.getUniformLocation(program, 'uMaterial.shininess');


    const uAmbientLightColor = gl.getUniformLocation(program, 'uAmbientLight.color');
    gl.uniform3fv(uAmbientLightColor, lights.ambientLight.color);

    const uDirectionalLightDirectionLocation = gl.getUniformLocation(program, 'uDirectionalLight.direction');
    const uDirectionalLightColorLocation = gl.getUniformLocation(program, 'uDirectionalLight.color');
    const uDirectionalLightIntensityLocation = gl.getUniformLocation(program, 'uDirectionalLight.intensity');
    gl.uniform3fv(uDirectionalLightDirectionLocation, lights.directionalLight.direction);
    gl.uniform3fv(uDirectionalLightColorLocation,     lights.directionalLight.color);
    gl.uniform1f(uDirectionalLightIntensityLocation,  lights.directionalLight.intensity);

    const uSpotLightPositionLocation = gl.getUniformLocation(program, 'uSpotLight.position');
    const uSpotLightDirectionLocation = gl.getUniformLocation(program, 'uSpotLight.direction');
    const uSpotLightColorLocation = gl.getUniformLocation(program, 'uSpotLight.color');
    const uSpotLightIntensityLocation = gl.getUniformLocation(program, 'uSpotLight.intensity');
    const uSpotLightCutOffLocation = gl.getUniformLocation(program, 'uSpotLight.cutoff');

    let viewMatrix = mat4.create();
    let projectionMatrix = mat4.create();

    let max = 50;
    let min = -50;
    let cloudsPositions = [];
    let cloudsRotations = [];
    for (let i = 0; i < 5; i++) {
      cloudsPositions.push([Math.floor(Math.random() * (max - min + 1)) + min, Math.floor(Math.random() * (40 - 30 + 1)) + 30, Math.floor(Math.random() * (max - min + 1)) + min]);
      cloudsRotations.push(Math.random());
    }

    let balloonsPositions = [];
    for (let i = 0; i < 2; i++) {
      balloonsPositions.push([Math.floor(Math.random() * (max - min + 1)) + min, Math.floor(Math.random() * (40 - 30 + 1)) + 30, Math.floor(Math.random() * (max - min + 1)) + min]);
    }

    let camMode = false;
    document.addEventListener('keydown', (event) => {
      if (event.key == 'n') { 
        if (camMode){
          camera.rotation =vec3.fromValues(0, 0, 0);
          camera.position[2] -= 20;
          camera.position[1] += -25;
          camMode = false;
        }
        else {
          camMode = true;
          camera.rotation = vec3.fromValues(20, 0, 0);
          camera.position[2] += 20;
          camera.position[1] -= -25;
        }
      }
    });
    
    let time = 0;
    let cloudsTimestamp = [0, 0, 0, 0, 0];
    function render() {
        time += 0.01;

        gl.clearColor(0.1, 0.1, 0.1, 1.0);
        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
        gl.enable(gl.DEPTH_TEST);
        
        projectionMatrix = getCameraProjectionMatrix();
        viewMatrix = getCameraViewMatrix();

        gl.uniformMatrix4fv(uViewMatrixLocation, false, viewMatrix);
        gl.uniformMatrix4fv(uProjectionMatrixLocation, false, projectionMatrix);

        gl.uniform1i(uHeightMapLocation, 5);
        gl.uniform1i(uNormalMapLocation, 6);

        gl.uniform3fv(uSpotLightPositionLocation, lights.spotLight.position);
        gl.uniform3fv(uSpotLightDirectionLocation, lights.spotLight.direction);
        gl.uniform3fv(uSpotLightColorLocation, lights.spotLight.color);
        gl.uniform1f(uSpotLightIntensityLocation, lights.spotLight.intensity);
        gl.uniform1f(uSpotLightCutOffLocation, lights.spotLight.cutoff);

        for (let index = 0; index < sceneObjects.length; index++) {
            const obj = sceneObjects[index];

            gl.uniform3fv(uMaterialAmbientLocation, obj.material.ambient);
            gl.uniform3fv(uMaterialDiffuseLocation, obj.material.diffuse);
            gl.uniform3fv(uMaterialSpecularLocation, obj.material.specular);
            gl.uniform1f( uMaterialShininessLocation, obj.material.shininess);

            gl.uniform1i(uTextureLocation, index);

            gl.uniform1i(uIsTerrainLocation, index == 4);
            gl.uniform1i(uIsAirshipLocation, index == 1);

            //const modelMatrix = getObjViewMatrix(viewMatrix, obj.position, obj.rotation, obj.scale);
            
            // воздушные шары
            if (index == 3)
            {
              for (let i = 0; i < 2; i++) {
                const modelMatrix = mat4.create();
                mat4.translate(modelMatrix, modelMatrix, balloonsPositions[i]);
                mat4.rotateX(modelMatrix, modelMatrix, obj.rotation[0]);
                mat4.rotateY(modelMatrix, modelMatrix, obj.rotation[1]);
                mat4.rotateZ(modelMatrix, modelMatrix, obj.rotation[2]);
                mat4.scale(modelMatrix, modelMatrix, obj.scale);
                gl.uniformMatrix4fv(uModelMatrixLocation, false, modelMatrix);

                const normalMatrix = mat4.create();
                mat4.invert(normalMatrix, modelMatrix);
                mat4.transpose(normalMatrix, normalMatrix);
                gl.uniformMatrix4fv(uNormalMatrixLocation, false, normalMatrix);

                

                gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.vertexBuffer);
                gl.vertexAttribPointer(positionAttribute, 3, gl.FLOAT, false, 0, 0);
                gl.enableVertexAttribArray(positionAttribute);
        
                gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.normalBuffer);
                gl.vertexAttribPointer(normalAttribute, 3, gl.FLOAT, false, 0, 0);
                gl.enableVertexAttribArray(normalAttribute);
        
                gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.uvBuffer);
                gl.vertexAttribPointer(uvAttribute, 2, gl.FLOAT, false, 0, 0);
                gl.enableVertexAttribArray(uvAttribute);
        
                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, obj.model.indexBuffer);
                gl.drawElements(gl.TRIANGLES, obj.model.indices.length, gl.UNSIGNED_SHORT, 0);
                
              }

            }
            //облака
            else if (index == 2)
            {
              for (let i = 0; i < 5; i++) {
                const modelMatrix = mat4.create();

                let pos = [3*Math.sin(time*(i*0.1+0.2)) + cloudsPositions[i][0], Math.sin(3*time*(i*0.1+0.2))+ cloudsPositions[i][1], 3*Math.cos(time*(i*0.1+0.2)) + cloudsPositions[i][2]];
                let rot = obj.rotation[1] + cloudsRotations[i];
                mat4.translate(modelMatrix, modelMatrix, pos);
                mat4.rotateX(modelMatrix, modelMatrix, obj.rotation[0]);
                mat4.rotateY(modelMatrix, modelMatrix, rot);
                mat4.rotateZ(modelMatrix, modelMatrix, obj.rotation[2]);
                mat4.scale(modelMatrix, modelMatrix, obj.scale);
                gl.uniformMatrix4fv(uModelMatrixLocation, false, modelMatrix);

                const normalMatrix = mat4.create();
                mat4.invert(normalMatrix, modelMatrix);
                mat4.transpose(normalMatrix, normalMatrix);
                gl.uniformMatrix4fv(uNormalMatrixLocation, false, normalMatrix);

                //if ( (time * 10) % (i+1) <= 0.05 )
                //if (time - cloudsTimestamp[i] > 0.5*(i*5)+1 ) {
                if (Math.abs(Math.sin(time + i*0.1)) < 0.05 ) {
                  gl.uniform1f( uMaterialShininessLocation, 0.1);
                  cloudsTimestamp[i] = time;
                }
                  

                gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.vertexBuffer);
                gl.vertexAttribPointer(positionAttribute, 3, gl.FLOAT, false, 0, 0);
                gl.enableVertexAttribArray(positionAttribute);
        
                gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.normalBuffer);
                gl.vertexAttribPointer(normalAttribute, 3, gl.FLOAT, false, 0, 0);
                gl.enableVertexAttribArray(normalAttribute);
        
                gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.uvBuffer);
                gl.vertexAttribPointer(uvAttribute, 2, gl.FLOAT, false, 0, 0);
                gl.enableVertexAttribArray(uvAttribute);
        
                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, obj.model.indexBuffer);
                gl.drawElements(gl.TRIANGLES, obj.model.indices.length, gl.UNSIGNED_SHORT, 0);
                
              }
            }
            else
            {

              const modelMatrix = mat4.create();
              mat4.translate(modelMatrix, modelMatrix, obj.position);
              mat4.rotateX(modelMatrix, modelMatrix, obj.rotation[0]);
              mat4.rotateY(modelMatrix, modelMatrix, obj.rotation[1]);
              mat4.rotateZ(modelMatrix, modelMatrix, obj.rotation[2]);
              mat4.scale(modelMatrix, modelMatrix, obj.scale);
              gl.uniformMatrix4fv(uModelMatrixLocation, false, modelMatrix);

              const normalMatrix = mat4.create();
              mat4.invert(normalMatrix, modelMatrix);
              mat4.transpose(normalMatrix, normalMatrix);
              gl.uniformMatrix4fv(uNormalMatrixLocation, false, normalMatrix);

              

              gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.vertexBuffer);
              gl.vertexAttribPointer(positionAttribute, 3, gl.FLOAT, false, 0, 0);
              gl.enableVertexAttribArray(positionAttribute);
      
              gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.normalBuffer);
              gl.vertexAttribPointer(normalAttribute, 3, gl.FLOAT, false, 0, 0);
              gl.enableVertexAttribArray(normalAttribute);
      
              gl.bindBuffer(gl.ARRAY_BUFFER, obj.model.uvBuffer);
              gl.vertexAttribPointer(uvAttribute, 2, gl.FLOAT, false, 0, 0);
              gl.enableVertexAttribArray(uvAttribute);
      
              gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, obj.model.indexBuffer);
              gl.drawElements(gl.TRIANGLES, obj.model.indices.length, gl.UNSIGNED_SHORT, 0);

            }
            
            

        }


        //  gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
        //  gl.drawElements(gl.TRIANGLES, indices.length, gl.UNSIGNED_INT, 0);

         requestAnimationFrame(render);
    }
    
    function resizeCanvas(){
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
        gl.viewport(0, 0, canvas.width, canvas.height);
    }
    
    window.addEventListener('resize', resizeCanvas);
    resizeCanvas();
    render();

};

main();