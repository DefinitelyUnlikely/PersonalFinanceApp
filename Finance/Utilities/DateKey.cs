using System.Globalization;
using Finance.Models;

namespace Finance.Utilities;

public class DateKey
{

    public static string GetYearKey(DateTime date)
    {
        return $"Year: {date.Year.ToString()}";
    }

    public static string GetMonthKey(DateTime date)
    {
        string month = date.Month < 10 ? $"0{date.Month}" : $"{date.Month}";
        return $"Year: {date.Year} Month: {month}";
    }

    public static string GetWeekKey(DateTime date)
    {
        string month = date.Month < 10 ? $"0{date.Month}" : $"{date.Month}";
        string week = ISOWeek.GetWeekOfYear(date) < 10 ? $"0{ISOWeek.GetWeekOfYear(date)}" : $"{ISOWeek.GetWeekOfYear(date)}";
        return $"Year: {date.Year} Month: {month} Week: {week}";
    }

    public static string GetDayKey(DateTime date)
    {
        string month = date.Month < 10 ? $"0{date.Month}" : $"{date.Month}";
        string week = ISOWeek.GetWeekOfYear(date) < 10 ? $"0{ISOWeek.GetWeekOfYear(date)}" : $"{ISOWeek.GetWeekOfYear(date)}";
        return $"Year: {date.Year} Month: {month} Week: {week} Day of Month: {date.Day}";
    }

    public static List<Dictionary<string, List<Transaction>>> CreateTransactionDicts(List<Transaction> transactions)
    {
        Dictionary<string, List<Transaction>> yearDict = [];
        Dictionary<string, List<Transaction>> monthDict = [];
        Dictionary<string, List<Transaction>> weekDict = [];
        Dictionary<string, List<Transaction>> dayDict = [];

        foreach (Transaction transaction in transactions)
        {

            if (!yearDict.ContainsKey(GetYearKey(transaction.Date)))
            {
                yearDict[GetYearKey(transaction.Date)] = [];
            }

            yearDict[GetYearKey(transaction.Date)].Add(transaction);

            if (!monthDict.ContainsKey(GetMonthKey(transaction.Date)))
            {
                monthDict[GetMonthKey(transaction.Date)] = [];
            }

            monthDict[GetMonthKey(transaction.Date)].Add(transaction);

            if (!weekDict.ContainsKey(GetWeekKey(transaction.Date)))
            {
                weekDict[GetWeekKey(transaction.Date)] = [];
            }

            weekDict[GetWeekKey(transaction.Date)].Add(transaction);

            if (!dayDict.ContainsKey(GetDayKey(transaction.Date)))
            {
                dayDict[GetDayKey(transaction.Date)] = [];
            }

            dayDict[GetDayKey(transaction.Date)].Add(transaction);
        }

        return [yearDict, monthDict, weekDict, dayDict];

    }

}


