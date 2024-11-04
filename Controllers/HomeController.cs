using System.Diagnostics;
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
        return View();
    }


    public IActionResult TipoRegistro(char tipo);

    public IActionResult Registro(string nombre, string apellido, string foto, string nombreUsuario, string fechaNac, string mail, string bio, string cursada)
    {

        switch(tipo)
        {
    
            case 'E':
            return RedirectToAction("RegistroEst");
            
            case 'U':
            return RedirectToAction("RegistroUni");
            
            case 'E':
            return RedirectToAction("RegistroProf");
            
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
        DB.RegistroEst(universidad);
        DB.RegistroUsuario(usuario);
        return RedirectToAction("Index");
    }
    public IActionResult ActualizarInfo(string nombre, string apellido, string foto, string fechaNac, string mail, string bio, string cursada)
    {
        DB.ActualizarInfoEst(nombre, apellido, foto, nombreUsuario, fechaNac, mail, bio, cursada);
        return RedirectToAction("PerfilEst");
    }
    public IActionResult Busqueda(string dato)
    {
        ViewBag.resultados = DB.Busqueda(dato);
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
    }

    public IActionResult ResultadoTest(List<Preguntas> rtas)
    {
        int idMax;
        int cantId;
        List<int> ids = new List<int>();
        for (int i = 0; i < rtas.Count; i++)
        {
            if (rtas[i].Marcada)
            {
                ids.Add(rtas[i].IdCarrera); 
            }  
                     
        }       
    }

}