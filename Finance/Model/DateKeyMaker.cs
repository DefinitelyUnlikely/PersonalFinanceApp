using System.Globalization;

namespace Finance.Model;

public class DateKeyMaker
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

}
