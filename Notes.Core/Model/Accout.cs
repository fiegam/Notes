using System;

namespace Notes.Core.Model
{
    public class Account
    {
        public string EmailAddress { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}