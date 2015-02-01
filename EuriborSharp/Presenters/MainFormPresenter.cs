#region

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using EuriborSharp.CustonEventArgs;
using EuriborSharp.Enums;
using EuriborSharp.Interfaces;
using EuriborSharp.Model;
using EuriborSharp.Views;
using System.Reflection;

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
        private readonly BackgroundWorker _downloader;

        public MainFormPresenter()
        {
            WriteResourcesToDisk();

            _downloader = new BackgroundWorker {WorkerSupportsCancellation = true};
            _downloader.DoWork += _downloader_DoWork;
            _downloader.RunWorkerCompleted += _downloader_RunWorkerCompleted;

            _mainForm = new MainForm();
            _mainForm.HelpSelected += _mainForm_HelpSelected;
            _mainForm.ExitSelected += _mainForm_ExitSelected;
            _mainForm.LineSmoothChanged += _mainForm_LineSmoothChanged;
            _mainForm.GraphStyleChanged += _mainForm_GraphStyleChanged;
            _mainForm.RendererChanged += _mainForm_RendererChanged;
            _mainForm.DotLineSelected += _mainForm_DotLineSelected;
            _mainForm.UpdateIntervalChanged += _mainForm_UpdateIntervalChanged;
            _mainForm.UpdateRequested += _mainForm_UpdateRequested;
            _mainForm.View30DaysSelected += _mainForm_View30DaysSelected;

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
            _aboutFormPresenter.UpdateFonts(EuriborSharpSettings.Default.XkcdSelected);

            _mainForm.UpdateIntervalSelection(EuriborSharpSettings.Default.UpdateInterval.TotalHours);
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
            _logControl.UpdateAddress(EuriborSharpSettings.Default.EuriborDefaultUrl);            

            if (EuriborSharpSettings.Default.Autoload) _downloader.RunWorkerAsync();
#else
            if (TheEuribors.NeedUpdatating())
                _downloader.RunWorkerAsync();
            else
                UpdateCompleted();
#endif
        }

        void WriteResourcesToDisk()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EuriborSharp.Resources.EuriborSources.xml"))
            {
                using (FileStream fileStream = new FileStream("EuriborSources.xml", FileMode.Create))
                {
                    stream.CopyTo(fileStream);
                }
            }
        }
        
        void _mainForm_View30DaysSelected(object sender, EventArgs e)
        {
            InitGraphs();
            _graphControl1Month.ViewLastDays(30);
            _graphControl3Month.ViewLastDays(30);
            _graphControl6Month.ViewLastDays(30);
            _graphControl12Month.ViewLastDays(30);
            _graphControlAll.ViewLastDays(30);
        }

        void _mainForm_UpdateRequested(object sender, EventArgs e)
        {
            _downloader.RunWorkerAsync();
        }

        static void _mainForm_UpdateIntervalChanged(object sender, TimeSpaneEventArgs e)
        {
            EuriborSharpSettings.Default.UpdateInterval = e.value;
            EuriborSharpSettings.Default.Save();
        }

        void UpdateCompleted()
        {
            _mainForm.UpdateTitle("EuriborSharp - Updatated " + TheEuribors.GetLastUpdateTime().ToShortDateString() +
                "@" + TheEuribors.GetLastUpdateTime().ToShortTimeString());
            TheEuribors.ParseValues();
            InitGraphs();
            UpdateGraphView();
        }
        
        void _downloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _mainForm.UpdateTitle("EuriborSharp - Updatated " + DateTime.Now.ToShortDateString() + "@" + DateTime.Now.ToShortTimeString());
            UpdateCompleted();
        }

        void _downloader_DoWork(object sender, DoWorkEventArgs e)
        {
            _mainForm.UpdateTitle("EuriborSharp - Updatating...");

            var downloader = new WebClient();

            foreach (var item in TheEuribors.EuriborFiles)
            {
                if (!File.Exists(item.Filename) || item.FileYear.Year == 2015)
                {
                    downloader.DownloadFile(new Uri(item.Url), item.Filename);
                    _logControl.AddText("Downloading " + item.Url + Environment.NewLine, true);
                }
            }
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
           _aboutFormPresenter.UpdateFonts(EuriborSharpSettings.Default.XkcdSelected);
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
            _graphControl1Month.UpdateGraph(TimePeriods.OneMonth);
            _graphControl3Month.UpdateGraph(TimePeriods.ThreeMonths);
            _graphControl6Month.UpdateGraph(TimePeriods.SixMonths);
            _graphControl12Month.UpdateGraph(TimePeriods.TwelveMonths);
            _graphControlAll.UpdateGraph(TimePeriods.Default);
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
            _downloader.CancelAsync();
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
            _downloader.RunWorkerAsync();
        }

        public Form GetMainForm()
        {
            return (Form) _mainForm;
        }
    }
}
