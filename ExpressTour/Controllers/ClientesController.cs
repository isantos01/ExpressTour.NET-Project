using System.Web.Mvc;
using BusinessLayer;  // Importa la capa de negocio
using DataLayer;      // Importa la capa de datos

namespace ExpressTourWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService clienteService;  // Instancia del servicio

        public ClienteController()
        {
            clienteService = new ClienteService();  // Inicializa el servicio de clientes
        }

        // Acción para ver todos los clientes
        public ActionResult Index()
        {
            var clientes = clienteService.ObtenerClientes();  // Obtén todos los clientes
            return View(clientes);  // Pasar los clientes a la vista
        }

        // Acción para crear un cliente
        [HttpPost]
        public ActionResult Crear(cliente cliente)
        {
            if (ModelState.IsValid)  // Verifica si el modelo es válido
            {
                clienteService.AgregarCliente(cliente);  // Agrega el nuevo cliente
                return RedirectToAction("Index");  // Redirige a la lista de clientes
            }

            return View(cliente);  // Si el modelo no es válido, regresa al formulario
        }

        // Acción para editar un cliente
        [HttpPost]
        public ActionResult Editar(cliente cliente)
        {
            if (ModelState.IsValid)  // Verifica si el modelo es válido
            {
                clienteService.ActualizarCliente(cliente);  // Actualiza el cliente
                return RedirectToAction("Index");  // Redirige a la lista de clientes
            }

            return View(cliente);  // Si el modelo no es válido, regresa al formulario
        }

        // Acción para eliminar un cliente
        public ActionResult Eliminar(int id)
        {
            clienteService.EliminarCliente(id);  // Elimina el cliente por su Id
            return RedirectToAction("Index");  // Redirige a la lista de clientes
        }
    }
}
