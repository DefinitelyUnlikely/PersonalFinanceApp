using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data;
using Finance.Models;
using Finance.Managers;

namespace Finance.ViewModels
{
    public partial class TransactionViewModel : ObservableObject
    {

        [ObservableProperty]
        ObservableCollection<Transaction> transactions;

        [ObservableProperty]
        double balance;

        [ObservableProperty]
        Transaction? selectedTransaction;

        public TransactionViewModel(FinanceDatabase financeDatabase)
        {

            LoadItems();
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

    }
}
