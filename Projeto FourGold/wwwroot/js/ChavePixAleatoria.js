const select = document.getElementById("tipoChave");

const divCampo = document.getElementById("campoChave");

select.addEventListener("change", () => {
    if (select.options[select.selectedIndex].value == 3)
        divCampo.style.display = "none"
})