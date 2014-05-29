using System;
using System.Windows.Forms;
using EuriborSharp.Presenters;

namespace EuriborSharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var presenter = new MainFormPresenter();
            Application.Run(presenter.GetMainForm());
        }
    }
}
