// Закрасить канвас 
function paintCanvas(pixels) {
    for (let i = 0; i < width; ++i) {
        for (let j = 0; j < height; ++j) {
            color = pixels[i][j];
            ctx.fillStyle = `rgb(${color.r},${color.g},${color.b})`;
            ctx.fillRect(i, j, 1, 1);
        }
    }
}

// Отрендерить сцену
function render() {
    for (let i = 0; i < width; ++i) {
        for (let j = 0; j < height; ++j) {
            x = (2*(i + 0.5)/width - 1)*Math.tan(camera.fieldOfView/2)*(width/height);
            y = -(2*(j + 0.5)/height - 1)*Math.tan(camera.fieldOfView/2);
            let ray = new Ray(camera.position, new Vector(x, y, camera.fieldOfView).normalize());
            color = trace(ray, 1);
            pixelGrid[i][j] = color;
        }
    }
    paintCanvas(pixelGrid);
}

// Пустить луч
function trace(ray, depth, currentObj) {
    if (depth <= 0) { return scene.bgcolor; }

    // Пускаем луч к объектам
    let results = scene.objects.filter(o => o != currentObj).map(o => o.collision(ray))
    // Ищем ближайшее пересечение
    let result = results
        .filter(a => a.collide) // Убираем объекты, с которыми луч не пересекся
        .reduce((a, b) => {
            return a.dist <= b.dist ? a : b
        }, { collide: false, dist: Infinity })

    // Луч не пересек никакой из объектов
    if (!result.collide) { return scene.bgcolor; }

    // Считаем освещенность в точке
    let lightIntensity = 0;
    for (let light of scene.lights) {
        let lightDirection = Vector.subtract(light.position, result.point).normalize();
        lightIntensity += light.intensity * Math.max(0, Vector.dot(lightDirection, result.normal));
    }

    // let glow = result.obj.properties.glow

    // let reflectivity = result.obj.properties.reflectivity
    
    // let reflection = { r: 0, g: 0, b: 0 };
    // if (reflectivity.r > 0) {
    //     reflection = trace(reflect(result.point, ray, result.normal, result.obj.properties.roughness), depth - 1, result.obj);
    // }
            

    // return ({
    //     r: glow.r + reflectivity.r * reflection.r,
    //     g: glow.g + reflectivity.g * reflection.g,
    //     b: glow.b + reflectivity.b * reflection.b
    // })
    let color = result.obj.properties.color
    return Color.scale(color, lightIntensity);
}