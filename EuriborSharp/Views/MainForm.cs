using System;
using System.Windows.Forms;
using EuriborSharp.CustonEventArgs;
using EuriborSharp.Interfaces;

namespace EuriborSharp.Views
{
    public partial class MainForm : Form, IMainForm
    {
        public event EventHandler<BooleanEventArg> XkcdChanged;
        public event EventHandler<BooleanEventArg> LineSmoothChanged;
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpSelected(this, EventArgs.Empty);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitSelected(this, EventArgs.Empty);
        }

        private void noneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LineStyleNoneSelected(this, EventArgs.Empty);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineStyleNormalSelected(this, EventArgs.Empty);
        }

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineSmoothChanged(this, new BooleanEventArg(smoothToolStripMenuItem.Checked));
        }

        private void xkcdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XkcdChanged(this, new BooleanEventArg(xkcdToolStripMenuItem.Checked));
        }
    }
}
