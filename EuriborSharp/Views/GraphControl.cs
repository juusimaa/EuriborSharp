using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using EuriborSharp.Enums;
using EuriborSharp.Interfaces;
using EuriborSharp.Model;
using EuriborSharp.Properties;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using HorizontalAlignment = OxyPlot.HorizontalAlignment;

namespace EuriborSharp.Views
{
    public partial class GraphControl : UserControl, IGraphControl
    {
        private const double DATE_AXIS_OFFSET = 2.0;
        private const double INTEREST_MAX_OFFSET = 0.02;
        private const double INTEREST_MIN_OFFSET = 0.02;

        private PlotView _graphPlotView;
        private PlotModel _euriborPlotModel;

        private LineSeries _euriborSeriesSixMonth;
        private LineSeries _euriborSeriesOneMonth;
        private LineSeries _euriborSeriesThreeMonth;
        private LineSeries _euriborSeriesTwelveMonth;

        private ColumnSeries _euriborSeriesSixMonthCol;
        private ColumnSeries _euriborSeriesOneMonthCol;
        private ColumnSeries _euriborSeriesThreeMonthCol;
        private ColumnSeries _euriborSeriesTwelveMonthCol;

        private CategoryAxis _categoryAxis;
        private DateTimeAxis _xAxis;
        private LinearAxis _yAxis;
        private LineAnnotation _minLineAnnotation;
        private LineAnnotation _maxLineAnnotation;
        private TextAnnotation _textAnnotation;

        private TimePeriods _currentTimePeriod;
        private GraphStyle _currentStyle;

        public GraphControl()
        {
            InitializeComponent();
        }

        public void Init(TimePeriods period, bool smoothSelected, GraphStyle style, Renderer renderer, bool dotLine)
        {
            if (_graphPlotView != null) _graphPlotView.Dispose();
            
            _textAnnotation = new TextAnnotation();
            _minLineAnnotation = new LineAnnotation();
            _maxLineAnnotation = new LineAnnotation();

            _currentStyle = style;
            _currentTimePeriod = period;
            Dock = DockStyle.Fill;

            _graphPlotView = new PlotView
            {
                Dock = DockStyle.Fill
            };

            switch (renderer)
            {
                case Renderer.Normal:
                    _euriborPlotModel = new PlotModel
                    {
                        PlotType = PlotType.XY,
                        Title = TheEuribors.GetInterestName(period),
                        PlotAreaBackground = OxyColors.White,
                        LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                        LegendBorderThickness = 0,
                        LegendFontSize = 10
                    };
                    break;
                case Renderer.Xkcd:
                    _euriborPlotModel = new PlotModel
                    {
                        PlotType = PlotType.XY,
                        Title = TheEuribors.GetInterestName(period),
                        PlotAreaBackground = OxyColors.White,
                        RenderingDecorator = rc => new XkcdRenderingDecorator(rc),
                        LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                        LegendBorderThickness = 0,
                        LegendFontSize = 20
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException("renderer");
            }

            switch (style)
            {
                case GraphStyle.Line:
                    SetupLinearSeries(period, smoothSelected, renderer, dotLine);
                    break;
                case GraphStyle.Bar:
                    SetupColumnSeries();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }

            if (_currentTimePeriod != TimePeriods.Default)
            {
                _euriborPlotModel.Annotations.Add(_textAnnotation);
                _euriborPlotModel.Annotations.Add(_minLineAnnotation);
                _euriborPlotModel.Annotations.Add(_maxLineAnnotation);
                _euriborPlotModel.IsLegendVisible = false;
            }
            else
            {
                _euriborPlotModel.LegendPlacement = LegendPlacement.Inside;
                _euriborPlotModel.LegendPosition = LegendPosition.BottomLeft;
                _euriborPlotModel.IsLegendVisible = true;
            }

            _graphPlotView.Model = _euriborPlotModel;
            graphTableLayoutPanel.Controls.Add(_graphPlotView, 0, 0);
            graphTableLayoutPanel.SetColumnSpan(_graphPlotView, 2);
            graphTableLayoutPanel.SetRowSpan(_graphPlotView, 2);
        }

        private void SetupLinearSeries(TimePeriods period, bool smoothSelected, Renderer renderer, bool dotLine)
        {
            _euriborSeriesSixMonth = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = renderer == Renderer.Xkcd ? 7 : 4,
                CanTrackerInterpolatePoints = false,
                Smooth = smoothSelected,
                LineStyle = dotLine ? LineStyle.Dot : LineStyle.Solid,
                Title = Resources.SIX_MONTH_SERIE_TITLE
            };

            _euriborSeriesOneMonth = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = renderer == Renderer.Xkcd ? 7 : 4,
                CanTrackerInterpolatePoints = false,
                Smooth = smoothSelected,
                LineStyle = dotLine ? LineStyle.Dot : LineStyle.Solid,
                Title = Resources.ONE_MONTH_SERIE_TITLE
            };

            _euriborSeriesThreeMonth = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = renderer == Renderer.Xkcd ? 7 : 4,
                CanTrackerInterpolatePoints = false,
                Smooth = smoothSelected,
                LineStyle = dotLine ? LineStyle.Dot : LineStyle.Solid,
                Title = Resources.THREE_MONTH_SERIE_TITLE
            };

            _euriborSeriesTwelveMonth = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = renderer == Renderer.Xkcd ? 7 : 4,
                CanTrackerInterpolatePoints = false,
                Smooth = smoothSelected,
                LineStyle = dotLine ? LineStyle.Dot : LineStyle.Solid,
                Title = Resources.TWELVE_MONTH_SERIE_TITLE
            };

            _euriborPlotModel.Series.Add(_euriborSeriesTwelveMonth);
            _euriborPlotModel.Series.Add(_euriborSeriesSixMonth);
            _euriborPlotModel.Series.Add(_euriborSeriesOneMonth);
            _euriborPlotModel.Series.Add(_euriborSeriesThreeMonth);

            _xAxis = new DateTimeAxis
            {
                Unit = Resources.X_AXIS_UNIT,
                Minimum = DateTimeAxis.ToDouble(TheEuribors.GetOldestDate().AddDays(-DATE_AXIS_OFFSET)),
                Maximum = DateTimeAxis.ToDouble(TheEuribors.GetNewestDate().AddDays(DATE_AXIS_OFFSET)),
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                StringFormat = "d.M.yy",
                FontSize = 20
            };

            _yAxis = new LinearAxis
            {
                Unit = Resources.Y_AXIS_UNIT,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                FontSize = 20,
                Maximum = Convert.ToDouble(TheEuribors.GetMaximumInterest(period)) + INTEREST_MAX_OFFSET,
                Minimum = Convert.ToDouble(TheEuribors.GetMinimumInterest(period)) - INTEREST_MIN_OFFSET
            };

            _euriborPlotModel.Axes.Add(_xAxis);
            _euriborPlotModel.Axes.Add(_yAxis);

            if (period != TimePeriods.Default) return;
            
            _euriborSeriesSixMonth.MarkerType = MarkerType.None;
            _euriborSeriesOneMonth.MarkerType = MarkerType.None;
            _euriborSeriesThreeMonth.MarkerType = MarkerType.None;
            _euriborSeriesTwelveMonth.MarkerType = MarkerType.None;
        }

        private void SetupColumnSeries()
        {
            _euriborSeriesOneMonthCol = new ColumnSeries
            {
                Title = Resources.ONE_MONTH_SERIE_TITLE,
                StrokeThickness = 1,
                FillColor = OxyColors.Red
            };

            _euriborSeriesSixMonthCol = new ColumnSeries
            {
                Title = Resources.SIX_MONTH_SERIE_TITLE,
                StrokeThickness = 1,
                FillColor = OxyColors.Plum
            };

            _euriborSeriesThreeMonthCol = new ColumnSeries
            {
                Title = Resources.THREE_MONTH_SERIE_TITLE,
                StrokeThickness = 1,
                FillColor = OxyColors.Peru
            };

            _euriborSeriesTwelveMonthCol = new ColumnSeries
            {
                Title = Resources.TWELVE_MONTH_SERIE_TITLE,
                StrokeThickness = 1,
                FillColor = OxyColors.Navy
            };

            _euriborPlotModel.Series.Add(_euriborSeriesTwelveMonthCol);
            _euriborPlotModel.Series.Add(_euriborSeriesSixMonthCol);
            _euriborPlotModel.Series.Add(_euriborSeriesOneMonthCol);
            _euriborPlotModel.Series.Add(_euriborSeriesThreeMonthCol);

            _categoryAxis = new CategoryAxis
            {
                GapWidth = 0,
                IsAxisVisible = false
            };

            _euriborPlotModel.Axes.Add(_categoryAxis);
        }

        public void UpdateGraph()
        {
            switch (_currentStyle)
            {
                case GraphStyle.Line:
                    AddPointsToLinearSeries();
                    break;
                case GraphStyle.Bar:
                    AddPointsToColumnSeries();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddPointsToColumnSeries()
        {
            _euriborSeriesOneMonthCol.ItemsSource = TheEuribors.InterestList;
            _euriborSeriesThreeMonthCol.ItemsSource = TheEuribors.InterestList;
            _euriborSeriesSixMonthCol.ItemsSource = TheEuribors.InterestList;
            _euriborSeriesTwelveMonthCol.ItemsSource = TheEuribors.InterestList;

            switch (_currentTimePeriod)
            {
                case TimePeriods.Default:
                    _euriborSeriesOneMonthCol.ValueField = "OneMonth";
                    _euriborSeriesThreeMonthCol.ValueField = "ThreeMonths";
                    _euriborSeriesSixMonthCol.ValueField = "SixMonths";
                    _euriborSeriesTwelveMonthCol.ValueField = "TwelveMonths";
                    break;
                case TimePeriods.OneWeek:
                    break;
                case TimePeriods.TwoWeeks:
                    break;
                case TimePeriods.OneMonth:
                    _euriborSeriesOneMonthCol.ValueField = "OneMonth";
                    break;
                case TimePeriods.ThreeMonths:
                    _euriborSeriesThreeMonthCol.ValueField = "ThreeMonths";
                    break;
                case TimePeriods.SixMonths:
                    _euriborSeriesSixMonthCol.ValueField = "SixMonths";
                    break;
                case TimePeriods.TwelveMonths:
                    _euriborSeriesTwelveMonthCol.ValueField = "TwelveMonths";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _categoryAxis.ItemsSource = TheEuribors.InterestList;
            _categoryAxis.LabelField = "Date";
        }

        private void AddPointsToLinearSeries()
        {
            _euriborSeriesOneMonth.Points.Clear();
            _euriborSeriesSixMonth.Points.Clear();
            _euriborSeriesThreeMonth.Points.Clear();
            _euriborSeriesTwelveMonth.Points.Clear();

            switch (_currentTimePeriod)
            {
                case TimePeriods.Default:
                    foreach (var item in TheEuribors.InterestList)
                    {
                        var dpOneMonth = new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.OneMonth));
                        var dpThreeMonths = new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.ThreeMonths));
                        var dpTwelveMonth = new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.TwelveMonths));
                        var dpSixMonths = new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.SixMonths));
                        _euriborSeriesOneMonth.Points.Add(dpOneMonth);
                        _euriborSeriesThreeMonth.Points.Add(dpThreeMonths);
                        _euriborSeriesTwelveMonth.Points.Add(dpTwelveMonth);
                        _euriborSeriesSixMonth.Points.Add(dpSixMonths);
                    }
                    break;
                case TimePeriods.OneWeek:
                case TimePeriods.TwoWeeks:
                case TimePeriods.OneMonth:
                case TimePeriods.ThreeMonths:
                case TimePeriods.SixMonths:
                case TimePeriods.TwelveMonths:
                    foreach (var item in TheEuribors.InterestList)
                    {
                        var value = TheEuribors.GetInterest(item, _currentTimePeriod);
                        var dp = new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(value));
                        _euriborSeriesOneMonth.Points.Add(dp);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_euriborSeriesOneMonth.Points.Count == 0) return;

            // Annotations
            var last = _euriborSeriesOneMonth.Points.OrderByDescending(e => e.X).First();
            var max = _euriborSeriesOneMonth.Points.Max(e => e.Y);
            var min = _euriborSeriesOneMonth.Points.Min(e => e.Y);

            var textForAnnotation = Resources.TEXT_ANNOTATION_LABEL + last.Y.ToString("0.000",CultureInfo.InvariantCulture);
            var pointForAnnotation = new DataPoint(last.X - (textForAnnotation.Length / 2.0), last.Y + ((max - min) / 2));

            _textAnnotation.TextPosition = pointForAnnotation;
            _textAnnotation.Text = textForAnnotation;
            _textAnnotation.TextColor = OxyColors.Black;
            _textAnnotation.FontSize = 20.0;
            _textAnnotation.TextHorizontalAlignment = HorizontalAlignment.Center;
            _textAnnotation.TextVerticalAlignment = VerticalAlignment.Top;
            _textAnnotation.StrokeThickness = 0;

            _minLineAnnotation.Type = LineAnnotationType.Horizontal;
            _minLineAnnotation.Y = Convert.ToDouble(TheEuribors.GetMinimumInterest(_currentTimePeriod));
            _minLineAnnotation.Text = Resources.MIN_LABEL;
            _minLineAnnotation.FontSize = 20.0;
            _minLineAnnotation.Color = OxyColors.Blue;

            _maxLineAnnotation.Type = LineAnnotationType.Horizontal;
            _maxLineAnnotation.Y = Convert.ToDouble(TheEuribors.GetMaximumInterest(_currentTimePeriod));
            _maxLineAnnotation.Text = Resources.MAX_LABEL;
            _maxLineAnnotation.FontSize = 20.0;
            _maxLineAnnotation.Color = OxyColors.Red;
        }
    }
}
