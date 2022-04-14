using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PlenkaWpf.Annotations;
using PlenkaWpf.Utils;

namespace PlenkaWpf.VM;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private RelayCommand _closeWindow;

    public RelayCommand CloseWindow
    {
        get { return _closeWindow ?? (_closeWindow = new RelayCommand(o =>
        {
            OnClosingRequest();
        })); }
    }



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