namespace Finance.Models;

// All getters and setters are placeholder, until I've fixed PostreSQL 
// and know what settings they need for the database to able to use them.
public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }

    // Do we save hashed passwords as strings? What else would they be saved as?
    public string Password { get; set; }

    // As we are going to be using a SQL database, I do not need a transactions field
    // or similar to get all the transactions for one account. That will be fixed with a 
    // foreign key inside the transaction table.
}