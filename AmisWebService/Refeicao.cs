//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AmisWebService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Refeicao
    {
        public int Id { get; set; }
        public string Hora { get; set; }
        public string refeicao1 { get; set; }
        public int IdPaciente { get; set; }
        public string Data { get; set; }
    
        public virtual Paciente Paciente { get; set; }
    }
}