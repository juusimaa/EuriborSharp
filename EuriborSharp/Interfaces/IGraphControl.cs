using EuriborSharp.Enums;

namespace EuriborSharp.Interfaces
{
    interface IGraphControl
    {
        void Init(Enums.TimePeriods period, bool smoothSelected, GraphStyle style);
        void UpdateGraph();
        void UpdateSmoothing(bool b);
        void SetLineStyleToNormal();
        void SetLineStyleToDot();
    }
}
