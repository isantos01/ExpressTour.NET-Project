using System;
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
                // Si la reserva no existe, crearla
                if (!_facturasService.ReservaExiste(model.IdReserva))
                {
                    TempData["MissingReservaId"] = model.IdReserva;
                    TempData["FacturaModel"] = model;
                    TempData["Warning"] = "La reserva especificada no existe. Puedes crear una nueva reserva asociada.";
                    return RedirectToAction("CrearReservaDesdeFactura");
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

        // Crear reserva desde factura cuando no existe el ID de reserva
        public ActionResult CrearReservaDesdeFactura()
        {
            var model = new ReservaViewModel
            {
                FechaReserva = DateTime.Now,
                Estado = "activo"
            };

            // Aquí pasas los datos para que el usuario pueda crear la reserva desde el modal
            return PartialView("_CreateReserva", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearReservaDesdeFactura(ReservaViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ExpressTourDataContext())
                {
                    // Crear un cliente si no existe
                    var nuevoCliente = new cliente
                    {
                        nombre = model.NombreCliente,
                        telefono = model.TelefonoCliente,
                        direccion = model.DireccionCliente,
                        correo = ""  // Por ahora lo dejamos vacío, luego se puede agregar el campo
                    };

                    db.clientes.InsertOnSubmit(nuevoCliente);
                    db.SubmitChanges();

                    // Crear la reserva asociada al cliente recién creado
                    var nuevaReserva = new reserva
                    {
                        id_cliente = nuevoCliente.id_cliente,
                        id_paquete = model.IdPaquete,
                        fecha_reserva = model.FechaReserva,
                        estado = model.Estado
                    };

                    db.reservas.InsertOnSubmit(nuevaReserva);
                    db.SubmitChanges();

                    // Ahora creamos la factura con la nueva reserva creada
                    var nuevaFactura = new factura
                    {
                        id_reserva = nuevaReserva.id_reserva,
                        total = model.Total,  // Usamos el total que el usuario haya ingresado
                        fecha_emision = DateTime.Now
                    };

                    db.facturas.InsertOnSubmit(nuevaFactura);
                    db.SubmitChanges();

                    TempData["Success"] = "Factura y reserva creadas correctamente.";
                    return RedirectToAction("Index");
                }
            }
            return PartialView("_CreateReserva", model);
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
