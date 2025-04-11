using System;

namespace ModelLayer.DTO
{
    public class ReservaViewModel
    {
        public int Id { get; set; }
        public int IdPaquete { get; set; }        // Seleccionado desde un dropdown
        // Si ya existe el cliente, podrías usar su ID; pero aquí queremos permitir ingresar nuevos datos:
        public string NombreCliente { get; set; }  // Nombre del cliente (si el cliente no existe)
        public string DireccionCliente { get; set; }
        public string TelefonoCliente { get; set; }

        public DateTime FechaReserva { get; set; }
        public string Estado { get; set; }         // Valor por defecto "activo"
        public decimal Total { get; set; }
        public string CorreoCliente { get; set; }

    }
}
