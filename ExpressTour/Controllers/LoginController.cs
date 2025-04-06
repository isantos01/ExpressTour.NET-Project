using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
    public class LoginController : Controller
    {
        private readonly ExpressTourDataContext db = new ExpressTourDataContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            var usuarioValido = db.Usuarios.FirstOrDefault(u =>
                u.NombreUsuario == user.NombreUsuario && u.Contrasena == user.Contrasena);

            if (usuarioValido != null)
            {
                Session["Usuario"] = usuarioValido.NombreUsuario;
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View("Index");
        }
    }
}