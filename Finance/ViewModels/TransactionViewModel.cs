using System.Collections.ObjectModel;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using Finance.Data;
using Finance.Models;
using Finance.Views;
using CommunityToolkit.Maui.Core;
using Finance.Data.Interfaces;

namespace Finance.ViewModels
{
    public partial class TransactionViewModel : ObservableObject
    {

        private readonly IPopupService popupSerivce;
        private readonly IUserRepository userRepo;
        private readonly ITransactionRepository transactionRepo;

        [ObservableProperty]
        ObservableCollection<Transaction> transactions = [];

        [ObservableProperty]
        double balance;

        [ObservableProperty]
        Transaction? selectedTransaction;

        [ObservableProperty]
        string? username;

        public TransactionViewModel(IPopupService ps, ITransactionRepository tr, IUserRepository ur)
        {
            popupSerivce = ps;
            userRepo = ur;
            transactionRepo = tr;

            Username = userRepo.CurrentUser!.Name;

            LoadItems();
            Console.WriteLine($"{MethodBase.GetCurrentMethod()!.DeclaringType!.Name} - Username is {Username}");
        }

        private async void LoadItems()
        {
            if (userRepo.CurrentUser is null)
            {
                return;
            }

            var transactions = await transactionRepo.GetUserTransactionsAsync(userRepo.CurrentUser.Id);
            Transactions = new ObservableCollection<Transaction>(transactions);

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
                popupSerivce.ShowPopup<PasswordPopupViewModel>();
            }
            catch (Exception e)
            {
                Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }

        }

    }
}
