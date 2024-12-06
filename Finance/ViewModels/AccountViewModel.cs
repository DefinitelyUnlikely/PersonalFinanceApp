using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Data.Interfaces;
using Finance.Models;
using Npgsql;

namespace Finance.ViewModels;

public partial class AccountViewModel : ObservableObject
{
    private readonly IUserRepository userRepo;
    private readonly IAccountRepository accountRepo;
    private readonly ITransactionRepository transactionRepo;

    [ObservableProperty]
    Account? selectedAccount;

    [ObservableProperty]
    ObservableCollection<Account> accounts = [];

    public AccountViewModel(IUserRepository ur, IAccountRepository ar, ITransactionRepository tr)
    {
        userRepo = ur;
        accountRepo = ar;
        transactionRepo = tr;

        LoadAccounts();
    }

    private async void LoadAccounts()
    {
        if (userRepo.CurrentUser is null)
        {
            throw new Exception("User is null, something has gone wrong.");
        }

        try
        {
            Accounts = new ObservableCollection<Account>(await accountRepo.GetUserAccountsAsync());
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Account error", "Could not load accounts" + e.Message, "OK");
            return;
        }
    }
}
