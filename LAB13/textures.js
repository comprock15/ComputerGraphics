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

const texture_active = [gl.TEXTURE0, gl.TEXTURE1];
const textures = [];
let images = loadImages([
    "Cow_BaseColor.png",
    "Hamburger_BaseColor.png",
    ], renderTexture);


function renderTexture() {
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
}