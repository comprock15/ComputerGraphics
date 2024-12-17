// Загрузка одного изображения
function loadImage(url, callback) {
  let image = new Image();
  image.src = url;
  image.onload = callback;
  return image;
}

// Загрузка массива изображений с вызовом callback по завершению
function loadImages(urls, callback) {
  let images = [];
  let imagesToLoad = urls.length;

  const onImageLoad = () => {
    if (--imagesToLoad === 0) callback(images);
  };

  for (let i = 0; i < urls.length; ++i)
    images.push(loadImage(urls[i], onImageLoad));
  return images;
}

// Активные текстурные блоки
const texture_active = [gl.TEXTURE0, gl.TEXTURE1];
const textures = [];

// Загрузка изображений и рендер текстур
const images = loadImages(["Cow_BaseColor.png", "Hamburger_BaseColor.png"], renderTexture);

function renderTexture() {
  for (let i = 0; i < images.length; ++i) {
    const texture = gl.createTexture();
    gl.bindTexture(gl.TEXTURE_2D, texture);

    // Параметры текстуры
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.NEAREST);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.NEAREST);

    // Загрузка изображения в текстуру
    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, images[i]);
    textures.push(texture);
  }
}
