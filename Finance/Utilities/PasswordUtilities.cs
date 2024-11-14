using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;

namespace Finance.Utilities;

// Decided to test using extensions for this. It felt like a good opportunity.
public static class PasswordUtilities
{
    public static (string, string) SaltAndHash(this string password)
    {
        string salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        string hashedPass = Argon2.Hash(salt + password);

        return (salt, hashedPass);
    }
}