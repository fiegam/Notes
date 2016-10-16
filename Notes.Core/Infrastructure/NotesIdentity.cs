using System;
using System.Security.Principal;

namespace Notes.Core.Infrastructure
{
    public class NotesIdentity : IIdentity
    {
        public NotesIdentity(Guid id, string name, string authenticationType)
        {
            Id = id;
            Name = name;
            AuthenticationType = authenticationType;
        }

        public Guid Id { get;  }

        public string AuthenticationType { get; }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name { get; }
    }
}