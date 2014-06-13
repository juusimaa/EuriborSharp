using System;
using System.Windows.Forms;
using EuriborSharp.CustonEventArgs;

namespace EuriborSharp.Interfaces
{
    interface IMainForm
    {
        event EventHandler<BooleanEventArg> LineSmoothChanged;
        event EventHandler LineStyleNormalSelected;
        event EventHandler LineStyleNoneSelected;
        event EventHandler HelpSelected;
        event EventHandler ExitSelected;

        void Close();
        void Dispose();
        void AddControl(UserControl control, string tabName);
        void UpdateTitle(string s);
        void UpdateSmoothSelection(bool selected);
    }
}
