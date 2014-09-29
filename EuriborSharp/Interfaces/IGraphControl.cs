using EuriborSharp.Enums;

namespace EuriborSharp.Interfaces
{
    interface IGraphControl
    {
        /// <summary>
        /// Initializies plot model and data series based on given parameters.
        /// </summary>
        /// <param name="period">Selected time period.</param>
        /// <param name="smoothSelected">Flag for smooth line.</param>
        /// <param name="style">Selected plot data serie type.</param>
        /// <param name="renderer">Selected plot renderer.</param>
        /// <param name="dotLine">Flag for dotted line.</param>
        void Init(TimePeriods period, bool smoothSelected, GraphStyle style, Renderer renderer, bool dotLine);
        
        /// <summary>
        /// Updated selected plot data series.
        /// </summary>
        void UpdateGraph(TimePeriods period);

        /// <summary>
        /// Updated all plot data series.
        /// </summary>
        void UpdateGraph();

        void View30Days();
    }
}
