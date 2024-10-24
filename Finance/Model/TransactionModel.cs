using SQLite;

namespace Finance.Model
{
    public class Transaction
    {

        [PrimaryKey, AutoIncrement]
        public int TransactionId { get; set; }

        public string TransactionName { get; set; }

        public double TransactionAmount { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime TransactionDate { get; set; }

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