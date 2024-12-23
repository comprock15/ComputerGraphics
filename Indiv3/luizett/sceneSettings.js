function sceneSetUp() {
    // Источники света
    const lights = {
        ambientLight: {
        color: [0.1, 0.1, 0.1]
        },

        directionalLight: {
        direction: vec3.fromValues(0, -20, -1),
        color: vec3.fromValues(1, 1, 1),
        intensity: 1.0
        },

    };

    const sceneObjects = [
        // Дерево
        {
          model: models.tree,
          texture: textures.tree,
          position: vec3.fromValues(0, 0, 0),
          rotation: vec3.fromValues(0, 0, 0),
          scale: vec3.fromValues(1.0, 1.0, 1.0),
          material: {
            ambient: 0.2,
            diffuse: 0.8,
            specular: 0.5,
            shininess: 32.0,
            roughness: 0.3,
          }
        },
        // Дирижабль
        {
          model: models.airship,
          texture: textures.airship,
          position: vec3.fromValues(0, 0, 0),
          rotation: vec3.fromValues(0, 0, 0),
          scale: vec3.fromValues(1.0, 1.0, 1.0),
          material: {
            ambient: 0.2,
            diffuse: 0.8,
            specular: 0.5,
            shininess: 32.0,
            roughness: 0.3,
          }
        },
        // Облако
        {
          model: models.cloud,
          texture: textures.cloud,
          position: vec3.fromValues(0, 0, 0),
          rotation: vec3.fromValues(0, 0, 0),
          scale: vec3.fromValues(1.0, 1.0, 1.0),
          program: phongProgram, 
          material: {
            ambient: 0.2,
            diffuse: 0.8,
            specular: 0.5,
            shininess: 32.0,
            roughness: 0.3,
          }
        },
        // Воздушный шар
        {
          model: models.balloon,
          texture: textures.balloon,
          position: vec3.fromValues(0, 0, 0),
          rotation: vec3.fromValues(0, 0, 0),
          scale: vec3.fromValues(1.0, 1.0, 1.0),
          program: phongProgram, 
          material: {
            ambient: 0.2,
            diffuse: 0.8,
            specular: 0.5,
            shininess: 32.0,
            roughness: 0.3,
          }
        },
    ];
};