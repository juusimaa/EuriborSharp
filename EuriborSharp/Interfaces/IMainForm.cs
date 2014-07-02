using System;
using System.Windows.Forms;
using EuriborSharp.CustonEventArgs;
using EuriborSharp.Enums;

namespace EuriborSharp.Interfaces
{
    interface IMainForm
    {
        event EventHandler<BooleanEventArg> LineSmoothChanged;
        event EventHandler<GraphStyleEventArgs> GraphStyleChanged;
        event EventHandler<RendererEventArgs> RendererChanged;
        event EventHandler<BooleanEventArg> DotLineSelected; 
        event EventHandler HelpSelected;
        event EventHandler ExitSelected;

        void Close();
        void Dispose();
        void AddControl(UserControl control, string tabName);
        void UpdateTitle(string s);
        void UpdateLineStyle(bool dotlineSelected);
        void UpdateSmoothSelection(bool selected);
        void UpdateSeriesStyle(GraphStyle g);
        void UpdateRenderer(Renderer r);
    }
}
