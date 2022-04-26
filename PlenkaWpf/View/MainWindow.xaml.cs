using System.Windows;
using System.Windows.Controls;

using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl _control;

        public MainWindow()
        {
            InitializeComponent();
            ChangeContent(null, new LoginWindow());
        }

        private void ChangeContent(object sender, UserControl control)
        {
            this._control = control;
            ((IСhangeableControl) this._control).ChangingRequest -= ChangeContent;
            ((IСhangeableControl) this._control).ChangingRequest += ChangeContent;
            content.Content = this._control;
            WindowState = ((IСhangeableControl) this._control).PreferedWindowState;

            if (WindowState != WindowState.Maximized)
            {
                Height = (double) ((IСhangeableControl) this._control).PreferedHeight;
                Width = (double) ((IСhangeableControl) this._control).PreferedWidth;
            }

            Title = ((IСhangeableControl) this._control).WindowTitle;
        }
    }
}
