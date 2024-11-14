using Finance.Utilities;

namespace Finance.Models;

// All getters and setters are placeholder, until I've fixed PostreSQL 
// and know what settings they need for the database to able to use them.
public class User
{
    // We most likely need both get and set for all fields to use them with postgreSQL
    // as this was true for SQLite.
    public Guid Id { get; set; }

    public string Email { get; set; }
    public string Name { get; set; }

    // Do we save hashed passwords as strings? What else would they be saved as?
    public string Salt { get; set; }
    public string HashedPassword { get; set; }

    // As we are going to be using a SQL database, I do not need a transactions field
    // or similar to get all the transactions for one account. It is simpler to just use 
    // our userID as a relational key inside the transaction model from the get go.


    // if we assume PostgreSQL works in a similar fashion as SQLite did in C#
    // we cannot use a setter to hash the password, as it will be hashed everytime
    // we get an item from the database the first time that section. 
    // So hashing should be done in the ViewModel or the constructor used by
    // the ViewModel. (SQLite used an empty constructor, and then set each field)
    public User(string name, string email, string password)
    {
        Id = new Guid();
        Email = email;
        Name = name;

        (Salt, HashedPassword) = password.SaltAndHash();
        Console.WriteLine(Salt);
        Console.WriteLine(HashedPassword);

    }
}