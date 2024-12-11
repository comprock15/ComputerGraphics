function task3() {
    const canvas = document.getElementById("canvas3");
    const gl = canvas.getContext("webgl2");

    if (!gl) {
        console.error("Ваш браузер не поддерживает WebGL");
        return;
    }

    const vertices = [
        // передняя грань
        -0.5, -0.5, +0.5, // 0
        -0.5, +0.5, +0.5, // 1
        +0.5, +0.5, +0.5, // 2
        +0.5, -0.5, +0.5, // 3

        // задняя грань 
        -0.5, -0.5, -0.5, // 4
        -0.5, +0.5, -0.5, // 5
        +0.5, +0.5, -0.5, // 6
        +0.5, -0.5, -0.5, // 7
          
        // левая грань
        -0.5, -0.5, +0.5, // 8  (0)
        -0.5, +0.5, +0.5, // 9  (1)
        -0.5, +0.5, -0.5, // 10 (5)
        -0.5, -0.5, -0.5, // 11 (4)
         
        // правая грань
        +0.5, -0.5, +0.5, // 12 (3)
        +0.5, +0.5, +0.5, // 13 (2)
        +0.5, +0.5, -0.5, // 14 (6)
        +0.5, -0.5, -0.5, // 15 (7)

        // верхняя грань
        +0.5, +0.5, +0.5, // 16 (2)
        -0.5, +0.5, +0.5, // 17 (1)
        -0.5, +0.5, -0.5, // 18 (5)
        +0.5, +0.5, -0.5, // 19 (6)

        // нижняя грань
        +0.5, -0.5, +0.5, // 20 (3)
        -0.5, -0.5, +0.5, // 21 (0)
        -0.5, -0.5, -0.5, // 22 (4)
        +0.5, -0.5, -0.5  // 23 (7)
    ];
          
    const indices = [ 
        // передняя грань
        0, 1, 2, 
        2, 3, 0,
        // задняя грань
        4, 5, 6,
        6, 7, 4,
        // левая грань
        8, 9, 10, 
        10, 11, 8,
        // правая грань
        12, 13, 14, 
        14, 15, 12,
        // верхняя грань
        16, 17, 18,
        18, 19, 16,
        // нижняя грань
        20, 21, 22,
        22, 23, 20
    ];

    let textureCoords = [];
    for (let i = 0; i < 6; i++) { 
        textureCoords.push(0.0, 1.0, 0.0, 0.0, 1.0, 0.0, 1.0, 1.0); 
    }

    const vertexShaderSource = `#version 300 es
    layout (location = 0) in vec3 position;
    layout (location = 1) in vec2 texCoord;
    out vec2 vTexCoord;
    uniform mat4 MVPmatr;
    void main() {
        gl_Position = MVPmatr * vec4(position, 1.0);
        vTexCoord = texCoord;
    }`;

    const fragmentShaderSource = `#version 300 es
    precision mediump float;
    in vec2 vTexCoord;
    out vec4 fragColor;
    uniform sampler2D ourTexture1;
    uniform sampler2D ourTexture2;
    uniform float mixRatio;
    void main() {
        vec4 textureColor1 = texture(ourTexture1, vTexCoord);
        vec4 textureColor2 = texture(ourTexture2, vTexCoord);
        fragColor = mix(textureColor1, textureColor2, mixRatio);
    }`;

    const program = initShaderProgram(gl, vertexShaderSource, fragmentShaderSource);
    gl.useProgram(program);

    const attrib_position = gl.getAttribLocation(program, 'position');
    const attrib_texCoord = gl.getAttribLocation(program, 'texCoord');
    const uniform_mixRatio = gl.getUniformLocation(program, "mixRatio");
    const uniform_MVP = gl.getUniformLocation(program, "MVPmatr");
    const uniform_texture1 = gl.getUniformLocation(program, "ourTexture1");
    const uniform_texture2 = gl.getUniformLocation(program, "ourTexture2");

    // Vertex Array Object - позволяет настроить атрибуты лишь единожды, потом использовать VAO
    const VAO = initVAO(gl);

    let modelViewMatrix = mat4.create();
    let projectionMatrix = mat4.create();
    let modelViewProjectionMatrix = mat4.create();

    // Корректировка камеры и положения
    mat4.perspective(projectionMatrix, Math.PI / 4, canvas.width / canvas.height, 0.1, 100.0);
    mat4.scale(projectionMatrix, projectionMatrix, [1, 1, -1]);
    let translation = [0.0, 0.0, 2.5];
    let rotationX = Math.PI / 6;
    let rotationY = -2*Math.PI / 3;
    let rotationZ = 0;

    function updateModelViewMatrix() {
        mat4.identity(modelViewMatrix);
        mat4.translate(modelViewMatrix, modelViewMatrix, translation);
        mat4.rotateX(modelViewMatrix, modelViewMatrix, rotationX);
        mat4.rotateY(modelViewMatrix, modelViewMatrix, rotationY);
        mat4.rotateZ(modelViewMatrix, modelViewMatrix, rotationZ);
        mat4.multiply(modelViewProjectionMatrix, projectionMatrix, modelViewMatrix);
        gl.uniformMatrix4fv(uniform_MVP, false, modelViewProjectionMatrix);
    }
    updateModelViewMatrix();

    //-----------------------------------------------------------------

    function getShader(gl, shaderType, shaderSource) {
        let shader = gl.createShader(shaderType);
        gl.shaderSource(shader, shaderSource);
        gl.compileShader(shader);
         
        if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
          alert("Ошибка компиляции шейдера: " + gl.getShaderInfoLog(shader));
          gl.deleteShader(shader);   
          return null;
        }
        return shader;  
    }

    function initShaderProgram(gl, vertexShaderSource, fragmentShaderSource) {
        let vertexShader = getShader(gl, gl.VERTEX_SHADER, vertexShaderSource);
        let fragmentShader = getShader(gl, gl.FRAGMENT_SHADER, fragmentShaderSource);
      
        let program = gl.createProgram();
        gl.attachShader(program, vertexShader);
        gl.attachShader(program, fragmentShader);
      
        gl.linkProgram(program);  
      
        if (!gl.getProgramParameter(program, gl.LINK_STATUS)) {
          console.error("Не удалсь установить шейдеры");
          return null;
        }
    
        return program;
    }

    function initArrayBuffer(gl, arr) {
        let buffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(arr), gl.STATIC_DRAW);
        return buffer;
    }

    function initVAO(gl) {
        const VAO = gl.createVertexArray();
        gl.bindVertexArray(VAO);
    
        const positionBuffer = initArrayBuffer(gl, vertices);
        gl.vertexAttribPointer(attrib_position, 3, gl.FLOAT, false, 0, 0); // Устанавливаем указатели на вершинные атрибуты
        gl.enableVertexAttribArray(attrib_position);     

        const textureCoordsBuffer = initArrayBuffer(gl, textureCoords);
        gl.vertexAttribPointer(attrib_texCoord, 2, gl.FLOAT, false, 0, 0); // Устанавливаем указатели на вершинные атрибуты
        gl.enableVertexAttribArray(attrib_texCoord);

        const IBO = initIBO(gl, indices);

        gl.bindVertexArray(null);

        return VAO;
    }

    function initIBO(gl, arr) {
        const IBO = gl.createBuffer();
        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, IBO);
        gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(arr), gl.STATIC_DRAW);
        return IBO;
    }

    function loadImage(url, callback) {
        let image = new Image();
        image.src = url;
        image.onload = callback;
        return image;
    }

    function loadImages(urls, callback) {
        let images = [];
        let imagesToLoad = urls.length;
       
        // вызывается каждый раз при загрузке изображения
        let onImageLoad = function() {
          --imagesToLoad;
          // если все объекты загрузились, вызываем callback
          if (imagesToLoad == 0) {
            callback(images);
          }
        };
       
        for (let ii = 0; ii < imagesToLoad; ++ii) {
          let image = loadImage(urls[ii], onImageLoad);
          images.push(image);
        }

        return images;
    }

    function draw() {
        gl.clearColor(0.0, 0.0, 0.0, 1.0);
        gl.enable(gl.DEPTH_TEST);
        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

        gl.useProgram(program);

        gl.bindVertexArray(VAO);

        // set which texture units to render with.
        gl.uniform1i(uniform_texture1, 0);  // texture unit 0
        gl.uniform1i(uniform_texture2, 1);  // texture unit 1

        // Set each texture unit to use a particular texture.
        gl.activeTexture(gl.TEXTURE0);
        gl.bindTexture(gl.TEXTURE_2D, textures[0]);
        gl.activeTexture(gl.TEXTURE1);
        gl.bindTexture(gl.TEXTURE_2D, textures[1]);

        gl.drawElements(gl.TRIANGLES, indices.length, gl.UNSIGNED_SHORT, 0);
    }

    // Перерисовка при изменении значения слайдера
    let slider = document.getElementById("slider2");
    gl.uniform1f(uniform_mixRatio, slider.value);

    slider.oninput = () => {
        gl.uniform1f(uniform_mixRatio, slider.value);
        draw();
    }
    let textures = [];
    let images = loadImages([
        "texture1.png",
        "texture2.png",
      ], render);

    function render() {
        // создаём 2 текстуры
        for (let ii = 0; ii < 2; ++ii) {
            let texture = gl.createTexture();
            gl.bindTexture(gl.TEXTURE_2D, texture);
            
            // задаём параметры для отображения изображения любого размера
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.NEAREST);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.NEAREST);
            
            // загружаем изображение в текстуру
            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, images[ii]);
            
            // добавляем текстуру в массив текстур
            textures.push(texture);
        }

        draw();
    }
}