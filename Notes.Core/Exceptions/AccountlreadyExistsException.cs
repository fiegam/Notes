namespace Notes.Core.Exceptions
{
    public class AccountlreadyExistsException : BadRequestException
    {
        public AccountlreadyExistsException(string accountName) : base($"An account {accountName} already exists.")
        {
        }
    }
}