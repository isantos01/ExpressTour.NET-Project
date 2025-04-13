using System;
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

        // GET: Excursiones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Excursiones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExcursionesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _excursionesService.AgregarExcursion(model);
                    TempData["Success"] = "Excursión creada correctamente.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Hubo un problema al crear la excursión: " + ex.Message);
                }
            }
            return View(model);
        }

        // GET: Excursiones/Edit/5
        public ActionResult Edit(int id)
        {
            var excursion = _excursionesService.ObtenerExcursionPorId(id);
            if (excursion == null)
            {
                return HttpNotFound();
            }
            return View(excursion);
        }

        // POST: Excursiones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExcursionesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _excursionesService.ActualizarExcursion(model);
                    TempData["Success"] = "Excursión actualizada correctamente.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Hubo un problema al actualizar la excursión: " + ex.Message);
                }
            }
            return View(model);
        }

        // GET: Excursiones/Delete/5
        public ActionResult Delete(int id)
        {
            var excursion = _excursionesService.ObtenerExcursionPorId(id);
            if (excursion == null)
            {
                return HttpNotFound();
            }
            return View(excursion);
        }

        // POST: Excursiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _excursionesService.EliminarExcursion(id);
                TempData["Success"] = "Excursión eliminada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hubo un problema al eliminar la excursión: " + ex.Message);
                return View();
            }
        }
    }
}
