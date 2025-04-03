using System.Collections.Generic;
using System.Web.Mvc;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
	public class DashboardController : Controller
	{
        //Obtenemos Dashboard
        public ActionResult Index()
        {
            //Simula la lista de tablas
            var tables = new List<TableInfoModel> {
            new TableInfoModel { TableName = "clientes", DisplayName = "Clientes" },
            new TableInfoModel { TableName = "paquetes", DisplayName = "Paquetes" },
            new TableInfoModel { TableName = "reservas", DisplayName = "Reservas"},
            new TableInfoModel { TableName = "excursiones", DisplayName = "Excursiones"},
            new TableInfoModel { TableName = "transporte", DisplayName = "Transporte"},
            new TableInfoModel { TableName = "proveedores", DisplayName = "Proveedores"},
            new TableInfoModel { TableName = "facturas", DisplayName = "Facturas"}
            };
            var model = new DashboardModelView
            {
                Tables = tables
            };
            return View(model);
        }
	}
}