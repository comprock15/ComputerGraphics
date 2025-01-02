#version 300 es
precision mediump float;

in vec3 vNormal;
in vec3 vFragPos;
in vec2 vTexCoord;
in vec3 vAmbient;

uniform sampler2D uTexture;
uniform sampler2D uNormalMap;

uniform struct Material {
    vec3 diffuse;
    vec3 specular;
    float shininess;
} uMaterial;

uniform struct AmbientLight {
    vec3 color;
} uAmbientLight;

uniform struct DirectionalLight {
    vec3 direction;
    vec3 color;
    float intensity;
} uDirectionalLight;

out vec4 fragColor;

vec3 calculateDirectionalLight(vec3 normal, vec3 fragPos){
    vec3 lightDirection = normalize(-uDirectionalLight.direction);
    float diffuseStrength = max(dot(normal, lightDirection), 0.0);
    vec3 diffuse = diffuseStrength * uDirectionalLight.color * uMaterial.diffuse;
    
    vec3 viewDirection = normalize(-fragPos);
    vec3 reflectDirection = reflect(-lightDirection, normal);
    float specularStrength = pow(max(dot(viewDirection, reflectDirection), 0.0), uMaterial.shininess);
    vec3 specular = specularStrength * uDirectionalLight.color * uMaterial.specular;

    return (diffuse + specular) * uDirectionalLight.intensity;
}

void main() {
    // Получаем вектор нормалей из карты нормалей
    vec3 normal = vec3(texture(uNormalMap, vTexCoord));
    // Переводим из [0.0, 1.0] в [-1.0, 1.0]
    normal = normal * 2.0 - 1.0;
    // Получаем скалярное произведение нормалей из карты нормалей и из вершинного шейдера
    normal = normalize(normal * vNormal);

    vec3 ambient = uAmbientLight.color * vAmbient;
    vec3 directionalLightResult = calculateDirectionalLight(normal, vFragPos);

    vec4 textureColor = texture(uTexture, vTexCoord);
    vec3 totalLight = ambient + directionalLightResult;
    fragColor = vec4(totalLight * textureColor.rgb , 1.0);
}