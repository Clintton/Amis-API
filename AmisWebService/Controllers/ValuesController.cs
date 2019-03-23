using AmisWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmisWebService.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/values/5
        [HttpPost]
        [Route("api/logar")]
        public bool realizarLogin(LoginModel loginModel)
        {
            using(AmisEntities amisEntities = new AmisEntities())
            {
                Pessoa user = amisEntities.Pessoa.Find(loginModel.Email);
                if (user.Password == Util.CriptografarSenha(loginModel.Senha))
                {
                    return true;
                }

                else return false;
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
