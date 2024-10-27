namespace Finance.Model
{
    public class TransactionManager
    {

        private static List<Transaction> transactions = new();

        public static void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public static void RemoveTransaction(Transaction transaction)
        {

            if (!transactions.Contains(transaction))
            {
                return;
            }

            transactions.Remove(transaction);
        }

        public static List<Model.Transaction> GetTransactions()
        {
            return transactions;
        }

    }
}