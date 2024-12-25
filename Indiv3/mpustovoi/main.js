import { createProgram } from "./utils.js";
import { Camera } from "./camera.js";
import { parseOBJ } from "./objLoader.js";
import { mat4, vec3, glMatrix } from "https://cdn.jsdelivr.net/npm/gl-matrix@3.4.3/+esm";


export class Object3D {
    constructor(gl, program, objData, textureUrl, scale) {
        this.gl = gl;
        this.program = program;

        const { positions, texCoords, normals } = parseOBJ(objData);

        for (let i = 0; i < positions.length; ++i)
            positions[i] *= scale;

        this.vertexCount = positions.length / 3;

        this.vao = gl.createVertexArray();
        gl.bindVertexArray(this.vao);

        this.positionBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, this.positionBuffer);
        gl.bufferData(gl.ARRAY_BUFFER, positions, gl.DYNAMIC_DRAW);
        gl.enableVertexAttribArray(0);
        gl.vertexAttribPointer(0, 3, gl.FLOAT, false, 0, 0);

        this.texCoordBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, this.texCoordBuffer);
        gl.bufferData(gl.ARRAY_BUFFER, texCoords, gl.DYNAMIC_DRAW);
        gl.enableVertexAttribArray(1);
        gl.vertexAttribPointer(1, 2, gl.FLOAT, false, 0, 0);

        this.vertexNormalsBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, this.vertexNormalsBuffer);
        gl.bufferData(gl.ARRAY_BUFFER, normals, gl.DYNAMIC_DRAW);
        gl.enableVertexAttribArray(2);
        gl.vertexAttribPointer(2, 3, gl.FLOAT, false, 0, 0);

        this.instanceMatrixBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, this.instanceMatrixBuffer);

        for (let i = 0; i < 4; ++i) {
            gl.enableVertexAttribArray(3 + i);
            gl.vertexAttribPointer(3 + i, 4, gl.FLOAT, false, 64, i * 16);
            gl.vertexAttribDivisor(3 + i, 1);
        }

        gl.bindVertexArray(null);

        this.texture = gl.createTexture();
        const image = new Image();
        image.src = textureUrl;
        image.onload = () => {
            gl.pixelStorei(gl.UNPACK_PREMULTIPLY_ALPHA_WEBGL, true);
            gl.bindTexture(gl.TEXTURE_2D, this.texture);
            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);
            gl.generateMipmap(gl.TEXTURE_2D);
        };
    }

    updateInstanceMatrices(matrices) {
        const gl = this.gl;
        gl.bindBuffer(gl.ARRAY_BUFFER, this.instanceMatrixBuffer);
        gl.bufferData(gl.ARRAY_BUFFER, matrices, gl.DYNAMIC_DRAW);
    }

    renderInstanced(instanceCount, viewMatrix) {
        const gl = this.gl;

        gl.useProgram(this.program);

        gl.uniformMatrix4fv(gl.getUniformLocation(this.program, "uMatrix"), false, viewMatrix);

        gl.bindVertexArray(this.vao);
        gl.bindTexture(gl.TEXTURE_2D, this.texture);
        gl.drawArraysInstanced(gl.TRIANGLES, 0, this.vertexCount, instanceCount);

        gl.bindVertexArray(null);
    }

    render(models, view, projection, numInstances = 1) {
        /** @type {WebGL2RenderingContext} */
        const gl = this.gl;

        this.updateInstanceMatrices(models);

        gl.useProgram(this.program);
        gl.uniformMatrix4fv(gl.getUniformLocation(this.program, "uViewMatrix"), false, view);
        gl.uniformMatrix4fv(gl.getUniformLocation(this.program, "uProjectionMatrix"), false, projection);

        const dirLightLoc = {
            direction: gl.getUniformLocation(this.program, "uDirLight.direction"),
            color: gl.getUniformLocation(this.program, "uDirLight.color"),
            intensity: gl.getUniformLocation(this.program, "uDirLight.intensity"),
        };

        gl.uniform3fv(dirLightLoc.direction, [-0.5, -1.0, -0.5])
        gl.uniform3fv(dirLightLoc.color, [1.0, 1.0, 1.0]); 
        gl.uniform1f(dirLightLoc.intensity, 1.2);


        gl.uniform3fv(gl.getUniformLocation(this.program, "uViewPos"), camera.position);

        gl.bindVertexArray(this.vao);
        gl.bindTexture(gl.TEXTURE_2D, this.texture);
        gl.drawArraysInstanced(gl.TRIANGLES, 0, this.vertexCount, numInstances);

        gl.bindVertexArray(null);
    }
}

const camera = new Camera([0, 2, 10]);
let lastFrame = 0;

const keys = {};

export class Airship {
    constructor(gl, program, objData, textureUrl) {
        this.object = new Object3D(gl, program, objData, textureUrl, 0.02);
        this.position = vec3.fromValues(0, 0, 0);
        this.rotation = vec3.fromValues(0, 0, 0);
        this.camera_forward = vec3.create();
        this.spotlight_on = true;
        this.camera_transitioning = false;
    }

    update(deltaTime, camera) {
        const moveSpeed = 1.5;
        const rotateSpeed = 0.7;

        if (keys["w"]) vec3.scaleAndAdd(this.position, this.position, [camera.front[0], 0, camera.front[2]], moveSpeed * deltaTime);
        if (keys["s"]) vec3.scaleAndAdd(this.position, this.position, [camera.front[0], 0, camera.front[2]], -moveSpeed * deltaTime);
        if (keys["a"]) vec3.scaleAndAdd(this.position, this.position, [camera.right[0], 0, camera.right[2]], -moveSpeed * deltaTime);
        if (keys["d"]) vec3.scaleAndAdd(this.position, this.position, [camera.right[0], 0, camera.right[2]], moveSpeed * deltaTime);
        this.camera_forward = camera.front;

        if (keys["w"] || keys["s"] || keys["a"] || keys["d"]) {
            const forward = vec3.fromValues(camera.front[0], 0, camera.front[2]);
            vec3.normalize(forward, forward);

            const targetYaw = -Math.atan2(forward[2], forward[0]);
            var deltaYaw = targetYaw - this.rotation[1];

            if (deltaYaw > Math.PI) deltaYaw -= 2 * Math.PI;
            if (deltaYaw < -Math.PI) deltaYaw += 2 * Math.PI;

            if (Math.abs(deltaYaw) >= 1e-2)
                this.rotation[1] += deltaYaw * deltaTime * rotateSpeed;
        }

        const target = vec3.create();
        const delta = vec3.create();
        vec3.copy(target, this.position);
        if (this.spotlight_on) {
            vec3.scaleAndAdd(target, target, camera.front, 4.5);
            vec3.scaleAndAdd(target, target, [camera.front[0], 0.5, 0], 0.1);
            camera.distance = 7.5;
            camera.minPitch = -25;
            camera.maxPitch = 20;
        }
        if (this.camera_transitioning) {
            const vecDiff = vec3.create();
            vec3.subtract(vecDiff, target, delta);
            if (vec3.length(vecDiff) < 0.15) {
                this.camera_transitioning = false;
            }
        }
        else {
            vec3.copy(delta, target);
        }

        camera.setTarget(delta);
    }

    render(viewMatrix, projectionMatrix) {
        const modelMatrix = mat4.create();
        mat4.translate(modelMatrix, modelMatrix, this.position);
        mat4.rotateY(modelMatrix, modelMatrix, Math.PI + this.rotation[1]);
        mat4.rotateZ(modelMatrix, modelMatrix, glMatrix.toRadian(this.rotation[2]));

        this.object.render(modelMatrix, viewMatrix, projectionMatrix);
    }
}

async function main() {
    const canvas = document.getElementById("gl-canvas");
    /** @type {WebGL2RenderingContext} */
    const gl = canvas.getContext("webgl2");
    if (!gl) {
        console.error("WebGL2 is not supported.");
        return;
    }

    const vertexShaderSource = `#version 300 es
    layout(location = 0) in vec3 aPosition;
    layout(location = 1) in vec2 aTexCoord;
    layout(location = 2) in vec3 aNormal;
    layout(location = 3) in mat4 aModelMatrix;

    uniform mat4 uViewMatrix, uProjectionMatrix;
    uniform vec3 uViewPos;
    out vec3 vNormal;
    out vec3 vFragPos;
    out vec2 vTexCoord;
    out vec3 viewDir;

    void main() {
        vNormal = normalize(mat3(transpose(inverse(aModelMatrix))) * aNormal);
        vFragPos = vec3(aModelMatrix * vec4(aPosition, 1.0));
        vTexCoord = aTexCoord;
        gl_Position = uProjectionMatrix * uViewMatrix * aModelMatrix * vec4(aPosition, 1.0);
        viewDir = normalize(uViewPos - vFragPos);
    }`;


    const fragmentShaderSource = `#version 300 es
    precision mediump float;

    in vec3 vNormal;
    in vec3 vFragPos;
    in vec2 vTexCoord;
    in vec3 viewDir;

    struct Light {
        vec3 position;
        vec3 direction;
        vec3 color;
        float intensity;
        vec3 attenuation;
    };

    uniform Light uDirLight;

    uniform sampler2D uTexture;

    out vec4 FragColor;

    void main() {
        vec3 normal = vNormal;
        vec4 texColor = texture(uTexture, vTexCoord);

        vec3 resultColor = vec3(0.0);

        vec3 lightDir = normalize(-uDirLight.direction);
        float diff = max(dot(normal, lightDir), 0.0);
        vec3 dirLightColor = uDirLight.color * diff * uDirLight.intensity;

        resultColor += dirLightColor;

        FragColor = vec4(resultColor * texColor.rgb, texColor.a);
    }`;

    const program = createProgram(gl, vertexShaderSource, fragmentShaderSource);
    const objData = await fetch("../models/airship.obj").then((res) => res.text());
    const textureUrl = "../textures/airship.png";

    const airship = new Airship(gl, program, objData, textureUrl);

    function resizeCanvasToDisplaySize(canvas) {
        const displayWidth = canvas.clientWidth * window.devicePixelRatio;
        const displayHeight = canvas.clientHeight * window.devicePixelRatio;
        if (canvas.width !== displayWidth || canvas.height !== displayHeight) {
            canvas.width = displayWidth;
            canvas.height = displayHeight;
            gl.viewport(0, 0, displayWidth, displayHeight);
        }
    }

    const firData = await fetch("../models/fir.obj").then((res) => res.text());
    const firTexture = "../textures/fir.png";
    const firObject = new Object3D(gl, program, firData, firTexture, 2.0);

    const cloudData = await fetch("../models/cloud.obj").then((res) => res.text());
    const cloudTexture = "../textures/cloud.png";
    const cloudObject = new Object3D(gl, program, cloudData, cloudTexture, 0.01);

    const landData = await fetch("../models/land.obj").then((res) => res.text());
    const landTexture = "../textures/cloud.png";
    const landObject = new Object3D(gl, program, landData, landTexture, 100.0);

    const numClouds = 20;
    const cloudMatrices = new Float32Array(16 * numClouds);
    for (var i = 0; i < numClouds; ++i) {
        const cloudMatrix = mat4.create();
        mat4.translate(cloudMatrix, cloudMatrix, [Math.random() * 60 - 15, -Math.random() * 4 + 8, Math.random() * 60 - 15]);
        mat4.rotateX(cloudMatrix, cloudMatrix, Math.random());
        mat4.rotateY(cloudMatrix, cloudMatrix, Math.random());
        mat4.rotateZ(cloudMatrix, cloudMatrix, Math.random());
        cloudMatrices.set(cloudMatrix, i * 16);
    }

    gl.clearColor(0.1, 0.1, 0.1, 1.0);
    gl.enable(gl.DEPTH_TEST);
    async function render(time) {
        resizeCanvasToDisplaySize(canvas);
        const deltaTime = (time - lastFrame) / 1000.0;
        lastFrame = time;

        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

        airship.update(deltaTime, camera);

        const projectionMatrix = mat4.create();
        mat4.perspective(projectionMatrix, glMatrix.toRadian(45), canvas.width / canvas.height, 0.1, 100.0);
        const viewMatrix = camera.getViewMatrix();

        airship.render(viewMatrix, projectionMatrix);

        const firModel = mat4.create();
        mat4.translate(firModel, firModel, [0, -10, 0]);

        const firsModels = new Float32Array(16 * 1);
        firsModels.set(firModel, 0);

        const landModel = mat4.create();
        mat4.translate(landModel, landModel, [0, -10, 0]);
        const landModels = new Float32Array(16 * 1);
        landModels.set(landModel, 0);

        firObject.render(firsModels, viewMatrix, projectionMatrix, 1);

        landObject.render(landModels, viewMatrix, projectionMatrix)

        gl.enable(gl.BLEND);
        gl.blendFunc(gl.ONE, gl.ONE_MINUS_SRC_ALPHA);

        cloudObject.render(cloudMatrices, viewMatrix, projectionMatrix, numClouds);

        requestAnimationFrame(render);
    }

    window.addEventListener("keydown", (e) => {
        keys[e.key.toLowerCase()] = true;
        if (e.key.toLowerCase() == "l") {
            airship.spotlight_on = !airship.spotlight_on;
            airship.camera_transitioning = true;
        }
    });
    window.addEventListener("keyup", (e) => (keys[e.key.toLowerCase()] = false));

    canvas.addEventListener("click", () => canvas.requestPointerLock());
    document.addEventListener("mousemove", (event) => {
        if (document.pointerLockElement === canvas) {
            camera.processMouseMovement(event.movementX, -event.movementY);
        }
    });

    canvas.addEventListener("wheel", (event) => {
        let delta = event.deltaY;
        camera.processMouseWheel(delta);
        event.preventDefault();
    });

    requestAnimationFrame(render);
}

main();