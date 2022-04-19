using System.Windows.Controls;
using PlenkaWpf.VM;

namespace PlenkaWpf.View;

/// <summary>
///     Логика взаимодействия для UserExplorer.xaml
/// </summary>
public partial class UserExplorer : UserControl
{
    public UserExplorer()
    {
        InitializeComponent();
        DataContext = new UserExplorerVM();
    }
}