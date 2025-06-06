﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataLayer;
using ModelLayer.DTO;

namespace ExpressTour.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ExpressTourDataContext _db = new ExpressTourDataContext();

        public ActionResult Index()
        {
            // Obtener las tablas dinámicamente (excluyendo "opiniones_clientes")
            var tables = _db.Mapping.GetTables()
                            .Select(t => t.TableName.Split('.').Last()) // Para quitar el schema si existe
                            .Where(t => t != "opiniones_clientes")      // <-- Única modificación
                            .ToList();

            // Si no hay tablas, devolver una lista vacía
            var viewModel = new DashboardViewModel
            {
                Tables = tables
            };

            return View(viewModel);
        }
    }
}