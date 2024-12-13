using Finance.Data.Interfaces;
using Finance.ViewModels;

namespace Finance.Views;

public partial class SortView : ContentPage
{
	public readonly IUserRepository userRepo;
	public readonly IAccountRepository accountRepo;
	public readonly SortViewModel sortViewModel;
	private readonly TransactionViewModel transactionViewModel;

	public SortView(IUserRepository ur, IAccountRepository ar, SortViewModel vm, TransactionViewModel tvm)
	{
		InitializeComponent();
		userRepo = ur;
		accountRepo = ar;
		sortViewModel = vm;
		transactionViewModel = tvm;
		BindingContext = sortViewModel;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		if (userRepo.CurrentUser is null)
		{
			throw new InvalidNavigationException("SortView.xaml.cs: User should not be null.\n");
		}

		transactionViewModel.DisplayName = userRepo.CurrentUser.DisplayName;
		transactionViewModel.UserName = userRepo.CurrentUser.UserName;

		if (accountRepo.CurrentAccount is null)
		{
			await sortViewModel.LoadItems("user");
			return;
		}

		await sortViewModel.LoadItems("account");

	}

	protected override bool OnBackButtonPressed()
	{
		Shell.Current.GoToAsync($"/{nameof(TransactionView)}");
		return true;
	}

}