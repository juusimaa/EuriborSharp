using System;
using System.Threading;
using System.Windows.Forms;
using EuriborSharp.Interfaces;
using EuriborSharp.Views;

namespace EuriborSharp.Presenters
{
    class LogControlPresenter : ILogControlPresenter
    {
        private LogControl _control;

        public event EventHandler UpdateClicked;

        public void Init()
        {
            _control = new LogControl();
            _control.UpdateClicked += _control_UpdateClicked;
            _control.ClearClicked += _control_ClearClicked;
        }

        void _control_ClearClicked(object sender, EventArgs e)
        {
            _control.ClearAll();
        }

        void _control_UpdateClicked(object sender, EventArgs e)
        {
            UpdateClicked(this, EventArgs.Empty);
        }

        public void AddText(string s, bool append)
        {
            _control.AddText(s, append);
        }

        public UserControl GetControl()
        {
            return _control;
        }
    }
}
