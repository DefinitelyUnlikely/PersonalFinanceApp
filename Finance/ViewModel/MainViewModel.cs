using System.Collections.ObjectModel;
using SQLite;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data;

namespace Finance.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        TransactionDatabase transactionDatabase;

        [ObservableProperty]
        ObservableCollection<Model.Transaction> transactions;

        [ObservableProperty]
        double balance;

        [ObservableProperty]
        Model.Transaction? selectedTransaction;

        public MainViewModel(TransactionDatabase transactionDatabase)
        {

            this.transactionDatabase = transactionDatabase;
            Transactions = [];
            LoadItems().ConfigureAwait(false);
            LoadBalance().ConfigureAwait(false);

        }

        private async Task LoadItems()
        {
            List<Model.Transaction> items = await transactionDatabase.GetItemsAsync();
            Transactions = new ObservableCollection<Model.Transaction>(items);
        }

        private async Task LoadBalance()
        {
            Balance = await transactionDatabase.GetBalanceAsync();
            Console.WriteLine();
        }

        public async Task AddTransaction(Model.Transaction transaction)
        {

            await transactionDatabase.SaveItemAsync(transaction);
            Transactions.Add(transaction);
            Balance += transaction.TransactionAmount;
        }

        [RelayCommand]
        public async Task Delete(Model.Transaction transaction)
        {

            if (transaction == null)
            {
                return;
            }

            await transactionDatabase.DeleteItemAsync(transaction);
            Transactions.Remove(transaction);
            Balance -= transaction.TransactionAmount;
        }

    }
}
