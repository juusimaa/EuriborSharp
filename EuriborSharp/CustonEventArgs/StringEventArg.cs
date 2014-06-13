using System;

namespace EuriborSharp.CustonEventArgs
{
    public class StringEventArg : EventArgs
    {
        public readonly string value;

        public StringEventArg(string s)
        {
            value = s;
        }
    }
}
