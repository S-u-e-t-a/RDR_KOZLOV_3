using HandyControl.Controls;

using Microsoft.EntityFrameworkCore;

using PlenkaAPI.Data;

using Window = System.Windows.Window;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Логика взаимодействия для MainAdminPanel.xaml
    /// </summary>
    public partial class MainAdminPanel : Window
    {
        public MainAdminPanel()
        {
            InitializeComponent();
            DbContextSingleton.GetInstance().SavedChanges -= NotifyDBUpdated;
            DbContextSingleton.GetInstance().SavedChanges += NotifyDBUpdated;
        }

        private static void NotifyDBUpdated(object? sender, SavedChangesEventArgs savedChangesEventArgs)
        {
            Growl.SuccessGlobal("Данные в базе обновлены");
        }
    }
}
