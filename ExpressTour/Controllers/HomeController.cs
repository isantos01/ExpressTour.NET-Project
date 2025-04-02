using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpressTour.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "Bienvenido a ExpressTour"; // Agregado para que funcione en la vista
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewData["Title"] = "Acerca de ExpressTour"; // Agregado para la vista
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewData["Title"] = "Contacto - ExpressTour"; // Agregado para la vista
            return View();
        }
    }
}
