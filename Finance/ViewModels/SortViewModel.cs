using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;
using Finance.Utilities;

namespace Finance.ViewModels;

public partial class SortViewModel : ObservableObject
{
    private readonly TransactionViewModel transactionViewModel;

    private List<Models.Transaction> Transactions;

    private List<Dictionary<string, List<Models.Transaction>>> dictionaries;

    [ObservableProperty]
    ObservableCollection<DisplayItem> displayList = [];

    // A mediator is used, as the program would crash if one tried to sort
    // an observableCollection. 
    List<DisplayItem> mediatorList = [];


    public SortViewModel(TransactionViewModel transactionViewModel)
    {
        this.transactionViewModel = transactionViewModel;
        Transactions = new List<Transaction>(transactionViewModel.Transactions);

        dictionaries = DateKey.CreateTransactionDicts(Transactions);

        Year();
    }


    [RelayCommand]
    void Year()
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
    void Month()
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
    void Week()
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
    void Day()
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