using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Services;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ProveedorService _proveedorService;

        public ProveedoresController()
        {
            _proveedorService = new ProveedorService();
        }

        // GET: Proveedores
        public ActionResult Index()
        {
            var proveedores = _proveedorService.ObtenerProveedores();
            return View(proveedores);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            var model = new ProveedorViewModel();
            return PartialView("_CreateProveedor", model);
        }

        // POST: Proveedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProveedorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _proveedorService.AgregarProveedor(model);
                TempData["Success"] = "Proveedor creado correctamente.";
                return RedirectToAction("Index");
            }
            return PartialView("_CreateProveedor", model);
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _proveedorService.ObtenerProveedorPorId(id);
            if (model == null)
                return HttpNotFound();

            return PartialView("_EditProveedor", model);
        }

        // POST: Proveedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProveedorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _proveedorService.ActualizarProveedor(model);
                TempData["Success"] = "Proveedor actualizado correctamente.";
                return RedirectToAction("Index");
            }
            return PartialView("_EditProveedor", model);
        }

        // POST: Proveedores/Delete
        // Se utiliza para obtener el proveedor a eliminar y mostrar la confirmación en el modal
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var proveedor = _proveedorService.ObtenerProveedorPorId(id);
            if (proveedor == null)
            {
                return Json(new { success = false, message = "Proveedor no encontrado." });
            }
            // Retorna los datos necesarios para mostrar en el modal (sin ejecutar la eliminación)
            return Json(new { success = true, id = proveedor.Id, nombre = proveedor.NombreProveedor });
        }

        // POST: Proveedores/DeleteConfirmed
        // Se ejecuta la eliminación definitivamente (en cascada si es necesario)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Si el proveedor tiene transportes asociados, se ejecuta la eliminación en cascada.
            if (_proveedorService.TieneTransportesAsociados(id))
            {
                _proveedorService.EliminarProveedorCascade(id);
            }
            else
            {
                _proveedorService.EliminarProveedor(id);
            }
            // Se retorna un JSON con la información de éxito para la llamada AJAX
            return Json(new { success = true, message = "Proveedor eliminado correctamente." });
        }

        protected override void Dispose(bool disposing)
        {
            // Liberar Recurso
            base.Dispose(disposing);
        }
    }
}
