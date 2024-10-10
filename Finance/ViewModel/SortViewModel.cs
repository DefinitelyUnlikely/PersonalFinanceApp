using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Model;

namespace Finance.ViewModel;

public partial class SortViewModel : ObservableObject
{
    private readonly MainViewModel mainViewModel;

    public List<Model.Transaction> Transactions;
    List<Dictionary<string, ObservableCollection<Transaction>>> listOfDicts;

    [ObservableProperty]
    public ObservableCollection<KeyValuePair<string, ObservableCollection<Transaction>>> selectedDict;

    public SortViewModel(MainViewModel mainViewModel)
    {
        this.mainViewModel = mainViewModel;
        Transactions = new List<Model.Transaction>(mainViewModel.Transactions);

        listOfDicts = DateKey.CreateTransactionDicts(Transactions);

        // se till att det finns värden till att börja med.
        SelectedDict = new ObservableCollection<KeyValuePair<string, ObservableCollection<Transaction>>>(listOfDicts[0]);

    }


    [RelayCommand]
    async Task Year()
    {
        SelectedDict = new ObservableCollection<KeyValuePair<string, ObservableCollection<Transaction>>>(listOfDicts[0]);
    }

    [RelayCommand]
    async Task Month()
    {
        SelectedDict = new ObservableCollection<KeyValuePair<string, ObservableCollection<Transaction>>>(listOfDicts[1]);
    }

    [RelayCommand]
    async Task Week()
    {
        SelectedDict = new ObservableCollection<KeyValuePair<string, ObservableCollection<Transaction>>>(listOfDicts[2]);
    }

    [RelayCommand]
    async Task Day()
    {
        SelectedDict = new ObservableCollection<KeyValuePair<string, ObservableCollection<Transaction>>>(listOfDicts[3]);
    }
}


