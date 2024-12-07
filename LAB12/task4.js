function task4() {

let canvas = document.getElementById('canvas4');
let gl = canvas.getContext('webgl2');

if (!gl) {
  alert('Браузер не поддерживает WebGL!');
  throw new Error("WebGL2 not supported");
}

let vsSource = `#version 300 es
  in vec4 aVertexPosition;
  in vec2 aTextureCoord;
  out vec2 vTextureCoord;
  void main() {
    gl_Position = aVertexPosition;
    vTextureCoord = aTextureCoord;
  }
`;

// Fragment shader
let fsSource = `#version 300 es
precision highp float;
uniform vec2 scale;
in vec2 vTextureCoord;
out vec4 fragColor;

vec3 hsv2rgb(vec3 c) {
    float h = c.x; 
    float s = c.y; 
    float v = c.z; 
    float r, g, b;

    h *= 6.0; // Расширяем диапазон оттенка до 0-6
    int i = int(floor(h)); // Целая часть оттенка (0-5)
    float f = fract(h); // Дробная часть оттенка (0-1)
    float p = v * (1.0 - s);
    float q = v * (1.0 - s * f);
    float t = v * (1.0 - s * (1.0 - f));

    if (i == 0)      { r = v; g = t; b = p; }
    else if (i == 1) { r = q; g = v; b = p; }
    else if (i == 2) { r = p; g = v; b = t; }
    else if (i == 3) { r = p; g = q; b = v; }
    else if (i == 4) { r = t; g = p; b = v; }
    else if (i == 5) { r = v; g = p; b = q; }
  
    return vec3(r, g, b);
}

//по сути мы рисует не круг а квадрат но чтобы получился круг мы не рисуем пиксели за кругом. текстырные координаты лежат по углам канваса и о

void main() {
    vec2 uv = (vTextureCoord * 2.0 - 1.0) * scale; 
    // расстояние от пикселя до центра круга = центр canvas = (0,0)
    float dist = length(uv); 
    
    // Если пиксель за границей круга рисуем чёрным
    if (dist > 1.0) { 
        fragColor = vec4(0.0, 0.0, 0.0, 1.0);
    }
    else 
    {
        // тангенс от отношения y/x 
        float hue = atan(uv.y, uv.x) / (3.14 * 2.0) + 0.5; 
        hue = clamp(hue, 0.0, 1.0); 

        fragColor = vec4(hsv2rgb(vec3(hue, dist, 1.0)), 1.0);
    }
}
`;
    function loadShader(gl, type, source) {
        let shader = gl.createShader(type);
        gl.shaderSource(shader, source);
        gl.compileShader(shader);

        if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
            console.error('Произошла ошибка компиляции шейдера: ' + gl.getShaderInfoLog(shader));
            gl.deleteShader(shader);
            throw new Error("Произошла ошибка компиляции шейдера");
        }
        return shader;
    }

    function initShaderProgram(gl, vsSource, fsSource) {
        let vertexShader = loadShader(gl, gl.VERTEX_SHADER, vsSource);
        let fragmentShader = loadShader(gl, gl.FRAGMENT_SHADER, fsSource);

        let shaderProgram = gl.createProgram();

        gl.attachShader(shaderProgram, vertexShader);
        gl.attachShader(shaderProgram, fragmentShader);

        gl.linkProgram(shaderProgram);
        if (!gl.getProgramParameter(shaderProgram, gl.LINK_STATUS)) {
            console.error('Невозможно инициализировать шейдерную программу: ' + gl.getProgramInfoLog(shaderProgram));
            throw new Error("Невозможно инициализировать шейдерную программу");
        }
        return shaderProgram;
    }

    
    let program = initShaderProgram(gl, vsSource, fsSource);

    let attrib_vertexPosition = gl.getAttribLocation(program, 'aVertexPosition');
    let attrib_textureCoord = gl.getAttribLocation(program, 'aTextureCoord');
    let uniform_scale = gl.getUniformLocation(program, 'scale');

    let positions = [
        -1.0,  1.0,
        1.0,  1.0,
        -1.0, -1.0,
        1.0, -1.0,
    ];

    const positionBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(positions), gl.STATIC_DRAW);

    let textureCoords = [
        0.0, 0.0,
        1.0, 0.0,
        0.0, 1.0,
        1.0, 1.0,
    ];

    const textureCoordBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, textureCoordBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);

    let scaleX = 1.0;
    let scaleY = 1.0;
  
    document.getElementById('scaleX-').addEventListener('click', () => { scaleX *= 1.1; drawScene();});
    document.getElementById('scaleX+').addEventListener('click', () => { scaleX /= 1.1; drawScene();});
    document.getElementById('scaleY-').addEventListener('click', () => { scaleY *= 1.1; drawScene();});
    document.getElementById('scaleY+').addEventListener('click', () => { scaleY /= 1.1; drawScene();});
  

    function drawScene() {
        gl.clearColor(0.0, 0.0, 0.0, 1.0);
        gl.clear(gl.COLOR_BUFFER_BIT);

        gl.useProgram(program);

        gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);
        gl.vertexAttribPointer(attrib_vertexPosition, 2, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(attrib_vertexPosition);

        gl.bindBuffer(gl.ARRAY_BUFFER, textureCoordBuffer);
        gl.vertexAttribPointer(attrib_textureCoord, 2, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(attrib_textureCoord);

        gl.uniform2f(uniform_scale, scaleX, scaleY);

        gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);
    }

    drawScene();

}

