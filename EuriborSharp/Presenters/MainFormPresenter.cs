using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Windows.Forms;
using System.Xml;
using EuriborSharp.Enums;
using EuriborSharp.Interfaces;
using EuriborSharp.Model;
using EuriborSharp.Views;

namespace EuriborSharp.Presenters
{
    public class MainFormPresenter : IDisposable
    {
        private const string FEED_ADDRESS = @"http://www.suomenpankki.fi/fi/_layouts/BOF/RSS.ashx/tilastot/Korot/fi";
        
        // Flag: Has Dispose already been called? 
        bool _disposed;
        
        private readonly IMainForm _mainForm;
        private readonly ILogControl _logControl;
        private readonly IGraphControl _graphControl1Month;
        private readonly IGraphControl _graphControl3Month;
        private readonly IGraphControl _graphControl6Month;
        private readonly IGraphControl _graphControl12Month;

        private readonly BackgroundWorker _feedReader;

        public MainFormPresenter()
        {
            TheEuribors.InterestList = new List<Euribors>();
            TheEuribors.Load();

            _feedReader = new BackgroundWorker {WorkerSupportsCancellation = true};
            _feedReader.DoWork += _feedReader_DoWork;
            _feedReader.RunWorkerCompleted += _feedReader_RunWorkerCompleted;

            _mainForm = new MainForm();

            _logControl = new LogControl();
            _logControl.Init();
            _logControl.UpdateClicked += _logControl_UpdateClicked;

            _graphControl1Month = new GraphControl();
            _graphControl1Month.Init(TimePeriods.OneMonth);
            _graphControl3Month = new GraphControl();
            _graphControl3Month.Init(TimePeriods.ThreeMonths);
            _graphControl6Month = new GraphControl();
            _graphControl6Month.Init(TimePeriods.SixMonths);
            _graphControl12Month = new GraphControl();
            _graphControl12Month.Init(TimePeriods.TwelveMonths);

            _mainForm.AddControl((UserControl)_logControl, "Log");
            _mainForm.AddControl((UserControl) _graphControl1Month, TheEuribors.GetInterestName(TimePeriods.OneMonth));
            _mainForm.AddControl((UserControl)_graphControl3Month, TheEuribors.GetInterestName(TimePeriods.ThreeMonths));
            _mainForm.AddControl((UserControl)_graphControl6Month, TheEuribors.GetInterestName(TimePeriods.SixMonths));
            _mainForm.AddControl((UserControl)_graphControl12Month, TheEuribors.GetInterestName(TimePeriods.TwelveMonths));

            _graphControl1Month.UpdateGraph();
            _graphControl3Month.UpdateGraph();
            _graphControl6Month.UpdateGraph();
            _graphControl12Month.UpdateGraph();
        }

        void _feedReader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _graphControl1Month.UpdateGraph();
            _graphControl3Month.UpdateGraph();
            _graphControl6Month.UpdateGraph();
            _graphControl12Month.UpdateGraph();
        }

        void _feedReader_DoWork(object sender, DoWorkEventArgs e)
        {
            ReadRssFeed();
        }

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
                _mainForm.Dispose();
            }

            // Free any unmanaged objects here. 
            //
            _disposed = true;
        }

        void _logControl_UpdateClicked(object sender, EventArgs e)
        {
            _feedReader.RunWorkerAsync();
        }

        public Form GetMainForm()
        {
            return (Form) _mainForm;
        }

        private void ReadRssFeed()
        {
            _logControl.AddText("Reading " + FEED_ADDRESS + Environment.NewLine, true);

            using (var reader = XmlReader.Create(FEED_ADDRESS))
            {
                var feed = SyndicationFeed.Load(reader);

                if (feed == null) return;

                var current = new Euribors();

                foreach (var subject in feed.Items.Select(item => item.Title.Text))
                {
                    TheEuribors.ParseInterestRates(subject, current);
                    _logControl.AddText(subject + Environment.NewLine, true);
                }

                var containsCurrentDate = TheEuribors.InterestList.Find(e => e.Date.Equals(current.Date)) != null;

                if (!containsCurrentDate)
                {
                    _logControl.AddText("Saving new item to storage." + Environment.NewLine, true);
                    TheEuribors.InterestList.Add(current);
                    TheEuribors.Save();
                }
                else
                {
                    _logControl.AddText("No new items found." + Environment.NewLine, true);
                }
            }

            _logControl.AddText("Ready." + Environment.NewLine + Environment.NewLine, true);
        }
    }
}
