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
                    glicemia.DataMedicao = glicemiaModel.Data;
                    glicemia.HoraMedicao = glicemiaModel.Hora;
                    glicemia.IdPaciente = paciente;
                    glicemia.NivelGlicemico = glicemiaModel.Nivel;
                    glicemia.TipoGlicemia = glicemiaModel.Tipo;
                    db.Glicemia.Add(glicemia);
                }
            }

            db.SaveChanges();
        }
        [HttpPost]
        [Route("api/listar_glicemia")]
        public List<GlicemiaModel> ListarGlicemia([FromBody]GlicemiaModel model)
        {
            var userID = db.Pessoa.Where(x => x.Email == model.Email).Select(x => x.Id);
            List<GlicemiaModel> listaGlicModels = new List<GlicemiaModel>();
            IEnumerable<Glicemia> listaGlic = null;
            IEnumerable<int> pacienteID = null;

            foreach (var item in userID)
            {
                pacienteID = db.Paciente.Where(x => x.IdPessoa == item).Select(x => x.Id);

                listaGlic = db.Glicemia.Where(x => x.IdPaciente == item);

            }

            foreach (var item in pacienteID)
            {

                listaGlic = db.Glicemia.Where(x => x.IdPaciente == item);

            }

            foreach (var itemGlic in listaGlic)
            {
                GlicemiaModel glicemiaModel = new GlicemiaModel();

                glicemiaModel.Id = itemGlic.Id;
                glicemiaModel.Data = itemGlic.DataMedicao;
                glicemiaModel.Hora = itemGlic.HoraMedicao;
                glicemiaModel.Nivel = itemGlic.NivelGlicemico;
                glicemiaModel.Tipo = itemGlic.TipoGlicemia;
                listaGlicModels.Add(glicemiaModel);
            }

            return listaGlicModels;
        }
    }
}
