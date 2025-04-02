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
        public string Title { get; set; }
        public string Welcome { get; set; }

        public DashboardModelView()
        {
        }
        public DashboardModelView(string title, string welcome)
        {
            Title = title;
            Welcome = welcome;
        }
    }
}
