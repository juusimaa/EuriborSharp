using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EuriborSharp.Interfaces;
using EuriborSharp.Model;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace EuriborSharp.Views
{
    public partial class GraphControl : UserControl, IGraphControl
    {
        private PlotModel _euriborPlot;
        private LineSeries _euribor1Month;
        private DateTimeAxis _xAxis;
        private LinearAxis _yAxis;

        public GraphControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Dock = DockStyle.Fill;

            _euriborPlot = new PlotModel
            {
                PlotType = PlotType.XY,
                Title = "Euribors"
            };

            _euribor1Month = new LineSeries
            {
                MarkerType = MarkerType.Circle
            };

            _xAxis = new DateTimeAxis
            {
                
            };

            _yAxis = new LinearAxis
            {
                Unit = "%"
            };

            _euriborPlot.Series.Add(_euribor1Month);
            _euriborPlot.Axes.Add(_xAxis);
            _euriborPlot.Axes.Add(_yAxis);

            graphPlot.Model = _euriborPlot;
        }

        public void UpdateGraph()
        {
            AddPointsToSeries(_euribor1Month);
        }

        private void AddPointsToSeries(LineSeries lineSeries)
        {
            foreach (var item in TheEuribors.InterestList)
            {
                var dp = new DataPoint(DateTimeAxis.ToDouble(item.Date), Convert.ToDouble(item.OneMonth));
                _euribor1Month.Points.Add(dp);
            }
        }
    }
}
