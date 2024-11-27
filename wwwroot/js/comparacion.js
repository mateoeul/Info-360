// Variables para almacenar las carreras seleccionadas
let selectedCareers = [];

// Obtener todos los checkboxes en la página
const checkboxes = document.querySelectorAll(".compare-checkbox");

// Contenedor de la barra de comparación
const comparisonBar = document.getElementById("comparison-bar");
const comparisonList = document.getElementById("comparison-list");

// Agregar evento a cada checkbox
checkboxes.forEach((checkbox) => {
    checkbox.addEventListener("change", function () {
        const id = this.dataset.id;
        const nombre = this.dataset.nombre;
        const logo = this.dataset.logo;

        if (this.checked) {
            // Agregar carrera seleccionada
            if (selectedCareers.length < 2) {
                selectedCareers.push({ id, nombre, logo });
                updateComparisonBar();
            } else {
                this.checked = false; // No permitir más de 2 selecciones
                alert("Solo puedes seleccionar hasta 2 carreras para comparar.");
            }
        } else {
            // Quitar carrera seleccionada
            selectedCareers = selectedCareers.filter((c) => c.id !== id);
            updateComparisonBar();
        }
    });
});

// Actualizar la barra de comparación
function updateComparisonBar() {
    // Si no hay carreras seleccionadas, ocultar la barra
    if (selectedCareers.length === 0) {
        comparisonBar.classList.add("hidden");
        comparisonList.innerHTML = "";
        return;
    }

    // Mostrar la barra
    comparisonBar.classList.remove("hidden");

    // Mostrar las carreras seleccionadas
    comparisonList.innerHTML = selectedCareers
        .map(
            (career) => `
            <div class="selected-career">
                <img src="${career.logo}" alt="${career.nombre}" />
                <span>${career.nombre}</span>
            </div>
        `
        )
        .join("");
}

// Botón para comparar
const compareButton = document.createElement("button");
compareButton.textContent = "Comparar Carreras";
compareButton.className = "compare-button";
compareButton.addEventListener("click", function () {
    if (selectedCareers.length === 2) {
        const url = `/Home/CompararCarreras?id1=${selectedCareers[0].id}&id2=${selectedCareers[1].id}`;
        window.location.href = url; // Redirigir a la acción en el controlador
    } else {
        alert("Debes seleccionar exactamente 2 carreras para comparar.");
    }
});

// Agregar el botón a la barra de comparación
comparisonBar.appendChild(compareButton);
