using Finance.ViewModels;

namespace Finance.Views;

public partial class SortView : ContentPage
{
	public SortView(SortViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override bool OnBackButtonPressed()
	{
		Shell.Current.GoToAsync($"/{nameof(TransactionView)}");
		return true;
	}

}