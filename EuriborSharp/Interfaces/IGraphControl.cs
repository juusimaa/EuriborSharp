namespace EuriborSharp.Interfaces
{
    interface IGraphControl
    {
        void Init(Enums.TimePeriods period, bool smoothSelected);
        void UpdateGraph();
        void UpdateSmoothing(bool b);
    }
}
