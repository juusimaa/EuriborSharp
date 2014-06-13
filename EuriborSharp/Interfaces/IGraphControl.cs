namespace EuriborSharp.Interfaces
{
    interface IGraphControl
    {
        void Init(Enums.TimePeriods period, bool smoothSelected, bool xkcd);
        void UpdateGraph();
        void UpdateSmoothing(bool b);
        void UpdateRenderer(bool xkcdSelected);
    }
}
