using System;

namespace EuriborSharp.Interfaces
{
    interface IMainForm
    {
        event EventHandler UpdateClicked;
        event EventHandler ClearClicked;

        void ClearAll();
        void AddText(string s, bool append);
    }
}
