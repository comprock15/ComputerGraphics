#version 300 es

in vec3 aPosition;
in vec3 aNormal;
in vec2 aTexCoord;

uniform mat4 uModelMatrix;
uniform mat4 uViewMatrix;
uniform mat4 uProjectionMatrix;
uniform mat4 uNormalMatrix;

uniform sampler2D uHeightMap;
uniform bool uIsTerrain;

out vec3 vNormal;
out vec3 vFragPos;
out vec2 vTexCoord;

void main() {
    vec4 pos = vec4(aPosition, 1.0);
    
    if (uIsTerrain)
    {
        float height = texture(uHeightMap, aTexCoord).r * 10.0;
        pos = vec4(aPosition.x, height, aPosition.z, 1.0);
    }
    vec4 worldPosition = uModelMatrix * pos;
    vFragPos = worldPosition.xyz;
    vNormal = mat3(uNormalMatrix) * aNormal;
    vTexCoord = aTexCoord;
    gl_Position = uProjectionMatrix * uViewMatrix * worldPosition;
}
//             in vec3 aVertexPosition;
//             in vec2 aTextureCoord;
//             in vec3 aVertexNormal;
            
//             uniform mat4 uModelViewMatrix;
//             uniform mat4 uProjectionMatrix;
//             uniform mat4 uNormalMatrix;
//             uniform float uTime;
//             uniform sampler2D uHeightMap;
//             uniform bool uIsTerrain;
            
//             out vec2 vTextureCoord;
//             out float vHeight;
//             out vec3 vNormal;
            
            
//             void main() {
//                 float height = 0.0;
//                 vec4 pos;

//                  if (uIsTerrain){
//                        height = texture(uHeightMap, aTextureCoord).r * 10.0;
//                        vHeight = height;
//                        pos = vec4(aVertexPosition.x, height, aVertexPosition.z, 1.0);
//                    }else{
//                       pos = vec4(aVertexPosition, 1.0);
//                     }
                
                
//                 gl_Position = uProjectionMatrix * uModelViewMatrix * pos;
                
//                 vTextureCoord = aTextureCoord;
//                 vNormal = mat3(uNormalMatrix) * aVertexNormal;
//              }

// #version 300 es

// #version 300 es
// in vec3 aVertexPosition;
// in vec2 aTextureCoord;
// in int aObjType;

// uniform mat4 uModelViewMatrix;
// uniform mat4 uProjectionMatrix;
// uniform float uTime;
// uniform sampler2D uHeightMap;
            
// out vec2 vTextureCoord;
// out float vHeight;
// flat out int vObjType;
            
// void main() {
//     float height = texture(uHeightMap, aTextureCoord).r * 10.0;
//     vHeight = height;

//     vec4 pos = vec4(aVertexPosition.x, height, aVertexPosition.z, 1.0);
                    
//     gl_Position = uProjectionMatrix * uModelViewMatrix * pos;

//     vTextureCoord = aTextureCoord;
//     vObjType = aObjType;
// }