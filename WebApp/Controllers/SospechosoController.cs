using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class SospechosoController : Controller
    {
        static Sistema s = Sistema.Instancia;
        public IActionResult Index()
        {
            try
            {
                List<Sospechoso> lista = s.ListaDeSospechosos();
                ViewBag.listaSospechosos = lista;
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }

        public IActionResult Filtrar(int edad)
        {
            try
            {
                List<Sospechoso> listaFiltrada = s.FiltrarSospechososXEdad(edad);
                ViewBag.listaSospechosos = listaFiltrada;
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            ViewBag.edad = edad;
            return View("Index");
        }
    }
}
