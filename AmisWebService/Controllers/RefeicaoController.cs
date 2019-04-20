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
    }
}