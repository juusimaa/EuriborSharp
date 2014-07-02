using System;

namespace EuriborSharp.Enums
{
    public enum TimePeriods
    {
        Default = 0,
        OneWeek,
        TwoWeeks,
        OneMonth,
        ThreeMonths,
        SixMonths,
        TwelveMonths
    }

    [Serializable]
    public enum GraphStyle
    {
        Line,
        XkcdLine,
        Bar,
        XkcdBar
    }
}