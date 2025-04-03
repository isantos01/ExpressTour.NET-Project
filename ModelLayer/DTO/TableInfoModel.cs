using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class TableInfoModel
    {
        // Referencia el nombre de la tabla
        public string TableName { get; set; }
        // Referencia el nombre de la vista de la tabla
        public string DisplayName { get; set; }

    }
}
