using System;
using EuriborSharp.CustonEventArgs;

namespace EuriborSharp.Interfaces
{
    public interface ILogControl
    {
        event EventHandler<StringEventArg> AddressChanged; 
        event EventHandler UpdateClicked;
        event EventHandler ClearClicked;

        void AddText(string s, bool append);
        void UpdateAddress(string s);
        void Init();
    }
}
