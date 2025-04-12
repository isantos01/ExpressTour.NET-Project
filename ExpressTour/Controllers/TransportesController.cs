using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Services;
using DataLayer;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
    public class TransportesController : Controller
    {
        private readonly TransporteService _transporteService;

        public TransportesController()
        {
            _transporteService = new TransporteService();
        }

        public ActionResult Index()
        {
            var data = _transporteService.ObtenerTransporte();

            var viewModel = data.Select(t => new TransporteViewModel
            {
                Id = t.id_transporte,
                Tipo = t.tipo,
                Capacidad = t.capacidad,
                IdProveedor = t.id_proveedor
            }).ToList();

            return View(viewModel);
        }

        // GET: Transporte/Create - Esta acción puede no usarse si el formulario está en el modal, pero es buena práctica tenerla.
        public ActionResult Create()
        {
            return View();
        }

// POST: Transporte/Create
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(TransporteViewModel model)
{
    if (ModelState.IsValid)
    {
        // Verificar si el proveedor existe
        if (!_transporteService.ProveedorExiste(model.IdProveedor))
        {
            // Agrega error al ModelState si no se encontró el proveedor
            ModelState.AddModelError("IdProveedor", "El proveedor ingresado no existe.");
            // Retorna la vista parcial con el modelo y los errores para que se muestren en el modal
            return View("_CreateTransporte", model);
        }
        
        // Crear la entidad "transporte" a partir del ViewModel
        var nuevoTransporte = new transporte
        {
            tipo = model.Tipo,
            capacidad = model.Capacidad,
            id_proveedor = model.IdProveedor
        };
        
        // Llama al servicio para agregar el transporte y obtener el ID del nuevo registro
        int newId = _transporteService.AgregarTransporte(nuevoTransporte);
        
        // Redirige al índice de transportes (podrías mostrar un mensaje de éxito en TempData si lo deseas)
        return RedirectToAction("Index");
    }
    
    // Si el modelo no es válido, retorna la vista parcial para que se corrijan los errores.
    return View("_CreateTransporte", model);
}


        // GET: Transporte/Edit/5
        public ActionResult Edit(int id)
        {
            var transporteEntity = _transporteService.ObtenerTransportePorId(id);
            if (transporteEntity == null)
            {
                return HttpNotFound();
            }
            var model = new TransporteViewModel
            {
                Id = transporteEntity.id_transporte,
                Tipo= transporteEntity.tipo,
                Capacidad = transporteEntity.capacidad,
                IdProveedor = transporteEntity.id_proveedor
            };
            // Devuelve el formulario de edición en un PartialView
            return PartialView("_EditTransporte", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransporteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transporteToUpdate = new transporte
                {
                    id_transporte = model.Id,
                    tipo = model.Tipo,
                    capacidad = model.Capacidad,
                    id_proveedor = model.IdProveedor
                };

                _transporteService.ActualizarTransporte(transporteToUpdate);
                return RedirectToAction("Index");
            }
            // Si hay errores, devuelve el PartialView con el modelo para corregirlos
            return PartialView("_EditTransporte", model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool eliminado = _transporteService.DeleteTransporte(id);

            if (!eliminado)
            {
                // Si no se puede eliminar porque tiene relaciones, redirige a una acción de confirmación
                TempData["Warning"] = "El transporte tiene registros asociados. Eliminar este transporte eliminará también sus paquetes y excursiones. ¿Desea proceder?";
                return RedirectToAction("ConfirmDelete", new { id });
            }
            else
            {
                TempData["Success"] = "transporte eliminado correctamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ConfirmDelete(int id)
        {
            // Obtiene el transporte para mostrar sus datos
            var transporteEntity = _transporteService.ObtenerTransportePorId(id);
            if (transporteEntity == null)
            {
                return HttpNotFound();
            }
            // Mapear a TransporteViewModel
            var model = new TransporteViewModel
            {
                Id = transporteEntity.id_transporte,
                Tipo = transporteEntity.tipo,
                Capacidad = transporteEntity.capacidad,
                IdProveedor= transporteEntity.id_proveedor
            };
            return View(model); // Vista de confirmación
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComplete(int id)
        {
            _transporteService.EliminarTransporteCascade(id);
            TempData["Success"] = "Transporte y sus registros asociados fueron eliminados.";
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            // Liberar Recurso
            base.Dispose(disposing);
        }
    }
}