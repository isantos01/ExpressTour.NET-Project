using System.Collections.Generic;
using DataLayer;

namespace BusinessLayer.Repository
{
    public interface IReservaRepository
    {
        List<reserva> GetAllReservas();
        reserva GetReservaById(int id);
        void AddReserva(reserva reserva);
        void UpdateReserva(reserva reserva);
        bool DeleteReserva(int id);
        // Aquí puedes definir métodos adicionales si es necesario, por ejemplo, para búsquedas por cliente o paquete.
    }
}
