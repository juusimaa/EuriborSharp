using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;
using EuriborSharp.Enums;
using EuriborSharp.Properties;

namespace EuriborSharp.Model
{
    [Serializable]
    public static class TheEuribors
    {
        public static Dictionary<string, string> UrlList = new Dictionary<string, string>
        {
            { "hist_EURIBOR_2014.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2014.csv" },
            { "hist_EURIBOR_2013.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2013.csv"},
            { "hist_EURIBOR_2012.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2012.csv"},
            { "hist_EURIBOR_2011.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2011.csv"},
            { "hist_EURIBOR_2010.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2010.csv" }
        }; 

        public static List<Euribors> InterestList { get; set; }

        public static void Save()
        {
            using (var fs = new FileStream(Resources.DATAFILE_NAME, FileMode.Create, FileAccess.Write))
            {
                var xs = new XmlSerializer(typeof(List<Euribors>));
                xs.Serialize(fs, InterestList);
            }
        }

        public static void Load()
        {
            try
            {
                foreach (var item in UrlList)
                {
                    using (var sr = new StreamReader(item.Key))
                    {
                        string line;

                        var dates = new List<string>();
                        var oneMonthValues = new List<string>();

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith(","))
                                dates = line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                            else if (line.StartsWith("1m"))
                                oneMonthValues = line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                        }

                        oneMonthValues.RemoveAt(0);

                        for (var index = 0; index < dates.Count; index++)
                        {
                            var e = new Euribors
                            {
                                Date = DateTime.Parse(dates[index]),
                                OneMonth = Convert.ToDecimal(oneMonthValues[index], CultureInfo.InvariantCulture)
                            };
                            InterestList.Add(e);
                        }
                    }
                }

                InterestList.Sort((item1, item2) => item1.Date.CompareTo(item2.Date));
            }
            catch (FileNotFoundException)
            {
                // TODO: file not found. Ignore?
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

        public static decimal GetMaximumInterest(TimePeriods periods)
        {
            if (InterestList.Count == 0) return 5M;

            switch (periods)
            {
                case TimePeriods.OneWeek:
                    return InterestList.Max(e => e.OneWeek);
                case TimePeriods.TwoWeeks:
                    return InterestList.Max(e => e.TwoWeeks);
                case TimePeriods.OneMonth:
                    return InterestList.Max(e => e.OneMonth);
                case TimePeriods.ThreeMonths:
                    return InterestList.Max(e => e.ThreeMonths);
                case TimePeriods.SixMonths:
                    return InterestList.Max(e => e.SixMonths);
                case TimePeriods.TwelveMonths:
                    return InterestList.Max(e => e.TwelveMonths);
                case TimePeriods.Default:
                    return InterestList.Max(e => new List<decimal> {e.OneMonth, e.OneWeek, e.SixMonths, e.ThreeMonths, e.TwelveMonths, e.TwoWeeks}.Max());
                default:
                    throw new ArgumentOutOfRangeException("periods");
            }
        }

        public static decimal GetMinimumInterest(TimePeriods periods)
        {
            if (InterestList.Count == 0) return 0M;

            switch (periods)
            {
                case TimePeriods.OneWeek:
                    return InterestList.Min(e => e.OneWeek);
                case TimePeriods.TwoWeeks:
                    return InterestList.Min(e => e.TwoWeeks);
                case TimePeriods.OneMonth:
                    return InterestList.Min(e => e.OneMonth);
                case TimePeriods.ThreeMonths:
                    return InterestList.Min(e => e.ThreeMonths);
                case TimePeriods.SixMonths:
                    return InterestList.Min(e => e.SixMonths);
                case TimePeriods.TwelveMonths:
                    return InterestList.Min(e => e.TwelveMonths);
               case TimePeriods.Default:
                    return InterestList.Min(e => new List<decimal> { e.OneMonth, e.OneWeek, e.SixMonths, e.ThreeMonths, e.TwelveMonths, e.TwoWeeks }.Min());
                default:
                    throw new ArgumentOutOfRangeException("periods");
            }
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
                    return Resources.CHART_TITLE_ALL;
                case TimePeriods.OneWeek:
                    return Resources.CHART_TITLE_1W;
                case TimePeriods.TwoWeeks:
                    return Resources.CHART_TITLE_2W;
                case TimePeriods.OneMonth:
                    return Resources.CHART_TITLE_1;
                case TimePeriods.ThreeMonths:
                    return Resources.CHART_TITLE_3;
                case TimePeriods.SixMonths:
                    return Resources.CHART_TITLE_6;
                case TimePeriods.TwelveMonths:
                    return Resources.CHART_TITLE_12;
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
    }
}
