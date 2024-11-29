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

public IActionResult Index()
{
    ViewBag.isUserLogged = HttpContext.Session.GetString("user") != null;
    return View();
}




    [HttpGet]
    public IActionResult RegistrarUsuario()
    {
        var usuario = new Usuarios();
        return View(usuario);
    }

    [HttpPost]
    public IActionResult RegistrarUsuario(Usuarios usuario)
    {  
        usuario.Id = 0; 

        HttpContext.Session.SetString("UsuarioTemp", JsonSerializer.Serialize(usuario));

        var usuarioJson = HttpContext.Session.GetString("UsuarioTemp");
        if (usuarioJson is null)
        {
            return RedirectToAction("RegistrarUsuario");
        }

        return RedirectToAction("RegistroEstudiante");
    }

    [HttpGet]
    public IActionResult RegistroEstudiante()
    {
        ViewBag.Carreras = DB.ObtenerCarreras();
        var estudiante = new Estudiantes();
        return View(estudiante);
    }

    [HttpPost]
    public IActionResult RegistroEstudiante(Estudiantes estudiante)
    {
        string? usuarioJson = HttpContext.Session.GetString("UsuarioTemp");

        if (usuarioJson is null)
        {
            return RedirectToAction("RegistrarUsuario");
        }

        Usuarios usuario = JsonSerializer.Deserialize<Usuarios>(usuarioJson);

        if (usuario == null)
        {
            return RedirectToAction("RegistrarUsuario");
        }

        int userId = DB.RegistroUsuario(usuario);

        Console.WriteLine(userId);

        estudiante.IdUsuario = userId;

        DB.RegistroEst(estudiante);

        HttpContext.Session.Remove("UsuarioTemp");

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
    public IActionResult Perfil(int id)
    {
        Console.WriteLine(id);
        ViewBag.estudiante = DB.MostrarInfoEst(id);
        //ViewBag.Usuario = DB.ObtenerUsuarioPorId(ViewBag.estudiante.IdUsuario);
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