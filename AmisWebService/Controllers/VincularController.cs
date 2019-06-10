using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmisWebService.Controllers
{
    public class VincularController : ApiController
    {
        private AmisEntities db = new AmisEntities();

        [HttpGet]
        [Route("api/listar_pacientes")]
        public object ListarPacientes()
        {
            var consulta = (from pa in db.Paciente
                            join pe in db.Pessoa on pa.IdPessoa equals pe.Id
                           select new
                           {
                               pa.Id,
                               pe.Nome
                           }).ToList();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(consulta);
            return json;
        }

        [HttpGet]
        [Route("api/vicular_paciente")]
        public void VincularPaciente([FromUri] int IdPaciente, [FromUri] string email)
        {
            //Pessoa pessoa = db.Pessoa.Find(email);

            var medicoId = (from med in db.Medico
                               join pe in db.Pessoa on med.IdPessoa equals pe.Id
                               where pe.Email == email
                            select new
                            {
                                med.Id,
                            }).ToList();

            Paciente_Vinculado paciente_Vinculado = new Paciente_Vinculado();
            paciente_Vinculado.IdMedico = medicoId[0].Id;
            paciente_Vinculado.IdPaciente = (int)IdPaciente;

            db.Paciente_Vinculado.Add(paciente_Vinculado);
            db.SaveChanges();
        }
    }
}
