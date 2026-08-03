using System;
using System.Globalization;


public static class NumberFormatter
{
    private static readonly string[] Units = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc" };

    public static string Format(double num)
    {
        double abs = Math.Abs(num);

        if (abs < 1000)
            return Math.Floor(num).ToString(CultureInfo.InvariantCulture);

        int tier = (int)Math.Floor(Math.Log10(abs) / 3);
        double scaled = num / Math.Pow(1000, tier);

        if (Math.Round(scaled, 1) >= 1000)
        {
            tier++;
            scaled = num / Math.Pow(1000, tier);
        }

        string unit = tier < Units.Length ? Units[tier] : "e" + (tier * 3);

        string formatted = (scaled % 1 == 0)
            ? scaled.ToString("F0", CultureInfo.InvariantCulture)
            : scaled.ToString("F1", CultureInfo.InvariantCulture);

        return formatted + unit;
    }

    public static string Format(long num) => Format((double)num);
    public static string Format(int num) => Format((double)num);

    public static string FormatMoney(double num, string currencySymbol = "₽")
    {
        return Format(num) + " " + currencySymbol;
    }
}