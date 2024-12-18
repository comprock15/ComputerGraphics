const canvas = document.getElementById('glcanvas');
const gl = canvas.getContext('webgl2');

// Проверка поддержки WebGL2
if (!gl) {
  alert('Ваш браузер не поддерживает WebGL2!');
  throw new Error('WebGL2 not supported');
}

// Вершинный шейдер
const vsSource = `#version 300 es
in vec4 aVertexPosition;
in vec3 aVertexNormal;
in vec2 aTextureCoord;
in mat4 aModelViewMatrix;

uniform mat4 uProjectionMatrix;

out vec3 vNormal;
out vec2 vTextureCoord;

void main() {
  gl_Position = uProjectionMatrix * aModelViewMatrix * aVertexPosition;
  vNormal = aVertexNormal;
  vTextureCoord = aTextureCoord;
}
`;

// Фрагментный шейдер
const fsSource = `#version 300 es
precision mediump float;

in vec3 vNormal;
in vec2 vTextureCoord;

uniform vec3 uLightDirection;
uniform sampler2D uSampler;

out vec4 fragColor;

void main() {
  vec3 normal = normalize(vNormal);
  vec4 texelColor = texture(uSampler, vTextureCoord);
  fragColor = vec4(texelColor.rgb, texelColor.a);
}
`;

// Создание и компиляция шейдера
function getShader(gl, shaderType, shaderSource) {
  let shader = gl.createShader(shaderType);
  gl.shaderSource(shader, shaderSource);
  gl.compileShader(shader);

  if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
    alert("Ошибка компиляции шейдера: " + gl.getShaderInfoLog(shader));
    gl.deleteShader(shader);
    return null;
  }
  return shader;
}

// Инициализация программы шейдеров
function initShaderProgram(gl, vertexShaderSource, fragmentShaderSource) {
  let vertexShader = getShader(gl, gl.VERTEX_SHADER, vertexShaderSource);
  let fragmentShader = getShader(gl, gl.FRAGMENT_SHADER, fragmentShaderSource);

  let program = gl.createProgram();
  gl.attachShader(program, vertexShader);
  gl.attachShader(program, fragmentShader);
  gl.linkProgram(program);

  if (!gl.getProgramParameter(program, gl.LINK_STATUS)) {
    console.error("Не удалсь установить шейдеры");
    return null;
  }

  return program;
}

console.debug('shaders.js compile');
