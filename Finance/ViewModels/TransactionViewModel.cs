using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;
using Finance.Views;
using CommunityToolkit.Maui.Core;
using Finance.Data.Interfaces;

namespace Finance.ViewModels;

public partial class TransactionViewModel : ObservableObject
{

    public readonly IPopupService popupService;
    public readonly IUserRepository userRepo;
    public readonly ITransactionRepository transactionRepo;
    public readonly IAccountRepository accountRepo;

    [ObservableProperty]
    ObservableCollection<Transaction> transactions = [];

    [ObservableProperty]
    double balance;

    [ObservableProperty]
    Transaction? selectedTransaction;

    [ObservableProperty]
    string? userName;

    [ObservableProperty]
    string? displayName;

    // To be able to await LoadItems, we are now splitting up the constructor.
    // It is now this part + an override of OnNaviateTo in the code-behind.
    public TransactionViewModel(IPopupService ps, ITransactionRepository tr, IUserRepository ur, IAccountRepository ar)
    {
        popupService = ps;
        userRepo = ur;
        transactionRepo = tr;
        accountRepo = ar;
    }

    public async Task LoadItems(string type)
    {

        if (type.Equals("user"))
        {
            if (userRepo.CurrentUser is null)
            {
                throw new ArgumentException("User argument selected, but user is currently NULL.\n");
            }

            var transactionsAsync = await transactionRepo.GetUserTransactionsAsync(userRepo.CurrentUser.Id);
            LoadBalance(transactionsAsync);
            Transactions = new ObservableCollection<Transaction>(transactionsAsync);
        }
        else if (type.Equals("account"))
        {
            if (accountRepo.CurrentAccount is null)
            {
                throw new ArgumentException("Account argument selected, but account is currently NULL.\n");
            }

            var transactionsAsync = await transactionRepo.GetAccountTransactionsAsync(accountRepo.CurrentAccount.Id);
            LoadBalance(transactionsAsync);
            Transactions = new ObservableCollection<Transaction>(transactionsAsync);
        }

    }

    private void LoadBalance(List<Transaction> transactions)
    {
        transactions.ForEach(x => Balance += x.Amount);
    }

    public async Task AddTransaction(Transaction transaction)
    {
        await transactionRepo.AddTransactionAsync(transaction);
        Transactions.Add(transaction);
        Balance += transaction.Amount;
    }

    [RelayCommand]
    public async Task Delete(Transaction transaction)
    {

        if (transaction == null)
        {
            return;
        }

        await transactionRepo.RemoveTransactionAsync(transaction.Id);
        Transactions.Remove(transaction);
        Balance -= transaction.Amount;
    }

    //Placeholder
    [RelayCommand]
    public async Task ChangeAccountName()
    {
        try
        {
            await Shell.Current.GoToAsync($"///{nameof(MainView)}");
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message + "\n", "OK");
        }

    }

}