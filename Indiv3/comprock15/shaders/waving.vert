#version 300 es

in vec3 aPosition;
in vec3 aNormal;
in vec2 aTexCoord;
in mat4 aModelMatrix;
in mat4 aNormalMatrix;
in vec3 aAmbient;

uniform mat4 uViewMatrix;
uniform mat4 uProjectionMatrix;
uniform float uTime;

out vec3 vNormal;
out vec3 vFragPos;
out vec2 vTexCoord;
out vec3 vAmbient;

void main() {
    // Настройки колыхания
    float waveStrength = 0.05;
    float waveSpeed = 70.0;
    vec4 position = vec4(aPosition, 1.0);
    position.x += waveStrength * sin(position.z * 5.0 + uTime * waveSpeed);
    position.z += waveStrength * cos(position.x * 5.0 + uTime * waveSpeed);

    vec4 worldPosition = aModelMatrix * position;
    vFragPos = worldPosition.xyz;
    vNormal = mat3(aNormalMatrix) * aNormal;
    vTexCoord = aTexCoord;
    gl_Position = uProjectionMatrix * uViewMatrix * worldPosition;
    vAmbient = aAmbient;
}