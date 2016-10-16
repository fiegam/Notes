using System;

namespace Notes.Contract.Model
{
    public class Account
    {
        public string EmailAddress { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}