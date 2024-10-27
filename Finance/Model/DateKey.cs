using System.Globalization;

namespace Finance.Model;

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

    public static List<Dictionary<string, List<Model.Transaction>>> CreateTransactionDicts(List<Model.Transaction> transactions)
    {
        Dictionary<string, List<Model.Transaction>> yearDict = [];
        Dictionary<string, List<Model.Transaction>> monthDict = [];
        Dictionary<string, List<Model.Transaction>> weekDict = [];
        Dictionary<string, List<Model.Transaction>> dayDict = [];

        foreach (Model.Transaction tn in transactions)
        {
            // År
            if (!yearDict.ContainsKey(GetYearKey(tn.TransactionDate)))
            {
                yearDict[GetYearKey(tn.TransactionDate)] = [];
            }

            yearDict[GetYearKey(tn.TransactionDate)].Add(tn);

            // Månad
            if (!monthDict.ContainsKey(GetMonthKey(tn.TransactionDate)))
            {
                monthDict[GetMonthKey(tn.TransactionDate)] = [];
            }

            monthDict[GetMonthKey(tn.TransactionDate)].Add(tn);

            // Vecka
            if (!weekDict.ContainsKey(GetWeekKey(tn.TransactionDate)))
            {
                weekDict[GetWeekKey(tn.TransactionDate)] = [];
            }

            weekDict[GetWeekKey(tn.TransactionDate)].Add(tn);

            // Dag
            if (!dayDict.ContainsKey(GetDayKey(tn.TransactionDate)))
            {
                dayDict[GetDayKey(tn.TransactionDate)] = [];
            }

            dayDict[GetDayKey(tn.TransactionDate)].Add(tn);
        }

        return [yearDict, monthDict, weekDict, dayDict];

    }

}


