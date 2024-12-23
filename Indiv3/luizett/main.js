async function main(){

    shadersSetUp();
    gl.useProgram(program);

    // === Получение locations uniform'ов ===
    const uModelViewMatrixLocation = gl.getUniformLocation(shaderProgram, 'uModelViewMatrix');
    const uProjectionMatrixLocation = gl.getUniformLocation(shaderProgram, 'uProjectionMatrix');
    const uHeightMapLocation = gl.getUniformLocation(shaderProgram, 'uHeightMap');
    const uSnowTextureLocation = gl.getUniformLocation(shaderProgram, 'uSnowTexture');

    
    let time = 0;
    function render() {
        time += 0.01;

        gl.clearColor(0.6, 0.8, 1.0, 1.0);
        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
        gl.enable(gl.DEPTH_TEST);

        gl.uniformMatrix4fv(uModelViewMatrixLocation, false, modelViewMatrix);
        gl.uniformMatrix4fv(uProjectionMatrixLocation, false, projectionMatrix);
        gl.uniform1f(uTimeLocation, time);

         gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
         gl.drawElements(gl.TRIANGLES, indices.length, gl.UNSIGNED_INT, 0);

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