using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace EuriborSharp.Model
{
    [Serializable]
    public static class TheEuribors
    {
        public static List<Euribors> InterestList { get; set; }

        public static void Save()
        {
            using (var fs = new FileStream("data.xml", FileMode.CreateNew, FileAccess.Write))
            {
                var xs = new XmlSerializer(typeof(List<Euribors>));
                xs.Serialize(fs, InterestList);
            }
        }

        public static void Load()
        {
            using (var fs = new FileStream("data.xml", FileMode.Open, FileAccess.Read))
            {
                var xs = new XmlSerializer(typeof(List<Euribors>));
                InterestList = (List<Euribors>)xs.Deserialize(fs);
            }
        }

        public static void ParseInterestRates(string text, Euribors current)
        {
            var periodPattern = new Regex(@"(\d)(\s\w+\s)");
            var interestPattern = new Regex(@"(\d+,\d+)");
            var datePattern = new Regex(@"(\d+[.]\d+[.]\d+)");

            var interestValue = interestPattern.Match(text).Value;
            var timePeriod = periodPattern.Match(text);
            var date = datePattern.Match(text).Value;

            var period = ParseTimePeriod(timePeriod);

            switch (period)
            {
                case Enums.TimePeriods.OneWeek:
                    current.OneWeek = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.TwoWeeks:
                    current.TwoWeeks = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.OneMonth:
                    current.OneMonth = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.ThreeMonths:
                    current.ThreeMonths = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.SixMonths:
                    current.SixMonths = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.TwelveMonths:
                    current.TwelveMonths = Convert.ToDecimal(interestValue);
                    break;
            }

            current.Date = DateTime.Parse(date, new CultureInfo("fi-FI"), DateTimeStyles.AssumeLocal);
        }

        private static Enums.TimePeriods ParseTimePeriod(Match value)
        {
            var intMatch = Convert.ToInt32(value.Groups[1].Value);
            var stringMatch = value.Groups[2].Value.Trim();

            switch (stringMatch)
            {
                case "kk":
                    switch (intMatch)
                    {
                        case 1:
                            return Enums.TimePeriods.OneMonth;
                        case 3:
                            return Enums.TimePeriods.ThreeMonths;
                        case 6:
                            return Enums.TimePeriods.SixMonths;
                        case 12:
                            return Enums.TimePeriods.TwelveMonths;
                        default:
                            return Enums.TimePeriods.Default;
                    }
                case "vko":
                    break;
                default:
                    return Enums.TimePeriods.Default;
            }

            return Enums.TimePeriods.Default;
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
