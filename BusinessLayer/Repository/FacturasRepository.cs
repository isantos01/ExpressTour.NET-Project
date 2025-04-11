using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace BusinessLayer.Repository
{
    public class FacturasRepository : IFacturasRepository
    {
        private readonly ExpressTourDataContext _db;

        public FacturasRepository()
        {
            _db = new ExpressTourDataContext();
        }

        public List<factura> GetAllFacturas()
        {
            return _db.facturas.ToList();
        }

        public factura GetFacturasById(int id)
        {
            return _db.facturas.FirstOrDefault(c => c.id_factura == id);
        }

        public void AddFacturas(factura factura)
        {
            _db.facturas.InsertOnSubmit(factura);
            _db.SubmitChanges();
        }

        public void UpdateFacturas(factura factura)
        {
            var existing = GetFacturasById(factura.id_factura);
            if (existing != null)
            {
                existing.id_factura = factura.id_factura;
                existing.id_reserva = factura.id_reserva;
                existing.total = factura.total;
                existing.fecha_emision = factura.fecha_emision;
                _db.SubmitChanges();
            }
        }

        public bool ReservasExiste(int idReserva)
        {
            return _db.reservas.Any(r => r.id_reserva == idReserva);
        }


        public bool DeleteFacturas(int id)
        {
            var facturaToDelete = GetFacturasById(id);
            if (facturaToDelete != null)
            {
                _db.facturas.DeleteOnSubmit(facturaToDelete);
                _db.SubmitChanges();
                return true;
            }
            return false;
        }
        public void DeleteFacturasCascade(int id)
        {
            var factura = GetFacturasById(id);
            if (factura != null)
            {
                // Primero eliminar las facturas asociadas a la reserva
                var facturasToDelete = _db.facturas.Where(f => f.id_reserva == factura.id_reserva).ToList();
                if (facturasToDelete.Any())
                {
                    _db.facturas.DeleteAllOnSubmit(facturasToDelete); // Eliminar las facturas asociadas
                }

                // Ahora eliminar la reserva asociada
                var reserva = _db.reservas.FirstOrDefault(r => r.id_reserva == factura.id_reserva);
                if (reserva != null)
                {
                    _db.reservas.DeleteOnSubmit(reserva); // Eliminar la reserva
                }

                // Eliminar la factura
                _db.facturas.DeleteOnSubmit(factura); // Eliminar la factura

                _db.SubmitChanges();
            }
        }
    }
}