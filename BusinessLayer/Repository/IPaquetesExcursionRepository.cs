using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataLayer;

namespace BusinessLayer.Repository
{
    public interface IPaquetesExcursionRepository
    {
        IEnumerable<paquetes_excursione> ObtenerTodos();
        paquetes_excursione ObtenerPorId(int id);
        void Agregar(paquetes_excursione entidad);

        void Actualizar(paquetes_excursione entidad);

        void Eliminar(int id);

        void EliminarPorPaquete(int idPaquete);

        // NUEVOS para dropdowns
        IEnumerable<SelectListItem> ObtenerDropdownPaquetes();
        IEnumerable<SelectListItem> ObtenerDropdownExcursiones();
    }

}
