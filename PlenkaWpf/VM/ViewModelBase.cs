using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PlenkaWpf.Annotations;

namespace PlenkaWpf.VM;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void ShowChildWindow(Window window)
    {
        window.Show();
    }

    public event EventHandler ClosingRequest;

    protected void OnClosingRequest()
    {
        if (ClosingRequest != null) ClosingRequest(this, EventArgs.Empty);
    }


    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}