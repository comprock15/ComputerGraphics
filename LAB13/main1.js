
const program = initShaderProgram(gl, vsSource, fsSource);
const attrib_vertexPosition = gl.getAttribLocation(program, 'aVertexPosition');
const attrib_vertexNormal = gl.getAttribLocation(program, 'aVertexNormal');
const attrib_textureCoord = gl.getAttribLocation(program, 'aTextureCoord');


const uniform_projectionMatrix = gl.getUniformLocation(program, 'uProjectionMatrix');
const uniform_modelViewMatrix=  gl.getUniformLocation(program, 'uModelViewMatrix');
const uniform_lightDirection = gl.getUniformLocation(program, 'uLightDirection');
const uniform_uSampler = gl.getUniformLocation(program, 'uSampler');

document.addEventListener('DOMContentLoaded', main);
function main()
{

    let frame = 0;
    function render() {
        if(objectsData[0] && objectsData[1] && textures[0] && textures[1]){
          draw(gl, frame);
        }
        requestAnimationFrame(render);
        frame += 0.01;
    }
    render();

}


function draw(gl, frame)
{
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.enable(gl.DEPTH_TEST);
    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

    gl.useProgram(program);

    const projectionMatrix = getCameraProjectionMatrix();
    const viewMatrix = getCameraViewMatrix();

    drawObj(gl, viewMatrix, projectionMatrix, frame, 0, [0, 10, 0], [0.1, 0.1, 0.1], 0.5 );
    drawObj(gl, viewMatrix, projectionMatrix, frame, 1, [0, 0, 0], [0.0, 1.0, 0.0], 1.3);

};

function drawObj(gl, camViewMatr, camProjMatr, frame, objIndex, position, rotation, scale)
{
    let objViewMatrix = mat4.clone(camViewMatr);
    mat4.translate(objViewMatrix, objViewMatrix, position);
    mat4.rotateX(objViewMatrix, objViewMatrix, frame * rotation[0]);
    mat4.rotateY(objViewMatrix, objViewMatrix, frame * rotation[1]);
    mat4.rotateZ(objViewMatrix, objViewMatrix, frame * rotation[2]);
    mat4.scale(objViewMatrix, objViewMatrix, [scale, scale, scale]);

    gl.uniformMatrix4fv(uniform_projectionMatrix, false, camProjMatr);
    gl.uniformMatrix4fv(uniform_modelViewMatrix, false, objViewMatrix);

    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].positionBuffer);
    gl.vertexAttribPointer(attrib_vertexPosition, 3, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(attrib_vertexPosition);
 
    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].normalBuffer);
    gl.vertexAttribPointer(attrib_vertexNormal, 3, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(attrib_vertexNormal);
 
    gl.bindBuffer(gl.ARRAY_BUFFER, objectsData[objIndex].textureCoordBuffer);
    gl.vertexAttribPointer(attrib_textureCoord, 2, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(attrib_textureCoord);

    gl.activeTexture(texture_active[objIndex]);
    gl.bindTexture(gl.TEXTURE_2D, textures[objIndex]);
    gl.uniform1i(uniform_uSampler, objIndex);

    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, objectsData[objIndex].indexBuffer);
    gl.drawElements(gl.TRIANGLES, objectsData[objIndex].indices.length, gl.UNSIGNED_INT, 0);
};
 
//  function drawScene(gl, programInfo, objData, texture, frame) {
//      gl.useProgram(programInfo.program);
 
//        const projectionMatrix = mat4.create();
//        mat4.perspective(projectionMatrix,
//                           45 * Math.PI / 180, // fovy
//                          gl.canvas.clientWidth / gl.canvas.clientHeight, // aspect
//                          0.1, // zNear
//                          1000.0 // zFar
//                      );
 
//          const modelViewMatrix = mat4.create();
//          mat4.translate(modelViewMatrix, modelViewMatrix, [0.0, 0.0, -300]);
//          mat4.rotateY(modelViewMatrix, modelViewMatrix, frame * 0.5)
//           mat4.rotateX(modelViewMatrix, modelViewMatrix, frame * 0.2)
 
//          gl.uniformMatrix4fv(programInfo.uniformLocations.projectionMatrix, false, projectionMatrix);
//          gl.uniformMatrix4fv(programInfo.uniformLocations.modelViewMatrix, false, modelViewMatrix);
 
//         // const lightDirection = vec3.create();
//         //  vec3.normalize(lightDirection, [0.5,0.5,1]);
//         //  gl.uniform3fv(programInfo.uniformLocations.lightDirection, lightDirection);
 
//          gl.bindBuffer(gl.ARRAY_BUFFER, objData.positionBuffer);
//          gl.vertexAttribPointer(programInfo.attribLocations.vertexPosition, 3, gl.FLOAT, false, 0, 0);
//          gl.enableVertexAttribArray(programInfo.attribLocations.vertexPosition);
 
//          gl.bindBuffer(gl.ARRAY_BUFFER, objData.normalBuffer);
//          gl.vertexAttribPointer(programInfo.attribLocations.vertexNormal, 3, gl.FLOAT, false, 0, 0);
//          gl.enableVertexAttribArray(programInfo.attribLocations.vertexNormal);
 
//        gl.bindBuffer(gl.ARRAY_BUFFER, objData.textureCoordBuffer);
//          gl.vertexAttribPointer(programInfo.attribLocations.textureCoord, 2, gl.FLOAT, false, 0, 0);
//          gl.enableVertexAttribArray(programInfo.attribLocations.textureCoord);
 
//          gl.activeTexture(gl.TEXTURE0);
//          gl.bindTexture(gl.TEXTURE_2D, texture);
//          gl.uniform1i(programInfo.uniformLocations.uSampler, 0);
 
 
 
//          gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, objData.indexBuffer);
//          gl.drawElements(gl.TRIANGLES, objData.indices.length, gl.UNSIGNED_INT, 0);
//          gl.enable(gl.DEPTH_TEST)
//          gl.enable(gl.CULL_FACE);
 
//      }
