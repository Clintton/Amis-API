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
    
    public partial class Glicemia
    {
        public int Id { get; set; }
        public System.DateTime DataMedicao { get; set; }
        public string HoraMedicao { get; set; }
        public int NivelGlicemico { get; set; }
        public string TipoGlicemia { get; set; }
        public int IdPaciente { get; set; }
    
        public virtual Paciente Paciente { get; set; }
    }
}
