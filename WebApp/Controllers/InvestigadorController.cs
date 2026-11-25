using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class InvestigadorController : Controller
    {
        static Sistema s = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string nombre, string email, string pass)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(pass))
            {
                ViewBag.msg = "Nombre, email y contraseña son requeridos.";
                return View();
            }

            try
            {
                Investigador investigador = new Investigador(email, pass, nombre, Rol.Detective);
                s.AltaInvestigador(investigador);
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();
            }
        }

        public IActionResult VerPerfil()
        {
            if(HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            try
            {
                Investigador investigador = s.BuscarInvestigadorXMail(HttpContext.Session.GetString("user"));
                return View(investigador);
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();
            }
        }
    }
}
