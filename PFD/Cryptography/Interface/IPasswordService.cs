namespace PFD.Cryptography.Interface
{
    internal interface IPasswordService
    {
        string Hash(string password, string salt);
        byte[] Hash(byte[] password, byte[] salt);
        bool Verify(string password, string salt, string hash);
        bool Verify(byte[] password, byte[] salt, byte[] hash);
    }
}
