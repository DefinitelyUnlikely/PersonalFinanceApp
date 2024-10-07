using Finance.ViewModel;

namespace Finance.View;

public partial class IncomeView : ContentPage
{
	public IncomeView(IncomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}


}