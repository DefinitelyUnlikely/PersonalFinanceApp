using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Model;

namespace Finance.ViewModel;

public partial class SortViewModel : ObservableObject
{
    private readonly MainViewModel mainViewModel;

    private List<Model.Transaction> Transactions;

    private List<Dictionary<string, List<Model.Transaction>>> dictionaries;

    [ObservableProperty]
    ObservableCollection<DisplayItem> displayList;

    // A mediator is used, as the program would crash if one tried to sort
    // and observableCollection. 
    List<DisplayItem> mediatorList = [];


    public SortViewModel(MainViewModel mainViewModel)
    {
        this.mainViewModel = mainViewModel;
        Transactions = new List<Model.Transaction>(mainViewModel.Transactions);

        dictionaries = DateKey.CreateTransactionDicts(Transactions);

    }


    [RelayCommand]
    void Year()
    {
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[0])
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
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[1])
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
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[2])
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
        foreach (KeyValuePair<string, List<Model.Transaction>> kvp in dictionaries[3])
        {
            mediatorList.Add(new DisplayItem(kvp.Key, kvp.Value));
        }
        mediatorList.Sort((x, y) => x.Key.CompareTo(y.Key));
        DisplayList = new ObservableCollection<DisplayItem>(mediatorList);
        mediatorList = [];
    }
}
