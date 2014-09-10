using System;
using System.Drawing;
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

            oxyLinkLabel.Text = Resources.ABOUT_OXY_LABEL;
            oxyLinkLabel.Links.Add(0, 7, "https://oxyplot.codeplex.com");
            oxyLinkLabel.Links.Add(37, 3, "http://oxyplot.codeplex.com/license");
            oxyLinkLabel.LinkClicked += linkLabel1_LinkClicked;

            moreLinqLinkLabel.Text = Resources.ABOUT_MORELINQ_LABEL;
            moreLinqLinkLabel.Links.Add(0, 8, "http://code.google.com/p/morelinq/");
            moreLinqLinkLabel.Links.Add(34, 8, "http://www.apache.org/licenses/LICENSE-2.0");
            moreLinqLinkLabel.LinkClicked += moreLinqLinkLabel_LinkClicked;
        }

        void moreLinqLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkClicked(this, e);
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

        public void UpdateFont(bool xkcdSelected)
        {
            Font titleFont;
            Font copyFont;

            if (xkcdSelected)
            {
                titleFont = new Font("Humor Sans", 14F);
                copyFont = new Font("Humor Sans", 9F);
            }
            else
            {
                titleFont = new Font("Segoe UI", 14F);
                copyFont = new Font("Segoe UI", 9F);
            }

            titleLabel.Font = titleFont;
            versionLabel.Font = titleFont;
            copyLabel.Font = copyFont;
            componentsLabel.Font = copyFont;
            oxyLinkLabel.Font = copyFont;
            moreLinqLinkLabel.Font = copyFont;
        }
    }
}
