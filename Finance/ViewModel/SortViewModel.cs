using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Model;

namespace Finance.ViewModel;

public partial class SortViewModel : ObservableObject
{
    private readonly MainViewModel mainViewModel;

    // Denna binder vi sedan till transaktionerna i vår MainViewModel.
    // Så vi kan lösa vår dictionary.
    private List<Model.Transaction> Transactions;

    // Denna behöver vi bara för att spara våra dictionaries.
    private List<Dictionary<string, List<Model.Transaction>>> dictionaries;

    [ObservableProperty]
    ObservableCollection<DictionaryItem> displayList;

    List<DictionaryItem> mediatorList = [];


    public SortViewModel(MainViewModel mainViewModel)
    {
        this.mainViewModel = mainViewModel;
        Transactions = new List<Model.Transaction>(mainViewModel.Transactions);

        dictionaries = DateKey.CreateTransactionDicts(Transactions);

    }


    [RelayCommand]
    async Task Year()
    {
        Console.WriteLine("Before creating the list");
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[0])
        {
            mediatorList.Add(new DictionaryItem(kvp.Key, kvp.Value));
        }
        Console.WriteLine("After the list");
        DisplayList = new ObservableCollection<DictionaryItem>(mediatorList);
        mediatorList = [];
    }

    [RelayCommand]
    async Task Month()
    {
        Console.WriteLine("Before creating the list");
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[1])
        {
            mediatorList.Add(new DictionaryItem(kvp.Key, kvp.Value));
        }
        Console.WriteLine("After the list");
        DisplayList = new ObservableCollection<DictionaryItem>(mediatorList);
        mediatorList = [];
    }


    [RelayCommand]
    async Task Week()
    {
        Console.WriteLine("Before creating the list");
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[2])
        {
            mediatorList.Add(new DictionaryItem(kvp.Key, kvp.Value));
        }
        Console.WriteLine("After the list");
        DisplayList = new ObservableCollection<DictionaryItem>(mediatorList);
        mediatorList = [];
    }


    [RelayCommand]
    async Task Day()
    {
        Console.WriteLine("Before creating the list");
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[3])
        {
            mediatorList.Add(new DictionaryItem(kvp.Key, kvp.Value));
        }
        Console.WriteLine("After the list");
        DisplayList = new ObservableCollection<DictionaryItem>(mediatorList);
        mediatorList = [];
    }
}
