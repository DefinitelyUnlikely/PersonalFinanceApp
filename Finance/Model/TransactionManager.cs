using System.Transactions;

namespace Finance.Model
{
    public class TransactionManager
    {

        private static List<Transaction> transactions = new();

        public static List<Transaction> LoadTransactions()
        {
            // Tänkt plan för denna: Öppna filen med en streamwriter
            // så att vi enklet kan gå rad för rad. Varje rad i filen skall representera ett transaction objekt. 
            // För varje rad (som går igenom) ökar vi även en counter med 1. Den sätter vi sedan transactionIdCounter till.
            // Så vi behöver en funktion som kan anropas för det, den är ju private.
            return [];
        }

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