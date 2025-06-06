﻿using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer;
using System.Linq;
using ModelLayer.DTO;
using BusinessLayer.Services;


namespace ExpressTour.Controllers
{
    public class ClientesController : Controller
    {
        //Instancia del Servicio
        private readonly ClienteService _clienteService;
        public ClientesController()
        {
            _clienteService = new ClienteService();
        }
        // Obtener Clientes
        public ActionResult Index()
        {

            // Obtener Clientes de Servicio
            var clientesData = _clienteService.ObtenerClientes();

            // Consulta cliente del ViewModel
            var clientes = clientesData.Select(c => new ClientViewModel
            {
                Id = c.id_cliente,
                Nombre = c.nombre,
                Correo = c.correo,
                Telefono = c.telefono,
                Direccion = c.direccion,
            }).ToList();

            return View(clientes);
        }
        // GET: Clientes/Create - Esta acción puede no usarse si el formulario está en el modal, pero es buena práctica tenerla.
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Mapear de ClientViewModel a la entidad cliente
                var nuevoCliente = new cliente
                {
                    nombre = model.Nombre,
                    correo = model.Correo,
                    telefono = model.Telefono,
                    direccion = model.Direccion
                };

                _clienteService.AgregarCliente(nuevoCliente);
                return RedirectToAction("Index");
            }
            // Si hay errores, se podría regresar la vista y mostrar mensajes (o manejar via Ajax)
            return View(model);
        }
        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            var clienteEntity = _clienteService.ObtenerClientePorId(id);
            if (clienteEntity == null)
            {
                return HttpNotFound();
            }
            var model = new ClientViewModel
            {
                Id = clienteEntity.id_cliente,
                Nombre = clienteEntity.nombre,
                Correo = clienteEntity.correo,
                Telefono = clienteEntity.telefono,
                Direccion = clienteEntity.direccion
            };
            // Devuelve el formulario de edición en un PartialView
            return PartialView("_EditClient", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var clienteToUpdate = new cliente
                {
                    id_cliente = model.Id,
                    nombre = model.Nombre,
                    correo = model.Correo,
                    telefono = model.Telefono,
                    direccion = model.Direccion
                };

                _clienteService.ActualizarCliente(clienteToUpdate);
                return RedirectToAction("Index");
            }
            // Si hay errores, devuelve el PartialView con el modelo para corregirlos
            return PartialView("_EditClient", model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool eliminado = _clienteService.DeleteClientes(id);

            if (!eliminado)
            {
                // Si no se puede eliminar porque tiene relaciones, redirige a una acción de confirmación
                TempData["Warning"] = "El cliente tiene registros asociados. Eliminar este cliente eliminará también sus reservas y facturas. ¿Desea proceder?";
                return RedirectToAction("ConfirmDelete", new { id = id });
            }
            else
            {
                TempData["Success"] = "Cliente eliminado correctamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ConfirmDelete(int id)
        {
            // Obtiene el cliente para mostrar sus datos
            var clienteEntity = _clienteService.ObtenerClientePorId(id);
            if (clienteEntity == null)
            {
                return HttpNotFound();
            }
            // Mapear a ClientViewModel
            var model = new ClientViewModel
            {
                Id = clienteEntity.id_cliente,
                Nombre = clienteEntity.nombre,
                Correo = clienteEntity.correo,
                Telefono = clienteEntity.telefono,
                Direccion = clienteEntity.direccion
            };
            return View(model); // Vista de confirmación
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComplete(int id)
        {
            _clienteService.EliminarClienteConReservas(id);
            TempData["Success"] = "Cliente y sus registros asociados fueron eliminados.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            // Liberar Recurso
            base.Dispose(disposing);
        }
    }
}