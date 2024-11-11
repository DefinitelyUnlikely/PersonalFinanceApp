using Finance.Models;


namespace Finance.Managers;

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

    public static List<Transaction> GetTransactions()
    {
        // according to VSCode this is equal to transactions.ToList();
        return [.. transactions];
    }

}
