using System;
using Notes.Contract.Commands;
using Notes.Core.Configuration;

namespace Notes.Core.Servants
{
    public class JwtTokenServant : IAuthTokenServant
    {
        private IConfiguration _configuration;

        public JwtTokenServant(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenPayload Decode(string token)
        {
            return Jose.JWT.Decode<TokenPayload>(token, _configuration.JwtKey);
        }

        public string Encode(TokenPayload payload)
        {
            return Jose.JWT.Encode(payload, _configuration.JwtKey, Jose.JwsAlgorithm.HS256);
        }
    }
}