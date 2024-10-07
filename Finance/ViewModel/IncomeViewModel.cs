using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.View;

namespace Finance.ViewModel;

public partial class IncomeViewModel : ObservableObject
{

    private readonly MainViewModel _mainViewModel;

    public IncomeViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
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
            _mainViewModel.AddTransaction(new(TransactionName, Amount, TransactionDate));

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
