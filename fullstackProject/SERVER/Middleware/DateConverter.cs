using System.Globalization;


public static class DateConverter
{
    public static bool TryConvertToDateOnly(string dateString, out DateOnly date)
    {
        date = default;
        bool success = DateOnly.TryParseExact(dateString, "dd.MM.yyyy",
                                             CultureInfo.InvariantCulture,
                                             DateTimeStyles.None, out var parsedDate);
        if (!success) return false;
        date = parsedDate;
        return true;
    }
}
