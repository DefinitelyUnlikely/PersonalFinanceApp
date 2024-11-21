namespace Finance.Models;

public class Transaction
{

    public Guid Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; }

    public double Amount { get; set; }

    public DateTime Date { get; set; }
    public DateTime Created { get; set; }

    // For new transactions.
    public Transaction(int userId, string name, double amount, DateTime date)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Amount = amount;
        Date = date;
        Created = DateTime.Now;

    }

    // For creating transactions already in the database
    public Transaction(Guid guid, int userId, string name, double amount, DateTime date, DateTime created)
    {
        Id = guid;
        UserId = userId;
        Name = name;
        Amount = amount;
        Date = date;
        Created = created;
    }
}
