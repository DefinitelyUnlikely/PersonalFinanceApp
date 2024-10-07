using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Finance.ViewModel;

public partial class ExpenseViewModel : ObservableObject
{
    private readonly MainViewModel mainViewModel;

    public ExpenseViewModel(MainViewModel mainViewModel)
    {
        this.mainViewModel = mainViewModel;
    }

    [ObservableProperty]
    string transactionName = string.Empty;

    [ObservableProperty]
    double amount;

    [ObservableProperty]
    DateTime transactionDate = DateTime.Now;

    [RelayCommand]
    async Task SubmitTransaction()
    {
        try
        {
            mainViewModel.AddTransaction(new(TransactionName, -Amount, TransactionDate));

            TransactionName = string.Empty;
            Amount = 0;
            TransactionDate = DateTime.Now;

        }
        catch (Exception ex)
        {
            Console.WriteLine("Didn't work: " + ex.Message);
        }
        finally
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
