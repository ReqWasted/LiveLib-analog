using System.Security.Cryptography;
using LiveLib.Application.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LiveLib.PasswordHasher
{
    public class PasswordHasher : IPassowrdHasher
    {
        private const int Iterations = 10000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = GenerateByteHash(salt, password);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        public bool Verify(string password, string hashedPassword)
        {
            string[] saltAndHash = hashedPassword.Split(':');
            if (saltAndHash.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(saltAndHash[0]);
            byte[] hash = Convert.FromBase64String(saltAndHash[1]);
            byte[] newHash = GenerateByteHash(salt, password);

            return CryptographicOperations.FixedTimeEquals(hash, newHash);
        }

        private byte[] GenerateByteHash(byte[] salt, string password)
        {
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: HashSize);

            return hash;
        }
    }
}
