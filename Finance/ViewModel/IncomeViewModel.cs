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
    string? transactionName;

    [ObservableProperty]
    double amount;

    [ObservableProperty]
    DateTime transactionDate = DateTime.Now;

    [RelayCommand]
    async Task SubmitTransaction()
    {
        try
        {
            // Note: Only checking for null on name is an active choice - i.e. I'm allowing transactions with a 0 amount.
            if (TransactionName is null)
            {
                throw new Exception("Input required");
            }
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
