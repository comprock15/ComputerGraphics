export class Camera {
    constructor(target = [0.0, 0.0, 0.0], distance = 4.0, up = [0.0, 1.0, 0.0], yaw = -90.0, pitch = -15.0) {
        this.target = vec3.clone(target);
        this.distance = distance;
        this.position = vec3.create();
        this.worldUp = vec3.clone(up);
        this.front = vec3.fromValues(0.0, 0.0, -1.0);
        this.right = vec3.create();
        this.up = vec3.clone(up);
        this.yaw = yaw;
        this.pitch = pitch;
        this.minPitch = -89;
        this.maxPitch = 89;
        this.updateVectors();
    }

    updateVectors() {
        // Передний вектор
        const radYaw = glMatrix.toRadian(this.yaw);
        const radPitch = glMatrix.toRadian(this.pitch);
        this.front[0] = Math.cos(radPitch) * Math.cos(radYaw);
        this.front[1] = Math.sin(radPitch);
        this.front[2] = Math.cos(radPitch) * Math.sin(radYaw);
        vec3.normalize(this.front, this.front);

        // Правый вектор
        vec3.cross(this.right, this.front, this.worldUp);
        vec3.normalize(this.right, this.right);

        // Верхний вектор
        vec3.cross(this.up, this.right, this.front);
        vec3.normalize(this.up, this.up);

        // Позиция камеры
        this.position[0] = this.target[0] - this.distance * this.front[0];
        this.position[1] = this.target[1] - this.distance * this.front[1];
        this.position[2] = this.target[2] - this.distance * this.front[2];
    }

    getViewMatrix() {
        return mat4.lookAt(mat4.create(), this.position, this.target, this.up);
    }

    processMouseWheel(delta) {
        const zoomSpeed = 0.2;
        const zoomDirection = delta > 0 ? 1 : -1;

        const zoomAmount = zoomDirection * zoomSpeed;

        camera.distance += zoomAmount;

        camera.distance = Math.max(2.0, Math.min(camera.distance, 10.0));

        this.updateVectors();
    }

    processMouseMovement(xoffset, yoffset) {
        xoffset *= 0.2; 
        yoffset *= 0.2;

        this.yaw += xoffset;
        this.pitch = Math.min(Math.max(this.pitch + yoffset, this.minPitch), this.maxPitch);;

        if (this.yaw < 0) this.yaw += 360;
        if (this.pitch > 360) this.yaw -= 360;

        this.updateVectors();
    }

    setTarget(newTarget) {
        vec3.copy(this.target, newTarget);
        this.updateVectors();
    }
}