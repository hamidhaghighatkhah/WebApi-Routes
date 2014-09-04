using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Helper
{
    public class HijriDateTime
    {
        private readonly HijriCalendar _cal = new HijriCalendar();

        public HijriDateTime()
        {
            var dateTime = DateTime.Now;
            var calendar = new HijriCalendar();
            Year = calendar.GetYear(dateTime);
            Month = calendar.GetMonth(dateTime);
            Day = calendar.GetDayOfMonth(dateTime);
            Hour = calendar.GetHour(dateTime);
            Minute = calendar.GetMinute(dateTime);
            Second = calendar.GetSecond(dateTime);
        }

        public HijriDateTime(PersianDateTime dateTimeTime)
        {
            Set(dateTimeTime.ToHijriDate());
        }

        public HijriDateTime(DateTime dateTime)
        {
            Set(Parse(dateTime));
        }

        public HijriDateTime(string dateTime)
        {
            Set(dateTime);
        }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Month { get; set; }

        public int Second { get; set; }

        public int Year { get; set; }

        public static HijriDateTime Parse(string dateTime)
        {
            return new HijriDateTime(dateTime);
        }

        public static HijriDateTime Parse(DateTime dateTime)
        {
            var cal = new HijriCalendar();
            var result = new HijriDateTime
            {
                Year = cal.GetYear(dateTime),
                Month = cal.GetMonth(dateTime),
                Day = cal.GetDayOfMonth(dateTime),
                Hour = cal.GetHour(dateTime),
                Minute = cal.GetMinute(dateTime),
                Second = cal.GetSecond(dateTime)
            };

            return result;
        }

        public static HijriDateTime Parse(PersianDateTime dateTimeTime)
        {
            return dateTimeTime.ToHijriDate();
        }

        public string ToDateString()
        {
            return string.Format("{0}/{1}/{2}",
                Year.ToString("0000"),
                Month.ToString("00"),
                Day.ToString("00"));
        }

        public string ToDateTimeString()
        {
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                Year.ToString("0000"),
                Month.ToString("00"),
                Day.ToString("00"),
                Hour.ToString("00"),
                Minute.ToString("00"),
                Second.ToString("00"));
        }

        public DateTime ToDateTime()
        {
            return _cal.ToDateTime(Year, Month, Day, Hour, Minute, Second, 0);
        }

        public PersianDateTime ToPersianDate()
        {
            return PersianDateTime.Parse(ToDateTime());
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                Year.ToString("0000"),
                Month.ToString("00"),
                Day.ToString("00"),
                Hour.ToString("00"),
                Minute.ToString("00"),
                Second.ToString("00"));
        }

        private void Set(HijriDateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
            Hour = dateTime.Hour;
            Minute = dateTime.Minute;
            Second = dateTime.Second;
        }

        private void Set(string dateStr)
        {
            if (dateStr.IndexOf(":", StringComparison.Ordinal) < 0 || dateStr.IndexOf("/", StringComparison.Ordinal) < 0)
                return;

            //date format => yyyy/mm/dd hh:MM:ss
            string[] obj = dateStr.Split(' ');
            string[] date = obj[0].Split('/');
            string[] time = obj[1].Split(':');

            Year = int.Parse(date[0]);
            Month = int.Parse(date[1]);
            Day = int.Parse(date[2]);
            Hour = int.Parse(time[0]);
            Minute = int.Parse(time[1]);
            Second = int.Parse(time[2]);
        }
    }

    public class PersianDateTime
    {
        private readonly PersianCalendar _cal = new PersianCalendar();

        public PersianDateTime()
        {
            var dateTime = DateTime.Now;
            var calendar = new PersianCalendar();
            Year = calendar.GetYear(dateTime);
            Month = calendar.GetMonth(dateTime);
            Day = calendar.GetDayOfMonth(dateTime);
            Hour = calendar.GetHour(dateTime);
            Minute = calendar.GetMinute(dateTime);
            Second = calendar.GetSecond(dateTime);
        }

        public PersianDateTime(HijriDateTime dateTimeTime)
        {
            Set(dateTimeTime.ToPersianDate());
        }

        public PersianDateTime(DateTime dateTime)
        {
            Set(Parse(dateTime));
        }

        public PersianDateTime(string dateTime)
        {
            Set(dateTime);
        }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Month { get; set; }

        public int Second { get; set; }

        public int Year { get; set; }

        public static PersianDateTime Parse(string dateTime)
        {
            return new PersianDateTime(dateTime);
        }

        public static PersianDateTime Parse(DateTime dateTime)
        {
            var cal = new PersianCalendar();
            var result = new PersianDateTime
            {
                Year = cal.GetYear(dateTime),
                Month = cal.GetMonth(dateTime),
                Day = cal.GetDayOfMonth(dateTime),
                Hour = cal.GetHour(dateTime),
                Minute = cal.GetMinute(dateTime),
                Second = cal.GetSecond(dateTime)
            };

            return result;
        }

        public static PersianDateTime Parse(HijriDateTime dateTimeTime)
        {
            return dateTimeTime.ToPersianDate();
        }

        public string ToDateString()
        {
            return string.Format("{0}/{1}/{2}",
                Year.ToString("0000"),
                Month.ToString("00"),
                Day.ToString("00"));
        }

        public string ToDateTimeString()
        {
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                Year.ToString("0000"),
                Month.ToString("00"),
                Day.ToString("00"),
                Hour.ToString("00"),
                Minute.ToString("00"),
                Second.ToString("00"));
        }

        public DateTime ToDateTime()
        {
            return _cal.ToDateTime(Year, Month, Day, Hour, Minute, Second, 0);
        }

        public HijriDateTime ToHijriDate()
        {
            return HijriDateTime.Parse(ToDateTime());
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                Year.ToString("0000"),
                Month.ToString("00"),
                Day.ToString("00"),
                Hour.ToString("00"),
                Minute.ToString("00"),
                Second.ToString("00"));
        }

        private void Set(PersianDateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
            Hour = dateTime.Hour;
            Minute = dateTime.Minute;
            Second = dateTime.Second;
        }

        private void Set(string dateStr)
        {
            if (dateStr.IndexOf("/", StringComparison.Ordinal) < 0)
                return;
            if (dateStr.IndexOf(":", StringComparison.Ordinal) < 0)
            {
                dateStr += " 00:00:00";
            }
            //date format => yyyy/mm/dd hh:MM:ss
            string[] obj = dateStr.Split(' ');
            string[] date = obj[0].Split('/');
            string[] time = obj[1].Split(':');

            Year = int.Parse(date[0]);
            Month = int.Parse(date[1]);
            Day = int.Parse(date[2]);
            Hour = int.Parse(time[0]);
            Minute = int.Parse(time[1]);
            Second = int.Parse(time[2]);
        }
    }

    public class DateHelper
    {
        public static string CulturedDate(DateTime date)
        {
            switch (CultureHelper.CurrentCultureIsoName)
            {
                case "fa":
                    return PersianDateTime.Parse(date).ToDateString();

                case "ar":
                    return HijriDateTime.Parse(date).ToDateString();

                default:
                    return date.ToString();
            }
        }

        public static string CulturedDateTime(DateTime date)
        {
            switch (CultureHelper.CurrentCultureIsoName)
            {
                case "fa":
                    return PersianDateTime.Parse(date).ToDateTimeString();

                case "ar":
                    return HijriDateTime.Parse(date).ToDateTimeString();

                default:
                    return date.ToShortDateString();
            }
        }

        public static DateTime ToDateTime(string culturedDate)
        {
            switch (CultureHelper.CurrentCultureIsoName)
            {
                case "fa":
                    return PersianDateTime.Parse(culturedDate).ToDateTime();

                case "ar":
                    return HijriDateTime.Parse(culturedDate).ToDateTime();

                default:
                    return DateTime.Parse(culturedDate);
            }
        }

        public static string ToPersianDate(DateTime date)
        {
            return PersianDateTime.Parse(date).ToDateString();
        }

        public static string NormalizeDate(string str)
        {
            try
            {
                Regex onlyDateReg = new Regex(@"\d{4}\/\d{1,2}\/\d{1,2}");
                Regex onlyDateRevrseReg = new Regex(@"\d{1,2}\/\d{1,2}\/\d{4}");
                Regex dateReg = new Regex(@"\d{4}\/\d{1,2}\/\d{1,2}\s\d{1,2}\:\d{1,2}");
                Regex dateRevrseReg = new Regex(@"\d{1,2}\/\d{1,2}\/\d{4}\s\d{1,2}\:\d{1,2}");
                //جدا سازی تاریخ و زمان
                var date_time = str.Split(' ');
                var date = date_time[0].Split('/');
                var time = new string[1];
                var year = "1300";
                var month = "01";
                var day = "01";
                var hour = "00";
                var minute = "00";
                var second = "00";
                if (dateReg.IsMatch(str))
                {
                    time = date_time[1].Split(':');
                    year = int.Parse(date[0]).ToString("00");
                    month = int.Parse(date[1]).ToString("00");
                    day = int.Parse(date[2]).ToString("00");
                    hour = int.Parse(time[0]).ToString("00");
                    minute = int.Parse(time[1]).ToString("00");
                    if (time.Length == 3)
                    {
                        second = int.Parse(time[2]).ToString("00");
                    }
                }
                else if (dateRevrseReg.IsMatch(str))
                {
                    time = date_time[1].Split(':');
                    year = int.Parse(date[2]).ToString("00");
                    month = int.Parse(date[1]).ToString("00");
                    day = int.Parse(date[0]).ToString("00");
                    hour = int.Parse(time[0]).ToString("00");
                    minute = int.Parse(time[1]).ToString("00");
                    if (time.Length == 3)
                    {
                        second = int.Parse(time[2]).ToString("00");
                    }
                }
                else if (onlyDateReg.IsMatch(str))
                {
                    year = int.Parse(date[0]).ToString("00");
                    month = int.Parse(date[1]).ToString("00");
                    day = int.Parse(date[2]).ToString("00");
                    hour = "00";
                    minute = "00";
                    if (time.Length == 3)
                    {
                        second = "00";
                    }
                }
                else if (onlyDateRevrseReg.IsMatch(str))
                {
                    year = int.Parse(date[2]).ToString("00");
                    month = int.Parse(date[1]).ToString("00");
                    day = int.Parse(date[0]).ToString("00");
                    hour = "00";
                    minute = "00";
                    if (time.Length == 3)
                    {
                        second = "00";
                    }
                }

                return year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
            }
            catch (Exception)
            {
                throw;
            }
            return str;
        }
    }

    public class CultureHelper
    {
        public static List<string> AvailableCultures
        {
            get { return new List<string> { "fa", "en", "ar" }; }
        }

        public static CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
            set
            {
                Thread.CurrentThread.CurrentCulture = value;
                Thread.CurrentThread.CurrentUICulture = value;
            }
        }

        public static string CurrentCultureIsoName
        {
            get { return CurrentCulture.TwoLetterISOLanguageName; }
        }

        public static string CurrentCultureName
        {
            get { return CurrentCulture.Name; }
        }

        public static CultureInfo DefaultCulture
        {
            get { return CultureInfo.GetCultureInfo("fa-IR"); }
        }

        public static void SetCurrentCulture(string info)
        {
            if (info.Contains("-"))
                info = info.Substring(0, info.IndexOf("-", StringComparison.Ordinal));

            if (!AvailableCultures.Contains(info.ToLower()))
                return;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(info);
            SetCurrentCulture(ci);
        }

        public static void SetCurrentCulture(CultureInfo info)
        {
            if (!AvailableCultures.Contains(info.TwoLetterISOLanguageName.ToLower()))
                return;
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
        }

        public static void SetDefaultCulture()
        {
            SetCurrentCulture(DefaultCulture);
        }

        public static bool IsRightToLeft()
        {
            return CurrentCulture.TextInfo.IsRightToLeft;
        }
    }
}
