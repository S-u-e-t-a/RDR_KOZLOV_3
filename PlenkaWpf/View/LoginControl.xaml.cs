using System.Windows;
using System.Windows.Controls;

using PlenkaAPI.Data;

using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginControl : UserControl, IСhangeableControl
    {
        public LoginControl()
        {
            InitializeComponent();
            var con = DbContextSingleton.GetInstance();
        }

        public WindowState PreferedWindowState { get; set; } = WindowState.Normal;
        public string WindowTitle { get; set; } = "Авторизация";
        public double? PreferedHeight { get; set; } = 390;
        public double? PreferedWidth { get; set; } = 270;
        public event IСhangeableControl.ChangingRequestHandler ChangingRequest;

        public void OnChangingRequest(UserControl newControl)
        {
            ChangingRequest?.Invoke(this, newControl);
        }


        private void Button_Click(object sender, RoutedEventArgs e) //todo убрать заглушку
        {
            OnChangingRequest(new MainAdminControl());
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            OnChangingRequest(new ResearcherControl());
        }
    }
}
