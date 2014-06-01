using System;

namespace EuriborSharp.Interfaces
{
    public interface ILogControl
    {
        event EventHandler UpdateClicked;
        event EventHandler ClearClicked;

        void AddText(string s, bool append);
        void Init();
    }
}
