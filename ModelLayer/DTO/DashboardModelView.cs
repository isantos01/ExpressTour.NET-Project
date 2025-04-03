using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class DashboardModelView
    {
        public List<TableInfoModel> Tables { get; set; }
    }
}
