using System;
using System.Windows.Forms;

namespace EuriborSharp.Interfaces
{
    public interface ILogControlPresenter
    {
        event EventHandler UpdateClicked;

        void Init();
        void AddText(string s, bool append);
        UserControl GetControl();
    }
}
