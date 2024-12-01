document.addEventListener("DOMContentLoaded", () => {
    const contenedores = document.querySelectorAll('.contenedor-busqueda');
    
    contenedores.forEach(contenedor => {
        let isDown = false;
        let startX;
        let scrollLeft;

        contenedor.addEventListener('mousedown', (e) => {
            isDown = true;
            contenedor.classList.add('active');
            startX = e.pageX - contenedor.offsetLeft;
            scrollLeft = contenedor.scrollLeft;
            e.preventDefault(); // Evita comportamientos predeterminados como selección de texto
        });

        contenedor.addEventListener('mouseleave', () => {
            isDown = false;
            contenedor.classList.remove('active');
        });

        contenedor.addEventListener('mouseup', () => {
            isDown = false;
            contenedor.classList.remove('active');
        });

        contenedor.addEventListener('mousemove', (e) => {
            if (!isDown) return;
            e.preventDefault(); // Evita comportamientos predeterminados mientras se arrastra
            const x = e.pageX - contenedor.offsetLeft;
            const walk = (x - startX) * 2; // Ajusta la velocidad de desplazamiento
            contenedor.scrollLeft = scrollLeft - walk;
        });

        // Evita que las imágenes interfieran con el arrastre
        contenedor.querySelectorAll('img').forEach(img => {
            img.addEventListener('dragstart', (e) => e.preventDefault());
        });
    });
});
