using System.Collections.Generic;
using BusinessLayer.Repository;
using DataLayer; 

namespace BusinessLayer.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService()
        {
            _clienteRepository = new ClienteRepository();
        }

        public List<cliente> ObtenerClientes()
        {
            return _clienteRepository.GetAllClientes();
        }

        public cliente ObtenerClientePorId(int id)
        {
            return _clienteRepository.GetClienteById(id);
        }

        public void AgregarCliente(cliente cliente)
        {
            _clienteRepository.AddCliente(cliente);
        }

        public void ActualizarCliente(cliente cliente)
        {
            _clienteRepository.UpdateCliente(cliente);
        }

        public void EliminarCliente(int id)
        {
            _clienteRepository.DeleteCliente(id);
        }
    }
}
