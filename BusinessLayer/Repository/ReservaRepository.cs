using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace BusinessLayer.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ExpressTourDataContext _db;

        public ReservaRepository()
        {
            _db = new ExpressTourDataContext();
        }

        public List<reserva> GetAllReservas()
        {
            return _db.reservas.ToList();
        }

        public reserva GetReservaById(int id)
        {
            return _db.reservas.FirstOrDefault(r => r.id_reserva == id);
        }

        public void AddReserva(reserva reserva)
        {
            _db.reservas.InsertOnSubmit(reserva);
            _db.SubmitChanges();
        }

        public void UpdateReserva(reserva reserva)
        {
            var existing = GetReservaById(reserva.id_reserva);
            if (existing != null)
            {
                existing.id_cliente = reserva.id_cliente;
                existing.id_paquete = reserva.id_paquete;
                existing.fecha_reserva = reserva.fecha_reserva;
                existing.estado = reserva.estado;
                _db.SubmitChanges();
            }
        }

        public bool DeleteReserva(int id)
        {
            var reservaToDelete = GetReservaById(id);
            if (reservaToDelete != null)
            {
                _db.reservas.DeleteOnSubmit(reservaToDelete);
                _db.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
