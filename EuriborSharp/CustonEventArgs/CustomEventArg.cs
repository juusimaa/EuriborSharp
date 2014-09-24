using System;
using EuriborSharp.Enums;

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

    public class TimeSpaneEventArgs : EventArgs
    {
        public readonly TimeSpan value;

        public TimeSpaneEventArgs(TimeSpan i)
        {
            value = i;
        }
    }

    public class GraphStyleEventArgs : EventArgs
    {
        public readonly GraphStyle style;

        public GraphStyleEventArgs(GraphStyle g)
        {
            style = g;
        }
    }

    public class RendererEventArgs : EventArgs
    {
        public readonly Renderer value;

        public RendererEventArgs(Renderer r)
        {
            value = r;
        }
    }
}
