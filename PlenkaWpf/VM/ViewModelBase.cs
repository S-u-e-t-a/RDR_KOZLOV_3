using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using PlenkaWpf.Annotations;

namespace PlenkaWpf.VM
{
    public class ViewModelBase: INotifyPropertyChanged
    {
        public void ShowChildWindow(Window window)
        {
            window.Show();
        }

        public event EventHandler ClosingRequest;

        protected void OnClosingRequest()
        {
            if (this.ClosingRequest != null)
            {
                this.ClosingRequest(this, EventArgs.Empty);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
