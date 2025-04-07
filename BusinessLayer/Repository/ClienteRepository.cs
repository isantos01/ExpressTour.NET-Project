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

        public bool DeleteCliente(int id)
        {
            var clienteToDelete = GetClienteById(id);
            if (clienteToDelete != null)
            {
                // Verifica si existen reservas asociadas
                bool tieneReservas = _db.reservas.Any(r => r.id_cliente == id);
                // Verifica si existen facturas asociadas (a través de reservas)
                bool tieneFacturas = _db.facturas.Any(f => _db.reservas.Any(r => r.id_cliente == id && r.id_reserva == f.id_reserva));

                if (tieneReservas || tieneFacturas)
                {
                    // Si tiene relaciones, no se elimina de forma parcial
                    return false;
                }

                // Si no tiene relaciones, eliminar normalmente
                _db.clientes.DeleteOnSubmit(clienteToDelete);
                _db.SubmitChanges();
                return true;
            }
            return false;
        }
        public void DeleteClienteCascade(int id)
        {
            var clienteToDelete = GetClienteById(id);
            if (clienteToDelete != null)
            {
                // Eliminar las reservas asociadas
                var reservasToDelete = _db.reservas.Where(r => r.id_cliente == id).ToList();
                foreach (var reserva in reservasToDelete)
                {
                    // Eliminar las facturas asociadas a cada reserva
                    var facturasToDelete = _db.facturas.Where(f => f.id_reserva == reserva.id_reserva).ToList();
                    if (facturasToDelete.Any())
                    {
                        _db.facturas.DeleteAllOnSubmit(facturasToDelete);
                    }
                }
                if (reservasToDelete.Any())
                {
                    _db.reservas.DeleteAllOnSubmit(reservasToDelete);
                }
                _db.clientes.DeleteOnSubmit(clienteToDelete);
                _db.SubmitChanges();
            }
        }


    }
}
