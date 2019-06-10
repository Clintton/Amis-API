using AmisWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmisWebService
{
    public class RefeicaoController : ApiController
    {
        private AmisEntities db = new AmisEntities();

        [HttpPost]
        [Route("api/inserir_refeicao")]
        public void InserirGlicemia([FromBody] RefeicaoModel refeicaoModel)
        {
            var userID = db.Pessoa.Where(x => x.Email == refeicaoModel.Email).Select(x => x.Id);
            foreach (var item in userID)
            {
                var pacienteID = db.Paciente.Where(x => x.IdPessoa == item).Select(x => x.Id);
                foreach (var paciente in pacienteID)
                {
                    Refeicao refeicao = new Refeicao();
                    refeicao.Data = refeicaoModel.Data;
                    refeicao.Hora = refeicaoModel.Hora;
                    refeicao.refeicao1 = refeicaoModel.Refeicao;
                    refeicao.IdPaciente = paciente;
                    db.Refeicao.Add(refeicao);
                }
            }

            db.SaveChanges();
        }

        [HttpPost]
        [Route("api/listar_refeicao")]
        public List<RefeicaoModel> ListarGlicemia([FromBody]RefeicaoModel model)
        {
            var userID = db.Pessoa.Where(x => x.Email == model.Email).Select(x => x.Id);
            List<RefeicaoModel> refeicaoList = new List<RefeicaoModel>();
            IEnumerable<Refeicao> listaGlic = null;
            IEnumerable<int> pacienteID = null;

            foreach (var item in userID)
            {
                pacienteID = db.Paciente.Where(x => x.IdPessoa == item).Select(x => x.Id);

                listaGlic = db.Refeicao.Where(x => x.IdPaciente == item);

            }

            foreach (var item in pacienteID)
            {

                listaGlic = db.Refeicao.Where(x => x.IdPaciente == item);

            }

            foreach (var itemGlic in listaGlic)
            {
                RefeicaoModel refeicaoModel = new RefeicaoModel();

                refeicaoModel.Data = itemGlic.Data;
                refeicaoModel.Hora = itemGlic.Hora;
                refeicaoModel.Id = itemGlic.Id;
                refeicaoModel.Refeicao = itemGlic.refeicao1;
                refeicaoList.Add(refeicaoModel);
            }

            return refeicaoList;
        }
    }
}