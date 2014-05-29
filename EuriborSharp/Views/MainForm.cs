using System;
using System.Windows.Forms;
using EuriborSharp.Interfaces;

namespace EuriborSharp.Views
{
    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void updateButton_Click(object sender, System.EventArgs e)
        {
            UpdateClicked(this, EventArgs.Empty);
        }

        private void clearButton_Click(object sender, System.EventArgs e)
        {
            ClearClicked(this, EventArgs.Empty);
        }

        public event EventHandler UpdateClicked;
        public event EventHandler ClearClicked;

        public void ClearAll()
        {
            rssTextBox.Clear();
        }

        public void AddText(string s, bool append)
        {
            if (append)
                rssTextBox.Text += s;
            else
                rssTextBox.Text = s;
        }
    }
}
