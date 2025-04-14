using System.IO;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Services;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService = new UsuarioService();

        public ActionResult Index()
        {
            var usuarios = _usuarioService.ObtenerUsuarios();
            return View(usuarios);
        }

        public ActionResult Create()
        {
            return PartialView("_CreateUsuario", new UsuarioViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    html = RenderPartialViewToString("_CreateUsuario", model)
                });
            }

            _usuarioService.AgregarUsuario(model);
            TempData["Success"] = "Usuario creado correctamente.";
            return Json(new { success = true });
        }

        public ActionResult Edit(int id)
        {
            var usuario = _usuarioService.ObtenerUsuarios().FirstOrDefault(u => u.Id == id);
            return PartialView("_EditUsuario", usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditUsuario", model);
            }

            _usuarioService.ActualizarUsuario(model);
            TempData["Success"] = "Usuario actualizado correctamente.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioService.ObtenerUsuarios().FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Usuario no encontrado." });
            }

            return Json(new { success = true, id = usuario.Id, nombre = usuario.NombreUsuario });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _usuarioService.EliminarUsuario(id);
            TempData["Success"] = "Usuario eliminado correctamente.";
            return RedirectToAction("Index");
        }

        // Método para renderizar parciales (igual que en PaquetesController)
        private string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);

                if (viewResult.View == null)
                {
                    throw new FileNotFoundException($"La vista parcial '{viewName}' no se encontró.");
                }

                ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw
                );

                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}