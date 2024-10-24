using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Finance.ViewModel;

public partial class IncomeViewModel : ObservableObject
{

    private readonly MainViewModel mainViewModel;

    public IncomeViewModel(MainViewModel mainViewModel)
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
            await mainViewModel.AddTransaction(new(TransactionName, Amount, TransactionDate));
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
