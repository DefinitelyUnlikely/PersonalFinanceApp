using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Finance.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        public static ObservableCollection<Model.Transaction> transactions;

        [ObservableProperty]
        Model.Transaction selectedTransaction;

        public MainViewModel()
        {

            CreateTransactionsForTesting();
            Transactions = new ObservableCollection<Model.Transaction>(Model.TransactionManager.GetTransactions());
        }

        public void AddTransaction(Model.Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        [RelayCommand]
        public void Delete(Model.Transaction transaction)
        {

            if (transaction != null)
            {

                Transactions.Remove(transaction);
            }
        }

        public static void CreateTransactionsForTesting()
        {
            Model.Transaction trans1 = new("Selling Books", 52.5, new DateTime(2024, 10, 04));
            Model.Transaction trans2 = new("Selling More Books", 52.5, new DateTime(2024, 10, 05));
            Model.Transaction trans3 = new("Buying Books", -75, new DateTime(2024, 10, 06));
            Model.Transaction trans4 = new("Food", -10, new DateTime(2024, 10, 07));
            Model.Transaction trans5 = new("Part time", 530, new DateTime(2024, 10, 08));

            Model.TransactionManager.AddTransaction(trans1);
            Model.TransactionManager.AddTransaction(trans2);
            Model.TransactionManager.AddTransaction(trans3);
            Model.TransactionManager.AddTransaction(trans4);
            Model.TransactionManager.AddTransaction(trans5);

        }

    }
}
