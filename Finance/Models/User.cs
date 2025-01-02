namespace Finance.Models;

public class User
{

    // Name and email will be unique, but I've decided to still have an id as primary key.
    public int Id { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }
    public string DisplayName { get; set; }

    public string Salt { get; set; }
    public string PasswordHash { get; set; }



    // for creating user objects from the database.
    public User(int id, string email, string displayName, string username, string salt, string password)
    {
        Id = id;
        Email = email;
        UserName = username;
        DisplayName = displayName;
        Salt = salt;
        PasswordHash = password;
    }
}