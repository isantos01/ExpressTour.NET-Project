using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer.Repository
{
    public interface IExcursionesRepository
    {
        IEnumerable<excursione> ObtenerExcursiones();
        excursione ObtenerExcursionPorId(int id);

        void AgregarExcursion(excursione excursion);
        void ActualizarExcursion(excursione excursion);
        void EliminarExcursion(int id);
    }
}
