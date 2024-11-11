using System.Collections.ObjectModel;
using SQLite;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data;
using Finance.Models;

namespace Finance.ViewModels
{
    public partial class TransactionViewModel : ObservableObject
    {
        TransactionDatabase transactionDatabase;

        [ObservableProperty]
        ObservableCollection<Transaction> transactions;

        [ObservableProperty]
        double balance;

        [ObservableProperty]
        Transaction? selectedTransaction;

        public TransactionViewModel(TransactionDatabase transactionDatabase)
        {

            this.transactionDatabase = transactionDatabase;
            Transactions = [];
            LoadItems().ConfigureAwait(false);
            LoadBalance().ConfigureAwait(false);

        }

        private async Task LoadItems()
        {
            List<Transaction> items = await transactionDatabase.GetItemsAsync();
            Transactions = new ObservableCollection<Transaction>(items);
        }

        private async Task LoadBalance()
        {
            Balance = await transactionDatabase.GetBalanceAsync();
            Console.WriteLine();
        }

        public async Task AddTransaction(Transaction transaction)
        {

            await transactionDatabase.SaveItemAsync(transaction);
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

            await transactionDatabase.DeleteItemAsync(transaction);
            Transactions.Remove(transaction);
            Balance -= transaction.Amount;
        }

    }
}
