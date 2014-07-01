using System;
using System.Windows.Forms;
using EuriborSharp.Properties;

namespace EuriborSharp.Views
{
    public sealed partial class AboutForm : Form
    {
        public event EventHandler<LinkLabelLinkClickedEventArgs> LinkClicked; 

        public AboutForm()
        {
            InitializeComponent();
            Text = Resources.ABOUT_TITLE;

            linkLabel1.Text = Resources.ABOUT_OXY_LABEL;
            linkLabel1.Links.Add(0, 7, "https://oxyplot.codeplex.com");
            linkLabel1.Links.Add(37, 3, "http://oxyplot.codeplex.com/license");

            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
        }

        void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkClicked(this, e);
        }

        public void UpdateTitle(string s)
        {
            titleLabel.Text = s;
        }

        public void UpdateCopyright(string s)
        {
            copyLabel.Text = s;
        }

        public void UpdateVersion(string s)
        {
            versionLabel.Text = s;
        }
    }
}
