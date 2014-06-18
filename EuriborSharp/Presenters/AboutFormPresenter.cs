using System.Windows.Forms;
using EuriborSharp.Interfaces;
using EuriborSharp.Views;

namespace EuriborSharp.Presenters
{
    class AboutFormPresenter : IAboutFormPresenter
    {
        public void ShowAboutForm()
        {
            var form = new AboutForm {StartPosition = FormStartPosition.CenterParent};
            form.ShowDialog();
        }
    }
}
