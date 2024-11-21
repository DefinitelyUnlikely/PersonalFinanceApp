using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data.Interfaces;
using Finance.Views;

namespace Finance.ViewModels;

public partial class IncomeViewModel : ObservableObject
{

    private readonly IUserRepository userRepo;
    private readonly ITransactionRepository transactionRepo;

    private readonly TransactionViewModel transactionViewModel;

    public IncomeViewModel(IUserRepository ur, ITransactionRepository tr, TransactionViewModel vm)
    {
        userRepo = ur;
        transactionRepo = tr;
        transactionViewModel = vm;
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
            // I'm allowing (atm) 0 sum transactions, so I only null check the name.
            if (TransactionName is null)
            {
                throw new Exception("Input required");
            }

            if (userRepo.CurrentUser is null)
            {
                throw new Exception("Something went wrong, CurrentUser is null.");
            }

            await transactionViewModel.AddTransaction(new(userRepo.CurrentUser.Id, TransactionName, -Amount, TransactionDate));

        }
        catch (Exception ex)
        {
            Console.WriteLine("Didn't work: " + ex.Message);
        }
        finally
        {
            await Shell.Current.GoToAsync($"/{nameof(TransactionView)}");
        }
    }
}
