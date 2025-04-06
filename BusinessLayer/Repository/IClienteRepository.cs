using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using ModelLayer.DTO;

namespace BusinessLayer.Repository
{
    public interface IClienteRepository
    {
        List<cliente> GetAllClientes();
        cliente GetClienteById(int id);
        void AddCliente(cliente cliente);
        void UpdateCliente(cliente cliente);
        void DeleteCliente(int id);
    }
}
