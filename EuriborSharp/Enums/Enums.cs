using System;

namespace EuriborSharp.Enums
{
    /// <summary>
    /// Available time periods.
    /// </summary>
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

    /// <summary>
    /// Available serie styles.
    /// </summary>
    [Serializable]
    public enum GraphStyle
    {
        Line,
        Bar
    }

    /// <summary>
    /// Available renderer for the graph.
    /// </summary>
    [Serializable]
    public enum Renderer
    {
        Normal,
        Xkcd
    }
}