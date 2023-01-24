using Konscious.Security.Cryptography;
using PFD.Cryptography.Interface;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PFD.Cryptography
{
    internal sealed class PasswordService : IPasswordService
    {
        public string Hash(string password, string salt)
        {
            byte[] p = Encoding.ASCII.GetBytes(password);
            byte[] s = Convert.FromBase64String(salt);

            byte[] h = Hash(p, s);

            return Convert.ToBase64String(h);
        }

        public byte[] Hash(byte[] password, byte[] salt)
        {
            using (var encoder = new Argon2id(password))
            {
                encoder.Salt = salt;
                encoder.DegreeOfParallelism = 4;
                encoder.MemorySize = 256;
                encoder.Iterations = 3;

                return encoder.GetBytes(32);
            }
        }

        public bool Verify(string password, string salt, string hash)
        {
            byte[] p = Encoding.ASCII.GetBytes(password);
            byte[] s = Convert.FromBase64String(salt);
            byte[] h = Convert.FromBase64String(hash);

            return Verify(p, s, h);
        }

        public bool Verify(byte[] password, byte[] salt, byte[] hash) => Hash(password, salt).SequenceEqual(hash);

        public string GetSalt()
        {
            var buffer = new byte[16];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(buffer);
            }

            return Convert.ToBase64String(buffer);
        }
    }
}
