using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Services;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
    public class PaquetesController : Controller
    {
        private readonly PaqueteService _paqueteService;
        private readonly TransporteService _transporteService;

        public PaquetesController()
        {
            _paqueteService = new PaqueteService();
            _transporteService = new TransporteService();
        }

        // GET: Paquetes/Index
        public ActionResult Index()
        {
            var paquetes = _paqueteService.ObtenerPaquetes();
            return View(paquetes);
        }

        // GET: Paquetes/Create - Retorna el parcial _CreatePaquete
        public ActionResult Create()
        {
            return PartialView("_CreatePaquete", new PaqueteViewModel());
        }

        // POST: Paquetes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaqueteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores de validación, se devuelve el parcial actualizado con los errores.
                return Json(new
                {
                    success = false,
                    validationError = true,
                    html = RenderPartialViewToString("_CreatePaquete", model)
                });
            }

            // Verificar si el transporte existe usando el ID ingresado
            var transporte = _transporteService.ObtenerTransportePorId(model.IdTransporte);
            if (transporte == null)
            {
                // Si el transporte no existe, se retorna JSON indicando que se requiere crear transporte.
                return Json(new
                {
                    success = false,
                    requiresTransporte = true,
                    message = "No se encontró transporte con el ID ingresado. Por favor, crea uno nuevo.",
                    html = RenderPartialViewToString("CreateTransporte", new TransporteViewModel())
                });
            }

            // Si el transporte existe, se crea el paquete
            _paqueteService.AgregarPaquete(model);
            TempData["Success"] = "Paquete creado correctamente.";
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Paquetes") });
        }

        // GET: Paquetes/CreateTransporte (para cargar el modal de creación de transporte)
        [HttpGet]
        public ActionResult CreateTransporte()
        {
            // Se retorna la vista parcial desde la carpeta Shared
            return PartialView("~/Views/Shared/_CreateTransporte.cshtml", new TransporteViewModel());
        }

        // POST: Paquetes/CreateTransporte
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTransporte(TransporteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Shared/_CreateTransporte.cshtml", model);
            }
            if (!_transporteService.ProveedorExiste(model.IdProveedor))
            {
                ModelState.AddModelError("IdProveedor", "El proveedor ingresado no existe.");
                return PartialView("_CreateTransporte", model);
            }

            var nuevoTransporte = new DataLayer.transporte
            {
                tipo = model.Tipo,
                capacidad = model.Capacidad,
                id_proveedor = model.IdProveedor
            };

            int newId = _transporteService.AgregarTransporte(nuevoTransporte);
            return Json(new { success = true, newTransporteId = newId, message = "Transporte creado correctamente." });
        }

        // GET: Paquetes/Edit
        public ActionResult Edit(int id)
        {
            var paquete = _paqueteService.ObtenerPaquetePorId(id);
            if (paquete == null)
                return HttpNotFound();

            return PartialView("_EditPaquete", paquete);
        }

        // POST: Paquetes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaqueteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditPaquete", model);
            }

            var transporte = _transporteService.ObtenerTransportePorId(model.IdTransporte);
            if (transporte == null)
            {
                ModelState.AddModelError("IdTransporte", "El ID de transporte ingresado no existe.");
                return PartialView("_EditPaquete", model);
            }

            _paqueteService.ActualizarPaquete(model);
            TempData["Success"] = "Paquete actualizado correctamente.";
            return RedirectToAction("Index");
        }

        // POST: Paquetes/Delete (obtiene datos para confirmar la eliminación vía AJAX)
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var paquete = _paqueteService.ObtenerPaquetePorId(id);
            if (paquete == null)
                return Json(new { success = false, message = "Paquete no encontrado." });

            return Json(new { success = true, id = paquete.Id, nombre = paquete.Nombre });
        }

        // POST: Paquetes/DeleteConfirmed (elimina en cascada)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _paqueteService.EliminarPaqueteCascade(id);
            TempData["Success"] = "Paquete eliminado correctamente.";
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
