using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Windows.Forms;
using System.Xml;
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
        private readonly IGraphControl _graphControl;

        private readonly BackgroundWorker _feedReader;

        public MainFormPresenter()
        {
            _feedReader = new BackgroundWorker {WorkerSupportsCancellation = true};
            _feedReader.DoWork += _feedReader_DoWork;
            _feedReader.RunWorkerCompleted += _feedReader_RunWorkerCompleted;

            _mainForm = new MainForm();

            _logControl = new LogControl();
            _logControl.Init();
            _logControl.UpdateClicked += _logControl_UpdateClicked;

            _graphControl = new GraphControl();
            _graphControl.Init();

            _mainForm.AddControl((UserControl)_logControl, "Log");
            _mainForm.AddControl((UserControl) _graphControl, "Graph");

            TheEuribors.InterestList = new List<Euribors>();
            TheEuribors.Load();
        }

        void _feedReader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _graphControl.UpdateGraph();
            TheEuribors.Save();
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
                    TheEuribors.InterestList.Add(current);
            }
        }
    }
}
