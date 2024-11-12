using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace Finance.Models
{
    public class Transaction
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Don't matter, as we are switching to PostgreSQL. 
        // But adding it as a placeholder.
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime Date { get; set; }

        public Transaction() { }


        public Transaction(string name, double amount, DateTime transactionDate)
        {
            Name = name;
            Amount = amount;
            Date = transactionDate;

        }

        public Transaction(int id, string name, double amount, DateTime date, DateTime created)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Date = date;
            CreatedDate = created;

        }

    }
}