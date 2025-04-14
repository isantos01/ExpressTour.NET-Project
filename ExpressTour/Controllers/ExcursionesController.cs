using System;
using System.IO;
using System.Web.Mvc;
using BusinessLayer.Services;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
    public class ExcursionesController : Controller
    {
        private readonly ExcursionesService _excursionesService;

        public ExcursionesController()
        {
            _excursionesService = new ExcursionesService();
        }

        // GET: Excursiones/Index
        public ActionResult Index()
        {
            var excursiones = _excursionesService.ObtenerExcursiones();
            return View(excursiones);
        }

        // GET: Excursiones/Create (retorna parcial)
        public ActionResult Create()
        {
            return PartialView("_CreateExcursiones", new ExcursionesViewModel());
        }

        // POST: Excursiones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExcursionesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    html = RenderPartialViewToString("_CreateExcursiones", model)
                });
            }

            _excursionesService.AgregarExcursion(model);
            TempData["Success"] = "Excursión creada correctamente.";

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Index")
            });
        }

        // GET: Excursiones/Edit/5 (retorna parcial)
        public ActionResult Edit(int id)
        {
            var excursion = _excursionesService.ObtenerExcursionPorId(id);
            if (excursion == null)
                return HttpNotFound();

            return PartialView("_EditExcursiones", excursion);
        }

        // POST: Excursiones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExcursionesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditExcursiones", model);
            }

            _excursionesService.ActualizarExcursion(model);
            TempData["Success"] = "Excursión actualizada correctamente.";
            return RedirectToAction("Index");
        }

        // GET: Excursiones/Delete/5 (retorna parcial)
        public ActionResult Delete(int id)
        {
            var excursion = _excursionesService.ObtenerExcursionPorId(id);
            if (excursion == null)
                return HttpNotFound();

            return PartialView("_DeleteExcursiones", excursion);
        }

        // POST: Excursiones/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _excursionesService.EliminarExcursion(id);
            TempData["Success"] = "Excursión eliminada correctamente.";
            return RedirectToAction("Index");
        }

        // Método auxiliar para renderizar vistas parciales como string (para AJAX)
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                if (viewResult.View == null)
                {
                    throw new FileNotFoundException("No se encontró la vista parcial", viewName);
                }

                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
