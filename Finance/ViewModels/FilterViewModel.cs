using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Models;
using Finance.Data.Interfaces;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Npgsql;

namespace Finance.ViewModels;

public partial class FilterViewModel : ObservableObject
{
    private readonly TransactionViewModel transactionViewModel;
    private readonly ITransactionRepository transactionRepo;

    [ObservableProperty]
    ObservableCollection<Transaction> transactions;

    public FilterViewModel(ITransactionRepository tr, TransactionViewModel transactionViewModel)
    {
        transactionRepo = tr;
        this.transactionViewModel = transactionViewModel;
        Transactions = new ObservableCollection<Transaction>(transactionViewModel.Transactions);
    }

    [RelayCommand]
    async Task Year()
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
                    reader.GetInt32(0),
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
