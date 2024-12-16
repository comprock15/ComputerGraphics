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

export async function loadOBJ(gl, objPath) {
    const response = await fetch(objPath);
    const text = await response.text();

    const vertices = [];
    const normals = [];
    const uvs = [];
    const faces = [];

    const lines = text.split('\n');
    for (const line of lines) {
        const trimmedLine = line.trim();
        if (!trimmedLine || trimmedLine.startsWith('#')) continue;

        const parts = trimmedLine.split(/\s+/);
        const command = parts[0];

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
        else if (command === 'f') {
            const face = parts.slice(1).map(part => {
                const indices = part.split('/').map(index => parseInt(index) - 1);
                return indices;
            });
            faces.push(face);
        }
    }

     const processedVertices = [];
     const processedNormals = [];
     const processedUVs = [];
     const indices = [];

    for(const face of faces){
       if(face.length === 3){
            for (let i = 0; i < 3; i++) {
                const [vIndex, uvIndex, nIndex] = face[i];
                 
                processedVertices.push(...vertices[vIndex])
                processedNormals.push(...normals[nIndex])
                 processedUVs.push(...uvs[uvIndex]);
                indices.push(processedVertices.length / 3 - 1);
            }
       } else if(face.length === 4){
            const [v1, uv1, n1] = face[0];
            const [v2, uv2, n2] = face[1];
            const [v3, uv3, n3] = face[2];
           const [v4, uv4, n4] = face[3];

                processedVertices.push(...vertices[v1])
                processedNormals.push(...normals[n1])
                 processedUVs.push(...uvs[uv1]);
                indices.push(processedVertices.length / 3 - 1);

              processedVertices.push(...vertices[v2])
            processedNormals.push(...normals[n2])
              processedUVs.push(...uvs[uv2]);
            indices.push(processedVertices.length / 3 - 1);
             
              processedVertices.push(...vertices[v3])
            processedNormals.push(...normals[n3])
              processedUVs.push(...uvs[uv3]);
            indices.push(processedVertices.length / 3 - 1);

               processedVertices.push(...vertices[v1])
              processedNormals.push(...normals[n1])
                processedUVs.push(...uvs[uv1]);
                indices.push(processedVertices.length / 3 - 1);

             processedVertices.push(...vertices[v3])
            processedNormals.push(...normals[n3])
                processedUVs.push(...uvs[uv3]);
            indices.push(processedVertices.length / 3 - 1);

             processedVertices.push(...vertices[v4])
            processedNormals.push(...normals[n4])
                processedUVs.push(...uvs[uv4]);
            indices.push(processedVertices.length / 3 - 1);
       }
   
    }

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
    

    return {
        vertexBuffer,
        normalBuffer,
          uvBuffer,
        indexBuffer,
        indices
    };
}