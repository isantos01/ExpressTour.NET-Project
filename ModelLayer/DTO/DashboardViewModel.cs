﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class DashboardViewModel
    {
        public List<string> Tables { get; set; } = new List<string>(); // Evita nulos
    }
}

