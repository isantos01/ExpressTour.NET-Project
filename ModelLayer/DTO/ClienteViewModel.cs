using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using ModelLayer;

namespace ModelLayer.DTO
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

    }
}