using System;
using System.Collections.Generic;

namespace EuriborSharp.Model
{
    [Serializable]
    public static class TheEuribors
    {
        public static List<Euribors> InterestList { get; set; }
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
