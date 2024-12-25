// Загрузка шейдера
export async function loadShader(gl, shaderPath, type) {
  const response = await fetch(shaderPath);
  const shaderSource = await response.text();

  const shader = gl.createShader(type);
  gl.shaderSource(shader, shaderSource);
  gl.compileShader(shader);

  if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
    console.error(`Error compiling shader ${shaderPath}:`, gl.getShaderInfoLog(shader));
    gl.deleteShader(shader);
    return null;
  }

  return shader;
}

// Загрузка и парсинг OBJ-файла
export async function loadOBJ(gl, objPath) {
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
      uvs.push([uv[0], 1.0 - uv[1]]);
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


// Загрузка текстуры
export async function loadTexture(gl, url) {
  return new Promise((resolve, reject) => {
    const image = new Image();
    image.src = url;
    image.onload = () => {
      const texture = gl.createTexture();
      gl.bindTexture(gl.TEXTURE_2D, texture);
      gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);
      gl.generateMipmap(gl.TEXTURE_2D);
      gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_LINEAR);
      gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR);

      resolve(texture);
    };

    image.onerror = () => {
      reject(`Could not load texture at ${url}`);
    };
  });
}

// Создание шейдерной программы
export async function createProgram(gl, vertexShaderPath, fragmentShaderPath) {
  const vertexShader = await loadShader(gl, vertexShaderPath, gl.VERTEX_SHADER);
  const fragmentShader = await loadShader(gl, fragmentShaderPath, gl.FRAGMENT_SHADER);

  const program = gl.createProgram();
  gl.attachShader(program, vertexShader);
  gl.attachShader(program, fragmentShader);
  gl.linkProgram(program);

  if (!gl.getProgramParameter(program, gl.LINK_STATUS)) {
    console.error('Error linking program:', gl.getProgramInfoLog(program));
    gl.deleteProgram(program);
    return null;
  }
  gl.deleteShader(vertexShader)
  gl.deleteShader(fragmentShader)

  return program;
}