using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Services;
using ModelLayer.DTO;
using DataLayer;
using System.Data.Linq;
using System;

namespace ExpressTour.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ReservaService _reservaService;

        public ReservasController()
        {
            _reservaService = new ReservaService();
        }

        // --------------------------
        // INDEX
        // --------------------------
        public ActionResult Index()
        {
            var reservas = _reservaService.ObtenerReservas();

            var viewModel = reservas.Select(r => new ReservaViewModel
            {
                Id = r.id_reserva,
                IdPaquete = r.id_paquete,
                FechaReserva = (System.DateTime)r.fecha_reserva,
                Estado = r.estado,
                NombreCliente = r.cliente.nombre,
                TelefonoCliente = r.cliente.telefono,
                DireccionCliente = r.cliente.direccion
            }).ToList();

            return View(viewModel);
        }

        // --------------------------
        // JSON: Obtener paquetes
        // --------------------------
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

        // --------------------------
        // CREATE (formulario parcial normal)
        // --------------------------
        public ActionResult Create()
        {
            var model = new ReservaViewModel
            {
                FechaReserva = System.DateTime.Now,
                Estado = "pendiente"
            };
            return PartialView("_CreateReserva", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservaViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ExpressTourDataContext())
                {
                    // Crea nuevo cliente con los datos del ViewModel
                    var nuevoCliente = new cliente
                    {
                        nombre = model.NombreCliente,
                        correo = model.CorreoCliente, // Puede extenderse
                        telefono = model.TelefonoCliente,
                        direccion = model.DireccionCliente
                    };
                    db.clientes.InsertOnSubmit(nuevoCliente);
                    db.SubmitChanges();

                    var nuevaReserva = new reserva
                    {
                        id_cliente = nuevoCliente.id_cliente,
                        id_paquete = model.IdPaquete,
                        fecha_reserva = model.FechaReserva,
                        estado = model.Estado
                    };

                    db.reservas.InsertOnSubmit(nuevaReserva);
                    db.SubmitChanges();

                    TempData["Success"] = "Reserva y cliente creados correctamente.";
                }
                return RedirectToAction("Index");
            }

            return PartialView("_CreateReserva", model);
        }

        // --------------------------
        // CREATE desde Factura
        // --------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearReservaDesdeFactura(ReservaViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ExpressTourDataContext())
                {
                    var nuevoCliente = new cliente
                    {
                        nombre = model.NombreCliente,
                        correo = model.CorreoCliente,
                        telefono = model.TelefonoCliente,
                        direccion = model.DireccionCliente
                    };
                    db.clientes.InsertOnSubmit(nuevoCliente);
                    db.SubmitChanges();

                    var nuevaReserva = new reserva
                    {
                        id_cliente = nuevoCliente.id_cliente,
                        id_paquete = model.IdPaquete,
                        fecha_reserva = model.FechaReserva,
                        estado = model.Estado
                    };
                    db.reservas.InsertOnSubmit(nuevaReserva);
                    db.SubmitChanges();
                }

                TempData["Success"] = "Reserva creada correctamente.";
                return RedirectToAction("Index");
            }

            return PartialView("_CreateReserva", model);
        }

        // --------------------------
        // EDIT
        // --------------------------
        public ActionResult Edit(int id)
        {
            using (var db = new ExpressTourDataContext())
            {
                // Eager loading del cliente asociado (opcional, pero recomendable)
                var dlo = new DataLoadOptions();
                dlo.LoadWith<reserva>(r => r.cliente);
                db.LoadOptions = dlo;

                var reserva = db.reservas.FirstOrDefault(r => r.id_reserva == id);
                if (reserva == null) return HttpNotFound();

                // Llenar ViewBag.Paquetes
                ViewBag.Paquetes = db.paquetes
                    .Select(p => new SelectListItem
                    {
                        Value = p.id_paquete.ToString(),
                        Text = p.nombre
                    })
                    .ToList();

                var model = new ReservaViewModel
                {
                    Id = reserva.id_reserva,
                    IdPaquete = reserva.id_paquete,
                    FechaReserva = reserva.fecha_reserva ?? DateTime.Now,
                    Estado = reserva.estado,
                    NombreCliente = reserva.cliente.nombre,
                    TelefonoCliente = reserva.cliente.telefono,
                    DireccionCliente = reserva.cliente.direccion
                };

                // IMPORTANTE: devolver PartialView, no View
                return PartialView("_EditReserva", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservaViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ExpressTourDataContext())
                {
                    var reserva = db.reservas.FirstOrDefault(r => r.id_reserva == model.Id);
                    if (reserva == null) return HttpNotFound();

                    var cliente = db.clientes.FirstOrDefault(c => c.id_cliente == reserva.id_cliente);
                    if (cliente != null)
                    {
                        cliente.nombre = model.NombreCliente;
                        cliente.telefono = model.TelefonoCliente;
                        cliente.direccion = model.DireccionCliente;
                    }

                    reserva.id_paquete = model.IdPaquete;
                    reserva.fecha_reserva = model.FechaReserva;
                    reserva.estado = model.Estado;

                    db.SubmitChanges();
                }

                TempData["Success"] = "Reserva actualizada correctamente.";
                return RedirectToAction("Index");
            }

            return PartialView("_EditReserva", model);
        }

        // --------------------------
        // DELETE
        // --------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            bool eliminado = _reservaService.EliminarReserva(id);
            TempData[eliminado ? "Success" : "Error"] = eliminado
                ? "Reserva eliminada correctamente."
                : "No se pudo eliminar la reserva.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
