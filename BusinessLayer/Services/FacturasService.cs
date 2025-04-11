using System.Collections.Generic;
using BusinessLayer.Repository;
using DataLayer;

namespace BusinessLayer.Services
{
    public class FacturasService
    {
        private readonly IFacturasRepository _facturasRepository;

        public FacturasService()
        {
            _facturasRepository = new FacturasRepository();
        }

        public List<factura> ObtenerFacturas()
        {
            return _facturasRepository.GetAllFacturas();
        }

        public factura ObtenerFacturaPorId(int id)
        {
            return _facturasRepository.GetFacturasById(id);
        }

        public void AgregarFactura(factura factura)
        {
            _facturasRepository.AddFacturas(factura);
        }

        public void ActualizarFactura(factura factura)
        {
            _facturasRepository.UpdateFacturas(factura);
        }

        public bool EliminarFactura(int id)
        {
            return _facturasRepository.DeleteFacturas(id);
        }

        public void EliminarFacturaCascade(int id)
        {
            _facturasRepository.DeleteFacturasCascade(id);
        }

        public bool ReservaExiste(int idReserva)
        {
            return _facturasRepository.ReservasExiste(idReserva);
        }

    }
}
