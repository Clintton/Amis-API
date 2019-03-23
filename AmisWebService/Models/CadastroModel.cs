using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmisWebService.Models
{
    public class CadastroModel
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string TipoUsuario { get; set; }
    }
}