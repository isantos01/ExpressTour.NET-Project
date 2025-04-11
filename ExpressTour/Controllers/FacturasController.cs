using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Services;
using DataLayer;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
    public class FacturasController : Controller
    {
        private readonly FacturasService _facturasService;

        public FacturasController()
        {
            _facturasService = new FacturasService();
        }

        public ActionResult Index()
        {
            var data = _facturasService.ObtenerFacturas();

            var viewModel = data.Select(f => new FacturasViewModel
            {
                Id = f.id_factura,
                IdReserva = f.id_reserva,
                Total = f.total,
                Fecha_Emision = f.fecha_emision ?? DateTime.Now
            }).ToList();

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var model = new FacturasViewModel
            {
                Fecha_Emision = DateTime.Now
            };
            return PartialView("_CreateFactura", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacturasViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verifica si la reserva ya existe
                if (!_facturasService.ReservaExiste(model.IdReserva))
                {
                    // Si no existe la reserva, mostrar el modal de reserva
                    TempData["MissingReservaId"] = model.IdReserva;
                    TempData["FacturaModel"] = model;
                    TempData["Warning"] = "La reserva especificada no existe. Puedes crear una nueva reserva asociada.";
                    return Json(new { success = false, html = RenderPartialViewToString("_CreateReserva", new ReservaViewModel()) });
                }

                var nuevaFactura = new factura
                {
                    id_reserva = model.IdReserva,
                    total = model.Total,
                    fecha_emision = model.Fecha_Emision
                };

                _facturasService.AgregarFactura(nuevaFactura);
                TempData["Success"] = "Factura creada correctamente.";
                return RedirectToAction("Index");
            }

            return PartialView("_CreateFactura", model);
        }


        // Carga paquetes disponibles en dropdown dinámico
        public JsonResult ObtenerPaquetes()
        {
            using (var db = new ExpressTourDataContext())
            {
                var paquetes = db.paquetes.Select(p => new
                {
                    Id = p.id_paquete,
                    Nombre = p.nombre
                }).ToList();

                return Json(paquetes, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            var entidad = _facturasService.ObtenerFacturaPorId(id);
            if (entidad == null)
                return HttpNotFound();

            var model = new FacturasViewModel
            {
                Id = entidad.id_factura,
                IdReserva = entidad.id_reserva,
                Total = entidad.total,
                Fecha_Emision = entidad.fecha_emision ?? DateTime.Now
            };

            return PartialView("_EditFactura", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FacturasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var facturaActualizada = new factura
                {
                    id_factura = model.Id,
                    id_reserva = model.IdReserva,
                    total = model.Total,
                    fecha_emision = model.Fecha_Emision
                };

                _facturasService.ActualizarFactura(facturaActualizada);
                TempData["Success"] = "Factura actualizada.";
                return RedirectToAction("Index");
            }

            return PartialView("_EditFactura", model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool eliminado = _facturasService.EliminarFactura(id);

            if (!eliminado)
            {
                TempData["Warning"] = "La factura está relacionada con una reserva. ¿Deseas eliminarla junto con la reserva?";
                return RedirectToAction("ConfirmDelete", new { id });
            }

            TempData["Success"] = "Factura eliminada correctamente.";
            return RedirectToAction("Index");
        }

        public ActionResult ConfirmDelete(int id)
        {
            var entidad = _facturasService.ObtenerFacturaPorId(id);
            if (entidad == null) return HttpNotFound();

            var model = new FacturasViewModel
            {
                Id = entidad.id_factura,
                IdReserva = entidad.id_reserva,
                Total = entidad.total,
                Fecha_Emision = entidad.fecha_emision ?? DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComplete(int id)
        {
            _facturasService.EliminarFacturaCascade(id);
            TempData["Success"] = "Factura y su reserva asociada fueron eliminadas.";
            return RedirectToAction("Index");
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
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
