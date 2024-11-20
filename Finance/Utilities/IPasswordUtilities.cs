namespace Finance.Utilities;

public interface IPasswordUtilities
{
    (string salt, string hashedPass) HashPassword(string password);
    bool VerifyPassword(string username, string password);
}