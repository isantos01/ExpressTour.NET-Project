using System.Collections.Generic;
using System.Linq;
using DataLayer;  

namespace BusinessLayer.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ExpressTourDataContext _db;

        public ClienteRepository()
        {
            _db = new ExpressTourDataContext();
        }

        public List<cliente> GetAllClientes()
        {
            return _db.clientes.ToList();
        }

        public cliente GetClienteById(int id)
        {
            return _db.clientes.FirstOrDefault(c => c.id_cliente == id);
        }

        public void AddCliente(cliente cliente)
        {
            _db.clientes.InsertOnSubmit(cliente);
            _db.SubmitChanges();
        }

        public void UpdateCliente(cliente cliente)
        {
            var existing = GetClienteById(cliente.id_cliente);
            if (existing != null)
            {
                existing.nombre = cliente.nombre;
                existing.correo = cliente.correo;
                existing.telefono = cliente.telefono;
                existing.direccion = cliente.direccion;
                _db.SubmitChanges();
            }
        }

        public void DeleteCliente(int id)
        {
            var clienteToDelete = GetClienteById(id);
            if (clienteToDelete != null)
            {
                _db.clientes.DeleteOnSubmit(clienteToDelete);
                _db.SubmitChanges();
            }
        }
    }
}
