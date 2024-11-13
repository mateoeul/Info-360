using System.Diagnostics;
using System.Text;
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

    /*public IActionResult Index()
    {
        return View();
    }
    public IActionResult InicioSesion()
    {
        return View();
    }
    public IActionResult Perfil()
    {
        return View();
    }

    public IActionResult Test()
    {
        return View();
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
    public IActionResult RegistroEst(Estudiantes estudiante, Usuarios usuario)
    {
        DB.RegistroEst(estudiante);
        DB.RegistroUsuario(usuario);
        return RedirectToAction("Index");
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
    }
    public IActionResult ActualizarInfo(int id, string nombre, string apellido, string foto, string fechaNac, string bio, string cursada)
    {
        DB.ActualizarInfoEst(nombre, apellido, foto, fechaNac, bio, cursada);
        return RedirectToAction("PerfilEst");
    }
    public IActionResult Busqueda(string dato, Busqueda busqueda)
    {
        ViewBag.resultados = DB.Busqueda(dato, busqueda);
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
    public IActionResult ResultadoTest(List<Preguntas> rtas)
    {
        List<int> idsMax = new List<int>();
        int cantId = 0;
        Dictionary<int, int> contador = new Dictionary<int, int>();
        for (int i = 0; i < rtas.Count; i++)
        {
            if (rtas[i].Marcada){
                if(contador.ContainsKey(rtas[i].IdCarrera))
                contador[rtas[i].IdCarrera]++;
                else
                contador.Add(rtas[i].IdCarrera, 1); 
            }   
        }      
        foreach(var par in contador){
            if(par.Value > cantId){
                idsMax.Clear();
                idsMax.Add(par.Key);
                cantId = par.Value;
            } else if (par.Value == cantId){
                idsMax.Add(par.Key);
            }
        }
        return View();
    }
}