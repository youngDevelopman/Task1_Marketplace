using System.Security.Cryptography;
using System.Text;

namespace Task1_Marketplace.Helpers
{
    public static class PasswordHasher
    {
        public static (string Hash, string Salt) HashPassword(string password)
        {
            // Generate a random salt
            byte[] saltBytes = GenerateSalt();
            string salt = Convert.ToBase64String(saltBytes);

            // Combine the password and salt, then hash
            string hashedPassword = HashPasswordWithSalt(password, saltBytes);

            return (hashedPassword, salt);
        }

        public static string HashPasswordWithSalt(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                // Combine password and salt, then hash
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);

                // Convert hashed bytes to a string representation
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[32];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public static bool ValidatePassword(string password, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            string hashedPassword = HashPasswordWithSalt(password, saltBytes);

            return hashedPassword == storedHash;
        }
    }
}
