using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using EuriborSharp.Enums;

namespace EuriborSharp.Model
{
    [Serializable]
    public static class TheEuribors
    {
        public static List<Euribors> InterestList { get; set; }

        public static void Save()
        {
            using (var fs = new FileStream("data.xml", FileMode.Create, FileAccess.Write))
            {
                var xs = new XmlSerializer(typeof(List<Euribors>));
                xs.Serialize(fs, InterestList);
            }
        }

        public static void Load()
        {
            try
            {
                using (var fs = new FileStream("data.xml", FileMode.Open, FileAccess.Read))
                {
                    var xs = new XmlSerializer(typeof (List<Euribors>));
                    InterestList = (List<Euribors>) xs.Deserialize(fs);
                }
            }
            catch (FileNotFoundException)
            {
                // TODO: saved file not found. Ignore?
            }
        }

        public static DateTime GetOldestDate()
        {
            return InterestList.Count == 0 ? DateTime.Now : InterestList.Min(e => e.Date);
        }

        public static DateTime GetNewestDate()
        {
            return InterestList.Count == 0 ? DateTime.Now : InterestList.Max(e => e.Date);
        }

        public static decimal GetInterest(Euribors item, TimePeriods period)
        {
            switch (period)
            {
                case TimePeriods.Default:
                    return 0M;
                case TimePeriods.OneWeek:
                    return item.OneWeek;
                case TimePeriods.TwoWeeks:
                    return item.TwoWeeks;
                case TimePeriods.OneMonth:
                    return item.OneMonth;
                case TimePeriods.ThreeMonths:
                    return  item.ThreeMonths;
                case TimePeriods.SixMonths:
                    return  item.SixMonths;
                case TimePeriods.TwelveMonths:
                    return item.TwelveMonths;
                default:
                    throw new ArgumentOutOfRangeException("period");
            }
        }

        public static string GetInterestName(TimePeriods period)
        {
            switch (period)
            {
                case TimePeriods.Default:
                    return String.Empty;
                case TimePeriods.OneWeek:
                    return "1 week";
                case TimePeriods.TwoWeeks:
                    return "2 weeks";
                case TimePeriods.OneMonth:
                    return "1 month";
                case TimePeriods.ThreeMonths:
                    return  "3 months";
                case TimePeriods.SixMonths:
                    return "6 months";
                case TimePeriods.TwelveMonths:
                    return "12 months";
                default:
                    throw new ArgumentOutOfRangeException("period");
            }
        }

        public static void ParseInterestRates(string text, Euribors current)
        {
            var periodPattern = new Regex(@"(\d+)(\s\w+\s)");
            var interestPattern = new Regex(@"(\d+,\d+)");
            var datePattern = new Regex(@"(\d+[.]\d+[.]\d+)");

            var interestValue = interestPattern.Match(text).Value;
            var timePeriod = periodPattern.Match(text);
            var date = datePattern.Match(text).Value;

            var period = ParseTimePeriod(timePeriod);

            switch (period)
            {
                case TimePeriods.OneWeek:
                    current.OneWeek = Convert.ToDecimal(interestValue);
                    break;
                case TimePeriods.TwoWeeks:
                    current.TwoWeeks = Convert.ToDecimal(interestValue);
                    break;
                case TimePeriods.OneMonth:
                    current.OneMonth = Convert.ToDecimal(interestValue);
                    break;
                case TimePeriods.ThreeMonths:
                    current.ThreeMonths = Convert.ToDecimal(interestValue);
                    break;
                case TimePeriods.SixMonths:
                    current.SixMonths = Convert.ToDecimal(interestValue);
                    break;
                case TimePeriods.TwelveMonths:
                    current.TwelveMonths = Convert.ToDecimal(interestValue);
                    break;
            }

            current.Date = DateTime.Parse(date, new CultureInfo("fi-FI"), DateTimeStyles.AssumeLocal);
        }

        private static TimePeriods ParseTimePeriod(Match value)
        {
            var intMatch = Convert.ToInt32(value.Groups[1].Value);
            var stringMatch = value.Groups[2].Value.Trim();

            switch (stringMatch)
            {
                case "kk":
                    switch (intMatch)
                    {
                        case 1:
                            return TimePeriods.OneMonth;
                        case 3:
                            return TimePeriods.ThreeMonths;
                        case 6:
                            return TimePeriods.SixMonths;
                        case 12:
                            return TimePeriods.TwelveMonths;
                        default:
                            return TimePeriods.Default;
                    }
                case "vko":
                    break;
                default:
                    return TimePeriods.Default;
            }

            return TimePeriods.Default;
        }
    }

    /// <summary>
    /// Public class for storing Euribor values from single RSS read. 
    /// </summary>
    [Serializable]
    public class Euribors
    {
        public decimal OneMonth { get; set; }
        public decimal ThreeMonths { get; set; }
        public decimal SixMonths { get; set; }
        public decimal TwelveMonths { get; set; }
        public decimal OneWeek { get; set; }
        public decimal TwoWeeks { get; set; }
        public DateTime Date { get; set; }

        public Euribors()
        {}
    }
}
