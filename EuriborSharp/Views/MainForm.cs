using System;
using System.Drawing;
using System.Windows.Forms;
using EuriborSharp.CustonEventArgs;
using EuriborSharp.Enums;
using EuriborSharp.Interfaces;

namespace EuriborSharp.Views
{
    public partial class MainForm : Form, IMainForm
    {
        public event EventHandler<BooleanEventArg> LineSmoothChanged;
        public event EventHandler<GraphStyleEventArgs> GraphStyleChanged;
        public event EventHandler<RendererEventArgs> RendererChanged;
        public event EventHandler<BooleanEventArg> DotLineSelected;
        public event EventHandler<EventArgs> UpdateRequested;
        public event EventHandler<TimeSpaneEventArgs> UpdateIntervalChanged;
        public event EventHandler HelpSelected;
        public event EventHandler ExitSelected;
        public event EventHandler View30DaysSelected;

        public MainForm()
        {
            InitializeComponent();
        }

        public void AddControl(UserControl control, string tabName)
        {
            var page = new TabPage(tabName);
            page.Controls.Add(control);
            mainTabControl.TabPages.Add(page);
        }

        public void UpdateTitle(string s)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateTitle(s)));
            }
            Text = s;
        }

        public void UpdateLineStyle(bool dotlineSelected)
        {
            normalLineStyleToolStripMenuItem.Checked = !dotlineSelected;
            dotLineStyleToolStripMenuItem.Checked = dotlineSelected;
        }

        public void UpdateSmoothSelection(bool selected)
        {
            smoothToolStripMenuItem.Checked = selected;
        }

        public void UpdateSeriesStyle(GraphStyle g)
        {
            //lineToolStripMenuItem.Checked = g == GraphStyle.Line;
            //barToolStripMenuItem.Checked = g == GraphStyle.Bar;
        }

        public void UpdateRenderer(Renderer r)
        {
            normalToolStripMenuItem.Checked = r == Renderer.Normal;
            xkcdToolStripMenuItem.Checked = r == Renderer.Xkcd;
        }

        public void UpdateGui(bool xkcdSelected)
        {
            Font f;

            if (xkcdSelected)
            {
                f = new Font("Humor Sans", 9F);

            }
            else
            {
                f = new Font("Segoe UI", 9F);
            }

            mainTabControl.Font = f;
            menuStrip.Font = f;
        }

        public void UpdateIntervalSelection(double hours)
        {
            switch (Convert.ToInt32(hours))
            {
                case 24:
                    oneDayToolStripMenuItem.Checked = true;
                    twelveHoursToolStripMenuItem.Checked = false;
                    sixHoursToolStripMenuItem.Checked = false;
                    break;
                case 12:
                    oneDayToolStripMenuItem.Checked = false;
                    twelveHoursToolStripMenuItem.Checked = true;
                    sixHoursToolStripMenuItem.Checked = false;
                    break;
                case 6:
                    oneDayToolStripMenuItem.Checked = false;
                    twelveHoursToolStripMenuItem.Checked = false;
                    sixHoursToolStripMenuItem.Checked = true;
                    break;
            }
        }

        public void UpdateLineStyleSelection(bool normalSelected)
        {
            normalLineStyleToolStripMenuItem.Checked = normalSelected;
            dotLineStyleToolStripMenuItem.Checked = !normalSelected;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpSelected(this, EventArgs.Empty);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitSelected(this, EventArgs.Empty);
        }

        private void dotLineStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DotLineSelected(this, new BooleanEventArg(dotLineStyleToolStripMenuItem.Checked));
        }

        private void normalLineStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DotLineSelected(this, new BooleanEventArg(dotLineStyleToolStripMenuItem.Checked));
        }

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineSmoothChanged(this, new BooleanEventArg(smoothToolStripMenuItem.Checked));
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphStyleChanged(this, new GraphStyleEventArgs(GraphStyle.Line));
        }

        private void barToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphStyleChanged(this, new GraphStyleEventArgs(GraphStyle.Bar));
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RendererChanged(this, new RendererEventArgs(Renderer.Normal));
        }

        private void xkcdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RendererChanged(this, new RendererEventArgs(Renderer.Xkcd));
        }

        private void oneDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            twelveHoursToolStripMenuItem.Checked = false;
            sixHoursToolStripMenuItem.Checked = false;

            if (UpdateIntervalChanged != null)
                UpdateIntervalChanged(this, new TimeSpaneEventArgs(new TimeSpan(1, 0, 0, 0)));
        }

        private void twelveHoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oneDayToolStripMenuItem.Checked = false;
            sixHoursToolStripMenuItem.Checked = false;

            if (UpdateIntervalChanged != null)
                UpdateIntervalChanged(this, new TimeSpaneEventArgs(new TimeSpan(12, 0, 0)));
        }

        private void sixHoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oneDayToolStripMenuItem.Checked = false;
            twelveHoursToolStripMenuItem.Checked = false;

            if (UpdateIntervalChanged != null)
                UpdateIntervalChanged(this, new TimeSpaneEventArgs(new TimeSpan(6, 0, 0)));
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UpdateRequested != null)
                UpdateRequested(this, EventArgs.Empty);
        }

        private void last30DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (View30DaysSelected != null)
                View30DaysSelected(this, EventArgs.Empty);
        }
    }
}
