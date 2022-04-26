using System.Windows;
using System.Windows.Controls;

using PlenkaAPI.Models;

using PlenkaWpf.VM;


namespace PlenkaWpf.View
{
    /// <summary>
    ///     Логика взаимодействия для MaterialExplorer.xaml
    /// </summary>
    public partial class MaterialExplorer : UserControl
    {
        public MaterialExplorer()
        {
            InitializeComponent();
            DataContext = new MaterialExplorerVm();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var win = new MaterialEdit((DataContext as MaterialExplorerVm).SelectedMemObject);
            win.ShowDialog();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new MaterialEdit(new MembraneObject());
            win.ShowDialog();
        }
    }
}
