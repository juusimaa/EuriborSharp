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

    public class BooleanEventArg : EventArgs
    {
        public readonly bool value;

        public BooleanEventArg(bool b)
        {
            value = b;
        }
    }
}
