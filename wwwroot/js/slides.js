// Inicializamos las variables
let currentSlide = 0; // Slide actual
const totalSlides = 3; // Total de slides

// Función para avanzar al siguiente slide
function avanzarSlide() {
    if (currentSlide < totalSlides - 1) {
        // Ocultar el slide actual
        document.getElementById(`slide-${currentSlide}`).style.display = "none";
        
        // Avanzar al siguiente slide
        currentSlide++;
        
        // Mostrar el siguiente slide
        document.getElementById(`slide-${currentSlide}`).style.display = "block";
        
        // Actualizar el contador de la página
        document.getElementById("pagina-actual").textContent = currentSlide + 1;
    } else {
        // Cuando llegamos al último slide, cambiar el texto de "Continuar" a "Finalizar"
        document.getElementById("btnContinuar").textContent = "Finalizar";
        document.getElementById("btnContinuar").setAttribute("type", "submit");
    }
}

// Asegurarse de que el botón "Continuar" ejecuta la función cuando se hace clic
document.getElementById("btnContinuar").addEventListener("click", avanzarSlide);
