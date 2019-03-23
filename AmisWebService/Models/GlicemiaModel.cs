using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmisWebService.Models
{
    public class GlicemiaModel
    {
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Tipo { get; set; }
        public int Nivel { get; set; }
        public string Email { get; set; }
    }
}