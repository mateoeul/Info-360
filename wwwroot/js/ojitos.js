    // Mostrar/ocultar contraseñas
    document.querySelectorAll('.toggle-password').forEach(span => {
        span.addEventListener('click', () => {
            const input = document.querySelector(span.getAttribute('data-target'));
            const icon = span.querySelector('i');

            if (input.type === 'password') {
                input.type = 'text';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            } else {
                input.type = 'password';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            }
        });
    });