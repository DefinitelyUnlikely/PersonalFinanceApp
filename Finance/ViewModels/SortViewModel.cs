using System.Collections.ObjectModel;
using System.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data.Interfaces;
using Finance.Models;
using Npgsql;

namespace Finance.ViewModels;


// TODO: Make this use SQL queryes rather than my current solution.
// It is a bit counterintuitive to do a database query when I already got all 
// the transactions I need in-memory. But I'm doing it to get better at SQL.
public partial class SortViewModel : ObservableObject
{
    private readonly TransactionViewModel transactionViewModel;
    private readonly ITransactionRepository transactionRepo;

    private List<Transaction> Transactions;

    private List<Dictionary<string, List<Transaction>>> dictionaries;

    [ObservableProperty]
    ObservableCollection<DisplayItem> displayList = [];


    public SortViewModel(ITransactionRepository tr)
    {
        transactionRepo = tr;
    }


    [RelayCommand]
    void Year()
    {

        var transactions = transactionRepo.ExecuteOperationAsync(async connection =>
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
        }
        );
    }

    [RelayCommand]
    void Month()
    {
        var transactions = transactionRepo.ExecuteOperationAsync(async connection =>
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
}
);
    }


    [RelayCommand]
    void Week()
    {
        var transactions = transactionRepo.ExecuteOperationAsync(async connection =>
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
        }
        );
    }


    [RelayCommand]
    void Day()
    {
        var transactions = transactionRepo.ExecuteOperationAsync(async connection =>
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
        }
        );
    }
}
