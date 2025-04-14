using System.Collections.Generic;
using System.Web.Mvc;

namespace ModelLayer.DTO
{
    public class PaquetesExcursionesViewModel
    {
        public int Id { get; set; } // id_paquete_excursion
        public int IdPaquete { get; set; }
        public int IdExcursion { get; set; }

        // Propiedades para los dropdowns
        public IEnumerable<SelectListItem> ListaPaquetes { get; set; }
        public IEnumerable<SelectListItem> ListaExcursiones { get; set; }

    }
}