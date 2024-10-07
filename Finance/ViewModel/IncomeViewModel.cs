using CommunityToolkit.Mvvm.ComponentModel;

namespace Finance.ViewModel;

public partial class IncomeViewModel : ObservableObject
{
    [ObservableProperty]
    DateOnly transactionDate = DateOnly.FromDateTime(DateTime.Now);
}
