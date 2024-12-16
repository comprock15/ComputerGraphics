let leftWallRadios = document.querySelectorAll('input[name=leftwall]');
let rightWallRadios = document.querySelectorAll('input[name=rightwall]');
let upWallRadios = document.querySelectorAll('input[name=upwall]');
let downWallRadios = document.querySelectorAll('input[name=downwall]');
let frontWallRadios = document.querySelectorAll('input[name=frontwall]');
let backWallRadios = document.querySelectorAll('input[name=backwall]');

let sphere1Radios = document.querySelectorAll('input[name=sphere1]');
let sphere2Radios = document.querySelectorAll('input[name=sphere2]');
let box1Radios = document.querySelectorAll('input[name=cube1]');
let box2Radios = document.querySelectorAll('input[name=cube2]');

function changeHandler(radio, obj) {
    if (radio.value == 'diffuse') {
        obj.properties.reflectivity = false;
        obj.properties.transparency = false;
    }
    else if (radio.value == 'reflective') {
        obj.properties.reflectivity = true;
        obj.properties.transparency = false;
    }
    else if (radio.value == 'transparent') {
        obj.properties.reflectivity = false;
        obj.properties.transparency = true;
    }
    console.log(obj.properties);
}

Array.prototype.forEach.call(leftWallRadios, function(radio) {
    radio.onchange = () => changeHandler(radio, leftWall);
});

Array.prototype.forEach.call(rightWallRadios, function(radio) {
    radio.onchange = () => changeHandler(radio, rightWall);
});

Array.prototype.forEach.call(upWallRadios, function(radio) {
    radio.onchange = () => changeHandler(radio, upWall);
});

Array.prototype.forEach.call(downWallRadios, function(radio) {
    radio.onchange = () => changeHandler(radio, downWall);
});
 
Array.prototype.forEach.call(frontWallRadios, function(radio) {
    radio.onchange = () => changeHandler(radio, frontWall);
});

Array.prototype.forEach.call(backWallRadios, function(radio) {
    radio.onchange = () => changeHandler(radio, backWall);
});


Array.prototype.forEach.call(sphere1Radios, function(radio) {
    radio.onchange = () => changeHandler(radio, sphere1);
});

Array.prototype.forEach.call(sphere2Radios, function(radio) {
    radio.onchange = () => changeHandler(radio, sphere2);
});

Array.prototype.forEach.call(box1Radios, function(radio) {
    radio.onchange = () => changeHandler(radio, box1);
});

Array.prototype.forEach.call(box2Radios, function(radio) {
    radio.onchange = () => changeHandler(radio, box2);
});