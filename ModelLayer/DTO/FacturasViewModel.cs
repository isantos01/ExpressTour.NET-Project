using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
   public class FacturasViewModel
    {
        public int Id { get; set; }
        public int IdReserva { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha_Emision { get; set; }
    }
}
