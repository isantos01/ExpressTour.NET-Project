using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
   public class TransporteViewModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int Capacidad { get; set; }
        public int IdProveedor { get; set; }

    }
}
