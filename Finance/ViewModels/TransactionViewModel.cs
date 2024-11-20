using System.Collections.ObjectModel;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using Finance.Data;
using Finance.Models;
using Finance.Views;
using CommunityToolkit.Maui.Core;

namespace Finance.ViewModels
{
    public partial class TransactionViewModel : ObservableObject
    {

        private readonly IPopupService popupSerivce;

        [ObservableProperty]
        ObservableCollection<Transaction> transactions = [];

        [ObservableProperty]
        double balance;

        [ObservableProperty]
        Transaction? selectedTransaction;

        [ObservableProperty]
        string? username;

        public TransactionViewModel(IPopupService popupService)
        {
            Username = UserManager.CurrentUser!.Name;
            this.popupSerivce = popupService;
            LoadItems();
            Console.WriteLine($"{MethodBase.GetCurrentMethod()!.DeclaringType!.Name} - Username is {Username}");
        }

        private void LoadItems()
        {
            Transactions = new ObservableCollection<Transaction>(TransactionManager.GetTransactions());
        }

        private void LoadBalance()
        {
            Console.WriteLine();
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            Balance += transaction.Amount;
        }

        [RelayCommand]
        public void Delete(Transaction transaction)
        {

            if (transaction == null)
            {
                return;
            }

            Transactions.Remove(transaction);
            Balance -= transaction.Amount;
        }

        [RelayCommand]
        public void ChangePassword()
        {
            try
            {
                this.popupSerivce.ShowPopup<PasswordPopupViewModel>();
            }
            catch (Exception e)
            {
                Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }

        }

    }
}
