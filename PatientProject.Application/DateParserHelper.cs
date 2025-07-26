using System.Globalization;

namespace PatientProject.Application;

public static class DateParserHelper
{
    private static readonly string[] KnownPrefixes = { "eq", "ne", "lt", "le", "gt", "ge" };

    public static (string prefix, DateTime date) ParseDateParam(string input)
    {
        foreach (var prefix in KnownPrefixes)
        {
            if (input.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                var datePart = input.Substring(prefix.Length);
                return (prefix.ToLower(), DateTime.Parse(datePart, null, DateTimeStyles.AdjustToUniversal));
            }
        }

        return ("eq", DateTime.Parse(input, null, DateTimeStyles.AdjustToUniversal));
    }
}
