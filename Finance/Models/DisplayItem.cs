namespace Finance.Models;

public class DisplayItem
{
    public string Key { get; }
    public List<Transaction> AllTransactions { get; }
    public double Income { get; }
    public double Expense { get; }
    public double Total { get; }

    public DisplayItem(string key, List<Transaction> value)
    {
        Key = key;
        AllTransactions = value;

        foreach (Transaction transaction in AllTransactions)
        {
            if (transaction.Amount >= 0)
            {
                Income += transaction.Amount;
            }
            else
            {
                Expense += transaction.Amount;
            }
        }

        Total = Income + Expense;
    }
}
