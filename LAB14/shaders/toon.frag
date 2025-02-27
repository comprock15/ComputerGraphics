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

uniform struct PointLight {
    vec3 position;
    vec3 color;
    float intensity;
} uPointLight;

uniform struct DirectionalLight {
    vec3 direction;
    vec3 color;
    float intensity;
} uDirectionalLight;

uniform struct SpotLight {
    vec3 position;
    vec3 direction;
    vec3 color;
    float intensity;
    float coneAngle;
    float cutoffAngle;
} uSpotLight;

out vec4 fragColor;

// Функция для "уровней" освещения
float toonShading(float intensity) {
    if (intensity > 0.95) return 1.0; // Яркий свет
    if (intensity > 0.5) return 0.7;  // Средний свет
    if (intensity > 0.25) return 0.4; // Тусклый свет
    return 0.15;                      // Тень
}

// Расчёт точечного света
vec3 calculatePointLight(vec3 normal, vec3 fragPos) {
    vec3 lightDirection = normalize(uPointLight.position - fragPos);
    float diffuseStrength = max(dot(normal, lightDirection), 0.0);
    vec3 diffuse = toonShading(diffuseStrength) * uPointLight.color * uMaterial.diffuse;

    float distance = length(uPointLight.position - fragPos);
    float attenuation = 1.0 / (1.0 + 0.09 * distance + 0.032 * distance * distance);

    return attenuation * diffuse * uPointLight.intensity;
}

// Расчёт направленного света
vec3 calculateDirectionalLight(vec3 normal) {
    vec3 lightDirection = normalize(-uDirectionalLight.direction);
    float diffuseStrength = max(dot(normal, lightDirection), 0.0);
    vec3 diffuse = toonShading(diffuseStrength) * uDirectionalLight.color * uMaterial.diffuse;

    return diffuse * uDirectionalLight.intensity;
}

// Расчёт прожекторного света
vec3 calculateSpotLight(vec3 normal, vec3 fragPos) {
    vec3 lightDirection = normalize(uSpotLight.position - fragPos);
    float distance = length(uSpotLight.position - fragPos);
    float theta = dot(lightDirection, normalize(-uSpotLight.direction));
    float epsilon = uSpotLight.cutoffAngle - uSpotLight.coneAngle;

    if (theta > cos(uSpotLight.cutoffAngle)) {
        float diffuseStrength = max(dot(normal, lightDirection), 0.0);
        vec3 diffuse = toonShading(diffuseStrength) * uSpotLight.color * uMaterial.diffuse;

        float attenuation = 1.0 / (0.01 + 0.01 * distance + 0.01 * distance * distance);
        float intensity = (theta - cos(uSpotLight.cutoffAngle)) / epsilon;

        return attenuation * diffuse * uSpotLight.intensity * intensity;
    }
    return vec3(0.0);
}

// Основная функция фрагментного шейдера
void main() {
    vec3 normal = normalize(vNormal);
    vec3 ambient = uAmbientLight.color * uMaterial.ambient;

    vec3 pointLightResult = calculatePointLight(normal, vFragPos);
    vec3 directionalLightResult = calculateDirectionalLight(normal);
    vec3 spotLightResult = calculateSpotLight(normal, vFragPos);

    vec4 textureColor = texture(uTexture, vTexCoord);
    vec3 totalLight = ambient + pointLightResult + directionalLightResult + spotLightResult;

    fragColor = vec4(totalLight * textureColor.rgb, 1.0);
}
