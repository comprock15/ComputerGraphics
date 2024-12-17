// Массив для хранения данных загруженных объектов
const objectsData = [null, null];

// Получаем элемент input для загрузки файлов
const fileInput = document.getElementById('fileInput');

// Обработчик события изменения в input (при выборе файлов)
fileInput.addEventListener('change', async (event) => {
    const file1 = event.target.files[0];
    const file2 = event.target.files[1];

    if (file1)
        objectsData[0] = await loadAndParseObj(file1);
    if (file2)
        objectsData[1] = await loadAndParseObj(file2);

    // Инициализация объектов после успешной загрузки обоих файлов
    if (objectsData[0] && objectsData[1]) {
        initObj(gl, objectsData[0]);
        initObj(gl, objectsData[1]);
    }
});

// Инициализация буферов для хранения данных объекта
function initObj(gl, objData) {
    const positionBuffer = gl.createBuffer(); // Буфер для вершин
    gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, objData.positions, gl.STATIC_DRAW);

    const normalBuffer = gl.createBuffer(); // Буфер для нормалей
    gl.bindBuffer(gl.ARRAY_BUFFER, normalBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, objData.normals, gl.STATIC_DRAW);

    const textureCoordBuffer = gl.createBuffer(); // Буфер для текстурных координат
    gl.bindBuffer(gl.ARRAY_BUFFER, textureCoordBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, objData.textureCoords, gl.STATIC_DRAW);

    const indexBuffer = gl.createBuffer(); // Буфер для индексов вершин
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, objData.indices, gl.STATIC_DRAW);

    // Добавляем буферы к данным объекта
    objData.positionBuffer = positionBuffer;
    objData.normalBuffer = normalBuffer;
    objData.textureCoordBuffer = textureCoordBuffer;
    objData.indexBuffer = indexBuffer;
};

// Загрузка файла и парсинг OBJ-данных
async function loadAndParseObj(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = async (event) => {
            const objText = event.target.result;
            try {
                const objData = parseObj(objText);
                resolve(objData);
            }
            catch (e) {
                reject(e);
            }
        };
        reader.onerror = () => {
            reject("Ошибка чтения файла.");
        };
        reader.readAsText(file);
    });
}

// Парсинг данных из OBJ-файла
function parseObj(objText) {
    const lines = objText.split('\n');
    const vertices = []; // Координаты вершин
    const normals = []; // Нормали
    const textures = []; // Текстурные координаты
    const faces = []; // Грани

    // Обработка каждой строки
    for (const line of lines) {
        const parts = line.trim().split(/\s+/);
        if (parts.length === 0) continue;

        const type = parts[0]; // Тип данных в строке (v, vn, vt, f)
        const data = parts.slice(1); // Остальная часть строки

        switch (type) {
            case 'v': // Вершина
                vertices.push(data.map(parseFloat));
                break;
            case 'vn': // Нормаль
                normals.push(data.map(parseFloat));
                break;
            case 'vt': // Текстурные координаты
                let cccc = data.map(parseFloat);
                textures.push([cccc[0], 1.0 - cccc[1]]); // Инверсия Y-координаты
                break;
            case 'f': // Грань
                const face = data.map(part => {
                    const indices = part.split('/').map(index => index ? parseInt(index) - 1 : null);
                    return indices;
                });
                faces.push(face);
                break;
            default:
                break;
        }
    }
    return processData(vertices, normals, textures, faces);
};

// Обработка данных вершин, текстур и граней для использования в WebGL
function processData(vertices, normals, textures, faces) {
    const positions = []; // Координаты вершин
    const normalData = []; // Нормали
    const textureCoords = []; // Текстурные координаты
    const indices = []; // Индексы для рисования треугольников

    for (const face of faces) {
        const baseIndex = positions.length / 3;
        if (face.length === 3) { // Если грань — треугольник
            for (const point of face) {
                positions.push(...vertices[point[0]]);

                if (normals.length)
                    normalData.push(...normals[point[2] || 0]);
                else
                    normalData.push(0, 0, 1);

                if (textures.length)
                    textureCoords.push(...textures[point[1] || 0]);
                else
                    textureCoords.push(0, 0);
            }
            indices.push(baseIndex, baseIndex + 1, baseIndex + 2);
        }
        else if (face.length === 4) { // Если грань — четырёхугольник (разбиваем на два треугольника)
            const v1 = face[0];
            const v2 = face[1];
            const v3 = face[2];
            const v4 = face[3];

            // Первый треугольник (v1, v2, v3)
            positions.push(...vertices[v1[0]]);
            textureCoords.push(...(textures.length ? textures[v1[1] || 0] : [0, 0]));
            normalData.push(...(normals.length ? normals[v1[2] || 0] : [0, 0, 1]));

            positions.push(...vertices[v2[0]]);
            textureCoords.push(...(textures.length ? textures[v2[1] || 0] : [0, 0]));
            normalData.push(...(normals.length ? normals[v2[2] || 0] : [0, 0, 1]));

            positions.push(...vertices[v3[0]]);
            textureCoords.push(...(textures.length ? textures[v3[1] || 0] : [0, 0]));
            normalData.push(...(normals.length ? normals[v3[2] || 0] : [0, 0, 1]));

            indices.push(baseIndex, baseIndex + 1, baseIndex + 2);

            // Второй треугольник (v1, v3, v4)
            positions.push(...vertices[v1[0]]);
            textureCoords.push(...(textures.length ? textures[v1[1] || 0] : [0, 0]));
            normalData.push(...(normals.length ? normals[v1[2] || 0] : [0, 0, 1]));

            positions.push(...vertices[v3[0]]);
            textureCoords.push(...(textures.length ? textures[v3[1] || 0] : [0, 0]));
            normalData.push(...(normals.length ? normals[v3[2] || 0] : [0, 0, 1]));

            positions.push(...vertices[v4[0]]);
            textureCoords.push(...(textures.length ? textures[v4[1] || 0] : [0, 0]));
            normalData.push(...(normals.length ? normals[v4[2] || 0] : [0, 0, 1]));

            indices.push(indices.length, indices.length + 1, indices.length + 2);
        }
    }

    // Возвращаем данные
    return {
        positions: new Float32Array(positions),
        normals: new Float32Array(normalData),
        textureCoords: new Float32Array(textureCoords),
        indices: new Uint32Array(indices),
    };
};

console.debug('objLoader.js compile');
