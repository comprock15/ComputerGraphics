#version 300 es

in vec3 aPosition;
in vec3 aNormal;
in vec2 aTexCoord;

uniform mat4 uModelMatrix;
uniform mat4 uViewMatrix;
uniform mat4 uProjectionMatrix;
uniform mat4 uNormalMatrix;

out vec3 vNormal;
out vec3 vFragPos;
out vec2 vTexCoord;

void main() {
    vec4 worldPosition = uModelMatrix * vec4(aPosition, 1.0);
    vFragPos = worldPosition.xyz;
    vNormal = mat3(uNormalMatrix) * aNormal;
    vTexCoord = aTexCoord;
    gl_Position = uProjectionMatrix * uViewMatrix * worldPosition;
}