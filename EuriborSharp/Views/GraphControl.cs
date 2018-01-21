using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using EuriborSharp.Enums;
using EuriborSharp.Interfaces;
using EuriborSharp.Model;
using EuriborSharp.Properties;
using MoreLinq;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace EuriborSharp.Views
{
    public partial class GraphControl : UserControl, IGraphControl
    {
        private const double DATE_AXIS_OFFSET = 20.0;
        private const double INTEREST_MAX_OFFSET = 0.2;
        private const double INTEREST_MIN_OFFSET = 0.2;
        private const double TEXT_ANNOTATION_OFFSET = 200.0;

        private PlotView _graphPlotView;
        private PlotModel _euriborPlotModel;

        private LineSeries _euriborLinearSeries;

        private LineSeries _combined1mSerie;
        private LineSeries _combined3mSerie;
        private LineSeries _combined6mSerie;
        private LineSeries _combined12mSerie;

        private ColumnSeries _euriborSeriesSixMonthCol;
        private ColumnSeries _euriborSeriesOneMonthCol;
        private ColumnSeries _euriborSeriesThreeMonthCol;
        private ColumnSeries _euriborSeriesTwelveMonthCol;

        private CategoryAxis _categoryAxis;
        private DateTimeAxis _xAxis;
        private LinearAxis _yAxis;
        private LineAnnotation _minLineAnnotation;
        private LineAnnotation _maxLineAnnotation;
        private TextAnnotation _textAnnotationCurrent;

        private TimePeriods _currentTimePeriod;
        private GraphStyle _currentStyle;

        public GraphControl()
        {
            InitializeComponent();

            _euriborLinearSeries = new LineSeries();
        }

        public void Init(TimePeriods period, bool smoothSelected, GraphStyle style, Renderer renderer, bool dotLine)
        {
            if (_graphPlotView != null) _graphPlotView.Dispose();
            
            _textAnnotationCurrent = new TextAnnotation();
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
                    SetupAxesAndLinearSeries(period, smoothSelected, renderer, dotLine);
                    break;
                case GraphStyle.Bar:
                    SetupColumnSeries();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }

            if (_currentTimePeriod != TimePeriods.Default)
            {
                _euriborPlotModel.Annotations.Add(_textAnnotationCurrent);
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

        private void SetupLinearSeries(Renderer r, bool d, bool s)
        {
            var title = String.Empty;

            switch (_currentTimePeriod)
            {
                case TimePeriods.Default:
                    break;
                case TimePeriods.OneMonth:
                    title = Resources.ONE_MONTH_SERIE_TITLE;
                    break;
                case TimePeriods.ThreeMonths:
                    title = Resources.THREE_MONTH_SERIE_TITLE;
                    break;
                case TimePeriods.SixMonths:
                    title = Resources.SIX_MONTH_SERIE_TITLE;
                    break;
                case TimePeriods.TwelveMonths:
                    title = Resources.TWELVE_MONTH_SERIE_TITLE;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _euriborLinearSeries = new LineSeries
            {
                MarkerType = MarkerType.None,
                MarkerSize = r == Renderer.Xkcd ? 7 : 4,
                CanTrackerInterpolatePoints = false,
                Smooth = s,
                LineStyle = d ? LineStyle.Dot : LineStyle.Solid,
                Title = title
            };
            _combined1mSerie = new LineSeries
            {
                MarkerType = MarkerType.None,
                MarkerSize = r == Renderer.Xkcd ? 5 : 2,
                CanTrackerInterpolatePoints = false,
                Smooth = s,
                LineStyle = d ? LineStyle.Dot : LineStyle.Solid,
                Title = Resources.ONE_MONTH_SERIE_TITLE
            };
            _combined3mSerie = new LineSeries
            {
                MarkerType = MarkerType.None,
                MarkerSize = r == Renderer.Xkcd ? 5 : 2,
                CanTrackerInterpolatePoints = false,
                Smooth = s,
                LineStyle = d ? LineStyle.Dot : LineStyle.Solid,
                Title = Resources.THREE_MONTH_SERIE_TITLE
            };
            _combined6mSerie = new LineSeries
            {
                MarkerType = MarkerType.None,
                MarkerSize = r == Renderer.Xkcd ? 5 : 2,
                CanTrackerInterpolatePoints = false,
                Smooth = s,
                LineStyle = d ? LineStyle.Dot : LineStyle.Solid,
                Title = Resources.SIX_MONTH_SERIE_TITLE
            };
            _combined12mSerie = new LineSeries
            {
                MarkerType = MarkerType.None,
                MarkerSize = r == Renderer.Xkcd ? 5 : 2,
                CanTrackerInterpolatePoints = false,
                Smooth = s,
                LineStyle = d ? LineStyle.Dot : LineStyle.Solid,
                Title = Resources.TWELVE_MONTH_SERIE_TITLE
            };
        }

        private void SetupAxesAndLinearSeries(TimePeriods period, bool smoothSelected, Renderer renderer, bool dotLine)
        {
            SetupLinearSeries(renderer, dotLine, smoothSelected);

            if (_currentTimePeriod != TimePeriods.Default)
            {
                _euriborPlotModel.Series.Add(_euriborLinearSeries);
            }
            else
            {
                _euriborPlotModel.Series.Add(_combined1mSerie);
                _euriborPlotModel.Series.Add(_combined3mSerie);
                _euriborPlotModel.Series.Add(_combined6mSerie);
                _euriborPlotModel.Series.Add(_combined12mSerie);
            }

            _xAxis = new DateTimeAxis
            {
                Title = Resources.X_AXIS_TITLE,
                Unit = Resources.X_AXIS_UNIT,
                Minimum = DateTimeAxis.ToDouble(TheEuribors.GetOldestDate()),
                Maximum = DateTimeAxis.ToDouble(TheEuribors.GetNewestDate().AddDays(DATE_AXIS_OFFSET)),
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                StringFormat = Resources.X_AXIS_UNIT,
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                FontSize = 20
            };

            _yAxis = new LinearAxis
            {
                Title =  Resources.Y_AXIS_TITLE,
                Unit = Resources.Y_AXIS_UNIT,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                FontSize = 20,
                Maximum = Convert.ToDouble(TheEuribors.GetMaximumInterest(period)) + INTEREST_MAX_OFFSET,
                Minimum = Convert.ToDouble(TheEuribors.GetMinimumInterest(period)) - INTEREST_MIN_OFFSET
            };

            _euriborPlotModel.Axes.Add(_xAxis);
            _euriborPlotModel.Axes.Add(_yAxis);
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

        public void ViewLastDays(int days)
        {
            AddOnlyLastPointsToLinearSeries(days);
        }

        public void UpdateGraph(TimePeriods period)
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
            _euriborSeriesOneMonthCol.ItemsSource = TheEuribors.NewInterestList;
            _euriborSeriesThreeMonthCol.ItemsSource = TheEuribors.NewInterestList;
            _euriborSeriesSixMonthCol.ItemsSource = TheEuribors.NewInterestList;
            _euriborSeriesTwelveMonthCol.ItemsSource = TheEuribors.NewInterestList;

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

            _categoryAxis.ItemsSource = TheEuribors.NewInterestList;
            _categoryAxis.LabelField = "Date";
        }

        private void AddOnlyLastPointsToLinearSeries(int days)
        {
            _euriborLinearSeries.Points.Clear();
            _combined1mSerie.Points.Clear();
            _combined3mSerie.Points.Clear();
            _combined6mSerie.Points.Clear();
            _combined12mSerie.Points.Clear();

            if (_currentTimePeriod == TimePeriods.Default)
            {
                // TODO
            }
            else
            {
                var values = TheEuribors.NewInterestList
                    .Where(e => e.TimePeriod == _currentTimePeriod && e.Date > (DateTime.Now - new TimeSpan(days, 0, 0, 0)));

                var newEuriborClasses = values as IList<NewEuriborClass> ?? values.ToList();
                foreach (var p in newEuriborClasses)
                {
                    var point = new DataPoint(DateTimeAxis.ToDouble(p.Date), Convert.ToDouble(p.EuriborValue));
                    _euriborLinearSeries.Points.Add(point);
                }

                _euriborPlotModel.Title = TheEuribors.GetInterestName(_currentTimePeriod) + " from last " + days + " days";

                if (!newEuriborClasses.Any()) return;

                _xAxis.Minimum = DateTimeAxis.ToDouble(newEuriborClasses.Min(e => e.Date));
                _xAxis.Maximum = DateTimeAxis.ToDouble(newEuriborClasses.Max(e => e.Date).AddDays(days / 10));

                _yAxis.Maximum = Convert.ToDouble(newEuriborClasses.MaxBy(e => e.EuriborValue).EuriborValue) + (Convert.ToDouble(days) / 1200);
                _yAxis.Minimum = Convert.ToDouble(newEuriborClasses.MinBy(e => e.EuriborValue).EuriborValue) - (Convert.ToDouble(days) / 1200);

                var last = _euriborLinearSeries.Points.OrderByDescending(e => e.X).First();
                var lastDate = newEuriborClasses.OrderBy(e => e.Date).Last();

                var textForAnnotationCurrent = Resources.TEXT_ANNOTATION_LABEL_CURRENT + last.Y.ToString("0.000", CultureInfo.InvariantCulture) +
                                               "\n(" + lastDate.Date.ToShortDateString() + ")";
                var pointForAnnotationCurrent = new DataPoint(last.X, last.Y);

                _textAnnotationCurrent.TextPosition = pointForAnnotationCurrent;
                _textAnnotationCurrent.Text = textForAnnotationCurrent;
                _textAnnotationCurrent.TextColor = OxyColors.Black;
                _textAnnotationCurrent.FontSize = 20.0;
                _textAnnotationCurrent.TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center;
                _textAnnotationCurrent.TextVerticalAlignment = VerticalAlignment.Top;
                _textAnnotationCurrent.StrokeThickness = 0;
            }
        }

        private void AddPointsToLinearSeries()
        {
            _euriborLinearSeries.Points.Clear();
            _combined1mSerie.Points.Clear();
            _combined3mSerie.Points.Clear();
            _combined6mSerie.Points.Clear();
            _combined12mSerie.Points.Clear();

            if (_currentTimePeriod == TimePeriods.Default)
            {
                foreach (var p in TheEuribors.NewInterestList.Where(e => e.TimePeriod == TimePeriods.OneMonth).Select(item => new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.EuriborValue))))
                {
                    _combined1mSerie.Points.Add(p);
                }
                foreach (var p in TheEuribors.NewInterestList.Where(e => e.TimePeriod == TimePeriods.ThreeMonths).Select(item => new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.EuriborValue))))
                {
                    _combined3mSerie.Points.Add(p);
                }
                foreach (var p in TheEuribors.NewInterestList.Where(e => e.TimePeriod == TimePeriods.SixMonths).Select(item => new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.EuriborValue))))
                {
                    _combined6mSerie.Points.Add(p);
                }
                foreach (var p in TheEuribors.NewInterestList.Where(e => e.TimePeriod == TimePeriods.TwelveMonths).Select(item => new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.EuriborValue))))
                {
                    _combined12mSerie.Points.Add(p);
                }
            }
            else
            {
                foreach (var p in TheEuribors.NewInterestList.Where(e => e.TimePeriod == _currentTimePeriod).Select(item => new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.EuriborValue))))
                {
                    _euriborLinearSeries.Points.Add(p);
                }
            }

            if (_euriborLinearSeries.Points.Count == 0) return;

            _xAxis.Minimum = DateTimeAxis.ToDouble(TheEuribors.GetOldestDate());
            _xAxis.Maximum = DateTimeAxis.ToDouble(TheEuribors.GetNewestDate().AddDays(DATE_AXIS_OFFSET));

            _yAxis.Maximum = Convert.ToDouble(TheEuribors.GetMaximumInterest(_currentTimePeriod)) + INTEREST_MAX_OFFSET;
            _yAxis.Minimum = Convert.ToDouble(TheEuribors.GetMinimumInterest(_currentTimePeriod)) - INTEREST_MIN_OFFSET;

            // Annotations
            var last = _euriborLinearSeries.Points.OrderByDescending(e => e.X).First();
            var lastDate = TheEuribors.NewInterestList.OrderBy(e => e.Date).Last();
            var max = _euriborLinearSeries.Points.Max(e => e.Y);
            var min = _euriborLinearSeries.Points.Min(e => e.Y);

            var textForAnnotationCurrent = Resources.TEXT_ANNOTATION_LABEL_CURRENT + last.Y.ToString("0.000", CultureInfo.InvariantCulture) + 
                "\n(" + lastDate.Date.ToShortDateString() + ")";
            var pointForAnnotationCurrent = new DataPoint(last.X - TEXT_ANNOTATION_OFFSET, last.Y + ((max - min) / 2));

            _textAnnotationCurrent.TextPosition = pointForAnnotationCurrent;
            _textAnnotationCurrent.Text = textForAnnotationCurrent;
            _textAnnotationCurrent.TextColor = OxyColors.Black;
            _textAnnotationCurrent.FontSize = 20.0;
            _textAnnotationCurrent.TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center;
            _textAnnotationCurrent.TextVerticalAlignment = VerticalAlignment.Top;
            _textAnnotationCurrent.StrokeThickness = 0;

            var euriborMin = TheEuribors.GetMinValue(_currentTimePeriod);
            _minLineAnnotation.Type = LineAnnotationType.Horizontal;
            _minLineAnnotation.X = (max - min) / 2;
            _minLineAnnotation.Y = Convert.ToDouble(TheEuribors.GetMinimumInterest(_currentTimePeriod));
            _minLineAnnotation.Text = Resources.MIN_LABEL + ": " + euriborMin.EuriborValue + " (" + euriborMin.Date.ToShortDateString() + ")";
            _minLineAnnotation.FontSize = 20.0;
            _minLineAnnotation.Color = OxyColors.Blue;

            var euriborMax = TheEuribors.GetMaxValue(_currentTimePeriod);
            _maxLineAnnotation.Type = LineAnnotationType.Horizontal;
            _maxLineAnnotation.Y = Convert.ToDouble(TheEuribors.GetMaximumInterest(_currentTimePeriod));
            _maxLineAnnotation.Text = Resources.MAX_LABEL + ": " + euriborMax.EuriborValue + " (" + euriborMax.Date.ToShortDateString() + ")";
            _maxLineAnnotation.FontSize = 20.0;
            _maxLineAnnotation.Color = OxyColors.Red;
        }
    }
}
