using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private static Sistema s = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(string email, string pass)
        {
            try 
            {
                
               if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(pass))
                {
                    Investigador i = s.Login(email, pass);
                    if(i == null)
                    {
                        ViewBag.msg = "Usuario y/o contraseña incorrecto/s";
                        return View("Index");
                    }

                    HttpContext.Session.SetString("user", i.Email);
                    HttpContext.Session.SetString("rol", i.Rol.ToString());
                    return RedirectToAction("Index", "Investigador");
                }
                else
                {
                    throw new Exception("Email y contraseña son requeridos");
                    return View("Index");
                }
            } 
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
