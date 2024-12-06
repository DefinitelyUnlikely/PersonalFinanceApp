using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        string? userName;

        [ObservableProperty]
        string? displayName;

        public TransactionViewModel(IPopupService ps, ITransactionRepository tr, IUserRepository ur)
        {
            popupSerivce = ps;
            userRepo = ur;
            transactionRepo = tr;

            DisplayName = userRepo.CurrentUser!.DisplayName;
            UserName = userRepo.CurrentUser!.UserName;

            LoadItems();
        }

        private void LoadItems()
        {
            if (userRepo.CurrentUser is null)
            {
                throw new Exception("User is null, something has gone wrong.");
            }

            var transactionsAsync = transactionRepo.GetUserTransactionsAsync(userRepo.CurrentUser.Id);
            LoadBalance(transactionsAsync.Result);
            Transactions = new ObservableCollection<Transaction>(transactionsAsync.Result);

        }

        private void LoadBalance(List<Transaction> transactions)
        {
            transactions.ForEach(x => Balance += x.Amount);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await transactionRepo.AddTransactionAsync(transaction);
            Transactions.Add(transaction);
            Balance += transaction.Amount;
        }

        [RelayCommand]
        public async Task Delete(Transaction transaction)
        {

            if (transaction == null)
            {
                return;
            }

            await transactionRepo.RemoveTransactionAsync(transaction.Id);
            Transactions.Remove(transaction);
            Balance -= transaction.Amount;
        }

        [RelayCommand]
        public async Task ChangePassword()
        {
            try
            {
                await popupSerivce.ShowPopupAsync<PasswordPopupViewModel>();
                await Shell.Current.GoToAsync($"///{nameof(MainView)}");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }

        }

        [RelayCommand]
        public async Task ChangeUsername()
        {
            try
            {
                await popupSerivce.ShowPopupAsync<UsernamePopupViewModel>();
                await Shell.Current.GoToAsync($"///{nameof(MainView)}");

            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }

        }

    }
}
