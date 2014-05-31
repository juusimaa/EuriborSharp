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



        public void AddControl(UserControl control)
        {
            Controls.Add(control);
        }
    }
}
