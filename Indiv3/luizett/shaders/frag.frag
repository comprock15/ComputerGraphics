#version 300 es
precision highp float;

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
    float cutoff;
} uSpotLight;

uniform bool uIsAirShip;
uniform sampler2D uNormalMap;

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

vec3 calculateSpotLight(vec3 normal, vec3 fragPos) {
    vec3 lightDir = normalize(uSpotLight.position - fragPos);
    float theta = dot(lightDir, normalize(-uSpotLight.direction));
    if (theta > uSpotLight.cutoff) {
            float diff = max(dot(normal, lightDir), 0.0);
            return uSpotLight.color * diff * uSpotLight.intensity;
    }
    return vec3(0.0);
}


void main() {
    vec3 normal = normalize(vNormal);
    if (uIsAirShip)
    {
        vec3 normalFromMap = texture(uNormalMap, vTexCoord).xyz * 2.0 - 1.0;
        //normal = normalize(vNormal + normalFromMap);
        normal = normalize(vNormal * normalFromMap);
    }
    
    vec3 ambient = uAmbientLight.color * uMaterial.ambient;
    vec3 directionalLightResult = calculateDirectionalLight(normal, vFragPos);
    vec3 spotLightResult = calculateSpotLight(normal, vFragPos);

    vec4 textureColor = texture(uTexture, vTexCoord);
    vec3 totalLight = ambient +  directionalLightResult + spotLightResult;

    fragColor = vec4(totalLight * textureColor.rgb , 1.0);
}


            // in vec2 vTextureCoord;
            
            // in vec3 vNormal;
            

            // uniform sampler2D uSnowTexture;
            //  uniform sampler2D uNormalMap;
            // uniform vec3 uLightDirection;
            // uniform float uTime;
            // uniform bool uIsTerrain;
            
            // out vec4 fragColor;
            
            // void main() {
            //     vec4 texColor;
                
            //     if (uIsTerrain){
            //      //   float snowLevel = 5.0;
            //        // float snowAmount = smoothstep(snowLevel - 1.0, snowLevel + 1.0, vHeight);
            //       //  vec4 snowColor = texture(uSnowTexture, vTextureCoord);
            // //        vec4 grassColor = vec4(0.0, 0.7, 0.0, 1.0);
            //     //    texColor = mix(grassColor, snowColor, snowAmount);
            //         texColor = texture(uSnowTexture, vTextureCoord);
            //     }
            //     else {
            //       vec3 normalFromMap = texture(uNormalMap, vTextureCoord).xyz * 2.0 - 1.0;
            //       vec3 finalNormal = normalize(vNormal + normalFromMap);

            //       float lightIntensity = max(dot(finalNormal, -normalize(uLightDirection)), 0.0);
            //       texColor = vec4(vec3(0.5, 0.5, 0.8) * (0.5 + lightIntensity * 0.5), 1.0);
            //      }
            
            //     fragColor = texColor;
            // }

// #version 300 es
// precision highp float;

// in vec2 vTextureCoord;
// in float vHeight;
// flat in int vObjType;

// uniform sampler2D uSnowTexture;
// uniform float uTime;

// out vec4 fragColor;
            
// void main() {

                 
//     float snowLevel = 5.0;
//     float snowAmount = smoothstep(snowLevel - 1.0, snowLevel + 1.0, vHeight);
//     vec4 snowColor = texture(uSnowTexture, vTextureCoord);
//     vec4 grassColor = vec4(0.9, 0.9, 0.9, 1.0);
                    
//     // fragColor = mix(grassColor, snowColor, snowAmount);
//     fragColor = snowColor;
// }           