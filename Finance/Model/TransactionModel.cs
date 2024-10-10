namespace Finance.Model
{
    public class Transaction
    {

        private static int transactionIdCounter = 0;
        public int TransactionId { get; }

        public string TransactionName { get; }

        public double TransactionAmount { get; }

        public DateTime CreatedDate { get; }
        public DateTime TransactionDate { get; }


        public Transaction(string name, double amount, DateTime transactionDate)
        {
            TransactionId = ++transactionIdCounter;

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