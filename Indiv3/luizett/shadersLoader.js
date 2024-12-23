async function shadersSetUp(){
    const program = await createProgram(gl, 'shaders/vert.vert', 'shaders/frag.frag');

}

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

async function loadShader(gl, shaderPath, type) {
    const response = await fetch(shaderPath);
    const shaderSource = await response.text();
  
    const shader = gl.createShader(type);
    gl.shaderSource(shader, shaderSource);
    gl.compileShader(shader);
  
    if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
      console.error(`Error compiling shader ${shaderPath}:`, gl.getShaderInfoLog(shader));
      gl.deleteShader(shader);
      return null;
    }
  
    return shader;
}