using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AmisWebService
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                string username = context.UserName;
                string password = Util.CriptografarSenha(context.Password);
                using (AmisEntities amisEntities = new AmisEntities())
                {
                    var consulta = amisEntities.Pessoa
                    .Where(x => x.Email == username && x.Password == password)
                    .Select(x => new { x.Id, x.Email, x.Tipo, x.Password });

                    foreach (var item in consulta)
                    {

                        if (item != null)
                        {

                            List<Claim> claims = new List<Claim>
                             {
                                new Claim(ClaimTypes.Name , item.Email),
                                new Claim("UserID", item.Id.ToString()),
                                new Claim(ClaimTypes.Role, item.Tipo)
                             };

                            ClaimsIdentity OAuthIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                            context.Validated(new Microsoft.Owin.Security.AuthenticationTicket(OAuthIdentity, new Microsoft.Owin.Security.AuthenticationProperties() { }));
                        }
                        else
                        {
                            context.SetError("erro", "erro");
                        }
                    }
                }

            });
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }

    }
}