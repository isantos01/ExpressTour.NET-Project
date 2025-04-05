using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer;
using System.Linq;
using ModelLayer.DTO;
using ExpressTour.Models;


namespace ExpressTour.Controllers
{
	public class ClientesController : Controller
	{
        // Instancia del DataContext
        private readonly ExpressTourDataContext db = new ExpressTourDataContext();

        // Obtener Clientes
        public ActionResult Index()
        {
            // Consulta LINQ para obtener Lista de Clientes
            var clientes = db.clientes.Select (c => new ClientViewModel
            {
                Id = c.id_cliente,
                Nombre = c.nombre,
                Correo = c.correo,
                Telefono = c.telefono,
                Direccion = c.direccion,
            }).ToList();
            // Devolver una lista vacía si no hay informacion
            if (clientes == null)
            {
                clientes = new List<ClientViewModel>();
            }
            return View(clientes);
        }
        // Crea un load balance en cuanto a recursos, los libera
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}