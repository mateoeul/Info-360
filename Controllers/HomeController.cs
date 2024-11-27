using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Uni_.Models;

namespace Uni_.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /*
    public IActionResult Registro(char tipo)
    {
        switch(tipo)
        {
            case 'E':
            return RedirectToAction("RegistroEst");
            
            case 'U':
            return RedirectToAction("RegistroUni");
            
            case 'P':
            return RedirectToAction("RegistroProf");

            default:
            return View("Index");
        }
    }
    public IActionResult RegistroProf(Profesores profesor, Usuarios usuario)
    {
        DB.RegistroProf(profesor);
        DB.RegistroUsuario(usuario);
        return RedirectToAction("Index");
    }
    public IActionResult RegistroUni(Universidades universidad, Usuarios usuario)
    {
        DB.RegistroUni(universidad);
        DB.RegistroUsuario(usuario);
        return RedirectToAction("Index");
    }*/

    public IActionResult Index()
    {
        ViewBag.User = Usuarios.FromString(HttpContext.Session.GetString("user"));
        if(ViewBag.User is null)
        {
            return RedirectToAction("Login", "Auth");
        }
        return View();
    }

    [HttpGet]
    public IActionResult RegistrarUsuario()
    {
        // Crear un modelo vacío de usuario
        var usuario = new Usuarios();
        return View(usuario);
    }

    [HttpPost]
    public IActionResult RegistrarUsuario(Usuarios usuario)
    {
        // Asegúrate de que no se guarde el Id del Usuario en el JSON
        usuario.Id = 0; // Esto asegura que no se pase el Id al registrar el usuario

        // Serializa el objeto y lo guarda en la sesión
        HttpContext.Session.SetString("UsuarioTemp", JsonSerializer.Serialize(usuario));

        // Verifica que los datos se guardaron correctamente
        var usuarioJson = HttpContext.Session.GetString("UsuarioTemp");
        if (usuarioJson is null)
        {
            // Si no se encuentra en la sesión, redirige de nuevo
            return RedirectToAction("RegistrarUsuario");
        }

        // Redirige al siguiente paso
        return RedirectToAction("RegistroEstudiante");
    }

    [HttpGet]
    public IActionResult RegistroEstudiante()
    {
        // Crear un modelo vacío de estudiante o inicializar según sea necesario
        ViewBag.Carreras = DB.ObtenerCarreras();
        var estudiante = new Estudiantes();
        return View(estudiante);
    }

    [HttpPost]
    public IActionResult RegistroEstudiante(Estudiantes estudiante)
    {
        // Recupera el usuario desde la sesión
        string? usuarioJson = HttpContext.Session.GetString("UsuarioTemp");

        if (usuarioJson is null)
        {
            // Si no existe el usuario, redirigir al primer paso
            return RedirectToAction("RegistrarUsuario");
        }

        // Deserializa el usuario desde la sesión
        Usuarios usuario = JsonSerializer.Deserialize<Usuarios>(usuarioJson);

        // Si el usuario es nulo, maneja el caso de error
        if (usuario == null)
        {
            return RedirectToAction("RegistrarUsuario");
        }

        // Registrar el usuario en la base de datos
        int userId = DB.RegistroUsuario(usuario);

        Console.WriteLine(userId);
        

        // Asocia el Id del usuario al estudiante
        estudiante.IdUsuario = userId;

        // Registrar al estudiante con el IdUsuario correctamente asignado
        DB.RegistroEst(estudiante);

        // Limpiar la sesión
        HttpContext.Session.Remove("UsuarioTemp");

        // Redirigir a login
        return RedirectToAction("Login", "Auth");
    }


    public IActionResult ActualizarInfo(Estudiantes estudiante, string nombre, string apellido, string foto, DateOnly fechaNac, string carrera, string cursada)
    {
        DB.ActualizarInfoEst(estudiante, nombre, apellido, foto, fechaNac, carrera, cursada);
        return RedirectToAction("PerfilEst");
    }
    public IActionResult Busqueda(string dato)
    {
        
        ViewBag.DatoBuscado = dato;
        ViewBag.Resultados = DB.Busqueda(dato);
        return View();
    }
    public IActionResult PerfilEst(int id)
    {
        ViewBag.estudiante = DB.MostrarInfoEst(id);
        return View();
    }
    public IActionResult PerfilUni(int id)
    {
        ViewBag.universidad = DB.MostrarInfoUni(id);
        ViewBag.becas = DB.BecasXUni(id);
        ViewBag.Condiciones = DB.CondicionesXUni(id);
        return View();
    }
    public IActionResult CompararCarreras(int id1, int id2)
    {
        ViewBag.carrera1 = DB.MostrarInfoCarrera(id1);
        ViewBag.carrera2 = DB.MostrarInfoCarrera(id2);
        return View();
    }
    public IActionResult Test()
    {
        ViewBag.Preguntas = DB.ObtenerPreguntasTest();
        return View();
    }
    public IActionResult ResultadoTest(List<int> preguntasSeleccionadas)
    {
        if (preguntasSeleccionadas == null || !preguntasSeleccionadas.Any())
        {
            Console.WriteLine("No se recibieron preguntas seleccionadas.");
            ViewBag.MensajeError = "No seleccionaste ninguna opción. Por favor, intenta nuevamente.";
            return View();
        }

        Console.WriteLine("IDs seleccionados: " + string.Join(", ", preguntasSeleccionadas));

        // Diccionario para contar las ocurrencias de cada carrera
        Dictionary<int, int> contador = new Dictionary<int, int>();

        foreach (var idCarrera in preguntasSeleccionadas)
        {
            if (contador.ContainsKey(idCarrera))
                contador[idCarrera]++;
            else
                contador[idCarrera] = 1;
        }

        Console.WriteLine("Contador: " + string.Join(", ", contador.Select(kv => $"Carrera {kv.Key}: {kv.Value}")));

        // Buscar las carreras con mayor cantidad de selecciones
        int maxSeleccion = contador.Values.Max();
        List<int> idsMax = contador.Where(kv => kv.Value == maxSeleccion)
                                    .Select(kv => kv.Key)
                                    .ToList();

        // Recuperar las carreras ganadoras
        ViewBag.CarrerasGanadoras = idsMax;
        ViewBag.Carreras = DB.ObtenerCarreras(); // Obtiene todas las carreras de la DB

        return View();
    }

}