using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data.Interfaces;
using Finance.Models;
using Finance.Utilities;

namespace Finance.ViewModels;

public partial class SortViewModel : ObservableObject
{

    public readonly IUserRepository userRepo;
    public readonly ITransactionRepository transactionRepo;
    public readonly IAccountRepository accountRepo;

    [ObservableProperty]
    ObservableCollection<Transaction> transactions = [];

    private List<Dictionary<string, List<Transaction>>> dictionaries = [];

    [ObservableProperty]
    ObservableCollection<DisplayItem> displayList = [];

    // A mediator is used, as the program would crash if one tried to sort
    // an observableCollection. 
    List<DisplayItem> mediatorList = [];

    // For some reason, 'Transactions' is empty when the constructor runs. Both the copy and list in the VM.
    // if you've gotten here from one of the accountViews. I would prefer not having to do
    // another database query to get the transactions yet again.
    public SortViewModel(ITransactionRepository tr, IUserRepository ur, IAccountRepository ar)
    {
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
            Transactions = new ObservableCollection<Transaction>(transactionsAsync);
            dictionaries = DateKey.CreateTransactionDicts(transactionsAsync);
        }
        else if (type.Equals("account"))
        {
            if (accountRepo.CurrentAccount is null)
            {
                throw new ArgumentException("Account argument selected, but account is currently NULL.\n");
            }

            var transactionsAsync = await transactionRepo.GetAccountTransactionsAsync(accountRepo.CurrentAccount.Id);
            Transactions = new ObservableCollection<Transaction>(transactionsAsync);
            dictionaries = DateKey.CreateTransactionDicts(transactionsAsync);
        }

        Year();
    }


    [RelayCommand]
    public void Year()
    {
        foreach (KeyValuePair<string, List<Transaction>> kvp in dictionaries[0])
        {
            mediatorList.Add(new DisplayItem(kvp.Key, kvp.Value));
        }
        mediatorList.Sort((x, y) => x.Key.CompareTo(y.Key));
        DisplayList = new ObservableCollection<DisplayItem>(mediatorList);
        mediatorList = [];
    }

    [RelayCommand]
    public void Month()
    {
        foreach (KeyValuePair<string, List<Transaction>> kvp in dictionaries[1])
        {
            mediatorList.Add(new DisplayItem(kvp.Key, kvp.Value));
        }
        mediatorList.Sort((x, y) => x.Key.CompareTo(y.Key));
        DisplayList = new ObservableCollection<DisplayItem>(mediatorList);
        mediatorList = [];
    }


    [RelayCommand]
    public void Week()
    {
        foreach (KeyValuePair<string, List<Transaction>> kvp in dictionaries[2])
        {
            mediatorList.Add(new DisplayItem(kvp.Key, kvp.Value));
        }
        mediatorList.Sort((x, y) => x.Key.CompareTo(y.Key));
        DisplayList = new ObservableCollection<DisplayItem>(mediatorList);
        mediatorList = [];
    }


    [RelayCommand]
    public void Day()
    {
        foreach (KeyValuePair<string, List<Transaction>> kvp in dictionaries[3])
        {
            mediatorList.Add(new DisplayItem(kvp.Key, kvp.Value));
        }
        mediatorList.Sort((x, y) => x.Key.CompareTo(y.Key));
        DisplayList = new ObservableCollection<DisplayItem>(mediatorList);
        mediatorList = [];
    }
}