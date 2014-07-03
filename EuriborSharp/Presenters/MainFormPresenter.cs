#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Windows.Forms;
using System.Xml;
using EuriborSharp.CustonEventArgs;
using EuriborSharp.Enums;
using EuriborSharp.Interfaces;
using EuriborSharp.Model;
using EuriborSharp.Views;

#endregion

namespace EuriborSharp.Presenters
{
    public class MainFormPresenter : IDisposable
    {
        // Flag: Has Dispose already been called? 
        private bool _disposed;
        private bool _valuesReseted;

        private readonly IMainForm _mainForm;
        private readonly ILogControl _logControl;
        private readonly IGraphControl _graphControl1Month;
        private readonly IGraphControl _graphControl3Month;
        private readonly IGraphControl _graphControl6Month;
        private readonly IGraphControl _graphControl12Month;
        private readonly IGraphControl _graphControlAll;
        private readonly IAboutFormPresenter _aboutFormPresenter;

        private readonly BackgroundWorker _feedReader;

        public MainFormPresenter()
        {
            TheEuribors.InterestList = new List<Euribors>();
            TheEuribors.Load();

            _feedReader = new BackgroundWorker {WorkerSupportsCancellation = true};
            _feedReader.DoWork += _feedReader_DoWork;
            _feedReader.RunWorkerCompleted += _feedReader_RunWorkerCompleted;

            _mainForm = new MainForm();
            _mainForm.HelpSelected += _mainForm_HelpSelected;
            _mainForm.ExitSelected += _mainForm_ExitSelected;
            _mainForm.LineSmoothChanged += _mainForm_LineSmoothChanged;
            _mainForm.GraphStyleChanged += _mainForm_GraphStyleChanged;
            _mainForm.RendererChanged += _mainForm_RendererChanged;
            _mainForm.DotLineSelected += _mainForm_DotLineSelected;

            _logControl = new LogControl();
            _logControl.Init();
            _logControl.UpdateClicked += _logControl_UpdateClicked;
            _logControl.AddressChanged += _logControl_AddressChanged;
            _logControl.AutoloadChanged += _logControl_AutoloadChanged;

            _graphControl1Month = new GraphControl();
            _graphControl3Month = new GraphControl();
            _graphControl6Month = new GraphControl();
            _graphControl12Month = new GraphControl();
            _graphControlAll = new GraphControl();

            InitGraphs();

            _aboutFormPresenter = new AboutFormPresenter();

            _mainForm.UpdateSmoothSelection(EuriborSharpSettings.Default.SmoothLine);
            _mainForm.UpdateRenderer(EuriborSharpSettings.Default.SelectedRenderer);
            _mainForm.UpdateSeriesStyle(EuriborSharpSettings.Default.SelectedGraphStyle);
            _mainForm.UpdateLineStyle(EuriborSharpSettings.Default.DotLineSelected);
            _mainForm.UpdateGui(EuriborSharpSettings.Default.XkcdSelected);

            _mainForm.AddControl((UserControl) _graphControl1Month, TheEuribors.GetInterestName(TimePeriods.OneMonth));
            _mainForm.AddControl((UserControl) _graphControl3Month, TheEuribors.GetInterestName(TimePeriods.ThreeMonths));
            _mainForm.AddControl((UserControl) _graphControl6Month, TheEuribors.GetInterestName(TimePeriods.SixMonths));
            _mainForm.AddControl((UserControl) _graphControl12Month, TheEuribors.GetInterestName(TimePeriods.TwelveMonths));
            _mainForm.AddControl((UserControl) _graphControlAll, TheEuribors.GetInterestName(TimePeriods.Default));
#if DEBUG
            _mainForm.AddControl((UserControl) _logControl, "Log");
            _logControl.SetupAutoload(EuriborSharpSettings.Default.Autoload);
            _logControl.UpdateAddress(EuriborSharpSettings.Default.RssFeedAddress);

            if (EuriborSharpSettings.Default.Autoload) _feedReader.RunWorkerAsync();
#else
            _feedReader.RunWorkerAsync();
#endif
            UpdateGraphView();
        }

        private void _mainForm_DotLineSelected(object sender, BooleanEventArg e)
        {
            EuriborSharpSettings.Default.DotLineSelected = e.value;
            EuriborSharpSettings.Default.Save();
            InitGraphs();
            UpdateGraphView();
            UpdateMainFormMenuItemStatus();
        }

       private void _mainForm_RendererChanged(object sender, RendererEventArgs e)
       {
           EuriborSharpSettings.Default.XkcdSelected = (e.value == Renderer.Xkcd);
           EuriborSharpSettings.Default.SelectedRenderer = e.value;
           EuriborSharpSettings.Default.Save();
           InitGraphs();
           UpdateGraphView();
           UpdateMainFormMenuItemStatus();
           _mainForm.UpdateGui(EuriborSharpSettings.Default.XkcdSelected);
        }

        private static void _logControl_AutoloadChanged(object sender, BooleanEventArg e)
        {
            EuriborSharpSettings.Default.Autoload = e.value;
            EuriborSharpSettings.Default.Save();
        }

        private void _mainForm_GraphStyleChanged(object sender, GraphStyleEventArgs e)
        {
            EuriborSharpSettings.Default.SelectedGraphStyle = e.style;
            EuriborSharpSettings.Default.Save();
            InitGraphs();
            UpdateGraphView();
            UpdateMainFormMenuItemStatus();
        }

        private void UpdateMainFormMenuItemStatus()
        {
            _mainForm.UpdateLineStyle(EuriborSharpSettings.Default.DotLineSelected);
            _mainForm.UpdateRenderer(EuriborSharpSettings.Default.SelectedRenderer);
            _mainForm.UpdateSeriesStyle(EuriborSharpSettings.Default.SelectedGraphStyle);
            _mainForm.UpdateSmoothSelection(EuriborSharpSettings.Default.SmoothLine);
        }

        private void UpdateGraphView()
        {
            _graphControl1Month.UpdateGraph();
            _graphControl3Month.UpdateGraph();
            _graphControl6Month.UpdateGraph();
            _graphControl12Month.UpdateGraph();
            _graphControlAll.UpdateGraph();
        }

        private void InitGraphs()
        {
            try
            {
                _graphControl1Month.Init(TimePeriods.OneMonth, EuriborSharpSettings.Default.SmoothLine, EuriborSharpSettings.Default.SelectedGraphStyle,
                    EuriborSharpSettings.Default.SelectedRenderer, EuriborSharpSettings.Default.DotLineSelected);
                _graphControl3Month.Init(TimePeriods.ThreeMonths, EuriborSharpSettings.Default.SmoothLine, EuriborSharpSettings.Default.SelectedGraphStyle,
                    EuriborSharpSettings.Default.SelectedRenderer, EuriborSharpSettings.Default.DotLineSelected);
                _graphControl6Month.Init(TimePeriods.SixMonths, EuriborSharpSettings.Default.SmoothLine, EuriborSharpSettings.Default.SelectedGraphStyle,
                    EuriborSharpSettings.Default.SelectedRenderer, EuriborSharpSettings.Default.DotLineSelected);
                _graphControl12Month.Init(TimePeriods.TwelveMonths, EuriborSharpSettings.Default.SmoothLine, EuriborSharpSettings.Default.SelectedGraphStyle,
                    EuriborSharpSettings.Default.SelectedRenderer, EuriborSharpSettings.Default.DotLineSelected);
                _graphControlAll.Init(TimePeriods.Default, EuriborSharpSettings.Default.SmoothLine, EuriborSharpSettings.Default.SelectedGraphStyle,
                    EuriborSharpSettings.Default.SelectedRenderer, EuriborSharpSettings.Default.DotLineSelected);
            }
            catch (ArgumentException ex)
            {
                _logControl.AddText(ex.Message, true);

                // avoid infinite loop
                if (_valuesReseted) throw;

                _valuesReseted = true;
                EuriborSharpSettings.Default.Reset();
                InitGraphs();
            }
        }

        private void _mainForm_LineSmoothChanged(object sender, BooleanEventArg e)
        {
            EuriborSharpSettings.Default.SmoothLine = e.value;
            EuriborSharpSettings.Default.Save();
            InitGraphs();
            UpdateGraphView();
            UpdateMainFormMenuItemStatus();
        }

        private void _mainForm_ExitSelected(object sender, EventArgs e)
        {
            _feedReader.CancelAsync();
            _mainForm.Close();
        }

        private void _mainForm_HelpSelected(object sender, EventArgs e)
        {
            _aboutFormPresenter.ShowAboutForm();
        }

        private static void _logControl_AddressChanged(object sender, StringEventArg e)
        {
            EuriborSharpSettings.Default.RssFeedAddress = e.value;
            EuriborSharpSettings.Default.Save();
        }

        private void _feedReader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _graphControl1Month.Init(TimePeriods.OneMonth, EuriborSharpSettings.Default.SmoothLine, EuriborSharpSettings.Default.SelectedGraphStyle,
                EuriborSharpSettings.Default.SelectedRenderer, EuriborSharpSettings.Default.DotLineSelected);
            _graphControl3Month.Init(TimePeriods.ThreeMonths, EuriborSharpSettings.Default.SmoothLine, EuriborSharpSettings.Default.SelectedGraphStyle,
                EuriborSharpSettings.Default.SelectedRenderer, EuriborSharpSettings.Default.DotLineSelected);
            _graphControl6Month.Init(TimePeriods.SixMonths, EuriborSharpSettings.Default.SmoothLine, EuriborSharpSettings.Default.SelectedGraphStyle,
                EuriborSharpSettings.Default.SelectedRenderer, EuriborSharpSettings.Default.DotLineSelected);
            _graphControl12Month.Init(TimePeriods.TwelveMonths, EuriborSharpSettings.Default.SmoothLine, EuriborSharpSettings.Default.SelectedGraphStyle,
                EuriborSharpSettings.Default.SelectedRenderer, EuriborSharpSettings.Default.DotLineSelected);

            _graphControl1Month.UpdateGraph();
            _graphControl3Month.UpdateGraph();
            _graphControl6Month.UpdateGraph();
            _graphControl12Month.UpdateGraph();

            _mainForm.UpdateTitle("EuriborSharp - Updatated " + DateTime.Now.ToShortDateString() + "@" + DateTime.Now.ToShortTimeString());
        }

        private void _feedReader_DoWork(object sender, DoWorkEventArgs e)
        {
            _mainForm.UpdateTitle("EuriborSharp - Updatating...");
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

        private void _logControl_UpdateClicked(object sender, EventArgs e)
        {
            _feedReader.RunWorkerAsync();
        }

        public Form GetMainForm()
        {
            return (Form) _mainForm;
        }

        private void ReadRssFeed()
        {
            _logControl.AddText("Reading " + EuriborSharpSettings.Default.RssFeedAddress + Environment.NewLine, true);

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(EuriborSharpSettings.Default.RssFeedAddress);
            httpWebRequest.UserAgent = "Googlebot/1.0 (googlebot@googlebot.com http://googlebot.com/)";

            // Use The Default Proxy
            httpWebRequest.Proxy = WebRequest.DefaultWebProxy;

            // Use The Thread's Credentials (Logged In User's Credentials)
            if (httpWebRequest.Proxy != null)
                httpWebRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;

            using (var httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse())
            {
                using (var responseStream = httpWebResponse.GetResponseStream())
                {
                    Debug.Assert(responseStream != null);
#if !DEBUG
                    if (responseStream == null) return;
#endif

                    using (var xmlReader = XmlReader.Create(responseStream))
                    {
                        var feed = SyndicationFeed.Load(xmlReader);

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
                }
            }

            _logControl.AddText("Ready." + Environment.NewLine + Environment.NewLine, true);
        }
    }
}
