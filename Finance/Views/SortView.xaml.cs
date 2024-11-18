using Finance.ViewModels;

namespace Finance.Views;

public partial class SortView : ContentPage
{
	public SortView(SortViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}