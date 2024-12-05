function task3() {
    console.log("Здесь пишем код задания 3");

    // Пример использования слайдера
    let slider = document.getElementById("slider2");
    slider.oninput = () => {
        console.log("Слайдер задания 3: " + slider.value);
    }
}