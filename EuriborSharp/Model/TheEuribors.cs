﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using EuriborSharp.Enums;
using EuriborSharp.Properties;
using MoreLinq;

namespace EuriborSharp.Model
{
    [Serializable]
    public static class TheEuribors
    {
        public static Dictionary<string, string> urlList = new Dictionary<string, string>
        {
            { "hist_EURIBOR_2014.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2014.csv" },
            { "hist_EURIBOR_2013.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2013.csv"},
            { "hist_EURIBOR_2012.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2012.csv"},
            { "hist_EURIBOR_2011.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2011.csv"},
            { "hist_EURIBOR_2010.csv", "http://www.emmi-benchmarks.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2010.csv" }
        }; 

        public static List<NewEuriborClass> NewInterestList { get; private set; }

        static TheEuribors()
        {
            NewInterestList = new List<NewEuriborClass>();
        }

        public static void ParseValues()
        {
            try
            {
                foreach (var item in urlList)
                {
                    using (var sr = new StreamReader(item.Key))
                    {
                        string line;

                        var dates = new List<string>();
                        var oneMonthValues = new List<string>();
                        var threeMonthValues = new List<string>();
                        var sixMonthValues = new List<string>();
                        var twelveMonthValues = new List<string>();

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith(","))
                                dates = line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                            else if (line.StartsWith("1m"))
                                oneMonthValues = line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                            else if (line.StartsWith("3m"))
                                threeMonthValues = line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                            else if (line.StartsWith("6m"))
                                sixMonthValues = line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                            else if (line.StartsWith("12m"))
                                twelveMonthValues = line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                        }

                        // remove first item (label)
                        oneMonthValues.RemoveAt(0);
                        threeMonthValues.RemoveAt(0);
                        sixMonthValues.RemoveAt(0);
                        twelveMonthValues.RemoveAt(0);

                        for (var index = 0; index < dates.Count; index++)
                        {
                            var d = DateTime.Parse(dates[index]);
                            NewInterestList.Add(new NewEuriborClass(TimePeriods.OneMonth, d, Convert.ToDecimal(oneMonthValues[index], CultureInfo.InvariantCulture)));
                            NewInterestList.Add(new NewEuriborClass(TimePeriods.ThreeMonths, d, Convert.ToDecimal(threeMonthValues[index], CultureInfo.InvariantCulture)));
                            NewInterestList.Add(new NewEuriborClass(TimePeriods.SixMonths, d, Convert.ToDecimal(sixMonthValues[index], CultureInfo.InvariantCulture)));
                            NewInterestList.Add(new NewEuriborClass(TimePeriods.TwelveMonths, d, Convert.ToDecimal(twelveMonthValues[index], CultureInfo.InvariantCulture)));
                        }
                    }
                }

                NewInterestList.Sort((item1, item2) => item1.Date.CompareTo(item2.Date));
            }
            catch (FileNotFoundException)
            {
                // TODO: file not found. Ignore?
            }
        }

        public static NewEuriborClass GetMaxValue(TimePeriods t)
        {
            return NewInterestList.Where(e => e.TimePeriod == t).MaxBy(m => m.EuriborValue);
        }

        public static NewEuriborClass GetMinValue(TimePeriods t)
        {
            return NewInterestList.Where(e => e.TimePeriod == t).MinBy(m => m.EuriborValue);
        }

        public static DateTime GetOldestDate()
        {
            return NewInterestList.Count == 0 ? DateTime.Now : NewInterestList.Min(e => e.Date);
        }

        public static DateTime GetNewestDate()
        {
            return NewInterestList.Count == 0 ? DateTime.Now : NewInterestList.Max(e => e.Date);
        }

        public static decimal GetMaximumInterest()
        {
            return NewInterestList.Max(e => e.EuriborValue);
        }

        public static decimal GetMaximumInterest(TimePeriods periods)
        {
            return NewInterestList.Count == 0
                ? 0M
                : NewInterestList.Where(e => e.TimePeriod == periods).MaxBy(r => r.EuriborValue).EuriborValue;
        }

        public static decimal GetMinimumInterest(TimePeriods periods)
        {
            return NewInterestList.Count == 0 ? 
                0M : 
                NewInterestList.Where(e => e.TimePeriod == periods).MinBy(r => r.EuriborValue).EuriborValue;
        }

        public static decimal GetMinimumInterest()
        {
            return NewInterestList.Min(e => e.EuriborValue);
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
    }

    public class NewEuriborClass
    {
        public TimePeriods TimePeriod { get; set; }
        public DateTime Date { get; set; }
        public decimal EuriborValue { get; set; }

        public NewEuriborClass(TimePeriods t, DateTime d, decimal e)
        {
            TimePeriod = t;
            Date = d;
            EuriborValue = e;
        }
    }
}
