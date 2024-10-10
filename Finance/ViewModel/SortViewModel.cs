using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Model;

namespace Finance.ViewModel;

public partial class SortViewModel : ObservableObject
{
    private readonly MainViewModel mainViewModel;

    [ObservableProperty]
    ObservableCollection<Model.Transaction> transactions;

    readonly Dictionary<string, ObservableCollection<Model.Transaction>> yearDict = [];
    readonly Dictionary<string, ObservableCollection<Model.Transaction>> monthDict = [];
    readonly Dictionary<string, ObservableCollection<Model.Transaction>> weekDict = [];
    readonly Dictionary<string, ObservableCollection<Model.Transaction>> dayDict = [];

    [ObservableProperty]
    Dictionary<string, ObservableCollection<Model.Transaction>> selectedSort;

    public SortViewModel(MainViewModel mainViewModel)
    {
        this.mainViewModel = mainViewModel;
        Transactions = mainViewModel.Transactions;

        // Gå igenom alla transaktioner, lägg till dem i alla fyra dictionaries.
        // En dictionary för varje sätt att sortera. 
        foreach (Model.Transaction tn in Transactions)
        {
            // År
            if (!yearDict.ContainsKey(DateKeyMaker.GetYearKey(tn.TransactionDate)))
            {
                yearDict[DateKeyMaker.GetYearKey(tn.TransactionDate)] = [];
            }

            yearDict[DateKeyMaker.GetYearKey(tn.TransactionDate)].Add(tn);

            // Månad
            if (!monthDict.ContainsKey(DateKeyMaker.GetMonthKey(tn.TransactionDate)))
            {
                monthDict[DateKeyMaker.GetMonthKey(tn.TransactionDate)] = [];
            }

            monthDict[DateKeyMaker.GetMonthKey(tn.TransactionDate)].Add(tn);

            // Vecka
            if (!weekDict.ContainsKey(DateKeyMaker.GetWeekKey(tn.TransactionDate)))
            {
                weekDict[DateKeyMaker.GetWeekKey(tn.TransactionDate)] = [];
            }

            weekDict[DateKeyMaker.GetWeekKey(tn.TransactionDate)].Add(tn);

            // Dag
            if (!dayDict.ContainsKey(DateKeyMaker.GetDayKey(tn.TransactionDate)))
            {
                dayDict[DateKeyMaker.GetDayKey(tn.TransactionDate)] = [];
            }

            dayDict[DateKeyMaker.GetDayKey(tn.TransactionDate)].Add(tn);
        }

        SelectedSort = yearDict;
    }


    // Vi vill antagligen vissa totalen för ett givet år, månad osv, så vi kan lika gärna skapa en funktion
    // för det. Även om jag tror att man kan göra det med en lambda (tack vare .Sum()), så blir det lite tydligare att det är 
    // just räkna totalen jag vill göra, då metoden kan heta "CalculateSumOfAmount" eller något.
    [RelayCommand]
    async Task Year()
    {
        try
        {
            Console.WriteLine("Inside Year, Try Clause");
            SelectedSort = yearDict;
            Console.WriteLine("Inside Year, Try Clause, after");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Didn't work: " + ex.Message);
        }
    }


    [RelayCommand]
    async Task Month()
    {
        try
        {
            Console.WriteLine("Inside the Month Task, Try clause");
            foreach (KeyValuePair<string, ObservableCollection<Model.Transaction>> kvp in monthDict)
            {
                Console.WriteLine(kvp.Key);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Didn't work: " + ex.Message);
        }
    }


    [RelayCommand]
    async Task Week()
    {
        try
        {
            Console.WriteLine("Inside the Week Task, Try clause");
            foreach (KeyValuePair<string, ObservableCollection<Model.Transaction>> kvp in weekDict)
            {
                Console.WriteLine(kvp.Key);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Didn't work: " + ex.Message);
        }
    }


    [RelayCommand]
    async Task Day()
    {
        try
        {
            Console.WriteLine("Inside the Day Task, Try clause");
            foreach (KeyValuePair<string, ObservableCollection<Model.Transaction>> kvp in dayDict)
            {
                Console.WriteLine(kvp.Key);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Didn't work: " + ex.Message);

        }
    }
}
