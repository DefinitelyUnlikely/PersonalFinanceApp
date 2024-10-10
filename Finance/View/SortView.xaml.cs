using Finance.ViewModel;

namespace Finance.View;

public partial class SortView : ContentPage
{
	public SortView(SortViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}