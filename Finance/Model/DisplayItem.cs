namespace Finance.Model;

public class DisplayItem
{
    public string Key { get; }
    public List<Model.Transaction> AllTransactions { get; }
    public double Income { get; }
    public double Expense { get; }
    public double Total { get; }

    public DisplayItem(string key, List<Model.Transaction> value)
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
