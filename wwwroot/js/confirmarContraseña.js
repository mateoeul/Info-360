// Escuchar el evento submit del formulario
document.querySelector('form').addEventListener('submit', function (event) {
    // Obtener los valores de los campos
    const password = document.getElementById('Contraseña').value;
    const confirmPassword = document.getElementById('ConfirmarContraseña').value;

    // Obtener el mensaje de error
    const errorMessage = document.getElementById('error-message');

    // Validar si las contraseñas coinciden
    if (password !== confirmPassword) {
        event.preventDefault(); // Detener el envío del formulario
        errorMessage.style.display = 'block'; // Mostrar el mensaje de error
        document.getElementById('ConfirmarContraseña').style.borderColor = 'red'; // Resaltar el campo
    } else {
        errorMessage.style.display = 'none'; // Ocultar el mensaje de error si coincide
        document.getElementById('ConfirmarContraseña').style.borderColor = ''; // Quitar el borde rojo
    }
});
