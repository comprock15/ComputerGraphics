#version 300 es

in vec3 aPosition;
in vec3 aNormal;
in vec2 aTexCoord;
in mat4 aModelMatrix;
in mat4 aNormalMatrix;
in vec3 aAmbient;

uniform mat4 uViewMatrix;
uniform mat4 uProjectionMatrix;

out vec3 vNormal;
out vec3 vFragPos;
out vec2 vTexCoord;
out vec3 vAmbient;

void main() {
    vec4 worldPosition = aModelMatrix * vec4(aPosition, 1.0);
    vFragPos = worldPosition.xyz;
    vNormal = mat3(aNormalMatrix) * aNormal;
    vTexCoord = aTexCoord;
    gl_Position = uProjectionMatrix * uViewMatrix * worldPosition;
    vAmbient = aAmbient;
}