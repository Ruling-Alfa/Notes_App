using Amazon.SecurityToken.Model;
using CrossCutting.Security.Configurations;
using CrossCutting.Security.Interfaces;
using CrossCutting.Security.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Drawing;
using System.Security.Cryptography;

namespace CrossCutting.Security
{
    public class HMACSHA256_Hasher : IHasher
    {
        public Credential Hash(string plainText)
        {

            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes.
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

            return Hash(plainText, salt);
        }

        public Credential Hash(string plainText, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainText!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
            numBytesRequested: 256 / 8));

            var creds = new Credential()
            {
                Hash = hashed,
                Salt = Convert.ToBase64String(salt)
            };
            return creds;
        }

        public bool VerifyHash(string plainText, string hashedString, string salt)
        {
            var plainTextHash =  Hash(plainText, Convert.FromBase64String(salt));
            return plainTextHash.Hash == hashedString;
        }

    }
}
