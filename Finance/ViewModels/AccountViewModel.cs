using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data.Interfaces;
using Finance.Models;
using Finance.Views;
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

    [ObservableProperty]
    string? userName;

    [ObservableProperty]
    string? displayName;

    public AccountViewModel(IUserRepository ur, IAccountRepository ar, ITransactionRepository tr)
    {
        userRepo = ur;
        accountRepo = ar;
        transactionRepo = tr;

        DisplayName = userRepo.CurrentUser!.DisplayName;
        UserName = userRepo.CurrentUser!.UserName;

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

    [RelayCommand]
    private async Task AccountDetails()
    {
        if (SelectedAccount is null)
        {
            await Shell.Current.DisplayAlert("Account error", "Current account is NULL", "OK");
            return;
        }

        accountRepo.SetAccount(SelectedAccount.Id);
        await Shell.Current.GoToAsync($"{nameof(TransactionView)}");

    }

    [RelayCommand]
    private async Task ShowAllTransactions()
    {
        Console.WriteLine("Is the button working?");
        accountRepo.SetAccount(null);
        await Shell.Current.GoToAsync($"{nameof(TransactionView)}");
    }

    // Placeholder
    [RelayCommand]
    public async Task ChangeAccountName()
    {
        try
        {
            await Shell.Current.GoToAsync($"///{nameof(MainView)}");
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message, "OK");
        }

    }
}
