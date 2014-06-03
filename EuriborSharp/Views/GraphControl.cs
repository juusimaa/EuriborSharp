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
        private const double INTEREST_OFFSET = 0.001;

        private PlotView _graphPlotView;
        private PlotModel _euriborPlotModel;
        private LineSeries _euriborSeries;
        private DateTimeAxis _xAxis;
        private LinearAxis _yAxis;
        private PointAnnotation _pointAnnotation;
        private LineAnnotation _minLineAnnotation;
        private LineAnnotation _maxLineAnnotation;

        private TimePeriods _currentTimePeriod;

        public GraphControl()
        {
            InitializeComponent();
        }

        public void Init(TimePeriods period)
        {
            _pointAnnotation = new PointAnnotation();
            _minLineAnnotation = new LineAnnotation();
            _maxLineAnnotation = new LineAnnotation();

            _currentTimePeriod = period;
            Dock = DockStyle.Fill;

            _graphPlotView = new PlotView
            {
                Dock = DockStyle.Fill
            };

            _euriborPlotModel = new PlotModel
            {
                PlotType = PlotType.XY,
                Title = "Euribor " + TheEuribors.GetInterestName(period),
                PlotAreaBackground = OxyColors.White,
                RenderingDecorator = rc => new XkcdRenderingDecorator(rc)
            };

            _euriborSeries = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 7,
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.None
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
                FontSize = 20,
                Maximum = Convert.ToDouble(TheEuribors.GetMaximumInterest(period)) + INTEREST_OFFSET,
                Minimum = Convert.ToDouble(TheEuribors.GetMinimumInterest(period)) - INTEREST_OFFSET
            };

            _euriborPlotModel.Series.Add(_euriborSeries);
            _euriborPlotModel.Axes.Add(_xAxis);
            _euriborPlotModel.Axes.Add(_yAxis);

            _euriborPlotModel.Annotations.Add(_pointAnnotation);
            _euriborPlotModel.Annotations.Add(_minLineAnnotation);
            _euriborPlotModel.Annotations.Add(_maxLineAnnotation);

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
            _euriborSeries.Points.Clear();

            foreach (var item in TheEuribors.InterestList)
            {
                var value = TheEuribors.GetInterest(item, _currentTimePeriod);
                var dp = new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(value));
                _euriborSeries.Points.Add(dp);
            }

            if (_euriborSeries.Points.Count == 0) return;

            // Annotations
            var last = _euriborSeries.Points.OrderByDescending(e => e.X).First();
            var max = _euriborSeries.Points.Max(e => e.Y);
            var min = _euriborSeries.Points.Min(e => e.Y);

            _pointAnnotation.X = last.X;
            _pointAnnotation.Y = last.Y;
            _pointAnnotation.Text = last.Y.ToString(CultureInfo.InvariantCulture);
            _pointAnnotation.TextColor = OxyColors.Black;
            _pointAnnotation.Shape = MarkerType.None;

            _minLineAnnotation.Type = LineAnnotationType.Horizontal;
            _minLineAnnotation.Y = min;
            _minLineAnnotation.Text = "Min";

            _maxLineAnnotation.Type = LineAnnotationType.Horizontal;
            _maxLineAnnotation.Y = max;
            _maxLineAnnotation.Text = "Max";
        }
    }
}
