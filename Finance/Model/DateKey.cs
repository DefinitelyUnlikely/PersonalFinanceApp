using System.Globalization;
using System.Collections.ObjectModel;

namespace Finance.Model;

public class DateKey
{

    public static string GetYearKey(DateTime date)
    {
        return $"Year: {date.Year.ToString()}";
    }

    public static string GetMonthKey(DateTime date)
    {
        return $"Year: {date.Year} Month: {date.Month}";
    }

    public static string GetWeekKey(DateTime date)
    {
        return $"Year: {date.Year} Month: {date.Month} Week: {ISOWeek.GetWeekOfYear(date)}";
    }

    public static string GetDayKey(DateTime date)
    {
        return $"Year: {date.Year} Month: {date.Month} Week: {ISOWeek.GetWeekOfYear(date)} Day of Month: {date.Day}";
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
            if (!yearDict.ContainsKey(DateKeyMaker.GetYearKey(tn.TransactionDate)))
            {
                yearDict[DateKeyMaker.GetYearKey(tn.TransactionDate)] = [];
            }

            yearDict[DateKeyMaker.GetYearKey(tn.TransactionDate)].Add(tn);

            // Månad
            if (!monthDict.ContainsKey(DateKeyMaker.GetMonthKey(tn.TransactionDate)))
            {
                monthDict[DateKeyMaker.GetMonthKey(tn.TransactionDate)] = [];
            }

            monthDict[DateKeyMaker.GetMonthKey(tn.TransactionDate)].Add(tn);

            // Vecka
            if (!weekDict.ContainsKey(DateKeyMaker.GetWeekKey(tn.TransactionDate)))
            {
                weekDict[DateKeyMaker.GetWeekKey(tn.TransactionDate)] = [];
            }

            weekDict[DateKeyMaker.GetWeekKey(tn.TransactionDate)].Add(tn);

            // Dag
            if (!dayDict.ContainsKey(DateKeyMaker.GetDayKey(tn.TransactionDate)))
            {
                dayDict[DateKeyMaker.GetDayKey(tn.TransactionDate)] = [];
            }

            dayDict[DateKeyMaker.GetDayKey(tn.TransactionDate)].Add(tn);
        }

        return [yearDict, monthDict, weekDict, dayDict];

    }

}


