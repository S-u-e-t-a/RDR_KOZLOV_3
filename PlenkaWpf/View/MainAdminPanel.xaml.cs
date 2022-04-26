using System.Windows;
using System.Windows.Controls;

using HandyControl.Controls;

using Microsoft.EntityFrameworkCore;

using PlenkaAPI.Data;

using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Логика взаимодействия для MainAdminPanel.xaml
    /// </summary>
    public partial class MainAdminPanel : UserControl, IСhangeableControl
    {
        public MainAdminPanel()
        {
            InitializeComponent();
            DbContextSingleton.GetInstance().SavedChanges -= NotifyDbUpdated;
            DbContextSingleton.GetInstance().SavedChanges += NotifyDbUpdated;
            var vm = new MainAdminPanelVm();
            DataContext = vm;
        }

        public WindowState PreferedWindowState { get; set; } = WindowState.Maximized;
        public string WindowTitle { get; set; } = "Панель администратора";
        public double? PreferedHeight { get; set; }
        public double? PreferedWidth { get; set; }
        public event IСhangeableControl.ChangingRequestHandler ChangingRequest;

        public void OnChangingRequest(UserControl newControl)
        {
            ChangingRequest.Invoke(this, newControl);
        }

        private static void NotifyDbUpdated(object? sender, SavedChangesEventArgs savedChangesEventArgs)
        {
            Growl.SuccessGlobal("Данные в базе обновлены");
        }

        private void ChangeUser(object sender, RoutedEventArgs e)
        {
            OnChangingRequest(new LoginWindow());
        }
    }
}
