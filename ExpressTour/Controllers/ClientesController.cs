using System.Collections.Generic;
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

        protected override void Dispose(bool disposing)
        {
            // Liberar Recurso
            base.Dispose(disposing);
        }
    }
}