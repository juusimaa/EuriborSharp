using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using EuriborSharp.Enums;
using EuriborSharp.Interfaces;
using EuriborSharp.Model;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace EuriborSharp.Views
{
    public partial class GraphControl : UserControl, IGraphControl
    {
        private const double DATE_AXIS_OFFSET = 2.0;

        private PlotView _graphPlotView;
        private PlotModel _euriborPlotModel;
        private LineSeries _euriborSeries;
        private DateTimeAxis _xAxis;
        private LinearAxis _yAxis;

        private TimePeriods _currentTimePeriod;

        public GraphControl()
        {
            InitializeComponent();
        }

        public void Init(TimePeriods pediod)
        {
            _currentTimePeriod = pediod;
            Dock = DockStyle.Fill;

            _graphPlotView = new PlotView
            {
                Dock = DockStyle.Fill
            };

            _euriborPlotModel = new PlotModel
            {
                PlotType = PlotType.XY,
                Title = "Euribor " + TheEuribors.GetInterestName(pediod),
                PlotAreaBackground = OxyColors.White,
                RenderingDecorator = rc => new XkcdRenderingDecorator(rc)
            };

            _euriborSeries = new LineSeries
            {
                MarkerType = MarkerType.Circle
            };

            _xAxis = new DateTimeAxis
            {
                Unit = "Date",
                Minimum = DateTimeAxis.ToDouble(TheEuribors.GetOldestDate().AddDays(-DATE_AXIS_OFFSET)),
                Maximum = DateTimeAxis.ToDouble(TheEuribors.GetNewestDate().AddDays(DATE_AXIS_OFFSET)),
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                StringFormat = "d.M.yy",
                FontSize = 20
            };

            _yAxis = new LinearAxis
            {
                Unit = "%",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                FontSize = 20
            };

            _euriborPlotModel.Series.Add(_euriborSeries);
            _euriborPlotModel.Axes.Add(_xAxis);
            _euriborPlotModel.Axes.Add(_yAxis);

            _graphPlotView.Model = _euriborPlotModel;

            graphTableLayoutPanel.Controls.Add(_graphPlotView, 0, 0);
            graphTableLayoutPanel.SetColumnSpan(_graphPlotView, 2);
            graphTableLayoutPanel.SetRowSpan(_graphPlotView, 2);
        }

        public void UpdateGraph()
        {
            AddPointsToSeries();
        }

        private void AddPointsToSeries()
        {
            foreach (var item in TheEuribors.InterestList)
            {
                var value = TheEuribors.GetInterest(item, _currentTimePeriod);
                var dp = new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(value));
                _euriborSeries.Points.Add(dp);
            }

            if (_euriborSeries.Points.Count == 0) return;

            // Annotate last point
            var last = _euriborSeries.Points.OrderByDescending(e => e.X).First();
            var pa = new PointAnnotation
            {
                X = last.X,
                Y = last.Y,
                Text = last.Y.ToString(CultureInfo.InvariantCulture),
                Size = 7,
                TextColor = OxyColors.Black,
                Fill = OxyColors.Red,
                FontSize = 16
            };
            _euriborPlotModel.Annotations.Add(pa);
        }
    }
}
