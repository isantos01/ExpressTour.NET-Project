using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using ModelLayer.DTO;

namespace BusinessLayer.Repository
{
    public interface IFacturasRepository
    {
        List<factura> GetAllFacturas();
        factura GetFacturasById(int id);
        void AddFacturas(factura factura);
        void UpdateFacturas(factura factura);
        bool DeleteFacturas(int id);
        void DeleteFacturasCascade(int id);

        bool ReservasExiste(int IdReserva);
    }
}