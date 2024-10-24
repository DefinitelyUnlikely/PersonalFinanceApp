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


        // Not needed with database
        // public static void CreateTransactionsForTesting()
        // {
        //     Model.Transaction trans1 = new("Selling Books", 52.5, new DateTime(2020, 09, 04));
        //     Model.Transaction trans2 = new("Selling More Books", 52.5, new DateTime(2021, 10, 05));
        //     Model.Transaction trans3 = new("Buying Books", -75, new DateTime(2023, 11, 06));
        //     Model.Transaction trans4 = new("Food", -10, new DateTime(2024, 12, 07));
        //     Model.Transaction trans5 = new("Part time", 530, new DateTime(2024, 12, 08));
        //     Model.Transaction trans6 = new("Pizza Night", -50, new DateTime(2024, 12, 08));
        //     Model.Transaction trans7 = new("Birthday Money", 300, new DateTime(2024, 01, 08));
        //     Model.Transaction trans8 = new("Buying a cat", -400, new DateTime(2024, 01, 08));
        //     Model.Transaction trans9 = new("Part time", 530, new DateTime(2024, 01, 09));

        //     AddTransaction(trans1);
        //     AddTransaction(trans2);
        //     AddTransaction(trans3);
        //     AddTransaction(trans4);
        //     AddTransaction(trans5);
        //     AddTransaction(trans5);
        //     AddTransaction(trans6);
        //     AddTransaction(trans7);
        //     AddTransaction(trans8);
        //     AddTransaction(trans9);

        // }
    }
}