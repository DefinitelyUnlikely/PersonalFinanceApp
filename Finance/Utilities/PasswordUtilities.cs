// https://github.com/mheyman/Isopoh.Cryptography.Argon2
using Finance.Data.Interfaces;
using Finance.Models;
using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;

namespace Finance.Utilities;

// Decided to test using extensions for this. It felt like a good opportunity.
public class PasswordUtilities : IPasswordUtilities
{


    private readonly IUserRepository userRepo;

    public PasswordUtilities(IUserRepository ur)
    {
        userRepo = ur;
    }
    public (string salt, string hashedPass) HashPassword(string password)
    {
        string salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        string hashedPass = Argon2.Hash(salt + password);

        return (salt, hashedPass);
    }

    public async Task<bool> VerifyPassword(string username, string password)
    {

        User? user = await userRepo.GetUserAsync(username.ToUpper());

        if (user is null)
        {
            return false;
        }

        return Argon2.Verify(user.PasswordHash, user.Salt + password);
    }
}