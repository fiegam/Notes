using Nancy;
using Nancy.Authentication.Stateless;
using Notes.Core.Infrastructure;
using Notes.Core.Servants;
using System;
using System.Security.Claims;

namespace Notes.WebApi.Infrastructure
{
    public interface IAuthenticationProvider
    {
        void Enable(INancyModule module);
    }

    public class AuthenticationProvider : IAuthenticationProvider
    {
        private IAuthTokenServant _tokenServant;

        public AuthenticationProvider(IAuthTokenServant tokenServant)
        {
            _tokenServant = tokenServant;
        }

        public void Enable(INancyModule module)
        {
            StatelessAuthentication.Enable(module, new StatelessAuthenticationConfiguration(ctx =>
             {
                 var token = ctx.Request.Headers.Authorization;

                 try
                 {
                     var payload = _tokenServant.Decode(token);

                     var tokenExpires = payload.ValidUntil;

                     if (tokenExpires > DateTime.UtcNow)
                     {
                         var notesIdentity = new NotesIdentity(payload.AccountId, payload.AccountName, "token");
                         var identity = new ClaimsIdentity(notesIdentity);
                         identity.AddClaim(new Claim(ClaimType.AccountId, payload.AccountId.ToString()));
                         return new ClaimsPrincipal(identity);
                     }
                     return null;
                 }
                 catch (Exception)
                 {
                     return null;
                 }
             }));
        }
    }
}