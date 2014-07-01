using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using EuriborSharp.Interfaces;
using EuriborSharp.Views;

namespace EuriborSharp.Presenters
{
    class AboutFormPresenter : IAboutFormPresenter
    {
        public void ShowAboutForm()
        {
            var form = new AboutForm {StartPosition = FormStartPosition.CenterParent};
            form.LinkClicked += form_LinkClicked;

            form.UpdateTitle(AssemblyTitle);
            form.UpdateCopyright(AssemblyCopyright);
            form.UpdateVersion(AssemblyVersion.Major + "." + AssemblyVersion.Minor + " (build " + AssemblyVersion.Build + ")");

            form.ShowDialog();
        }

        static void form_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var l = e.Link.LinkData.ToString();

            if (!String.IsNullOrEmpty(l))
                Process.Start(l);
        }

        #region Assembly Attribute Accessors

        private static string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                
                if (attributes.Length <= 0) return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                
                return titleAttribute.Title != "" ? titleAttribute.Title : Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        private static Version AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        private static string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        #endregion
    }
}
