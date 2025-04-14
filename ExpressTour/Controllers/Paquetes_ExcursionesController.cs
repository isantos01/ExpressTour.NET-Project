using System;
using System.IO;
using System.Web.Mvc;
using BusinessLayer.Services;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
    public class Paquetes_ExcursionesController : Controller
    {
        private readonly PaquetesExcursionesService _peService;

        public Paquetes_ExcursionesController()
        {
            _peService = new PaquetesExcursionesService();
        }

        // GET: Paquetes_Excursiones/Index
        public ActionResult Index()
        {
            var registros = _peService.ObtenerPaquetesExcursiones();
            return View(registros); // Vista Index que muestra la lista
        }

        // GET: Paquetes_Excursiones/Create
        // Retorna el parcial para crear un registro, con los dropdowns cargados
        public ActionResult Create()
        {
            var model = new PaquetesExcursionesViewModel
            {
                ListaPaquetes = _peService.ObtenerDropdownPaquetes(),
                ListaExcursiones = _peService.ObtenerDropdownExcursiones()
            };
            return PartialView("_CreatePaquetesExcursiones", model);
        }

        // POST: Paquetes_Excursiones/Create
        // Se utiliza AJAX; si no es válido se devuelve el parcial actualizado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaquetesExcursionesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Re-cargar los dropdowns
                model.ListaPaquetes = _peService.ObtenerDropdownPaquetes();
                model.ListaExcursiones = _peService.ObtenerDropdownExcursiones();
                return Json(new
                {
                    success = false,
                    validationError = true,
                    html = RenderPartialViewToString("_CreatePaquetesExcursiones", model)
                });
            }

            _peService.AgregarPaqueteExcursione(model);
            TempData["Success"] = "Registro creado correctamente.";
            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Index", "Paquetes_Excursiones")
            });
        }

        // GET: Paquetes_Excursiones/Edit/{id}
        // Carga el parcial de edición con los dropdowns actualizados
        public ActionResult Edit(int id)
        {
            var registro = _peService.ObtenerPaqueteExcursionePorId(id);
            if (registro == null)
            {
                return HttpNotFound();
            }

            registro.ListaPaquetes = _peService.ObtenerDropdownPaquetes();
            registro.ListaExcursiones = _peService.ObtenerDropdownExcursiones();

            return PartialView("_EditPaquetesExcursiones", registro);
        }

        // POST: Paquetes_Excursiones/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaquetesExcursionesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ListaPaquetes = _peService.ObtenerDropdownPaquetes();
                model.ListaExcursiones = _peService.ObtenerDropdownExcursiones();
                return PartialView("_EditPaquetesExcursiones", model);
            }

            _peService.ActualizarPaqueteExcursione(model);
            TempData["Success"] = "Registro actualizado correctamente.";
            return RedirectToAction("Index");
        }

        // POST: Paquetes_Excursiones/Delete
        // Se usa para obtener los datos y confirmar vía AJAX
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var registro = _peService.ObtenerPaqueteExcursionePorId(id);
            if (registro == null)
                return Json(new { success = false, message = "Registro no encontrado." });

            return Json(new
            {
                success = true,
                id = registro.Id,
                nombre = $"Paquete {registro.IdPaquete} / Excursión {registro.IdExcursion}"
            });
        }

        // POST: Paquetes_Excursiones/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _peService.EliminarPaquetesExcursionesPorPaquete(id);
            TempData["Success"] = "Registro eliminado correctamente.";
            return RedirectToAction("Index");
        }

        // Método auxiliar para renderizar parciales a string (para respuestas AJAX)
        protected string RenderPartialViewToString(string viewName, object model)
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
                    throw new FileNotFoundException("No se encontró la vista parcial", viewName);
                }
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
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
