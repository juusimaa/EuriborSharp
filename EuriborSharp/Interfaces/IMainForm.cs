using System.Windows.Forms;

namespace EuriborSharp.Interfaces
{
    interface IMainForm
    {
        void Dispose();
        void AddControl(UserControl control, string tabName);
        void UpdateTitle(string s);
    }
}
