using Notes.Contract.Commands;

namespace Notes.Core.Servants
{
    public interface IAuthTokenServant
    {
        string Encode(TokenPayload payload);

        TokenPayload Decode(string token);
    }
}