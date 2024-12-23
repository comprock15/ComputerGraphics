async function loadSceneTextures() {
    const textures = {};
    //textures.snow = await loadTexture(gl, 'textures/texture1.jpg');
    textures.airship = loadTexture(gl, './source/airship/airship.png', gl.TEXTURE0);
    textures.airship_normalmap = loadTexture(gl, './source/airship/normalmap.png', gl.TEXTURE1);
    textures.terrain = loadTexture(gl, './source/terrain/snow.png', gl.TEXTURE2);
    textures.terrain_heightmap = loadTexture(gl, './source/terrain/heightmap.png', gl.TEXTURE3);
    textures.balloon = loadTexture(gl, './source/balloon/balloon.png', gl.TEXTURE4);
    textures.cloud = loadTexture(gl, './source/cloud/cloud.png', gl.TEXTURE5);
    textures.tree = loadTexture(gl, './source/tree/tree.png', gl.TEXTURE6);
}

// async function loadTexture(gl, url) {
//     return new Promise((resolve, reject) => {
//         const image = new Image();
//         image.src = url;
//         image.onload = () => {
//             const texture = gl.createTexture();
//             gl.bindTexture(gl.TEXTURE_2D, texture);
//             gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);
//             gl.generateMipmap(gl.TEXTURE_2D);
//             gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_LINEAR);
//             gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR);
    
//             resolve(texture);
//         };
    
//         image.onerror = () => {
//             reject(`Could not load texture at ${url}`);
//         };
//     });
// }

function loadTexture(gl, url, textureUnit) {
    const texture = gl.createTexture();
    gl.activeTexture(textureUnit);
    gl.bindTexture(gl.TEXTURE_2D, texture);

    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, 1, 1, 0, gl.RGBA, gl.UNSIGNED_BYTE, new Uint8Array([0, 0, 255, 255]));

    const image = new Image();
    image.onload = function () {
        gl.bindTexture(gl.TEXTURE_2D, texture);
        gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);
        
        if (isPowerOf2(image.width) && isPowerOf2(image.height)) {
            gl.generateMipmap(gl.TEXTURE_2D);
        } else {
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR);
        }
    };

    image.src = url;
    return texture;
}

function isPowerOf2(value) {
    return (value & (value - 1)) === 0;
}