using System;
using System.Windows.Forms;

namespace EuriborSharp.Interfaces
{
    interface IMainForm
    {
        event EventHandler HelpSelected;
        event EventHandler ExitSelected;

        void Close();
        void Dispose();
        void AddControl(UserControl control, string tabName);
        void UpdateTitle(string s);
    }
}
