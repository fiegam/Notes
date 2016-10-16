using System;
using System.Security.Cryptography;
using System.Text;

namespace Notes.Core.Servants
{
    public class HashingServant : IHashingServant
    {
        private const int saltSize = 256;

        public byte[] CreateSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltSize];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return buff;
        }

        public string CreatePasswordHash(string pwd, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            var pwdBytes = Encoding.ASCII.GetBytes(pwd);

            byte[] bytesToBeHashed =
              new byte[pwdBytes.Length + salt.Length];

            for (int i = 0; i < pwdBytes.Length; i++)
            {
                bytesToBeHashed[i] = pwdBytes[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                bytesToBeHashed[pwdBytes.Length + i] = salt[i];
            }

            var hashed = algorithm.ComputeHash(bytesToBeHashed);

            return Convert.ToBase64String(hashed);
        }
    }
}