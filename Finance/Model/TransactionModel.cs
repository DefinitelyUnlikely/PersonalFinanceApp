namespace Finance.Model
{
    public class Transaction
    {
        // Att vi startar på noll är en placeholder, tanken är att vi ändrar denna 
        // så fort vi laddat upp från filen.
        private static int transactionIdCounter = 0;
        public int TransactionId { get; }

        public string TransactionName { get; }

        // positiva värden för inkomst, negativa värden för utgift. 
        public double TransactionAmount { get; }

        public DateTime CreatedDate { get; }
        public DateTime TransactionDate { get; }



        // Vi behöver två olika constructors. En som vi använder i programmet
        // och en som vi använder när vi vill läsa in från fil (Då den kommer ha lite annorlunda CreatedDate osv.)
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