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
    float shininess;
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

vec3 calculatePointLight(vec3 normal, vec3 fragPos) {
    vec3 lightDirection = normalize(uPointLight.position - fragPos);
    float diffuseStrength = max(dot(normal, lightDirection), 0.0);
    vec3 diffuse = diffuseStrength * uPointLight.color * uMaterial.diffuse;

    vec3 viewDirection = normalize(-fragPos);
    vec3 reflectDirection = reflect(-lightDirection, normal);
    float specularStrength = pow(max(dot(viewDirection, reflectDirection), 0.0), uMaterial.shininess);
    vec3 specular = specularStrength * uPointLight.color * uMaterial.specular;

    float distance = length(uPointLight.position - fragPos);
    float attenuation = 1.0 / (1.0 + 0.09 * distance + 0.032 * distance * distance);
   
     return  attenuation * (diffuse + specular) * uPointLight.intensity;
}

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

vec3 calculateSpotLight(vec3 normal, vec3 fragPos) {
    vec3 lightDirection = normalize(uSpotLight.position - fragPos);
     float distance = length(uSpotLight.position - fragPos);
    vec3 lightDir = normalize(-lightDirection);
    float theta = dot(lightDir, normalize(-uSpotLight.direction));
    float epsilon = uSpotLight.cutoffAngle - uSpotLight.coneAngle;

    if(theta > cos(uSpotLight.cutoffAngle)){
        float diffuseStrength = max(dot(normal, lightDirection), 0.0);
        vec3 diffuse = diffuseStrength * uSpotLight.color * uMaterial.diffuse;

        vec3 viewDirection = normalize(-fragPos);
        vec3 reflectDirection = reflect(-lightDirection, normal);
        float specularStrength = pow(max(dot(viewDirection, reflectDirection), 0.0), uMaterial.shininess);
        vec3 specular = specularStrength * uSpotLight.color * uMaterial.specular;
        float attenuation = 1.0 / (1.0 + 0.09 * distance + 0.032 * distance * distance);
       
      float intensity = (theta - cos(uSpotLight.cutoffAngle)) / epsilon;
        return  attenuation *  (diffuse + specular) * uSpotLight.intensity * intensity;
    }
        return vec3(0,0,0);
}


void main() {
    vec3 normal = normalize(vNormal);
    vec3 ambient = uAmbientLight.color * uMaterial.ambient;

    vec3 pointLightResult = calculatePointLight(normal, vFragPos);
    vec3 directionalLightResult = calculateDirectionalLight(normal, vFragPos);
    vec3 spotLightResult = calculateSpotLight(normal, vFragPos);

    vec4 textureColor = texture(uTexture, vTexCoord);
    vec3 totalLight = ambient + pointLightResult + directionalLightResult + spotLightResult;
    fragColor = vec4(totalLight * textureColor.rgb , 1.0);
}