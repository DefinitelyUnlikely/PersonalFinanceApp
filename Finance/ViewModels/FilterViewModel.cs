using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Models;
using Finance.Data.Interfaces;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Npgsql;

namespace Finance.ViewModels;

public partial class FilterViewModel : ObservableObject
{
    private readonly IAccountRepository accountRepo;
    private readonly TransactionViewModel transactionViewModel;
    private readonly ITransactionRepository transactionRepo;

    [ObservableProperty]
    string textSearch = string.Empty;

    [ObservableProperty]
    string dateFrom = string.Empty;

    [ObservableProperty]
    string dateTo = string.Empty;

    [ObservableProperty]
    ObservableCollection<Transaction> transactions;

    public FilterViewModel(IAccountRepository ar, ITransactionRepository tr, TransactionViewModel transactionViewModel)
    {
        accountRepo = ar;
        transactionRepo = tr;
        this.transactionViewModel = transactionViewModel;
        Transactions = new ObservableCollection<Transaction>(transactionViewModel.Transactions);
    }

    [RelayCommand]
    async Task Filter()
    {

        var transactions = await transactionRepo.ExecuteOperationAsync(async (connection) =>
        {
            List<Transaction>? returnList = [];

            var sql = @"";
            await using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);
            command.Parameters.AddWithValue(@"", "");

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                returnList.Add(
                    new(
                    reader.GetGuid(0),
                    reader.GetString(1),
                    reader.GetDouble(2),
                    reader.GetDateTime(3)
                    )
                );
            }
            return returnList;
        });
    }

}
