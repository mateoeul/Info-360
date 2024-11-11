function mostrarSeccion(seccion) {
    // Cambiar el contenido visible
    document.getElementById('contenidoPublicaciones').classList.toggle('contenido-activo', seccion === 'publicaciones');
    document.getElementById('contenidoReels').classList.toggle('contenido-activo', seccion === 'reels');
    
    // Ocultar la sección que no está seleccionada
    document.getElementById('contenidoPublicaciones').classList.toggle('contenido-oculto', seccion !== 'publicaciones');
    document.getElementById('contenidoReels').classList.toggle('contenido-oculto', seccion !== 'reels');

    // Cambiar el estilo del botón activo
    document.getElementById('botonPublicaciones').classList.toggle('boton-activo', seccion === 'publicaciones');
    document.getElementById('botonReels').classList.toggle('boton-activo', seccion === 'reels');
}