// тут хранится загруженная по кнопке информация о 2х файлах

// //центральный объект
// let objData1 = null;
// //орбитальные объекты
// let objData2 = null;

const objectsData = [null, null];

const fileInput = document.getElementById('fileInput');
fileInput.addEventListener('change', async (event) => {
    const file1 = event.target.files[0];
    const file2 = event.target.files[1];
    if (file1) {
        objectsData[0] = await loadAndParseObj(file1);
    }
    if (file2) {
        objectsData[1] = await loadAndParseObj(file2)
    }
    if(objectsData[0] && objectsData[1]) {
        initObj(gl, objectsData[0]);
        initObj(gl, objectsData[1]);
    }
});

function initObj(gl, objData) {
    const positionBuffer = gl.createBuffer();
     gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);
     gl.bufferData(gl.ARRAY_BUFFER, objData.positions, gl.STATIC_DRAW);
 
     const normalBuffer = gl.createBuffer();
     gl.bindBuffer(gl.ARRAY_BUFFER, normalBuffer);
     gl.bufferData(gl.ARRAY_BUFFER, objData.normals, gl.STATIC_DRAW);
 
     const textureCoordBuffer = gl.createBuffer();
      gl.bindBuffer(gl.ARRAY_BUFFER, textureCoordBuffer);
      gl.bufferData(gl.ARRAY_BUFFER, objData.textureCoords, gl.STATIC_DRAW);
 
     const indexBuffer = gl.createBuffer();
     gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
     gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, objData.indices, gl.STATIC_DRAW);
 
    objData.positionBuffer = positionBuffer;
    objData.normalBuffer = normalBuffer;
    objData.textureCoordBuffer = textureCoordBuffer;
    objData.indexBuffer = indexBuffer;
};


async function loadAndParseObj(file) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = async (event) => {
          const objText = event.target.result;
          try {
              const objData = parseObj(objText);
              resolve(objData);
          }
          catch(e){
              reject(e)
          }
      };
      reader.onerror = () => {
          reject("Ошибка чтения файла.");
      }
      reader.readAsText(file);
    });
}


function parseObj(objText) {
    const lines = objText.split('\n');
    const vertices = [];
    const normals = [];
    const textures = [];
    const faces = [];
  
    for (const line of lines) {
        const parts = line.trim().split(/\s+/);
        if (parts.length === 0) continue;
  
          const type = parts[0];
          const data = parts.slice(1);
  
          switch (type) {
              case 'v':
                  vertices.push(data.map(parseFloat));
                  break;
              case 'vn':
                  normals.push(data.map(parseFloat));
                  break;
              case 'vt':
                  textures.push(data.map(parseFloat));
                  break;
              case 'f':
                  const face = data.map(part => {
                      const indices = part.split('/').map(index => index ? parseInt(index) - 1: null);
                      return indices;
                  });
                  faces.push(face);
                  break;
               default:
                   break;
          }
      }
      return  processData(vertices, normals, textures, faces);
};

function processData(vertices, normals, textures, faces) {
    const positions = [];
    const normalData = [];
    const textureCoords = [];
    const indices = [];
  
  
    for(const face of faces){
        const baseIndex = positions.length / 3;
        if(face.length === 3) { // Triangles
            for(const point of face){
                positions.push(...vertices[point[0]]);

                if(normals.length)
                   normalData.push(...normals[point[2] || 0]);
                else 
                   normalData.push(0,0,1);
                if(textures.length)
                    textureCoords.push(...textures[point[1] || 0]);
                else
                   textureCoords.push(0,0)

            }
  
            //indices.push(indices.length, indices.length+1, indices.length+2)
            indices.push(baseIndex, baseIndex + 1, baseIndex + 2);
        }
        else if(face.length === 4) { // Quad to 2 triangles
            const v1 = face[0];
            const v2 = face[1];
            const v3 = face[2];
            const v4 = face[3];
  
// Triangle 1 (v1, v2, v3)
            positions.push(...vertices[v1[0]]);
            textureCoords.push(...(textures.length ? textures[v1[1] || 0] : [0, 0]));
            normalData.push(...(normals.length? normals[v1[2] || 0] : [0, 0, 1]));
            
            positions.push(...vertices[v2[0]]);
            textureCoords.push(...(textures.length ? textures[v2[1] || 0] : [0, 0]));
            normalData.push(...(normals.length? normals[v2[2] || 0] : [0, 0, 1]));
            
            positions.push(...vertices[v3[0]]);
            textureCoords.push(...(textures.length ? textures[v3[1] || 0] : [0, 0]));
            normalData.push(...(normals.length? normals[v3[2] || 0] : [0, 0, 1]));

            //indices.push(indices.length, indices.length+1, indices.length+2);
            indices.push(baseIndex, baseIndex + 1, baseIndex + 2);

// Triangle 2 (v1, v3, v4)
            positions.push(...vertices[v1[0]]);
            textureCoords.push(...(textures.length ? textures[v1[1] || 0] : [0, 0]));
            normalData.push(...(normals.length? normals[v1[2] || 0] : [0, 0, 1]));
            
            positions.push(...vertices[v3[0]]);
            textureCoords.push(...(textures.length ? textures[v3[1] || 0] : [0, 0]));
            normalData.push(...(normals.length? normals[v3[2] || 0] : [0, 0, 1]));
            
            positions.push(...vertices[v4[0]]);
            textureCoords.push(...(textures.length ? textures[v4[1] || 0] : [0, 0]));
            normalData.push(...(normals.length? normals[v4[2] || 0] : [0, 0, 1]));
            
            indices.push(indices.length, indices.length+1, indices.length+2)
            //indices.push(baseIndex, baseIndex + 2, baseIndex + 3);
        }
    }
  
     // console.debug(textureCoords);
  
    return {
        positions: new Float32Array(positions),
        normals: new Float32Array(normalData),
        textureCoords: new Float32Array(textureCoords),
        indices: new Uint32Array(indices),
    };
};

console.debug('objLoader.js compile');