using CommunityToolkit.Maui.Views;
using Finance.ViewModels;

namespace Finance.Views;

public partial class AccountPopup : Popup
{
	public AccountPopup(AccountPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}