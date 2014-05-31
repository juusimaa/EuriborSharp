using System;
using System.Windows.Forms;

namespace EuriborSharp.Views
{
    public partial class LogControl : UserControl
    {
        public event EventHandler UpdateClicked;
        public event EventHandler ClearClicked;

        public LogControl()
        {
            InitializeComponent();
        }

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

        private void clearButton_Click_1(object sender, EventArgs e)
        {
            ClearClicked(this, EventArgs.Empty);
        }

        private void updateButton_Click_1(object sender, EventArgs e)
        {
            UpdateClicked(this, EventArgs.Empty);
        }
    }
}
