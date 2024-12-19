#version 300 es
precision highp float;

in vec3 vFragPos;
in vec3 vNormal;
in vec2 vTexCoord;

out vec4 fragColor;

// Материалы
struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float roughness; // Шероховатость (от 0.0 до 1.0)
};

uniform Material uMaterial;

// Точечный свет
struct PointLight {
    vec3 position;
    vec3 color;
    float intensity;
};

uniform PointLight uPointLight;

// Глобальный свет
struct AmbientLight {
    vec3 color;
    float intensity;
};

uniform AmbientLight uAmbientLight;

// Текстура
uniform sampler2D uTexture;

// Орен-Наяр
vec3 orenNayar(vec3 normal, vec3 lightDir, vec3 viewDir, vec3 diffuseColor, float roughness) {
    float NdotL = max(dot(normal, lightDir), 0.0);
    float NdotV = max(dot(normal, viewDir), 0.0);
    
    if (NdotL <= 0.0 || NdotV <= 0.0) return vec3(0.0);

    float roughnessSq = roughness * roughness;

    // Угловая зависимость
    float cosTheta = NdotL;
    float cosPhi = max(0.0, dot(viewDir - normal * NdotV, lightDir - normal * NdotL));
    
    float A = 1.0 - 0.5 * (roughnessSq / (roughnessSq + 0.33));
    float B = 0.45 * (roughnessSq / (roughnessSq + 0.09));

    float theta = acos(NdotL);
    float alpha = max(NdotL, NdotV);
    float beta = min(NdotL, NdotV);

    float diffFactor = A + B * cosPhi * sin(alpha) * tan(beta);

    return diffuseColor * NdotL * diffFactor;
}

// Расчёт точечного света
vec3 calculatePointLight(vec3 normal, vec3 fragPos, vec3 viewDir) {
    vec3 lightDir = normalize(uPointLight.position - fragPos);
    vec3 diffuse = orenNayar(normal, lightDir, viewDir, uMaterial.diffuse, uMaterial.roughness);

    float distance = length(uPointLight.position - fragPos);
    float attenuation = 1.0 / (1.0 + 0.09 * distance + 0.032 * distance * distance);

    return diffuse * uPointLight.color * uPointLight.intensity * attenuation;
}

void main() {
    vec3 normal = normalize(vNormal);
    vec3 viewDir = normalize(-vFragPos);
    vec3 ambient = uAmbientLight.color * uMaterial.ambient * uAmbientLight.intensity;

    vec3 pointLightResult = calculatePointLight(normal, vFragPos, viewDir);
    vec4 textureColor = texture(uTexture, vTexCoord);

    vec3 totalLight = ambient + pointLightResult;

    fragColor = vec4(totalLight * textureColor.rgb, 1.0);
}
