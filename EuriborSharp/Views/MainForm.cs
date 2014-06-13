using System;
using System.Windows.Forms;
using EuriborSharp.Interfaces;

namespace EuriborSharp.Views
{
    public partial class MainForm : Form, IMainForm
    {
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpSelected(this, EventArgs.Empty);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitSelected(this, EventArgs.Empty);
        }
    }
}
