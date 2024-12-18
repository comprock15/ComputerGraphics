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
    float roughness;
    vec3 F0;
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

float distributionGGX(vec3 N, vec3 H, float roughness) {
    float a = roughness * roughness;
    float a2 = a * a;
    float NdotH = max(dot(N, H), 0.0);
    float NdotH2 = NdotH * NdotH;

    float nom = a2;
    float denom = (NdotH2 * (a2 - 1.0) + 1.0);
    denom =  3.14159 * denom * denom;
    return nom / denom;
}

float geometrySchlickGGX(float NdotV, float roughness) {
    float r = roughness + 1.0;
    float k = (r * r) / 8.0;

    float nom = NdotV;
    float denom = NdotV * (1.0 - k) + k;

    return nom / denom;
}
float geometrySmith(vec3 N, vec3 V, vec3 L, float roughness) {
    float NdotV = max(dot(N, V), 0.0);
    float NdotL = max(dot(N, L), 0.0);
    float ggx2 = geometrySchlickGGX(NdotV, roughness);
    float ggx1 = geometrySchlickGGX(NdotL, roughness);

    return ggx1 * ggx2;
}

vec3 fresnelSchlick(float cosTheta, vec3 F0) {
    return F0 + (1.0 - F0) * pow(1.0 - cosTheta, 5.0);
}

vec3 calculatePointLight(vec3 normal, vec3 fragPos, vec3 viewDir) {
    vec3 lightDir = normalize(uPointLight.position - fragPos);
    vec3 H = normalize(lightDir + viewDir);
   
    float NdotL = max(dot(normal, lightDir), 0.0);
    float NdotV = max(dot(normal, viewDir), 0.0);
    
    if(NdotL == 0.0 || NdotV == 0.0){
        return vec3(0,0,0);
    }
    
    vec3 F = fresnelSchlick(max(dot(H, viewDir), 0.0), uMaterial.F0);
    float G = geometrySmith(normal, viewDir, lightDir, uMaterial.roughness);
    float D = distributionGGX(normal, H, uMaterial.roughness);

    vec3 numerator = F * G * D;
    float denominator = 4.0 * NdotV * NdotL;
    vec3 specular = numerator / max(denominator, 0.0001);
    
    vec3 kd = vec3(1.0) - F;
    vec3 diffuse = kd * uMaterial.diffuse / 3.14159;
    
    float distance = length(uPointLight.position - fragPos);
     float attenuation = 1.0 / (1.0 + 0.09 * distance + 0.032 * distance * distance);

     return (diffuse + specular) * uPointLight.color * uPointLight.intensity * NdotL * attenuation;
}
vec3 calculateDirectionalLight(vec3 normal, vec3 fragPos, vec3 viewDir) {
    vec3 lightDir = normalize(-uDirectionalLight.direction);
     vec3 H = normalize(lightDir + viewDir);
    float NdotL = max(dot(normal, lightDir), 0.0);
      float NdotV = max(dot(normal, viewDir), 0.0);
    
     if(NdotL == 0.0 || NdotV == 0.0){
        return vec3(0,0,0);
    }
      vec3 F = fresnelSchlick(max(dot(H, viewDir), 0.0), uMaterial.F0);
    float G = geometrySmith(normal, viewDir, lightDir, uMaterial.roughness);
    float D = distributionGGX(normal, H, uMaterial.roughness);

    vec3 numerator = F * G * D;
    float denominator = 4.0 * NdotV * NdotL;
     vec3 specular = numerator / max(denominator, 0.0001);
    
    vec3 kd = vec3(1.0) - F;
      vec3 diffuse = kd * uMaterial.diffuse / 3.14159;

     return (diffuse + specular) * uDirectionalLight.color * uDirectionalLight.intensity * NdotL;
}
vec3 calculateSpotLight(vec3 normal, vec3 fragPos, vec3 viewDir) {
   vec3 lightDir = normalize(uSpotLight.position - fragPos);
    float distance = length(uSpotLight.position - fragPos);
       vec3 lightDirNormalized = normalize(-lightDir);
     vec3 H = normalize(lightDir + viewDir);
    float NdotL = max(dot(normal, lightDir), 0.0);
    float NdotV = max(dot(normal, viewDir), 0.0);
    
     if(NdotL == 0.0 || NdotV == 0.0){
        return vec3(0,0,0);
    }
    float theta = dot(lightDirNormalized, normalize(-uSpotLight.direction));
     float epsilon = uSpotLight.cutoffAngle - uSpotLight.coneAngle;
    if(theta > cos(uSpotLight.cutoffAngle)){
         vec3 F = fresnelSchlick(max(dot(H, viewDir), 0.0), uMaterial.F0);
        float G = geometrySmith(normal, viewDir, lightDir, uMaterial.roughness);
        float D = distributionGGX(normal, H, uMaterial.roughness);

        vec3 numerator = F * G * D;
         float denominator = 4.0 * NdotV * NdotL;
        vec3 specular = numerator / max(denominator, 0.0001);
        
        vec3 kd = vec3(1.0) - F;
          vec3 diffuse = kd * uMaterial.diffuse / 3.14159;

        float attenuation = 1.0 / (1.0 + 0.09 * distance + 0.032 * distance * distance);
         float intensity = (theta - cos(uSpotLight.cutoffAngle)) / epsilon;
       return (diffuse + specular) * uSpotLight.color * uSpotLight.intensity * NdotL * attenuation * intensity;
   }
   return vec3(0,0,0);
}
void main() {
    vec3 normal = normalize(vNormal);
    vec3 viewDir = normalize(-vFragPos);
    vec3 ambient = uAmbientLight.color * uMaterial.ambient;
    
    vec3 pointLightResult = calculatePointLight(normal, vFragPos, viewDir);
      vec3 directionalLightResult = calculateDirectionalLight(normal, vFragPos, viewDir);
    vec3 spotLightResult = calculateSpotLight(normal, vFragPos, viewDir);

    vec4 textureColor = texture(uTexture, vTexCoord);
     vec3 totalLight = ambient + pointLightResult + directionalLightResult + spotLightResult;

    fragColor = vec4(totalLight * textureColor.rgb , 1.0);
}