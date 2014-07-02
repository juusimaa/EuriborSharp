using EuriborSharp.Enums;

namespace EuriborSharp.Interfaces
{
    interface IGraphControl
    {
        void Init(TimePeriods period, bool smoothSelected, GraphStyle style, Renderer renderer, bool dotLine);
        void UpdateGraph();
        void UpdateSmoothing(bool b);
    }
}
