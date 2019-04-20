using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmisWebService.Models
{
    public class RefeicaoModel
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Refeicao { get; set; }
        public string Email { get; set; }

    }
}