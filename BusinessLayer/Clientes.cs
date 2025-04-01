using System.Linq;
using DataLayer;  // Importamos la capa de datos para acceder a los modelos generados con LINQ to SQL

namespace BusinessLayer
{
    public class ClienteService
    {
        private ExpressTourDataContext db;  // Instancia del contexto de datos

        // Constructor de la clase
        public ClienteService()
        {
            db = new ExpressTourDataContext();  // Inicializamos el contexto de datos
        }

        // Método para obtener todos los clientes
        public IQueryable<cliente> ObtenerClientes()
        {
            return db.clientes;  // Devuelve todos los clientes
        }

        // Método para obtener un cliente por su Id
        public cliente ObtenerClientePorId(int clienteId)
        {
            // Retorna el primer cliente que coincida con el Id proporcionado
            return db.clientes.FirstOrDefault(c => c.Id == clienteId);
        }

        // Método para agregar un nuevo cliente
        public void AgregarCliente(cliente nuevoCliente)
        {
            db.clientes.InsertOnSubmit(nuevoCliente);  // Inserta el nuevo cliente en la base de datos
            db.SubmitChanges();  // Guardamos los cambios en la base de datos
        }

        // Método para actualizar un cliente existente
        public void ActualizarCliente(cliente clienteActualizado)
        {
            var clienteExistente = db.clientes.FirstOrDefault(c => c.Id == clienteActualizado.Id);
            if (clienteExistente != null)
            {
                clienteExistente.nombre = clienteActualizado.nombre;
                clienteExistente.Apellido = clienteActualizado.Apellido;
                clienteExistente.Email = clienteActualizado.correo;
                // Aquí puedes agregar más propiedades si las tienes

                db.SubmitChanges();  // Guardamos los cambios en la base de datos
            }
        }

        // Método para eliminar un cliente
        public void EliminarCliente(int clienteId)
        {
            var cliente = db.clientes.FirstOrDefault(c => c.Id == clienteId);
            if (cliente != null)
            {
                db.clientes.DeleteOnSubmit(cliente);  // Elimina el cliente de la base de datos
                db.SubmitChanges();  // Guardamos los cambios en la base de datos
            }
        }
    }
}
