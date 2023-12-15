using CrossCutting.Security.Models;

namespace CrossCutting.Security.Interfaces
{
    public interface IHasher
    {
        Credential Hash(string plainText);
        bool VerifyHash(string plainText, string hashedString, string salt);
    }
}