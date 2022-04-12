using System.Windows;

using PlenkaAPI.Models;
using ViewModels;
namespace PlenkaWpf.View
{
    /// <summary>
    ///     Логика взаимодействия для MaterialExplorer.xaml
    /// </summary>
    public partial class MaterialExplorer : Window
    {
        public MaterialExplorer()
        {
            InitializeComponent();
            DataContext = new MaterialExplorerVM();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var win = new MaterialEdit3((DataContext as MaterialExplorerVM).SelectedMaterial);
            win.ShowDialog();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new MaterialEdit3(new Material());
            win.ShowDialog();
        }
    }
}