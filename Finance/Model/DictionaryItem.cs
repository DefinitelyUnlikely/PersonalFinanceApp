namespace Finance.Model;

public class DictionaryItem
{
    public string Key { get; }
    public List<Model.Transaction> AllTransactions { get; }
    public double Income { get; }
    public double Expense { get; }
    public double Total { get; }

    public DictionaryItem(string key, List<Model.Transaction> value)
    {
        Key = key;
        AllTransactions = value;

        foreach (Transaction transaction in AllTransactions)
        {
            if (transaction.TransactionAmount >= 0)
            {
                Income += transaction.TransactionAmount;
            }
            else
            {
                Expense += transaction.TransactionAmount;
            }
        }

        Total = Income + Expense;
    }
}
