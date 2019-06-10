using AmisWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmisWebService.Controllers
{
    public class PacientesVinculadosController : ApiController
    {
        private AmisEntities db = new AmisEntities();

        [HttpGet]
        [Route("api/exibe_glicemia_paciente")]
        public List<GlicemiaModel> buscaGlicemiaPaciente([FromUri] int id)
        {
            IEnumerable<Glicemia> listaGlic = db.Glicemia.Where(x => x.IdPaciente == id);
            List<GlicemiaModel> listaGlicModels = new List<GlicemiaModel>();

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

        [HttpGet]
        [Route("api/lista_pacientes_vinculados")]
        public object buscaGlicemiaPaciente([FromUri] string email)
        {
            var medicoId = (from med in db.Medico
                            join pe in db.Pessoa on med.IdPessoa equals pe.Id
                            where pe.Email == email
                            select new
                            {
                                med.Id,
                            }).ToList();

            int id = medicoId[0].Id;

            var listaVinculados = (from vinc in db.Paciente_Vinculado
                                   join pac in db.Paciente on vinc.IdPaciente equals pac.Id
                                   join pes in db.Pessoa on vinc.IdPaciente equals pes.Id
                                   where vinc.IdMedico == id
                                   select new
                                   {
                                       pac.Id,
                                       pes.Nome,
                                   }).ToList();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(listaVinculados);
            return json;
        }
    }
}
