using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using ModelLayer.DTO;

namespace BusinessLayer.Repository
{
    public interface ITransporteRepository
    {
        List<transporte> GetAllTransporte();
        transporte GetTransporteById(int id);
        void AddTransporte(transporte transporte);
        void UpdateTransporte(transporte transporte);
        bool DeleteTransporte(int id);
        void DeleteTransporteCascade(int id);
        bool ProveedorExiste(int IdProveedor);
    }
}