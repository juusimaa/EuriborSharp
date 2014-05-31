using System;
using System.Collections.Generic;

namespace EuriborSharp.Model
{
    [Serializable]
    public static class TheEuribors
    {
        public static List<Euribors> InterestList { get; set; }
    }

    [Serializable]
    public class Euribors
    {
        public decimal OneMonth { get; set; }
        public decimal ThreeMonths { get; set; }
        public decimal SixMonths { get; set; }
        public decimal TwelveMonths { get; set; }
<<<<<<< HEAD
=======
        public decimal OneWeek { get; set; }
        public decimal TwoWeeks { get; set; }

>>>>>>> 296e8e2a8e7ee5546da586ca5013b94b41a71abb
        public DateTime Date { get; set; }

        public Euribors()
        {}
    }
}
