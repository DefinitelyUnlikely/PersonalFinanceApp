using SQLite;

namespace Finance.Model
{
    public class Transaction
    {

        [PrimaryKey, AutoIncrement]
        public int TransactionId { get; set; }

        public string TransactionName { get; }

        public double TransactionAmount { get; }

        public DateTime CreatedDate { get; }
        public DateTime TransactionDate { get; }

        public Transaction() { }


        public Transaction(string name, double amount, DateTime transactionDate)
        {
            TransactionName = name;
            TransactionAmount = amount;
            TransactionDate = transactionDate;

        }

        public Transaction(int id, string name, double amount, DateTime date, DateTime created)
        {
            TransactionId = id;
            TransactionName = name;
            TransactionAmount = amount;
            TransactionDate = date;
            CreatedDate = created;

        }

    }
}