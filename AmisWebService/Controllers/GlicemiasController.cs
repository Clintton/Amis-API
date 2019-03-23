using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using AmisWebService;
using AmisWebService.Models;

namespace AmisWebService.Controllers
{
    public class GlicemiasController : ApiController
    {
        private AmisEntities db = new AmisEntities();

        [HttpPost]
        [Route("api/inserir_glicemia")]
        public void InserirGlicemia([FromBody] GlicemiaModel glicemiaModel)
        {
            var userID = db.Pessoa.Where(x => x.Email == glicemiaModel.Email).Select(x => x.Id);
            foreach (var item in userID)
            {
                var pacienteID = db.Paciente.Where(x => x.IdPessoa == item).Select(x => x.Id);
                foreach (var paciente in pacienteID)
                {
                    Glicemia glicemia = new Glicemia();
                    glicemia.DataMedicao = DateTime.Parse(glicemiaModel.Data);
                    glicemia.HoraMedicao = glicemiaModel.Hora;
                    glicemia.IdPaciente = paciente;
                    glicemia.NivelGlicemico = glicemiaModel.Nivel;
                    glicemia.TipoGlicemia = glicemiaModel.Tipo;
                    db.Glicemia.Add(glicemia);
                }
            }

            db.SaveChanges();
        }
    }
}
