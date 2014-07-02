using System;
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
        public event EventHandler HelpSelected;
        public event EventHandler ExitSelected;

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
            lineToolStripMenuItem.Checked = g == GraphStyle.Line;
            barToolStripMenuItem.Checked = g == GraphStyle.Bar;
        }

        public void UpdateRenderer(Renderer r)
        {
            normalToolStripMenuItem.Checked = r == Renderer.Normal;
            xkcdToolStripMenuItem.Checked = r == Renderer.Xkcd;
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
            normalLineStyleToolStripMenuItem.Checked = false;
            DotLineSelected(this, new BooleanEventArg(dotLineStyleToolStripMenuItem.Checked));
        }

        private void normalLineStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dotLineStyleToolStripMenuItem.Checked = false;
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
    }
}
