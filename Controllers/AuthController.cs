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

        // Acción para mostrar la vista de login
        public IActionResult Login()
        {
            // Si el usuario ya está logueado (sesión activa), redirige al home
            if (HttpContext.Session.GetString("user") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Acción para verificar el login
        [HttpPost]
        public IActionResult VerificarLogin(string email, string password)
        {
            // Llama al método que verifica si las credenciales son correctas
            if (DB.VerificarLogin(email, password))
            {
                // Obtener al usuario desde la base de datos por su email (esto depende de tu implementación en DB)
                var usuario = DB.ObtenerUsuarioPorEmail(email); // Asegúrate de tener este método en DB

                // Guardamos los datos del usuario en la sesión
                HttpContext.Session.SetString("user", usuario.ToString());
                HttpContext.Session.SetInt32("userId", usuario.Id); // Guarda el ID de usuario en la sesión

                // Redirige al home
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Si las credenciales son incorrectas, muestra un mensaje de error
                ViewBag.Error = "Email o contraseña incorrectos.";
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
