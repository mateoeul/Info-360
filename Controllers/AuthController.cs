using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Para HttpContext y Session
using Uni_.Models; // Asegúrate de que tienes el namespace correcto para 'Usuarios' y tu lógica de base de datos
using System.Linq;

namespace Uni_.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }


    public IActionResult Login(string? returnUrl = null)
    {
        if (HttpContext.Session.GetString("user") != null)
        {
            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            
        }

        ViewBag.ReturnUrl = returnUrl; // Guardar la URL de retorno
        return View();
    }

    [HttpPost]
    public IActionResult VerificarLogin(string email, string password, string? returnUrl = null)
    {
        if (DB.VerificarLogin(email, password))
        {
            var usuario = DB.ObtenerUsuarioPorEmail(email);

            HttpContext.Session.SetString("user", usuario.ToString());
            HttpContext.Session.SetInt32("userId", usuario.Id);

            // Redirige a la URL original o al Home si no hay URL original
            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
        }
        else
        {
            ViewBag.Error = "Email o contraseña incorrectos.";
            ViewBag.ReturnUrl = returnUrl;
            return View("Login");
        }
    }


        // Acción para cerrar sesión
        public IActionResult Logout()
        {
            // Elimina los datos del usuario de la sesión
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("userId");

            // Redirige a la página de login
            return RedirectToAction("Login");
        }
    }
}
