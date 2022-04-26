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
        private UserControl control;

        public MainWindow()
        {
            InitializeComponent();
            changeContent(null, new LoginWindow());
        }

        private void changeContent(object sender, UserControl control)
        {
            this.control = control;
            ((IСhangeableControl) this.control).ChangingRequest -= changeContent;
            ((IСhangeableControl) this.control).ChangingRequest += changeContent;
            content.Content = this.control;
            WindowState = ((IСhangeableControl) this.control).PreferedWindowState;

            if (WindowState != WindowState.Maximized)
            {
                Height = (double) ((IСhangeableControl) this.control).PreferedHeight;
                Width = (double) ((IСhangeableControl) this.control).PreferedWidth;
            }

            Title = ((IСhangeableControl) this.control).WindowTitle;
        }
    }
}
