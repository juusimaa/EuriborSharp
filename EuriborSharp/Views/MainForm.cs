using System;
using System.Windows.Forms;
using EuriborSharp.CustonEventArgs;
using EuriborSharp.Enums;
using EuriborSharp.Interfaces;

namespace EuriborSharp.Views
{
    public partial class MainForm : Form, IMainForm
    {
        public event EventHandler<BooleanEventArg> XkcdChanged;
        public event EventHandler<BooleanEventArg> LineSmoothChanged;
        public event EventHandler<GraphStyleEventArgs> GraphStyleChanged;
        public event EventHandler LineStyleNormalSelected;
        public event EventHandler LineStyleNoneSelected;
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

        public void UpdateSmoothSelection(bool selected)
        {
            smoothToolStripMenuItem.Checked = selected;
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
            LineStyleNoneSelected(this, EventArgs.Empty);
        }

        private void normalLineStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dotLineStyleToolStripMenuItem.Checked = false;
            LineStyleNormalSelected(this, EventArgs.Empty);
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

        private void xkcdLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphStyleChanged(this, new GraphStyleEventArgs(GraphStyle.XkcdLine));
        }

        private void xkcdBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphStyleChanged(this, new GraphStyleEventArgs(GraphStyle.XkcdBar));
        }
    }
}
