using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace BanqueNET.Helpers
{
    public class PasswordHelper
    {
        public static string hash(string username, string clearPassword)
        {
            // Generate a 128-bit salt using a sequence of cryptographically strong random bytes.
            string hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: clearPassword!,
                    salt: Encoding.UTF8.GetBytes(username),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8
                )
            );
            return hashedPassword;
        } 
    }
}