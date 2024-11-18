using CommunityToolkit.Maui.Views;
using Finance.ViewModels;

namespace Finance.Views;

public partial class PasswordPopup : Popup
{
	public PasswordPopup(PasswordPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}