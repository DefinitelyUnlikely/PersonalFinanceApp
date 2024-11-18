using Finance.Utilities;

namespace Finance.Models;

public class User
{

    // Name and email will be unique, but I've decided to still have an id as prmary key.
    public int Id { get; set; }

    public string Email { get; set; }
    public string Name { get; set; }

    public string Salt { get; set; }
    public string PasswordHash { get; set; }


    // As I decided to not use EF Core at the moment, this might need to change.
    public User(string email, string name, string password)
    {
        Email = email;
        Name = name;

        (Salt, PasswordHash) = password.SaltAndHash();
    }
}