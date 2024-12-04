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
        ViewBag.IdUsuario = userId;
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

    [HttpGet]
    public IActionResult ObtenerSugerencias(string dato)
    {
        if (string.IsNullOrWhiteSpace(dato))
            return Json(new { universidades = new List<string>(), carreras = new List<string>() });

        var resultados = DB.Busqueda(dato);

        var universidades = resultados.Universidadesr.Select(u => u.Nombre).ToList();
        var carreras = resultados.Carrerasr.Select(c => c.Nombre).ToList();

        return Json(new { universidades, carreras });
    }


    public IActionResult Busqueda(string dato)
    {
        ViewBag.DatoBuscado = dato;
        ViewBag.Resultados = DB.Busqueda(dato);
        return View();
    }

    public IActionResult Ayuda()
    {
        return View();
    }

    public IActionResult Perfil(int id)
    {
        ViewBag.estudiante = DB.MostrarInfoEst(id);
        ViewBag.Usuario = DB.ObtenerUsuarioPorId(id);
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
        var preguntas = DB.ObtenerPreguntasTest(); 

        // Dividir las preguntas en 3 partes (slides)
        ViewBag.PreguntasDivididas = preguntas
            .Select((pregunta, index) => new { Pregunta = pregunta, Index = index })
            .GroupBy(x => x.Index / 10) 
            .Select(g => g.Select(x => x.Pregunta).ToList())
            .ToList();

        return View();
    }

    public IActionResult ResultadoTest(List<int> preguntasSeleccionadas)
    {
        if (preguntasSeleccionadas == null || !preguntasSeleccionadas.Any())
        {
            ViewBag.MensajeError = "No seleccionaste ninguna opción. Por favor, intenta nuevamente.";
            return View();
        }

        Dictionary<int, int> contadorCarreras = new Dictionary<int, int>();
        var preguntas = DB.ObtenerPreguntasTest(); 

        foreach (var idPregunta in preguntasSeleccionadas)
        {
            var pregunta = preguntas.FirstOrDefault(p => p.Id == idPregunta);

            if (pregunta != null)
            {
                int idCarrera = pregunta.IdCarrera;  

                if (contadorCarreras.ContainsKey(idCarrera))
                    contadorCarreras[idCarrera]++;
                else
                    contadorCarreras[idCarrera] = 1;
            }
        }

        // Buscar las carreras con mayor cantidad de selecciones
        int maxSeleccion = contadorCarreras.Values.Max();
        List<int> idsMax = contadorCarreras.Where(kv => kv.Value == maxSeleccion)
                                        .Select(kv => kv.Key)
                                        .ToList();

        ViewBag.CarrerasGanadoras = idsMax;
        ViewBag.Carreras = DB.ObtenerCarreras(); 

        return View();
    }


}