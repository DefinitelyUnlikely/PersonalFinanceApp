namespace Finance.Models;

public class Transaction
{

    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public string Name { get; set; }

    public double Amount { get; set; }

    public DateTime Date { get; set; }
    public DateTime Created { get; set; }

    // For new transactions.
    public Transaction(Guid accId, string name, double amount, DateTime date)
    {
        Id = Guid.NewGuid();
        AccountId = accId;
        Name = name;
        Amount = amount;
        Date = date;
        Created = DateTime.Now;

    }

    // For creating transactions already in the database
    public Transaction(Guid guid, Guid accId, string name, double amount, DateTime date, DateTime created)
    {
        Id = guid;
        AccountId = accId;
        Name = name;
        Amount = amount;
        Date = date;
        Created = created;
    }
}
