using System.Collections.Generic;
using BusinessLayer.Repository;
using DataLayer;

namespace BusinessLayer.Services
{
    public class ReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService()
        {
            _reservaRepository = new ReservaRepository();
        }

        public List<reserva> ObtenerReservas()
        {
            return _reservaRepository.GetAllReservas();
        }

        public reserva ObtenerReservaPorId(int id)
        {
            return _reservaRepository.GetReservaById(id);
        }

        public void AgregarReserva(reserva reserva)
        {
            _reservaRepository.AddReserva(reserva);
        }

        public void ActualizarReserva(reserva reserva)
        {
            _reservaRepository.UpdateReserva(reserva);
        }

        public bool EliminarReserva(int id)
        {
            return _reservaRepository.DeleteReserva(id);
        }
    }
}
