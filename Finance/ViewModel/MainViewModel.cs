using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Finance.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        ObservableCollection<Model.Transaction> transactions;

        [ObservableProperty]
        double balance;

        [ObservableProperty]
        Model.Transaction selectedTransaction;

        public MainViewModel()
        {

            Model.TransactionManager.CreateTransactionsForTesting();
            Transactions = new ObservableCollection<Model.Transaction>(Model.TransactionManager.GetTransactions());
            foreach (Model.Transaction x in Transactions)
            {
                Balance += x.TransactionAmount;
            }
        }

        public void AddTransaction(Model.Transaction transaction)
        {
            Transactions.Add(transaction);
            Balance += transaction.TransactionAmount;
        }


        [RelayCommand]
        public void Delete(Model.Transaction transaction)
        {

            if (transaction != null)
            {

                Transactions.Remove(transaction);
                Balance -= transaction.TransactionAmount;
            }
        }

    }
}
