namespace EuriborSharp.Interfaces
{
    interface IGraphControl
    {
        void Init(Enums.TimePeriods period);
        void UpdateGraph();
    }
}
