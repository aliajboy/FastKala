using System.Globalization;

namespace FastKala.Utilities;

public static class datetimeExtensions
{
    /// <summary>
    /// Converts .Net DateTime to Persian Calendar String
    /// </summary>
    /// <param name="MiladiDate"></param>
    /// <returns>Persian Date String With "14021013" Format</returns>
    public static string MiladiToPersian(this DateTime MiladiDate)
    {
        try
        {
            DateTime dt = new(MiladiDate.Year, MiladiDate.Month, MiladiDate.Day);
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(dt).ToString("0000") + pc.GetMonth(dt).ToString("00") + pc.GetDayOfMonth(dt).ToString("00");
        }
        catch (Exception)
        {
            return "";
        }
    }

    /// <summary>
    /// Converts .Net DateTime to Persian Calendar and Time String
    /// </summary>
    /// <param name="MiladiDate"></param>
    /// <returns>Persian Datetime String with this Format : "140210132459" Without Seconds</returns>
    public static string MiladiToPersianWithTime(this DateTime MiladiDate)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(MiladiDate).ToString() + pc.GetMonth(MiladiDate).ToString("00") + pc.GetDayOfMonth(MiladiDate).ToString("00") + pc.GetHour(MiladiDate).ToString("00") + pc.GetMinute(MiladiDate).ToString("00");
        }
        catch (Exception)
        {
            return "";
        }
    }

    /// <summary>
    /// Converts Persian Date and Time String to Miladi DateTime.
    /// Input String Must be 12 Digits. For Example: 140209132459
    /// </summary>
    /// <param name="persianDateTime"></param>
    /// <returns>Miladi DateTime With Hour and Minutes (Doesn't Contain Seconds)</returns>
    public static DateTime PersianToMiladiDateTime(this string persianDateTime)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(Convert.ToInt32(persianDateTime.Substring(0, 4))
                                      , Convert.ToInt32(persianDateTime.Substring(4, 2))
                                      , Convert.ToInt32(persianDateTime.Substring(6, 2)), pc);
            TimeSpan ts = new TimeSpan(Convert.ToInt32(persianDateTime.Substring(8, 2)), Convert.ToInt32(persianDateTime.Substring(10, 2)), 0);
            dt = dt.Add(ts);
            return dt;
        }
        catch (Exception)
        {
            return DateTime.Now;
        }

    }

    /// <summary>
    /// Converts .Net DateTime to Persian Calendar and Time String
    /// </summary>
    /// <param name="MDate"></param>
    /// <returns>Persian Datetime String with this Format : "14021013245900" With Seconds</returns>
    public static string MiladiToPersianWithTimeSecond(this DateTime MDate)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.GetYear(MDate).ToString() + pc.GetMonth(MDate).ToString("00") + pc.GetDayOfMonth(MDate).ToString("00") +
            pc.GetHour(MDate).ToString("00") + pc.GetMinute(MDate).ToString("00") + pc.GetSecond(MDate).ToString("00");
    }

    /// <summary>
    /// Converts Persian Date String to Miladi DateTime.
    /// Input String Must be 8 Digits. For Example: 14020913
    /// </summary>
    /// <param name="persianDate"></param>
    /// <returns>Miladi DateTime (Just Date)</returns>
    public static DateTime PersianToMiladi(this string persianDate)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(Convert.ToInt32(persianDate.Substring(0, 4))
                                      , Convert.ToInt32(persianDate.Substring(4, 2))
                                      , Convert.ToInt32(persianDate.Substring(6, 2)), pc);
            return dt;
        }
        catch (Exception)
        {

            return DateTime.Now;
        }

    }

    /// <summary>
    /// Set Slash for date and seperate Date and time with a Dash (-)
    /// </summary>
    /// <param name="DateTime_"></param>
    /// <returns>Format: 1402/10/12 - 11:55</returns>
    public static string SetDateTimeSeprator(this string DateTime_)
    {
        if (DateTime_.Length < 12)
        {
            return DateTime_;
        }
        else
        {
            return DateTime_.Substring(8, 2) + ":" + DateTime_.Substring(10, 2) + " - " + DateTime_.Substring(0, 4) + "/" + DateTime_.Substring(4, 2) + "/" +
                DateTime_.Substring(6, 2);
        }
    }

    /// <summary>
    /// Returns Date with Slashes
    /// </summary>
    /// <param name="date"></param>
    /// <returns>Format : 1402/10/12</returns>
    public static string SetDateSlash(this string date)
    {
        string d = date;
        try
        {
            if (String.IsNullOrEmpty(date)) return "";

            if (date.Length == 6)
            {
                d = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 2);
            }
            if (date.Length == 8)
            {
                d = date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
            }
            if (date.Length != 8 && date.Length != 6)
            {
                if (date != null)
                    d = date;
                else
                    d = "";

            }
        }
        catch (Exception)
        {
            d = date;
        }
        return d;
    }

    /// <summary>
    /// Seperates Date with Dashes
    /// </summary>
    /// <param name="date"></param>
    /// <returns>Format : 1402-10-12</returns>
    public static string SetDateDash(this string date)
    {
        string d = date;
        try
        {
            if (string.IsNullOrEmpty(date)) return "";

            if (date.Length == 6)
            {
                d = date.Substring(0, 2) + "-" + date.Substring(2, 2) + "-" + date.Substring(4, 2);
            }
            if (date.Length == 8)
            {
                d = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
            }
            if (date.Length != 8 && date.Length != 6)
            {
                if (date != null)
                    d = date;
                else
                    d = "";

            }
        }
        catch (Exception)
        {
            d = date;
        }
        return d;
    }

    /// <summary>
    /// Get Persian Month Name based on Month Id
    /// </summary>
    /// <param name="MounthId"></param>
    /// <returns>For example monthId of 1 is فروردین</returns>
    public static string GetPersianMounth(this int MounthId)
    {
        switch (MounthId)
        {
            case 1: return "فروردین";
            case 2: return "اردیبهشت";
            case 3: return "خرداد";
            case 4: return "تیر";
            case 5: return "مرداد";
            case 6: return "شهریور";
            case 7: return "مهر";
            case 8: return "آبان";
            case 9: return "آذر";
            case 10: return "دی";
            case 11: return "بهمن";
            case 12: return "اسفند";
            default: return "";
        }
    }

    public static string MiladiToPersianWithMonthName(this DateTime date)
    {
        var pDate = MiladiToPersian(date);
        string day = pDate.Substring(6, 2);
        string month = GetPersianMounth(Convert.ToInt32(pDate.Substring(4, 2)));
        string year = pDate.Substring(0, 4);
        return day + " " + month + " " + year;
    }
}