namespace Finance.Utilities;

public interface IPasswordUtilities
{
    (string salt, string hashedPass) HashPassword(string password);
    Task<bool> VerifyPassword(string username, string password);
}