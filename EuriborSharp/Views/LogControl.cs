﻿using System;
using System.Windows.Forms;
using EuriborSharp.CustonEventArgs;
using EuriborSharp.Interfaces;

namespace EuriborSharp.Views
{
    public partial class LogControl : UserControl, ILogControl
    {
        public event EventHandler<StringEventArg> AddressChanged; 
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
            if (InvokeRequired)
            {
                Invoke((Action) (() => AddText(s, append)));
                return;
            }

            if (append)
                rssTextBox.AppendText(s);
            else
                rssTextBox.Text = s;
        }

        public void UpdateAddress(string s)
        {
            addressTextBox.Text = s;
        }

        public void Init()
        {
            Dock = DockStyle.Fill;
        }

        private void clearButton_Click_1(object sender, EventArgs e)
        {
            ClearClicked(this, EventArgs.Empty);
        }

        private void updateButton_Click_1(object sender, EventArgs e)
        {
            UpdateClicked(this, EventArgs.Empty);
        }

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {
            AddressChanged(this, new StringEventArg(addressTextBox.Text));
        }
    }
}
