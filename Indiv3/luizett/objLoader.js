const models = {};
async function loadSceneObjects(){
    models.tree = await loadOBJ(gl, './source/tree/tree.obj');
    models.cloud = await loadOBJ(gl, './source/cloud/cloud.obj');
    models.airship = await loadOBJ(gl, './source/airship/airship.obj');
    models.balloon = await loadOBJ(gl, './source/balloon/balloon.obj');
   // models.terrain = await loadTerrain();
   models.terrain = await loadOBJ(gl, './source/terrain/terrain.obj');
}


async function loadTerrain() 
{
    const terrainSize = 100;
    const gridSize = 100;
    const terrainVertices = [];
    const terrainTextureCoords = [];
    const terrainNormals = [];
        const indices = [];

        for (let z = 0; z <= gridSize; z++) {
            for (let x = 0; x <= gridSize; x++) {
                const xCoord = (x / gridSize) * terrainSize - terrainSize / 2;
                const zCoord = (z / gridSize) * terrainSize - terrainSize / 2;
                terrainVertices.push(xCoord, 0, zCoord);

                const u = x / gridSize;
                const v = z / gridSize;
                terrainTextureCoords.push(u,v);

                terrainNormals.push(0, 1, 0);
            }
        }

        for(let z = 0; z < gridSize; z++) {
            for(let x = 0; x < gridSize; x++){
                const topLeft = x + z * (gridSize + 1);
                const topRight = topLeft + 1;
                const bottomLeft = topLeft + (gridSize+1);
                const bottomRight = bottomLeft + 1;
                
                indices.push(topLeft, bottomLeft, topRight);
                indices.push(topRight, bottomLeft, bottomRight);
            }
        }

        // === Буферы террейна ===
        const vertexBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, vertexBuffer);
        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(terrainVertices), gl.STATIC_DRAW);
        // const aTerrainVertexPosition = gl.getAttribLocation(shaderProgram, 'aVertexPosition');
        // gl.vertexAttribPointer(aTerrainVertexPosition, 3, gl.FLOAT, false, 0, 0);
        // gl.enableVertexAttribArray(aTerrainVertexPosition);
        
        const normalBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, normalBuffer);
        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(terrainNormals), gl.STATIC_DRAW);
        // const aTerrainVertexNormal = gl.getAttribLocation(shaderProgram, 'aVertexNormal');
        // gl.vertexAttribPointer(aTerrainVertexNormal, 3, gl.FLOAT, false, 0, 0);
        // gl.enableVertexAttribArray(aTerrainVertexNormal);
        
        const uvBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, uvBuffer);
        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(terrainTextureCoords), gl.STATIC_DRAW);
        // const aTerrainTextureCoord = gl.getAttribLocation(shaderProgram, 'aTextureCoord');
        // gl.vertexAttribPointer(aTerrainTextureCoord, 2, gl.FLOAT, false, 0, 0);
        // gl.enableVertexAttribArray(aTerrainTextureCoord);

        const indexBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
        gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint32Array(indices), gl.STATIC_DRAW);

        return {
          vertexBuffer,
          normalBuffer,
          uvBuffer,
          indexBuffer,
          indices
        };
}

async function loadOBJ(gl, objPath) {
    // Загружаем текстовый файл OBJ
    const response = await fetch(objPath);
    const text = await response.text();
  
    const vertices = [];
    const normals = [];
    const uvs = [];
    const faces = [];
  
    // Разбираем каждую строку файла и извлекаем данные
    const lines = text.split('\n');
    for (const line of lines) {
      const trimmedLine = line.trim();
      if (!trimmedLine || trimmedLine.startsWith('#')) continue;  // Пропускаем пустые строки и комментарии
  
      const parts = trimmedLine.split(/\s+/);  // Разделяем строку на части
      const command = parts[0];  // Получаем команду (v, vn, vt, f)
  
      // Обрабатываем вершины, нормали и текстурные координаты
      if (command === 'v') {
        const vertex = parts.slice(1).map(parseFloat);
        vertices.push(vertex);
      } else if (command === 'vn') {
        const normal = parts.slice(1).map(parseFloat);
        normals.push(normal);
      } else if (command === 'vt') {
        const uv = parts.slice(1).map(parseFloat);
        uvs.push(uv);
      }
      // Обрабатываем грани
      else if (command === 'f') {
        const face = parts.slice(1).map(part => {
          const indices = part.split('/').map(index => parseInt(index) - 1); // Индексы вершин, текстурных координат и нормалей
          return indices;
        });
        faces.push(face);
      }
    }
  
    const processedVertices = [];
    const processedNormals = [];
    const processedUVs = [];
    const indices = [];
  
    // Обработка граней для создания массивов вершин, нормалей и текстурных координат
    for (const face of faces) {
      if (face.length === 3) {
        // Обрабатываем треугольные грани
        for (let i = 0; i < 3; i++) {
          const [vIndex, uvIndex, nIndex] = face[i];
          processedVertices.push(...vertices[vIndex]);
          processedNormals.push(...normals[nIndex]);
          processedUVs.push(...uvs[uvIndex]);
          indices.push(processedVertices.length / 3 - 1);
        }
      } else if (face.length === 4) {
        // Обрабатываем четырехугольные грани, разбивая их на два треугольника
        const [v1, uv1, n1] = face[0];
        const [v2, uv2, n2] = face[1];
        const [v3, uv3, n3] = face[2];
        const [v4, uv4, n4] = face[3];
  
        // Добавляем вершины, нормали и текстурные координаты для первого треугольника
        processedVertices.push(...vertices[v1]);
        processedNormals.push(...normals[n1]);
        processedUVs.push(...uvs[uv1]);
        indices.push(processedVertices.length / 3 - 1);
  
        processedVertices.push(...vertices[v2]);
        processedNormals.push(...normals[n2]);
        processedUVs.push(...uvs[uv2]);
        indices.push(processedVertices.length / 3 - 1);
  
        processedVertices.push(...vertices[v3]);
        processedNormals.push(...normals[n3]);
        processedUVs.push(...uvs[uv3]);
        indices.push(processedVertices.length / 3 - 1);
  
        // Добавляем вершины, нормали и текстурные координаты для второго треугольника
        processedVertices.push(...vertices[v1]);
        processedNormals.push(...normals[n1]);
        processedUVs.push(...uvs[uv1]);
        indices.push(processedVertices.length / 3 - 1);
  
        processedVertices.push(...vertices[v3]);
        processedNormals.push(...normals[n3]);
        processedUVs.push(...uvs[uv3]);
        indices.push(processedVertices.length / 3 - 1);
  
        processedVertices.push(...vertices[v4]);
        processedNormals.push(...normals[n4]);
        processedUVs.push(...uvs[uv4]);
        indices.push(processedVertices.length / 3 - 1);
      }
    }
  
    // Создание буферов WebGL для вершин, нормалей, текстурных координат и индексов
    const vertexBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, vertexBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(processedVertices), gl.STATIC_DRAW);
  
    const normalBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, normalBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(processedNormals), gl.STATIC_DRAW);
  
    const uvBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, uvBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(processedUVs), gl.STATIC_DRAW);
  
    const indexBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indices), gl.STATIC_DRAW);
  
    // Возвращаем объект с созданными буферами
    return {
      vertexBuffer,
      normalBuffer,
      uvBuffer,
      indexBuffer,
      indices
    };
}




  