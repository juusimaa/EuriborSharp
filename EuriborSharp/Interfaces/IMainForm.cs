using System;
using System.Windows.Forms;
using EuriborSharp.CustonEventArgs;
using EuriborSharp.Enums;

namespace EuriborSharp.Interfaces
{
    interface IMainForm
    {
        /// <summary>
        /// Occurs when smooth line enabled or disabled.
        /// </summary>
        event EventHandler<BooleanEventArg> LineSmoothChanged;

        /// <summary>
        /// Occurs when graph serie style is changed.
        /// </summary>
        event EventHandler<GraphStyleEventArgs> GraphStyleChanged;

        /// <summary>
        /// Occurs when renderer is changed.
        /// </summary>
        event EventHandler<RendererEventArgs> RendererChanged;

        /// <summary>
        /// Occurs when line style is changed.
        /// </summary>
        event EventHandler<BooleanEventArg> DotLineSelected; 

        /// <summary>
        /// Occurs when about menu is selected from the menu.
        /// </summary>
        event EventHandler HelpSelected;

        /// <summary>
        /// Occurs when exit is selected from the menu.
        /// </summary>
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
