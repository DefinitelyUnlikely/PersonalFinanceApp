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

        Console.WriteLine("Before creating the list");
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[0])
        {
            mediatorList.Add(new DictionaryItem(kvp.Key, kvp.Value));

        }
        Console.WriteLine("After the list");
        DisplayList = new ObservableCollection<DictionaryItem>(mediatorList);

    }


    [RelayCommand]
    async Task Year()
    {

    }

    [RelayCommand]
    async Task Month()
    {
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[1])
        {
            DisplayList.Add(new DictionaryItem(kvp.Key, kvp.Value));
        }
    }

    [RelayCommand]
    async Task Week()
    {
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[2])
        {
            DisplayList.Add(new DictionaryItem(kvp.Key, kvp.Value));
        }
    }

    [RelayCommand]
    async Task Day()
    {
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[3])
        {
            DisplayList.Add(new DictionaryItem(kvp.Key, kvp.Value));
        }
    }
}
