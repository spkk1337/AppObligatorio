using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CasosController : Controller
    {
        static Sistema s = Sistema.Instancia;
        public IActionResult Index()
        {
            try 
            {
                if(HttpContext.Session.GetString("rol") == Rol.Detective.ToString())
                {
                    List<Caso> lista = s.ObtenerCasosPorInvestigador(HttpContext.Session.GetString("user"));
                    ViewBag.listaCasos = lista;
                }
                else
                {
                    List<Caso> lista = s.ListaDeCasosOrdenados();
                    ViewBag.listaCasos = lista;
                }
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("rol") != Rol.Fiscal.ToString())
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Sospechosos = s.Sospechosos;
            ViewBag.Investigadores = s.Investigadores;

            return View();
        }

        [HttpPost]

        public IActionResult Create(string nombre, string desc, bool cerrado, string sospechosoCedula, string investigadorEmail)
        {
            if (HttpContext.Session.GetString("rol") != Rol.Fiscal.ToString())
        {
                return RedirectToAction("Index", "Login");
            }

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(desc))
            {
                ViewBag.msg = "El nombre y la descripción son requeridos.";
                ViewBag.Sospechosos = s.Sospechosos;
                ViewBag.Investigadores = s.Investigadores;
                return View();
            }

            try
            {
                Caso caso = new Caso(nombre, desc, cerrado, s.ObtenerSospechosoXCedula(sospechosoCedula), s.ObtenerInvestigadorXMail(investigadorEmail), new List<Evidencia>());
                s.AltaCaso(caso);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                ViewBag.Sospechosos = s.Sospechosos;
                ViewBag.Investigadores = s.Investigadores;
                return View();
            }
        }

        public IActionResult Detalles(int id)
        {
            if (HttpContext.Session.GetString("rol") != Rol.Fiscal.ToString())
            {
                return RedirectToAction("Index", "Login");
            }

            Caso caso = s.ObtenerCasoPorId(id);
            if (caso == null)
            {
                return RedirectToAction("Index");
            }

            return View(caso);
        }
    }
}
