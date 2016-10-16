using System;
using Nancy;
using Notes.Core.Infrastructure;
using System.Linq;

namespace Notes.WebApi.Infrastructure
{
    public class IdentityProvider : IIdentityProvider
    {
        private INancyContextWrapper _contextWrapper;
        private NotesIdentity _currentIdentity;

        public IdentityProvider(INancyContextWrapper contextWrapper)
        {
            _contextWrapper = contextWrapper;
        }

        public NotesIdentity CurrentIdentity
        {
            get
            {

                return _currentIdentity = _currentIdentity ?? GetCurrentIdentity();
            }
        }

        private NotesIdentity GetCurrentIdentity()
        {
            var identity = _contextWrapper.Context?.CurrentUser;
            if(identity == null)
            {
                return null;
            }


            return new NotesIdentity(Guid.Parse(identity.Claims.First(x=>x.Type == ClaimType.AccountId).Value),identity.Identity.Name,identity.Identity.AuthenticationType);
        }
    }
}