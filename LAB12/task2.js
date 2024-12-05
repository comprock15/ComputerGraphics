function task2() {
    console.log("Здесь пишем код задания 2");

    // Пример использования слайдера
    let slider = document.getElementById("slider1");
    slider.oninput = () => {
        console.log("Слайдер задания 2: " + slider.value);
    }
}