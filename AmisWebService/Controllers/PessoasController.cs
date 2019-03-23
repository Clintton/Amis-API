using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AmisWebService;
using AmisWebService.Models;
namespace AmisWebService.Controllers
{
    public class PessoasController : ApiController
    {
        private AmisEntities db = new AmisEntities();

        //[HttpPost]
        [Route("api/realizar_cadastro")]
        public void Cadastro([FromBody]CadastroModel cadastroModel)
        {
            Pessoa pessoa = new Pessoa();
            pessoa.Nome = cadastroModel.Nome;
            pessoa.Sobrenome = cadastroModel.SobreNome;
            pessoa.Email = cadastroModel.Email;
            string senhaCrip = Util.CriptografarSenha(cadastroModel.Senha);
            pessoa.Password = senhaCrip;
            pessoa.Tipo = cadastroModel.TipoUsuario;
            pessoa.Sexo = "";
            pessoa.Telefone = "";
            db.Pessoa.Add(pessoa);
            if(pessoa.Tipo == "paciente")
            {
                Paciente paciente = new Paciente();
                paciente.IdPessoa = pessoa.Id;
                paciente.Altura = "";
                paciente.Diagnostico = DateTime.Parse("1/1/1999");
                paciente.ObjetivoAntesDormir = 0;
                paciente.ObjetivoPosRefeicao = 0;
                paciente.ObjetivoPreRefeicao = 0;
                paciente.Peso = "";
                paciente.Terapia = "";
                paciente.TipoDiabetes = "";
                paciente.Pessoa = pessoa;
                db.Paciente.Add(paciente);
            }

            else
            {
                Medico medico = new Medico();
                medico.IdPessoa = pessoa.Id;
                medico.Cpf = "";
                medico.Crm = "";
                medico.EnderecoConsultorio = "";
                medico.uf = "";
                medico.Pessoa = pessoa;
                db.Medico.Add(medico);
            }

            db.SaveChanges();
        }
        // GET: api/Pessoas
        public IQueryable<Pessoa> GetPessoa()
        {
            return db.Pessoa;
        }

        // GET: api/Pessoas/5
        [ResponseType(typeof(Pessoa))]
        public IHttpActionResult GetPessoa(int id)
        {
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(pessoa);
        }

        // PUT: api/Pessoas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPessoa(int id, Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            db.Entry(pessoa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pessoas
        [ResponseType(typeof(Pessoa))]
        public IHttpActionResult PostPessoa(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pessoa.Add(pessoa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pessoa.Id }, pessoa);
        }

        // DELETE: api/Pessoas/5
        [ResponseType(typeof(Pessoa))]
        public IHttpActionResult DeletePessoa(int id)
        {
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            db.Pessoa.Remove(pessoa);
            db.SaveChanges();

            return Ok(pessoa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PessoaExists(int id)
        {
            return db.Pessoa.Count(e => e.Id == id) > 0;
        }
    }
}