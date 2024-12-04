$(document).ready(function () {
    console.log("Script cargado correctamente");

    $('#buscador').on('keyup', function () {
        console.log("Evento keyup disparado");
        let query = $(this).val();

        if (query.length > 2) {
            $.ajax({
                url: '/Home/ObtenerSugerencias', // Cambia "Home" si tu controlador tiene otro nombre
                type: 'GET',
                data: { dato: query },
                success: function (data) {
                    console.log("Resultados obtenidos:", data);
                    let resultados = $('#resultados'); // Asegúrate de tener un contenedor para mostrar los resultados
                    resultados.empty();

                    if (data.universidades.length > 0 || data.carreras.length > 0) {
                        resultados.append('<div><strong>Universidades</strong></div>');
                        data.universidades.forEach(function (item) {
                            resultados.append(`<div>${item}</div>`);
                        });

                        resultados.append('<div><strong>Carreras</strong></div>');
                        data.carreras.forEach(function (item) {
                            resultados.append(`<div>${item}</div>`);
                        });
                    } else {
                        resultados.append('<div>No se encontraron resultados</div>');
                    }
                },
                error: function () {
                    console.error("Error al obtener los resultados");
                }
            });
        } else {
            $('#resultados').empty();
        }
    });
});
