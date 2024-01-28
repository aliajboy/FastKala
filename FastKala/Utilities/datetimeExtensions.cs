using System.Globalization;

namespace FastKala.Utilities;

public static class datetimeExtensions
{
    public static string MiladiToPersian(this DateTime MDate)
    {
        try
        {
            int y = Convert.ToInt32(MDate.Year);
            int m = Convert.ToInt32(MDate.Month);
            int d = Convert.ToInt32(MDate.Day);
            DateTime dt = new DateTime(y, m, d);
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(dt).ToString() + "/" + pc.GetMonth(dt).ToString("00") + "/" + pc.GetDayOfMonth(dt).ToString("00");
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string MiladiToPersianWithTime(this DateTime MDate)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetHour(MDate).ToString("00") + ":" +
                pc.GetMinute(MDate).ToString("00") + " " +
            pc.GetYear(MDate).ToString("0000") + "/" +
                pc.GetMonth(MDate).ToString("00") + "/" +
                pc.GetDayOfMonth(MDate).ToString("00");
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string MiladiToPersianWithTimeSecond(this DateTime MDate)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(MDate).ToString() +
                pc.GetMonth(MDate).ToString("00") +
                pc.GetDayOfMonth(MDate).ToString("00") +
                pc.GetHour(MDate).ToString("00") +
                pc.GetMinute(MDate).ToString("00") +
                pc.GetSecond(MDate).ToString("00");
        }
        catch
        {
            return "";
        }
    }
}