namespace Finance.Models;

public class Transaction
{

    // Primary key, placeholder until I know more about 
    // using PostgreSQL.
    public Guid Id { get; set; }

    // Foreign key
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public double Amount { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime Date { get; set; }

    public Transaction(string name, double amount, DateTime transactionDate)
    {
        Name = name;
        Amount = amount;
        Date = transactionDate;

    }

    public Transaction(string name, double amount, DateTime date, DateTime created)
    {
        Name = name;
        Amount = amount;
        Date = date;
        CreatedDate = created;

    }

}
