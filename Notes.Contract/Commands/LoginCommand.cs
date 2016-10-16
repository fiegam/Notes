using System;

namespace Notes.Contract.Commands
{
    public class LoginCommand
    {
        public string AccountName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandResult
    {
        public string Token { get; set; }
        public string AccountName { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ValidUntil { get; set; }
    }

    public class TokenPayload
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}