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

            CreateTransactionsForTesting();
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

        public static void CreateTransactionsForTesting()
        {
            Model.Transaction trans1 = new("Selling Books", 52.5, new DateTime(2020, 09, 04));
            Model.Transaction trans2 = new("Selling More Books", 52.5, new DateTime(2021, 10, 05));
            Model.Transaction trans3 = new("Buying Books", -75, new DateTime(2023, 11, 06));
            Model.Transaction trans4 = new("Food", -10, new DateTime(2024, 12, 07));
            Model.Transaction trans5 = new("Part time", 530, new DateTime(2024, 01, 08));

            Model.TransactionManager.AddTransaction(trans1);
            Model.TransactionManager.AddTransaction(trans2);
            Model.TransactionManager.AddTransaction(trans3);
            Model.TransactionManager.AddTransaction(trans4);
            Model.TransactionManager.AddTransaction(trans5);

        }

    }
}
