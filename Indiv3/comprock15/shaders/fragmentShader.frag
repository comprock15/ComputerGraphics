#version 300 es
precision mediump float;

in vec3 vNormal;
in vec3 vFragPos;
in vec2 vTexCoord;

uniform sampler2D uTexture;
uniform struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
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

// Функция для "уровней" освещения
float toonShading(float intensity) {
    if (intensity > 0.95) return 1.0; // Яркий свет
    if (intensity > 0.5) return 0.7;  // Средний свет
    if (intensity > 0.25) return 0.4; // Тусклый свет
    return 0.15;                      // Тень
}

// Расчёт направленного света
vec3 calculateDirectionalLight(vec3 normal) {
    vec3 lightDirection = normalize(-uDirectionalLight.direction);
    float diffuseStrength = max(dot(normal, lightDirection), 0.0);
    vec3 diffuse = toonShading(diffuseStrength) * uDirectionalLight.color * uMaterial.diffuse;

    return diffuse * uDirectionalLight.intensity;
}


// Основная функция фрагментного шейдера
void main() {
    vec3 normal = normalize(vNormal);
    vec3 ambient = uAmbientLight.color * uMaterial.ambient;

    vec3 directionalLightResult = calculateDirectionalLight(normal);

    vec4 textureColor = texture(uTexture, vTexCoord);
    vec3 totalLight = ambient + directionalLightResult;

    fragColor = vec4(totalLight * textureColor.rgb, 1.0);
}
