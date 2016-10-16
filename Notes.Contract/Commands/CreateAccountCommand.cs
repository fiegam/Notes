namespace Notes.Contract.Commands
{
    public class CreateAccountCommand
    {
        public string AccountName { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }
    }
}