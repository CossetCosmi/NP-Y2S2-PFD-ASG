using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFD.Cryptography
{
    internal interface IPasswordService
    {
        string Hash(string password, string salt);
        byte[] Hash(byte[] password, byte[] salt);
        bool Verify(string password, string salt, string hash);
        bool Verify(byte[] password, byte[] salt, byte[] hash);
    }
}
